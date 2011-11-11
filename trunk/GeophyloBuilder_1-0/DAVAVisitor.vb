

'   GeoPhyloBuilder
'
'   Copyright (C) 2007, David Kidd, dk@nescent.org. 
'   Copyright (C) 2007, Xianhua Liu, xl24@duke.edu. 
'   Copyright (C) 2007, National Evolutionary Synthesis Center (NESCent).
' 
'  This library is free software; you can redistribute it and/or
'  modify it under the terms of the GNU Lesser General Public License
'  as published by the Free Software Foundation; either version 2.1
'  of the License, or (at your option) any later version.
'
'  This library is distributed in the hope that it will be useful,
'  but WITHOUT ANY WARRANTY; without even the implied warranty of
'  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
'  Lesser General Public License for more details.
'
'  You should have received a copy of the GNU Lesser General Public
'  License along with this library; if not, write to the Free
'  Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA
'  02111-1307 USA
'
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports System.Runtime.InteropServices
<ComClass(DAVAVisitor.ClassId, DAVAVisitor.InterfaceId, DAVAVisitor.EventsId)> _
Public Class DAVAVisitor
    Inherits BuildVisitor

    'Types
    Public Const DAVA_DISJUNCTION = 0
    Public Const DAVA_OVERLAP_BELOW_THRESH = 1
    Public Const DAVA_REMAINDER_BELOW_THRESH = 2
    Public Const DAVA_OVERLAP_ABOVE_THRESH = 3
    Public Const DAVA_REMAINDER_ABOVE_THRESH = 4
    Public Const DAVA_CHILD_POLY = 5

    Private m_DAVA_Threshold As Double  'Threshold for keeping overlay polygons
    Private m_pNodePolyFeatureClass As IFeatureClass
    Private m_pDavaPolyFeatureClass As IFeatureClass
#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "f6d14016-096e-4c40-b06e-5a795497d46c"
    Public Const InterfaceId As String = "84e1d90b-6c4d-4020-924c-9b477e87e3ff"
    Public Const EventsId As String = "18c5529f-d0dc-48e0-81b6-c9abe2a9b89b"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
        m_DAVA_Threshold = 50
    End Sub

    Public Property DAVA_Threshold() As Double
        Get
            Return m_DAVA_Threshold
        End Get
        Set(ByVal value As Double)
            m_DAVA_Threshold = value
        End Set
    End Property

    Public Property NodePolyFeatureClass() As IFeatureClass
        Get
            Return m_pNodePolyFeatureClass
        End Get
        Set(ByVal value As IFeatureClass)
            m_pNodePolyFeatureClass = value
        End Set
    End Property

    Public Property DavaPolyFeatureClass() As IFeatureClass
        Get
            Return m_pDavaPolyFeatureClass
        End Get
        Set(ByVal value As IFeatureClass)
            m_pDavaPolyFeatureClass = value
        End Set
    End Property


    Public Overrides Sub visit(ByVal node As PhyloTreeNode)

        'Direct Area Vicariance Analysis

        Dim child1 As PhyloTreeNode
        Dim child2 As PhyloTreeNode
        Dim pPolyFeature As IFeature
        Dim pParentPolyFeature As IFeature
        Dim pChild1PolyFeature As IFeature
        Dim pChild2PolyFeature As IFeature
        Dim pDAVAchd1PolyFeature As IFeature
        Dim pDAVAchd2PolyFeature As IFeature
        Dim pZAware As IZAware
        Dim pNodeQFilter As IQueryFilter
        Dim pFCursor As IFeatureCursor
        Dim geoDataset As IGeoDataset = CType(Me.FeatureClass, IGeoDataset)
        Dim count As Integer = 0
        Dim pTopoOp As ITopologicalOperator
        Dim pTopoOp2 As ITopologicalOperator
        Dim newPoly As IPolygon
        Dim newPoly2 As IPolygon
        Dim pChdPoly As IPolygon
        Dim pNewArea As IArea
        Dim pChild1Area As IArea
        Dim pChild2Area As IArea
        Dim over As Boolean
        Dim thresh As Boolean

        If node.Type = PhyloTreeNode.TREE_NODE_TYPE_ROOT Or node.Type = PhyloTreeNode.TREE_NODE_TYPE_INNER Then

            'Get node (parent) Polygon
            pNodeQFilter = New QueryFilter
            pNodeQFilter.WhereClause = "NodeID = " + CStr(node.Id)
            pFCursor = Me.NodePolyFeatureClass.Search(pNodeQFilter, False)
            pParentPolyFeature = pFCursor.NextFeature

            ' Get First Child
            child1 = node.Children(0)
            pNodeQFilter = New QueryFilter
            pNodeQFilter.WhereClause = "NodeID = " + CStr(child1.Id)
            pFCursor = Me.NodePolyFeatureClass.Search(pNodeQFilter, False)
            pChild1PolyFeature = pFCursor.NextFeature
            pChild1Area = pChild1PolyFeature.Shape

            'Get second child
            child2 = node.Children(1)
            pNodeQFilter = New QueryFilter
            pNodeQFilter.WhereClause = "NodeID = " + CStr(child2.Id)
            pFCursor = Me.NodePolyFeatureClass.Search(pNodeQFilter, False)
            pChild2PolyFeature = pFCursor.NextFeature
            pChild2Area = pChild2PolyFeature.Shape

            'Intersect child polygons
            thresh = False
            over = False
            pTopoOp = pChild1PolyFeature.Shape
            pTopoOp.Simplify()
            newPoly = pTopoOp.Intersect(pChild2PolyFeature.Shape, esriGeometryDimension.esriGeometry2Dimension)

            If Not newPoly.IsEmpty Then

                'Child MCPs intersect
                pNewArea = newPoly
                over = True

                'Save Overlap with z of parent 
                pZAware = newPoly
                pZAware.ZAware = True

                pPolyFeature = Me.DavaPolyFeatureClass.CreateFeature()
                pPolyFeature.Shape = newPoly
                count = count + 1
                pPolyFeature.Value(2) = count
                pPolyFeature.Value(3) = node.Id
                pPolyFeature.Value(4) = node.Name
                If (pNewArea.Area / pChild1Area.Area <= (Me.DAVA_Threshold / 100)) _
                    And (pNewArea.Area / pChild2Area.Area <= (Me.DAVA_Threshold / 100)) Then
                    thresh = True
                    pPolyFeature.Value(7) = DAVA_OVERLAP_BELOW_THRESH
                Else
                    pPolyFeature.Value(7) = DAVA_OVERLAP_ABOVE_THRESH
                End If
                pPolyFeature.Value(8) = Math.Max(pNewArea.Area / pChild1Area.Area, pNewArea.Area / pChild2Area.Area)
                pPolyFeature.Store()

                ' Calculate overlap, clip children MCPs and save

                'Child1
                pTopoOp2 = pChild1PolyFeature.Shape
                pTopoOp2.Simplify()
                pChdPoly = pTopoOp2.Difference(newPoly)

                pZAware = pChdPoly
                pZAware.ZAware = True
                AddZ_To_Geometry(pChdPoly, -9999)

                pDAVAchd1PolyFeature = Me.DavaPolyFeatureClass.CreateFeature()
                pDAVAchd1PolyFeature.Shape = pChdPoly
                count = count + 1
                pDAVAchd1PolyFeature.Value(2) = count
                pDAVAchd1PolyFeature.Value(3) = node.Id
                pDAVAchd1PolyFeature.Value(4) = node.Name
                pDAVAchd1PolyFeature.Value(5) = child1.Id
                pDAVAchd1PolyFeature.Value(6) = child1.Name
                pDAVAchd1PolyFeature.Value(7) = DAVA_CHILD_POLY
                pDAVAchd1PolyFeature.Value(8) = pNewArea.Area / pChild1Area.Area
                pDAVAchd1PolyFeature.Store()

                ' Child2
                pTopoOp2 = pChild2PolyFeature.Shape
                pTopoOp2.Simplify()
                pChdPoly = pTopoOp2.Difference(newPoly)

                pZAware = pChdPoly
                pZAware.ZAware = True
                AddZ_To_Geometry(pChdPoly, -9999)

                pDAVAchd2PolyFeature = Me.DavaPolyFeatureClass.CreateFeature()
                pDAVAchd2PolyFeature.Shape = pChdPoly
                count = count + 1
                pDAVAchd2PolyFeature.Value(2) = count
                pDAVAchd2PolyFeature.Value(3) = node.Id
                pDAVAchd2PolyFeature.Value(4) = node.Name
                pDAVAchd2PolyFeature.Value(5) = child2.Id
                pDAVAchd2PolyFeature.Value(6) = child2.Name
                pDAVAchd2PolyFeature.Value(7) = DAVA_CHILD_POLY
                pDAVAchd2PolyFeature.Value(8) = pNewArea.Area / pChild2Area.Area
                pDAVAchd2PolyFeature.Store()

                'unoccupied area
                'union child polygons and symmetric difference with parent poly

                pTopoOp2 = pChild1PolyFeature.Shape
                pTopoOp2.Simplify()
                newPoly.SpatialReference = pChild1PolyFeature.Shape.SpatialReference
                newPoly.SnapToSpatialReference()
                newPoly = pTopoOp.Union(pChild2PolyFeature.Shape)

                pTopoOp = newPoly
                newPoly2 = pTopoOp.SymmetricDifference(pParentPolyFeature.Shape)

            Else

                'Disjunct so save MCPs

                'Child1
                pChdPoly = pChild1PolyFeature.Shape
                pZAware = pChdPoly
                pZAware.ZAware = True
                AddZ_To_Geometry(pChdPoly, -9999)
                pDAVAchd1PolyFeature = Me.DavaPolyFeatureClass.CreateFeature()
                pDAVAchd1PolyFeature.Shape = pChdPoly
                count = count + 1
                pDAVAchd1PolyFeature.Value(2) = count
                pDAVAchd1PolyFeature.Value(3) = node.Id
                pDAVAchd1PolyFeature.Value(4) = node.Name
                pDAVAchd1PolyFeature.Value(5) = child1.Id
                pDAVAchd1PolyFeature.Value(6) = child1.Name
                pDAVAchd1PolyFeature.Value(7) = DAVA_CHILD_POLY
                pDAVAchd1PolyFeature.Value(8) = 0
                pDAVAchd1PolyFeature.Store()

                ' Child2
                pChdPoly = pChild2PolyFeature.Shape
                pZAware = pChdPoly
                pZAware.ZAware = True
                AddZ_To_Geometry(pChdPoly, -9999)
                pDAVAchd2PolyFeature = Me.DavaPolyFeatureClass.CreateFeature()
                pDAVAchd2PolyFeature.Shape = pChdPoly
                count = count + 1
                pDAVAchd2PolyFeature.Value(2) = count
                pDAVAchd2PolyFeature.Value(3) = node.Id
                pDAVAchd2PolyFeature.Value(4) = node.Name
                pDAVAchd2PolyFeature.Value(5) = child2.Id
                pDAVAchd2PolyFeature.Value(6) = child2.Name
                pDAVAchd2PolyFeature.Value(7) = DAVA_CHILD_POLY
                pDAVAchd2PolyFeature.Value(8) = 0
                pDAVAchd2PolyFeature.Store()

                'Difference between parent and child polys
                pTopoOp = pParentPolyFeature.Shape
                pTopoOp.Simplify()
                newPoly = pTopoOp.Difference(pChild1PolyFeature.Shape)
                newPoly.SpatialReference = pChild1PolyFeature.Shape.SpatialReference
                pTopoOp2 = newPoly
                pTopoOp2.Simplify()
                newPoly2 = pTopoOp2.Difference(pChild2PolyFeature.Shape)

            End If

            'Add to disjunct region to Featureclass
            'Give polygon z of parent 
            pZAware = newPoly2
            pZAware.ZAware = True
            AddZ_To_Geometry(newPoly2, -9999)
            pPolyFeature = Me.DavaPolyFeatureClass.CreateFeature()
            pPolyFeature.Shape = newPoly2
            count = count + 1
            pPolyFeature.Value(2) = count
            pPolyFeature.Value(3) = node.Id
            pPolyFeature.Value(4) = node.Name

            If over = True Then
                If thresh = True Then
                    pPolyFeature.Value(7) = DAVA_REMAINDER_BELOW_THRESH
                Else
                    pPolyFeature.Value(7) = DAVA_REMAINDER_ABOVE_THRESH
                End If
            Else
                pPolyFeature.Value(7) = DAVA_DISJUNCTION
            End If

            pPolyFeature.Store()

        End If
    End Sub
    Public Sub AddZ_To_Geometry(ByRef myGeometry As IGeometry, ByVal myZ As Double)

        'Adds my Z to all points in geometry

        Dim pZAware As IZAware
        Dim pPoint As IPoint
        Dim pPointColl As IPointCollection
        Dim i As Long

        pZAware = myGeometry
        pZAware.ZAware = True

        Select Case myGeometry.GeometryType

            Case esriGeometryType.esriGeometryPoint
                pPoint = myGeometry
                pPoint.Z = myZ
                pPoint = Nothing
            Case esriGeometryType.esriGeometryMultipoint, esriGeometryType.esriGeometryPolyline, esriGeometryType.esriGeometryPolygon
                pPointColl = myGeometry
                For i = 0 To pPointColl.PointCount - 1
                    pPoint = pPointColl.Point(i)
                    pPoint.Z = myZ
                    pPointColl.UpdatePoint(i, pPoint)
                    pPoint = Nothing
                Next i
                pPointColl = Nothing
            Case Else
        End Select
        pZAware = Nothing
    End Sub
End Class


