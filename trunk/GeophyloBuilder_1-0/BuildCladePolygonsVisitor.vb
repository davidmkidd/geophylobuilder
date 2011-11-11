'
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
<ComClass(BuildCladePolygonsVisitor.ClassId, BuildCladePolygonsVisitor.InterfaceId, BuildCladePolygonsVisitor.EventsId)> _
Public Class BuildCladePolygonsVisitor
    Inherits BuildVisitor
    Private m_PolyBuffer As Double      'Buffer around mcp polygons
    Private m_PandLBuffer As Double     'Buffer around single and pairs of samples
    Private count As Integer

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Shadows Const ClassId As String = "e3971abe-e099-418b-95ec-f79a3bc37973"
    Public Shadows Const InterfaceId As String = "56ba3d89-d815-456a-ad90-da988e4c4b3e"
    Public Shadows Const EventsId As String = "1ff7e030-4fc6-4ed4-8b9a-5237e31793b3"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
        count = 0
        m_PolyBuffer = -1
        m_PandLBuffer = -1
    End Sub

    Public Property PolyBufferSize() As Double
        Get
            Return m_PolyBuffer
        End Get
        Set(ByVal value As Double)
            m_PolyBuffer = value
        End Set
    End Property

    Public Property PandLBufferSize() As Double
        Get
            Return m_PandLBuffer
        End Get
        Set(ByVal value As Double)
            m_PandLBuffer = value
        End Set
    End Property


    Public Overrides Sub visit(ByVal node As PhyloTreeNode)
        Dim pPolyFeature As IFeature
        Dim pPolygon As IGeometry
        Dim pPointColl As IPointCollection
        Dim pMultiPoints As IMultipoint
        Dim pTopoOp As ITopologicalOperator
        Dim pLine As ILine
        Dim pSegmentColl As ISegmentCollection
        Dim pZAware As IZAware
        Dim pColl As ICollection
        Dim myNode As PhyloTreeNode
        Dim myPoint As IPoint

        If node.Type <> PhyloTreeNode.TREE_NODE_TYPE_SAMPLE Then

            pColl = node.GetSubcladeSamples()    '***superceeded by GetSubclade visitor
            pMultiPoints = New Multipoint
            pPointColl = pMultiPoints

            For Each myNode In pColl
                myPoint = New Point
                myPoint.X = myNode.X
                myPoint.Y = myNode.Y
                pPointColl.AddPoint(myPoint)
            Next

            Select Case pColl.Count

                Case 0
                    'Invalid option *** Need to catch exception

                Case 1
                    'Single observation so buffer using bufferdistance or point-line bufferdistance
                    pTopoOp = pPointColl.Point(0)
                    pPolygon = pTopoOp.Buffer(Me.PandLBufferSize)
                Case 2
                    pSegmentColl = New Polyline
                    pZAware = pSegmentColl
                    pZAware.ZAware = True

                    pLine = New Line
                    pLine.PutCoords(pPointColl.Point(0), pPointColl.Point(1))
                    pSegmentColl.AddSegment(pLine)
                    pTopoOp = pSegmentColl
                    pPolygon = pTopoOp.Buffer(Me.PandLBufferSize)
                    pLine = Nothing
                    pSegmentColl.RemoveSegments(0, pSegmentColl.SegmentCount, False)

                    pSegmentColl = Nothing
                    pZAware = Nothing
                Case Is > 2
                    pTopoOp = pMultiPoints
                    pPolygon = pTopoOp.ConvexHull

                    If Me.PolyBufferSize > 0 Then
                        pTopoOp = Nothing
                        pTopoOp = pPolygon
                        pPolygon = Nothing
                        pPolygon = pTopoOp.Buffer(Me.PolyBufferSize)
                    End If
            End Select

            'Give polygon z of node 

            pZAware = pPolygon
            pZAware.ZAware = True

            AddZ_To_Geometry(pPolygon, node.MyZ)

            pPolyFeature = FeatureClass.CreateFeature()
            pPolyFeature.Shape = pPolygon
            count = count + 1
            pPolyFeature.Value(2) = count
            pPolyFeature.Value(3) = node.Id
            pPolyFeature.Value(4) = node.Name
            'pPolyFeature.Value(5) = 1
            pPolyFeature.Store()

        End If

        If Not pColl Is Nothing Then pColl = Nothing
        If Not pLine Is Nothing Then pLine = Nothing
        If Not pPolyFeature Is Nothing Then pPolyFeature = Nothing
        If Not pPolygon Is Nothing Then pPolygon = Nothing
        If Not pPointColl Is Nothing Then pPointColl = Nothing
        If Not pMultiPoints Is Nothing Then pMultiPoints = Nothing
        If Not pTopoOp Is Nothing Then pTopoOp = Nothing
        If Not myNode Is Nothing Then myNode = Nothing

        If Not pSegmentColl Is Nothing Then
            pSegmentColl.RemoveSegments(0, pSegmentColl.SegmentCount, False)
            pSegmentColl = Nothing
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


