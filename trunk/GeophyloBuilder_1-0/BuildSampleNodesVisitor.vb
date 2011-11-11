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
<ComClass(BuildSampleNodesVisitor.ClassId, BuildSampleNodesVisitor.InterfaceId, BuildSampleNodesVisitor.EventsId)> _
Public Class BuildSampleNodesVisitor
    Inherits BuildVisitor
#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "f10d29ce-ba87-42e4-8529-672b1da1785c"
    Public Const InterfaceId As String = "015ddf6a-fa92-41bb-ad56-7dd3e0a00115"
    Public Const EventsId As String = "4162d573-f559-4224-9d32-b92eda2a204d"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides Sub visit(ByVal node As PhyloTreeNode)
        If Not node.Type = PhyloTreeNode.TREE_NODE_TYPE_SAMPLE Then
            Return
        End If

        Dim rankid As String
        rankid = "0"
        Dim chld As PhyloTreeNode
        Dim index As Integer

        If Not FeatureClass Is Nothing Then
            
            Dim pNodeFeature As IFeature
            Dim pNode As IPoint

            pNodeFeature = FeatureClass.CreateFeature()
            pNode = New Point()
            Dim pZAware As IZAware
            pZAware = pNode
            If pZAware.ZAware = True Then
                pZAware.DropZs()
                pZAware.ZAware = False
            End If

            pZAware.ZAware = True
            pNode.Z = -9999.0#

            With pNode
                .X = node.X
                .Y = node.Y
                If UseDistance Then
                    .Z = (MaxDepth - node.DepthFromRoot) * MultipleZ
                Else
                    .Z = (MaxDepth - node.LevelFromRoot) * MultipleZ
                End If

            End With

            pNodeFeature.Shape = pNode
            pNodeFeature.Value(2) = node.Id
            pNodeFeature.Value(3) = node.Name
            pNodeFeature.Value(4) = node.Type
            pNodeFeature.Value(5) = node.LevelFromRoot
            pNodeFeature.Value(6) = node.Distance
            pNodeFeature.Value(7) = node.DepthFromRoot
            pNodeFeature.Value(8) = node.LevelToTip
            pNodeFeature.Value(9) = node.HeightToTip

            index = 0
            If Not node.Type = PhyloTreeNode.TREE_NODE_TYPE_ROOT Then
                For Each chld In node.Parent.Children
                    If (chld.Id = node.Id) Then
                        Exit For
                    End If
                    index = index + 1
                Next

                rankid = node.Parent.RankId.Trim() + "." + Str(index).Trim()

            End If
            node.RankId = rankid

            pNodeFeature.Value(10) = rankid
            pNodeFeature.Store()
            pZAware = Nothing
            pNodeFeature = Nothing
            pNode = Nothing
        ElseIf Not Writer Is Nothing Then

            Writer.WriteLine("<Placemark>")
            Writer.WriteLine("<name>" & node.Name & "</name>")
            Writer.WriteLine("<visibility>1</visibility>")
            Writer.WriteLine("<description>tree node</description>")
            Writer.WriteLine("<styleUrl>#treeNodeSample</styleUrl>")
            Writer.WriteLine("<Point>")
            Writer.WriteLine("<altitudeMode>absolute</altitudeMode>")
            If node.Type = PhyloTreeNode.TREE_NODE_TYPE_SAMPLE Then
                node.Z = 0
            Else
                If UseDistance Then
                    node.Z = (MaxDepth - node.DepthFromRoot) * MultipleZ
                Else
                    node.Z = (MaxDepth - node.LevelFromRoot) * MultipleZ
                End If
            End If
            Writer.WriteLine("<coordinates>" & node.X & "," & node.Y & "," & node.Z & "</coordinates>")
            Writer.WriteLine("</Point>")
            Writer.WriteLine("</Placemark>")
        End If
    End Sub

End Class


