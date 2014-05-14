
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


<ComClass(PhyloTree.ClassId, PhyloTree.InterfaceId, PhyloTree.EventsId)> _
Public Class PhyloTree
    Dim mRoot As PhyloTreeNode
    Dim mTreeName As String
#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "948ef707-9b73-401b-8140-b5d216e76d83"
    Public Const InterfaceId As String = "145e2f59-8123-4838-9492-0cf3c38ec0a8"
    Public Const EventsId As String = "3facd10f-a172-4805-82c3-9c217e66337f"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

    Protected Overrides Sub finalize()
        mRoot = Nothing
        mTreeName = Nothing
    End Sub
    Public Property Root() As PhyloTreeNode
        Get
            Return mRoot
        End Get
        Set(ByVal value As PhyloTreeNode)
            mRoot = value
        End Set
    End Property

    Public Property TreeName() As String
        Get
            Return mTreeName
        End Get
        Set(ByVal value As String)
            mTreeName = value
        End Set
    End Property

    Public Sub VisitMe(ByVal visitor As Visitor, ByVal order As Integer)
        If mRoot Is Nothing Then
            MsgBox("Null PhyloTree")
            Return
        Else
            mRoot.VisitMe(visitor, order)
        End If

    End Sub
    Public Function FindById(ByVal key As String) As PhyloTreeNode
        If Root Is Nothing Then
            Return Nothing
        End If
        Return Root.FindById(key)
    End Function
    Public Function Find(ByVal key As String, Optional ByVal includeAttributes As Boolean = False) As PhyloTreeNode
        If Root Is Nothing Then
            Return Nothing
        End If
        Return Root.Find(key, includeAttributes)
    End Function
    Public Sub FindAll(ByVal key As String, ByRef list As ArrayList, Optional ByVal includeAttributes As Boolean = False)
        If Root Is Nothing Then
            Return
        End If
        Root.FindAll(key, list, includeAttributes)
    End Sub

    Public Function GetDepth(ByVal useDistance As Boolean) As Double
        Dim visitor As DepthVisitor
        visitor = New DepthVisitor
        visitor.UseDistance = useDistance
        VisitMe(visitor, PhyloTreeNode.VISITOR_TYPE_FORE)
        Return visitor.Depth()
    End Function
    Public Sub SetRoot(ByVal key As String)
        Dim fromNode As PhyloTreeNode
        fromNode = Find(key)

        SetRoot(fromNode)
    End Sub

    Public Sub SetRoot(ByVal splitPoint As SplitPoint)
        SetRoot(splitPoint.FromNode, splitPoint.ToNode, splitPoint.Distance, splitPoint.IsPercentage)
    End Sub

    Public Sub SetRoot(ByVal fromNode As PhyloTreeNode, ByVal toNode As PhyloTreeNode, ByVal distance As Double, ByVal isPercentage As Boolean)
        Dim fromNodeId As Integer
        Dim toNodeId As Integer

        Dim midNode As PhyloTreeNode
        Dim oldDistance As Double
        Dim fromDistance As Double
        Dim toDistance As Double
        Dim midRootId As String
        
        midRootId = fromNodeId * toNodeId
        fromNodeId = fromNode.Id
        toNodeId = toNode.Id

        'change root to from node
        oldDistance = toNode.Distance
        If fromNode.Type <> PhyloTreeNode.TREE_NODE_TYPE_ROOT Then
            SetRoot(fromNode)
        End If

        'get distance
        If toNode.IsDistanced Then
            If isPercentage Then
                fromDistance = oldDistance * distance
                toDistance = oldDistance - fromDistance
            Else
                fromDistance = distance
                toDistance = oldDistance - fromDistance
            End If
        Else
            fromDistance = 1
            toDistance = 1
        End If

        'remove toNode from the new root
        fromNode.RemoveChild(toNode)
        'add the middle point root
        midNode = New PhyloTreeNode
        midNode.Id = midRootId
        midNode.Name = "Root"
        midNode.Type = PhyloTreeNode.TREE_NODE_TYPE_INNER
        midNode.IsDistanced = toNode.IsDistanced
        midNode.Distance = fromDistance
        fromNode.AddChild(midNode)

        'add the toNode as child of midNode
        toNode.Distance = toDistance
        midNode.AddChild(toNode)

        'set new root to modNode
        SetRoot(midNode)
    End Sub


    Public Sub SetRoot(ByVal node As PhyloTreeNode)
        Dim parentNode As PhyloTreeNode = Nothing
        Dim child As PhyloTreeNode
        Dim path As ArrayList
        Dim n As PhyloTreeNode
        Dim p As PhyloTreeNode
        Dim num As Integer
        Dim olddis As Double

        num = Root.Children.Count


        If (Not node Is Nothing) Then
            'find out the new parent of all chidren of the old root
            For Each child In Root.Children
                If Not child.FindById(node.Id) Is Nothing Then
                    parentNode = child
                    Exit For
                End If
            Next

            If parentNode Is Nothing Then
                MsgBox("Failed to find the new root.")
                Exit Sub
            End If
            olddis = parentNode.Distance

            For Each child In Root.Children
                If child.Id <> node.Id Then
                    child.Distance = child.Distance + node.Distance
                End If
            Next
            'reverse distance
            path = GetPath(node, parentNode)

            Dim i As Integer
            For i = path.Count - 1 To 0 Step -1
                n = path.Item(i)
                If Not n.Parent Is Nothing And n.Id <> parentNode.Id Then
                    n.Parent.Distance = n.Distance
                    p = n.Parent
                    p.RemoveChild(n)
                    n.AddChild(p)
                End If
            Next

            node.Distance = 0

            If num < 3 Then
                For Each child In Root.Children
                    If child.Id <> parentNode.Id Then
                        parentNode.AddChild(child)
                    End If
                Next
                Root.Children.Clear()
                Root = Nothing
            Else
                Root.Type = PhyloTreeNode.TREE_NODE_TYPE_INNER
                Root.Distance = olddis
                Root.RemoveChild(parentNode)
                Root.Name = "node_" & Str(Root.Id).Trim
                Root.IsDistanced = True
                parentNode.AddChild(Root)
            End If
            Root = node
            node.Type = PhyloTreeNode.TREE_NODE_TYPE_ROOT
        End If
    End Sub
    Public Function IsDistanced() As Boolean
        If mRoot Is Nothing Then
            Return False
        End If
        If mRoot.IsDistanced Then
            Return True
        End If
        If mRoot.Children().Count > 0 Then
            Dim node As PhyloTreeNode
            node = mRoot.Children.Item(0)
            Return node.IsDistanced
        End If
        Return False
    End Function
    Public Function MidPoint() As SplitPoint
        Dim path As ArrayList
        Dim o As PhyloTreeNode
        Dim oNext As PhyloTreeNode
        Dim fromNode As PhyloTreeNode = Nothing
        Dim toNode As PhyloTreeNode = Nothing

        Dim cx As Double
        Dim i As Integer
        Dim midp As SplitPoint = New SplitPoint
        Dim totalLength As Double = 0
        Dim fromId As String = Nothing
        Dim toId As String = Nothing
        Dim distance As Double


        Dim length As Double = 0

        path = LongestPath()
        For Each o In path
            length += o.Distance
        Next
        cx = length / 2
        For i = 0 To path.Count - 1
            o = path.Item(i)
            oNext = path.Item(i + 1)
            If Not o.Parent Is Nothing Then
                If o.Parent.Id = oNext.Id Then
                    totalLength += o.Distance
                Else
                    totalLength += oNext.Distance
                End If
            Else
                totalLength += oNext.Distance
            End If
            If totalLength > cx Then
                If Not o.Parent Is Nothing Then
                    If o.Parent.Id = oNext.Id Then
                        fromNode = oNext
                        toNode = o
                        distance = totalLength - cx
                    Else
                        fromNode = o
                        toNode = oNext
                        distance = oNext.Distance - (totalLength - cx)
                    End If
                Else
                    fromNode = o
                    toNode = oNext
                    distance = oNext.Distance - (totalLength - cx)
                End If
                Exit For
            End If


        Next

        midp.FromNode = fromNode
        midp.ToNode = toNode
        midp.Distance = distance
        midp.IsPercentage = False
        Return midp
    End Function
    Public Function LongestPath() As ArrayList
        Dim lv As LongestTreePathVisitor = New LongestTreePathVisitor
        VisitMe(lv, PhyloTreeNode.VISITOR_TYPE_FORE)
        Return lv.Path
    End Function
    Public Function LongestPath(ByVal node As PhyloTreeNode) As ArrayList
        Dim n As PhyloTreeNode

        Dim firstPath As ArrayList
        Dim secondPath As ArrayList
        Dim firstNode As PhyloTreeNode = Nothing
        Dim secondNode As PhyloTreeNode = Nothing
        Dim firstDistance As Double = -999
        Dim secondDistance As Double = -999

        For Each n In node.Children
            If n.LongestDistance + n.Distance > firstDistance Then
                If Not firstNode Is Nothing Then
                    If firstNode.LongestDistance + firstNode.Distance > secondDistance Then
                        secondNode = firstNode
                        secondDistance = firstNode.LongestDistance + firstNode.Distance
                    End If
                End If
                firstNode = n
                firstDistance = n.LongestDistance + n.Distance
            Else
                If n.LongestDistance + n.Distance > secondDistance Then
                    secondNode = n
                    secondDistance = n.LongestDistance + n.Distance
                End If
            End If
        Next

        firstPath = firstNode.LongestPath
        secondPath = secondNode.LongestPath
        firstPath.Add(node)
        Dim i As Integer

        For i = secondPath.Count - 1 To 0 Step -1
            firstPath.Add(secondPath.Item(i))
        Next
        Return firstPath
    End Function
    Public Function GetPath(ByVal startNode As PhyloTreeNode, ByVal endNode As PhyloTreeNode) As ArrayList
        Dim path As ArrayList = New ArrayList

        Dim n As PhyloTreeNode
        n = startNode

        While Not n Is Nothing
            If n.Id <> endNode.Id Then
                path.Add(n)
                n = n.Parent
            Else
                path.Add(endNode)
                Exit While
            End If
        End While
        Return path

    End Function
End Class


