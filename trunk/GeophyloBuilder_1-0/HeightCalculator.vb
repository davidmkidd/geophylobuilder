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
<ComClass(HeightCalculator.ClassId, HeightCalculator.InterfaceId, HeightCalculator.EventsId)> _
Public Class HeightCalculator
    Implements Visitor


#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "8e0a8cf3-5234-4da7-a6dc-acb01bdb2314"
    Public Const InterfaceId As String = "e1abc2c2-a24d-45ff-9ff4-a4dad121be54"
    Public Const EventsId As String = "47ab1b93-ba8c-4730-8a07-392354d81880"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub visit(ByVal node As PhyloTreeNode) Implements Visitor.visit
        If node.Type = PhyloTreeNode.TREE_NODE_TYPE_TERMINAL Or node.Type = PhyloTreeNode.TREE_NODE_TYPE_SAMPLE Then
            node.HeightToTip = 0
            node.LevelToTip = 0
        Else
            Dim child As PhyloTreeNode
            Dim maxHeight As Double
            Dim maxLevel As Double
            Dim first As Boolean
            first = True
            'Find maximum level and height of childreg
            For Each child In node.Children
                If first = True Then
                    maxHeight = child.HeightToTip + child.Distance
                    maxLevel = child.LevelToTip + 1
                    first = False
                Else
                    If child.HeightToTip + child.Distance > maxHeight Then maxHeight = child.HeightToTip + child.Distance
                    If child.LevelToTip + 1 > maxLevel Then maxLevel = child.LevelToTip + 1
                End If
            Next
            node.LevelToTip = maxLevel
            node.HeightToTip = maxHeight
        End If

    End Sub
End Class


