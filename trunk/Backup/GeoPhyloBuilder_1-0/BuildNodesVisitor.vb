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

<ComClass(BuildNodesVisitor.ClassId, BuildNodesVisitor.InterfaceId, BuildNodesVisitor.EventsId)> _
Public Class BuildNodesVisitor
    Inherits BuildVisitor
    Private prop_to_brdepth As Boolean
    Private tip_fan As Double
    Private linear_stretch As Boolean


#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "20083902-1c41-4315-8a58-3ede62b7e263"
    Public Const InterfaceId As String = "366f7ec6-cbd1-4fba-8d28-230a2126513b"
    Public Const EventsId As String = "a4b832c3-a9d3-4e08-8488-ab975177d9d4"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
        prop_to_brdepth = False
        tip_fan = False
        linear_stretch = False
    End Sub

    Public Property PropBrDepth() As Boolean
        Get
            Return prop_to_brdepth
        End Get
        Set(ByVal value As Boolean)
            prop_to_brdepth = value
        End Set
    End Property
    Public Property TipFan() As Double
        Get
            Return tip_fan
        End Get
        Set(ByVal value As Double)
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
        Dim rankid As String
        rankid = "0"
        Dim chld As PhyloTreeNode
        Dim index As Integer

        If Not FeatureClass Is Nothing Then

            'Debug.Print(node.Name)
            'Debug.Print("n Child: " & node.Children.Count)

            Dim pNodeFeature As IFeature
            Dim pNode As IPoint

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
                    If Not (node.Type = PhyloTreeNode.TREE_NODE_TYPE_TERMINAL) Then
                        .Z = (MaxDepth - node.DepthFromRoot) * MultipleZ
                    Else
                        .Z = (MaxDepth - node.DepthFromRoot + (node.Distance * (Me.TipFan / 100))) * MultipleZ
                    End If
                Else
                    If Me.LinearStretch = False Then
                        If Not node.Type = PhyloTreeNode.TREE_NODE_TYPE_TERMINAL Then
                            .Z = (MaxDepth - node.LevelFromRoot) * MultipleZ
                        Else 
                            .Z = (MaxDepth - node.LevelFromRoot + (Me.TipFan / 100)) * MultipleZ
                        End If
                    Else
                        'Linear stretch
                        If node.Type = PhyloTreeNode.TREE_NODE_TYPE_ROOT Then
                            .Z = MaxDepth * MultipleZ
                        ElseIf node.Type = PhyloTreeNode.TREE_NODE_TYPE_TERMINAL Then
                            .Z = (((node.LevelToTip / (node.LevelFromRoot + node.LevelToTip)) * MaxDepth) * MultipleZ) + node.Parent.MyZ * (Me.TipFan / 100)
                        Else
                            .Z = ((node.LevelToTip / (node.LevelFromRoot + node.LevelToTip)) * MaxDepth) * MultipleZ
                        End If
                End If
                    End If

                node.MyZ = .Z
            End With


            pNodeFeature = FeatureClass.CreateFeature()
            pNodeFeature.Shape = pNode
            pNodeFeature.Value(2) = node.Id
            pNodeFeature.Value(3) = node.Name
            pNodeFeature.Value(4) = node.Type
            pNodeFeature.Value(5) = node.LevelFromRoot
            pNodeFeature.Value(6) = node.LevelToTip
            pNodeFeature.Value(7) = node.Distance
            pNodeFeature.Value(8) = node.DepthFromRoot
            pNodeFeature.Value(9) = node.HeightToTip
            pNodeFeature.Value(10) = node.MyZ

            index = 0

            If Not (node.Type = PhyloTreeNode.TREE_NODE_TYPE_ROOT) Then
                For Each chld In node.Parent.Children
                    If (chld.Id = node.Id) Then
                        Exit For
                    End If
                    index = index + 1
                Next

                rankid = node.Parent.RankId.Trim() + "." + Str(index).Trim()

            End If
            node.RankId = rankid

            pNodeFeature.Value(11) = rankid
            pNodeFeature.Store()
            pZAware = Nothing
            pNodeFeature = Nothing
            pNode = Nothing

        End If
    End Sub

End Class


