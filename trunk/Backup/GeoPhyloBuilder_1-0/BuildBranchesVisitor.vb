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
Imports ESRI.ArcGIS.Framework


Imports System.Runtime.InteropServices
<ComClass(BuildBranchesVisitor.ClassId, BuildBranchesVisitor.InterfaceId, BuildBranchesVisitor.EventsId)> _
Public Class BuildBranchesVisitor
    Inherits BuildVisitor
    Private count As Integer
    Private vertex_type As String
    Private vertex_no As Double
    Private vertex_shape As String
    Private vertex_xdepth As Double
    Private tip_fan As Boolean
    Private linear_stretch As Boolean

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Shadows Const ClassId As String = "448fa7fc-ceec-4abb-a5a4-f56d71d26c25"
    Public Shadows Const InterfaceId As String = "fa6258ee-2d6b-48aa-a046-71cd07dd3a75"
    Public Shadows Const EventsId As String = "ca319e7d-7e33-46fd-99be-7a674d618195"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
        count = 0
        vertex_type = "default"
        vertex_no = 1
        vertex_shape = "Triangular"
        vertex_xdepth = 0
    End Sub

    Public Property VertexType() As String
        Get
            Return vertex_type
        End Get
        Set(ByVal value As String)
            vertex_type = value
        End Set
    End Property

    Public Property VertexShape() As String
        Get
            Return vertex_shape
        End Get
        Set(ByVal value As String)
            vertex_shape = value
        End Set
    End Property

    Public Property VertexNo() As Double
        Get
            Return vertex_no
        End Get
        Set(ByVal value As Double)
            vertex_no = value
        End Set
    End Property

    Public Property VertexXDepth() As Double
        Get
            Return vertex_xdepth
        End Get
        Set(ByVal value As Double)
            vertex_xdepth = value
        End Set
    End Property

    Public Property TipFan() As Boolean
        Get
            Return tip_fan
        End Get
        Set(ByVal value As Boolean)
            tip_fan = value
        End Set
    End Property
    Public Property LinearStretch() As Boolean
        Get
            Return linear_stretch
        End Get
        Set(ByVal value As Boolean)
            linear_stretch = value
        End Set
    End Property

    Public Overrides Sub visit(ByVal node As PhyloTreeNode)
        Dim chd As PhyloTreeNode
        Dim parent As PhyloTreeNode
        If node.Children.Count > 0 Then
            If Not FeatureClass Is Nothing Then
                Dim pLineFeature As IFeature
                Dim pPolyLine As IPolyline
                Dim pLine As ILine
                Dim pFrom As IPoint
                Dim pTo As IPoint
                Dim pSpat As ISpatialReference
                Dim pFClass As IFeatureClass
                Dim pInGeoDataSet As IGeoDataset
                pFClass = FeatureClass
                pInGeoDataSet = pFClass
                pSpat = pInGeoDataSet.SpatialReference

                Dim pZAware As IZAware
                ' Dims for multipart
                Dim pGeomColl As IGeometryCollection
                Dim pSegPoly As IPolyline


                Dim pFromJoinPt As IPoint
                Dim pToJoinPt As IPoint
                Dim pFromLine As ILine
                Dim pToLine As ILine
                Dim pFromPath As ISegmentCollection
                Dim pToPath As ISegmentCollection

                Dim i As Integer

                pFrom = New Point
                pZAware = pFrom
                If pZAware.ZAware = True Then
                    pZAware.DropZs()
                    pZAware.ZAware = False
                End If

                pZAware.ZAware = True
                pFrom.Z = -9999.0#

                pZAware = Nothing
                pFrom.X = node.X
                pFrom.Y = node.Y
                pFrom.Z = node.MyZ


                ' Cycle through child nodes
                For i = 0 To node.Children.Count - 1
                    chd = node.Children.Item(i)


                    '' If child is sample and Tip Fan to be built
                    'If Me.tip_fan = True Then

                    '    Select Case chd.Type
                    '        Case PhyloTreeNode.TREE_NODE_TYPE_SAMPLE
                    '            parent = node.Parent
                    '            pFrom.X = parent.X
                    '            pFrom.Y = parent.Y
                    '            If UseDistance Then
                    '                pFrom.Z = (MaxDepth - parent.DepthFromRoot) * MultipleZ
                    '            Else
                    '                If Me.LinearStretch = False Then
                    '                    pFrom.Z = (MaxDepth - parent.LevelFromRoot) * MultipleZ
                    '                Else
                    '                    pFrom.Z = ((parent.LevelToTip / (parent.LevelFromRoot + parent.LevelToTip)) * MaxDepth) * MultipleZ
                    '                End If
                    '            End If

                    '        Case PhyloTreeNode.TREE_NODE_TYPE_TERMINAL
                    '            Exit Sub

                    '    End Select

                    'End If


                    pTo = New Point
                    pZAware = pTo
                    If pZAware.ZAware = True Then
                        pZAware.DropZs()
                        pZAware.ZAware = False
                    End If

                    pZAware.ZAware = True
                    pTo.Z = -9999.0#
                    pZAware = Nothing

                    pTo.X = chd.X
                    pTo.Y = chd.Y
                    pTo.Z = chd.MyZ


                    pLineFeature = FeatureClass.CreateFeature()
                    pPolyLine = New Polyline
                    pZAware = pPolyLine
                    pZAware.ZAware = True
                    pZAware = Nothing

                    'pLine = New Line
                    'pLine.FromPoint = pFrom
                    'pLine.ToPoint = pTo

                    pPolyLine.FromPoint = pFrom
                    pPolyLine.ToPoint = pTo

                    'If shortest path crosses -180/+180 long then multipart
                    If Math.Max(pFrom.X, pTo.X) - Math.Min(pFrom.X, pTo.X) > 180 _
                    And TypeOf pSpat Is IGeographicCoordinateSystem Then

                        'Create line parts
                        pToLine = New Line
                        pFromLine = New Line
                        pFromJoinPt = New Point
                        pToJoinPt = New Point
                        pFromPath = New Path
                        pToPath = New Path
                        pSegPoly = New Polyline
                        pZAware = pSegPoly
                        pZAware.ZAware = True
                        pZAware = Nothing

                        pFromLine.FromPoint = pFrom
                        pToLine.ToPoint = pTo

                        'Calculate difference between points in X and Y
                        Dim dx As Double    'difference from - to
                        Dim dx1 As Double   'distance from - line
                        Dim dy As Double    'difference from - to
                        Dim dy1 As Double   'distance from - line
                        Dim dz As Double    'difference from - to
                        Dim dz1 As Double   'distance from - line
                        dx = (Math.Min(pFrom.X, pTo.X) + 180) + (180 - Math.Max(pFrom.X, pTo.X))
                        dy = Math.Max(pFrom.Y, pTo.Y) - Math.Min(pFrom.Y, pTo.Y)
                        dz = Math.Max(pFrom.Z, pTo.Z) - Math.Min(pFrom.Z, pTo.Z)
                        If pFrom.X < pTo.X Then
                            dx1 = pFrom.X + 180
                        Else
                            dx1 = 180 - pFrom.X
                        End If

                        dy1 = dy / dx * dx1
                        dz1 = dz * dx1 / dx

                        If pFrom.Z > pTo.Z Then
                            pFromJoinPt.Z = pFrom.Z - dz1
                        Else
                            pFromJoinPt.Z = pFrom.Z + dz1
                        End If

                        If pFrom.X < pTo.X Then
                            pFromJoinPt.X = -180
                            If pFrom.Y < pTo.Y Then
                                pFromJoinPt.Y = pFrom.Y + dy1
                            Else
                                pFromJoinPt.Y = pFrom.Y - dy1
                            End If

                            pFromLine.ToPoint = pFromJoinPt

                            pToJoinPt.X = 180
                            pToJoinPt.Y = pFromJoinPt.Y
                            pToJoinPt.Z = pFromJoinPt.Z
                            pToLine.FromPoint = pToJoinPt
                        Else
                            pFromJoinPt.X = 180
                            If pFrom.Y < pTo.Y Then
                                pFromJoinPt.Y = pFrom.Y + dy1
                            Else
                                pFromJoinPt.Y = pFrom.Y - dy1
                            End If

                            pFromLine.ToPoint = pFromJoinPt

                            pToJoinPt.X = -180
                            pToJoinPt.Y = pFromJoinPt.Y
                            pToJoinPt.Z = pFromJoinPt.Z
                            pToLine.FromPoint = pToJoinPt
                        End If


                        pLineFeature.Shape = pPolyLine
                        pFromPath.AddSegment(pFromLine)
                        pToPath.AddSegment(pToLine)
                        pGeomColl = pSegPoly
                        pGeomColl.AddGeometry(pFromPath)
                        pGeomColl.AddGeometry(pToPath)

                        pLineFeature.Shape = pGeomColl
                        'Densify
                        If VertexType = "Number" And pPolyLine.FromPoint.X <> pPolyLine.ToPoint.X _
                        And pPolyLine.FromPoint.Y <> pPolyLine.ToPoint.Y Then
                            pPolyLine = pLineFeature.Shape
                            pPolyLine.Densify(pPolyLine.Length / VertexNo, 0)
                            pLineFeature.Shape = pPolyLine
                        End If

                    Else
                        'Densify
                        If VertexType = "Number" And pPolyLine.FromPoint.X <> pPolyLine.ToPoint.X _
                        And pPolyLine.FromPoint.Y <> pPolyLine.ToPoint.Y Then
                            pPolyLine.Densify((pPolyLine.Length / VertexNo), 0)
                        End If
                        pLineFeature.Shape = pPolyLine
                    End If

                    If Math.Max(pFrom.X, pTo.X) - Math.Min(pFrom.X, pTo.X) = 180 Then
                        sendMessage("comment about 180 deg branch direction")
                    End If

                    count = count + 1
                    pLineFeature.Value(2) = count
                    pLineFeature.Value(3) = (node.Name).Trim() & "-" & (chd.Name).Trim()
                    pLineFeature.Value(4) = node.Id
                    pLineFeature.Value(5) = chd.Id
                    If chd.Type = 3 Then pLineFeature.Value(6) = 2 Else pLineFeature.Value(6) = 1
                    If node.IsDistanced() Then
                        pLineFeature.Value(7) = chd.Distance
                    Else
                        pLineFeature.Value(7) = 1
                    End If

                    pLineFeature.Value(8) = Math.Sqrt((pFrom.X - pTo.X) * (pFrom.X - pTo.X) + (pFrom.Y - pTo.Y) * (pFrom.Y - pTo.Y) + (pFrom.Z - pTo.Z) * (pFrom.Z - pTo.Z))
                    pLineFeature.Value(9) = node.RankId
                    pLineFeature.Value(10) = chd.RankId
                    pLineFeature.Store()
                    chd = Nothing
                    pTo = Nothing
                    pLine = Nothing
                    pPolyLine = Nothing
                    pLineFeature = Nothing
                    pFromPath = Nothing
                    pToPath = Nothing
                    pGeomColl = Nothing
                    'pSegPoly = Nothing

                    Select Case VertexShape

                        Case "Rectangular"
                            ' Go through all lines assignining From z to all except To z
                            ' Duplicate ToPoint and change penumtimate to From z
                            Dim pFCursor As IFeatureCursor
                            Dim pFeature As IFeature
                            Dim pPline As IPolyline
                            Dim maxZ As Double
                            Dim minZ As Double
                            Dim brZ As Double
                            Dim pPointColl As IPointCollection
                            Dim j As Long
                            Dim k As Long
                            Dim pPoint As IPoint = New Point
                            Dim addZ1 As Double
                            Dim addZ2 As Double
                            Dim pAddPoint As IPoint = New Point
                            Dim pAddPoint2 As IPoint = New Point

                            pFCursor = pFClass.Update(Nothing, False)
                            pFeature = pFCursor.NextFeature
                            Do Until pFeature Is Nothing
                                pPline = pFeature.Shape
                                pGeomColl = pPline

                                ' get min and max z
                                For j = 0 To pGeomColl.GeometryCount - 1
                                    pPointColl = pGeomColl.Geometry(j)
                                    For k = 0 To pPointColl.PointCount - 1
                                        pPoint = pPointColl.Point(k)
                                        If j = 0 And k = 0 Then maxZ = pPoint.Z
                                        If j = pGeomColl.GeometryCount - 1 And k = pPointColl.PointCount - 1 Then minZ = pPoint.Z
                                    Next
                                Next

                                ' Now change values

                                Select Case VertexXDepth
                                    Case 0
                                        For j = 0 To pGeomColl.GeometryCount - 1
                                            pPointColl = pGeomColl.Geometry(j)
                                            For k = 0 To pPointColl.PointCount - 1
                                                pPoint = pPointColl.Point(k)
                                                pPoint.Z = maxZ
                                                pPointColl.UpdatePoint(k, pPoint)
                                                If j = pGeomColl.GeometryCount - 1 And k = pPointColl.PointCount - 1 Then
                                                    pAddPoint = pPoint
                                                    pAddPoint.Z = minZ
                                                    pPointColl.AddPoint(pAddPoint)
                                                End If
                                            Next
                                        Next

                                    Case 100
                                        For j = 0 To pGeomColl.GeometryCount - 1
                                            pPointColl = pGeomColl.Geometry(j)
                                            For k = 0 To pPointColl.PointCount - 1
                                                pPoint = pPointColl.Point(k)
                                                If j = 0 And k = 0 Then
                                                    pAddPoint = pPoint
                                                    addZ1 = maxZ
                                                End If
                                                pPoint.Z = minZ
                                                pPointColl.UpdatePoint(k, pPoint)
                                            Next
                                            If j = 0 Then
                                                pAddPoint.Z = addZ1
                                                pPointColl.AddPoint(pAddPoint, 0)
                                            End If

                                        Next


                                    Case Else
                                        brZ = minZ + ((maxZ - minZ) * ((100 - VertexXDepth) / 100.0))
                                        For j = 0 To pGeomColl.GeometryCount - 1
                                            pPointColl = pGeomColl.Geometry(j)
                                            For k = 0 To pPointColl.PointCount - 1
                                                pPoint = pPointColl.Point(k)
                                                If j = 0 And k = 0 Then
                                                    pAddPoint = pPoint
                                                    addZ1 = pPoint.Z
                                                End If
                                                If j = pGeomColl.GeometryCount - 1 And k = pPointColl.PointCount - 1 Then
                                                    pAddPoint2 = pPoint
                                                    addZ2 = pPoint.Z
                                                End If
                                                pPoint.Z = brZ
                                                pPointColl.UpdatePoint(k, pPoint)
                                            Next

                                            If j = 0 Then
                                                pAddPoint.Z = addZ1
                                                pPointColl.AddPoint(pAddPoint, 0)
                                            End If

                                            If j = pGeomColl.GeometryCount - 1 Then
                                                pAddPoint2.Z = addZ2
                                                pPointColl.AddPoint(pAddPoint2)
                                            End If

                                        Next


                                End Select


                                pFeature.Store()
                                pFeature = pFCursor.NextFeature

                            Loop

                            pFCursor.Flush()
                            pFCursor = Nothing

                    End Select

                Next

                pFrom = Nothing
                pLineFeature = Nothing


            ElseIf Not Writer Is Nothing Then
                Dim i As Integer

                For i = 0 To node.Children.Count - 1
                    chd = node.Children.Item(i)
                    If chd.Type = PhyloTreeNode.TREE_NODE_TYPE_SAMPLE Then
                        Continue For
                    End If
                    Dim toz As Double
                    Dim frz As Double

                    If chd.Type = PhyloTreeNode.TREE_NODE_TYPE_SAMPLE Then
                        toz = 0
                    Else
                        If UseDistance Then
                            toz = (MaxDepth - chd.DepthFromRoot) * MultipleZ
                        Else
                            toz = (MaxDepth - chd.LevelFromRoot) * MultipleZ
                        End If
                    End If
                    If node.Type = PhyloTreeNode.TREE_NODE_TYPE_SAMPLE Then
                        frz = 0
                    Else
                        If UseDistance Then
                            frz = (MaxDepth - node.DepthFromRoot) * MultipleZ
                        Else
                            frz = (MaxDepth - node.LevelFromRoot) * MultipleZ
                        End If
                    End If
                    Writer.WriteLine("<Placemark>")
                    Writer.WriteLine("<name>" & (node.Name).Trim() & "-" & (chd.Name).Trim() & "</name>")
                    Writer.WriteLine("<description>tree branch</description>")
                    Writer.WriteLine("<styleUrl>#treeBranch</styleUrl>")
                    Writer.WriteLine("<LineString>")
                    Writer.WriteLine("<tessellate>1</tessellate>")
                    Writer.WriteLine("<altitudeMode>absolute</altitudeMode>")
                    Writer.WriteLine("<coordinates>")
                    Writer.WriteLine(node.X & "," & node.Y & "," & frz & " " & chd.X & "," & chd.Y & "," & toz & "</coordinates>")
                    Writer.WriteLine("</LineString>")
                    Writer.WriteLine("</Placemark>")
                Next
            End If
        End If
    End Sub
    
End Class


