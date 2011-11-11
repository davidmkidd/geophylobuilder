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

<ComClass(AddZtoPolyVisitor.ClassId, AddZtoPolyVisitor.InterfaceId, AddZtoPolyVisitor.EventsId)> _
Public Class AddZtoPolyVisitor
    Implements Visitor
#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "0fbd6722-cd5b-4a37-a359-ccd64eafd9ac"
    Public Const InterfaceId As String = "d7beb453-e605-42b6-ab20-4f6b53a31366"
    Public Const EventsId As String = "5b7f3ea7-8084-4d2e-9754-9b4e52f77721"
#End Region
    Private m_PolyFClass As IFeatureClass
    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
        m_PolyFClass = Nothing
    End Sub
    Public Property PolyFClass() As IFeatureClass
        Get
            Return m_PolyFClass
        End Get
        Set(ByVal value As IFeatureClass)
            m_PolyFClass = value
        End Set
    End Property
    Public Sub visit(ByVal node As PhyloTreeNode) Implements Visitor.visit

        Dim pQFilter As IQueryFilter
        Dim pFCursor As IFeatureCursor
        Dim pFeature As IFeature
        Dim pPolygon As IPolygon
        Dim pZAware As IZAware
        pQFilter = New QueryFilter
        pQFilter.WhereClause = "NodeID = " + CStr(node.Id)
        pFCursor = Me.PolyFClass.Search(pQFilter, False)
        pFeature = pFCursor.NextFeature
        Do Until pFeature Is Nothing
            pPolygon = pFeature.Shape
            pZAware = pPolygon
            pZAware.ZAware = True
            AddZ_To_Geometry(pPolygon, node.MyZ)
            pFeature.Shape = pPolygon
            pFeature.Store()
            pFeature = pFCursor.NextFeature
        Loop

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


