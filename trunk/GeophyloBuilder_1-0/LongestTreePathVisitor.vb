<ComClass(LongestTreePathVisitor.ClassId, LongestTreePathVisitor.InterfaceId, LongestTreePathVisitor.EventsId)> _
Public Class LongestTreePathVisitor
    Implements Visitor
    Dim mPath As ArrayList
    Dim mLength As Double

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "78b42097-79e2-405a-8ffe-f2486ee7d3a3"
    Public Const InterfaceId As String = "a591e7a8-bd01-4e0f-9080-f71b3d55b12c"
    Public Const EventsId As String = "36f2c1ab-fc59-41ac-b378-761b849186db"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub
    Public Property Path() As ArrayList
        Get
            Return mPath
        End Get
        Set(ByVal value As ArrayList)
            mPath = value
        End Set
    End Property

    Public Property Length() As Double
        Get
            Return mLength
        End Get
        Set(ByVal value As Double)
            mLength = value
        End Set
    End Property
    Public Overridable Sub visit(ByVal node As PhyloTreeNode) Implements Visitor.visit
        If node.Type = PhyloTreeNode.TREE_NODE_TYPE_TERMINAL Or node.Type = PhyloTreeNode.TREE_NODE_TYPE_SAMPLE Then
            Return
        End If

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

        Dim d As Double = 0
        For Each n In firstPath
            d += n.Distance
        Next

        If d > mLength Then
            mLength = d
            mPath = firstPath
        End If
    End Sub

End Class


