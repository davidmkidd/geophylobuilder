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

<ComClass(BuildDropLinesVisitor.ClassId, BuildDropLinesVisitor.InterfaceId, BuildDropLinesVisitor.EventsId)> _
Public Class BuildDropLinesVisitor
    Inherits BuildVisitor
    Private count As Integer
    Private linear_stretch As Boolean
#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "bc092021-a365-4a08-88ee-d99306ca636b"
    Public Const InterfaceId As String = "d01c321c-5f1a-4dbb-8460-4cf6d84b2267"
    Public Const EventsId As String = "5cbcd22c-528d-46bb-bc12-e23748e94158"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
        count = 0
    End Sub

    Public Property LinearStretch() As Boolean
        Get
            Return linear_stretch
        End Get
        Set(ByVal value As Boolean)
            linear_stretch = value
        End Set
    End Property

   
    Public Overrides Sub visit(ByVal node As PhyloTreeNode)
        If Not FeatureClass Is Nothing Then
            Dim pLineFeature As IFeature
            Dim pPolyLine As IPolyline
            Dim pFrom As IPoint
            Dim pTo As IPoint
            Dim pZAware As IZAware

            pFrom = New Point
            pTo = New Point

            pZAware = pFrom
            If pZAware.ZAware = True Then
                pZAware.DropZs()
                pZAware.ZAware = False
            End If

            pZAware.ZAware = True
            pFrom.Z = -9999.0#
            pZAware = Nothing

            pZAware = pTo
            If pZAware.ZAware = True Then
                pZAware.DropZs()
                pZAware.ZAware = False
            End If

            pZAware.ZAware = True
            pFrom.Z = -9999.0#
            pZAware = Nothing

            pFrom.X = node.X
            pFrom.Y = node.Y
            pTo.X = node.X
            pTo.Y = node.Y

            ' MyZ
            'If UseDistance Then
            '    pFrom.Z = (MaxDepth - node.DepthFromRoot) * MultipleZ
            'Else
            '    If Me.LinearStretch = False Then
            '        pFrom.Z = (MaxDepth - node.LevelFromRoot) * MultipleZ
            '    Else
            '        If node.Name = "root" Then
            '            pFrom.Z = MaxDepth * MultipleZ
            '        Else
            '            pFrom.Z = ((node.LevelToTip / (node.LevelFromRoot + node.LevelToTip)) * MaxDepth) * MultipleZ
            '        End If
            '    End If
            'End If

            pFrom.Z = node.MyZ
            pTo.Z = DropToZ

            pLineFeature = FeatureClass.CreateFeature()
            pPolyLine = New Polyline
            pZAware = pPolyLine
            pZAware.ZAware = True
            pZAware = Nothing

            pPolyLine.FromPoint = pFrom
            pPolyLine.ToPoint = pTo
            pLineFeature.Shape = pPolyLine
            count = count + 1
            pLineFeature.Value(2) = count
            pLineFeature.Value(3) = node.Name & ":Drop"
            pLineFeature.Value(4) = node.Type
            pLineFeature.Value(5) = node.Id

            pLineFeature.Store()

            pFrom = Nothing
            pTo = Nothing
            pPolyLine = Nothing

            pLineFeature = Nothing

        ElseIf Not Writer Is Nothing Then
            If node.Type = PhyloTreeNode.TREE_NODE_TYPE_SAMPLE Then
                Return
            End If
            Dim z As Double

            If UseDistance Then
                z = (MaxDepth - node.DepthFromRoot) * MultipleZ

            Else
                z = (MaxDepth - node.LevelFromRoot) * MultipleZ

            End If

           
            Writer.WriteLine("<Placemark>")
            Writer.WriteLine("<name>" & (node.Name).Trim() & ":Drop Line</name>")
            Writer.WriteLine("<description>Drop Line form node " & (node.Name).Trim() & "</description>")
            Writer.WriteLine("<styleUrl>#treeBranchDrop</styleUrl>")
            Writer.WriteLine("<LineString>")
            Writer.WriteLine("<tessellate>1</tessellate>")
            Writer.WriteLine("<altitudeMode>absolute</altitudeMode>")
            Writer.WriteLine("<coordinates>")
            Writer.WriteLine(node.X & "," & node.Y & "," & z & " " & node.X & "," & node.Y & "," & DropToZ & "</coordinates>")
            Writer.WriteLine("</LineString>")
            Writer.WriteLine("</Placemark>")

        End If


    End Sub
End Class


