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

<ComClass(TreeViewVisitor.ClassId, TreeViewVisitor.InterfaceId, TreeViewVisitor.EventsId)> _
Public Class TreeViewVisitor
    Implements Visitor
    Dim mTreeView As TreeView

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "b2907906-a8af-4ea5-b1e2-72929c152f61"
    Public Const InterfaceId As String = "2f90990e-00a1-4cf7-89bd-1dad829b12aa"
    Public Const EventsId As String = "c6f718d2-fb90-46ab-88f9-3e4a0aac5805"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub
    Public Property TreeView() As TreeView
        Get
            Return mTreeView
        End Get
        Set(ByVal value As TreeView)
            mTreeView = value
        End Set
    End Property
    Public Sub visit(ByVal node As PhyloTreeNode) Implements Visitor.visit
        If TreeView Is Nothing Then
            Return
        End If

        Dim tn As TreeNode
        If node.Parent Is Nothing Then
            If mTreeView.Nodes.Count > 0 Then
                MsgBox("No parent found for node: " + node.Name)
                Return
            Else
                mTreeView.Nodes.Add(node.Name, node.Name)
            End If
        Else
            Dim tns As TreeNode()
            tns = mTreeView.Nodes.Find(node.Parent.Name, True)
            If (tns.Length > 0) Then
                tn = tns(0)
            End If
            If tn Is Nothing Then
                If mTreeView.Nodes.Count > 0 Then
                    MsgBox("No parent found for node: " + node.Name)
                    Return
                Else
                    mTreeView.Nodes.Add(node.Name, node.Name)
                End If
            Else
                tn.Nodes.Add(node.Name, node.Name)
            End If
        End If
    End Sub
End Class


