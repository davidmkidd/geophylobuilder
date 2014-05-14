<ComClass(LongestPathVisitor.ClassId, LongestPathVisitor.InterfaceId, LongestPathVisitor.EventsId)> _
Public Class LongestPathVisitor
    Implements Visitor
    Dim path As ArrayList

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "26f82ca7-c5fa-4f51-b745-e860c3b29d32"
    Public Const InterfaceId As String = "ad5cd833-daa2-4dbc-9bff-07a59c285cff"
    Public Const EventsId As String = "5334882f-2ce9-4fbf-a64a-73994ded489a"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub
    Public Overridable Sub visit(ByVal node As PhyloTreeNode) Implements Visitor.visit
        Dim n As PhyloTreeNode
        Dim maxDis As Double
        Dim longestNode As PhyloTreeNode = New PhyloTreeNode
        Dim path As ArrayList

        longestNode = Nothing
        maxDis = -9999

        For Each n In node.Children
            If n.LongestDistance + n.Distance > maxDis Then
                longestNode = n
                maxDis = n.LongestDistance + n.Distance
            End If
        Next
        If longestNode Is Nothing Then
            node.LongestDistance = 0
            path = New ArrayList
            path.Add(node)
            node.LongestPath = path
        Else
            node.LongestDistance = maxDis
            path = New ArrayList
            For Each n In longestNode.LongestPath
                path.Add(n)
            Next
            node.LongestPath = path
            node.LongestPath.Add(node)
        End If
        


    End Sub
End Class


