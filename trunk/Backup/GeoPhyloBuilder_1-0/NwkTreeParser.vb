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
<ComClass(NwkTreeParser.ClassId, NwkTreeParser.InterfaceId, NwkTreeParser.EventsId)> _
Public Class NwkTreeParser
    Dim m_treeString As String

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "8c9edd18-9a91-47d4-b8af-a3080f8b9b54"
    Public Const InterfaceId As String = "a88b38f1-7454-4ded-9693-071eff6b0b0b"
    Public Const EventsId As String = "bd730a66-03ec-4352-b9af-a8e0a81ed95c"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub
    Public Property TreeString() As String
        Get
            Return m_treeString

        End Get
        Set(ByVal value As String)
            m_treeString = value
        End Set
    End Property

    Public Function parse(ByVal str As String) As PhyloTreeNode

        Dim st As Stack
        st = New Stack


        Dim treeNode As PhyloTreeNode
        Dim root As PhyloTreeNode = Nothing
        Dim MyID As Long
        Dim NodeID As Long
        Dim parent As PhyloTreeNode
        Dim parent1 As PhyloTreeNode

        MyID = 0
        NodeID = 1
        Dim tk As NwkToken
        Dim nwktokenizer As NwkTokenizer = New NwkTokenizer

        nwktokenizer.TokenString = str
        tk = nwktokenizer.NextToken
        While tk.Type <> NwkToken.TOKEN_TYPE_END
            Select Case tk.Type
                Case NwkToken.TOKEN_TYPE_LEFT_PARENTHESIS
                    If st.Count > 0 Then
                        parent = st.Peek()
                    End If


                    treeNode = New PhyloTreeNode

                    treeNode.Id = MyID

                    If parent Is Nothing Then
                        treeNode.Name = "root"
                        treeNode.Type = PhyloTreeNode.TREE_NODE_TYPE_ROOT
                        root = treeNode
                        st.Push(treeNode)
                        treeNode = Nothing
                    Else
                        treeNode.Name = "node_" + Microsoft.VisualBasic.Str(NodeID).Trim()
                        treeNode.Type = PhyloTreeNode.TREE_NODE_TYPE_INNER
                        parent.AddChild(treeNode)
                        st.Push(treeNode)
                        treeNode = Nothing
                        NodeID = NodeID + 1
                    End If

                Case NwkToken.TOKEN_TYPE_RIGHT_PARENTHESIS
                    If st.Count = 0 Then
                        Throw New System.Exception("NWK Tree Syntex error: no left parenthesis accompanying the right parenthesis.")
                    End If

                    parent1 = st.Pop()
                    If parent1 Is Nothing Then
                        Throw New System.Exception("NWK Tree Syntex error: no left parenthesis accompanying the right parenthesis.")
                    End If
                Case NwkToken.TOKEN_TYPE_COLON
                    If parent1 Is Nothing Then
                        Throw New System.Exception("NWK Tree Syntex error: no tree node specified for the colon")
                    End If
                    Dim tktmp As NwkToken
                    tktmp = nwktokenizer.NextToken
                    If tktmp.Type <> NwkToken.TOKEN_TYPE_NUMBER Then
                        Throw New System.Exception("NWK Tree Syntex error: no distance specified after the colon")
                    End If

                    If CDbl(tktmp.Value) > 0.0 Then parent1.Distance = tktmp.Value Else parent1.Distance = 0.0
                    parent1.IsDistanced = True
                Case NwkToken.TOKEN_TYPE_COMMA
                Case NwkToken.TOKEN_TYPE_STRING
                    If st.Count = 0 Then
                        Throw New System.Exception("NWK Tree Syntex error: no beginning left parenthesis.")
                    End If
                    parent = st.Peek()
                    If parent Is Nothing Then
                        Throw New System.Exception("NWK Tree Syntex error: no beginning left parenthesis.")
                    End If
                    treeNode = New PhyloTreeNode
                    treeNode.Name = tk.Value
                    treeNode.Type = PhyloTreeNode.TREE_NODE_TYPE_TERMINAL
                    treeNode.Id = MyID
                    parent1 = treeNode
                    parent.AddChild(treeNode)
                    treeNode = Nothing

            End Select
            MyID = MyID + 1
            tk = nwktokenizer.NextToken
        End While
        st.Clear()
        st = Nothing
        Return root
    End Function
End Class


