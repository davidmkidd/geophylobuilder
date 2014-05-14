<ComClass(GetSubcladeVisitor.ClassId, GetSubcladeVisitor.InterfaceId, GetSubcladeVisitor.EventsId)> _
Public Class GetSubcladeVisitor
    ' Returns subtree nodes
    Implements Visitor
    Public Const NODE_TYPE_ROOT As Integer = 0
    Public Const NODE_TYPE_INNER As Integer = 1
    Public Const NODE_TYPE_TERMINAL As Integer = 2
    Public Const NODE_TYPE_SAMPLE As Integer = 3
    Public Const NODE_TYPE_TREE As Integer = 4  '4 = 0 + 1 + 2
    Public Const NODE_TYPE_INTERNAL As Integer = 5  '4 = 0 + 1
    Public Const NODE_TYPE_ALL As Integer = 6  '4 = 0 + 1 + 2 + 3

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "634a6e1d-c4bd-4069-8e1c-f3cc8efb9d22"
    Public Const InterfaceId As String = "2c42594b-5fb7-4583-a921-8586e587a612"
    Public Const EventsId As String = "fedd4895-7a42-4362-9eb8-e187269ad927"
#End Region
    Private mCollection As Collection
    Private mNodeType As Integer

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
        mCollection = New Collection
        mNodeType = 6
    End Sub
    Public Property GetCollection() As Collection
        Get
            Return mCollection
        End Get
        Set(ByVal value As Collection)
            mCollection = value
        End Set
    End Property

    Public Property NodeType() As Integer
        Get
            Return mNodeType
        End Get
        Set(ByVal value As Integer)
        End Set
    End Property

    Public Sub visit(ByVal node As PhyloTreeNode) Implements Visitor.visit

        Select Case Me.mNodeType
            Case GetSubcladeVisitor.NODE_TYPE_ROOT
                If node.Type = PhyloTreeNode.TREE_NODE_TYPE_ROOT Then mCollection.Add(node)
            Case GetSubcladeVisitor.NODE_TYPE_INNER
                If node.Type = PhyloTreeNode.TREE_NODE_TYPE_INNER Then mCollection.Add(node)
            Case GetSubcladeVisitor.NODE_TYPE_TERMINAL
                If node.Type = PhyloTreeNode.TREE_NODE_TYPE_TERMINAL Then mCollection.Add(node)
            Case GetSubcladeVisitor.NODE_TYPE_SAMPLE
                If node.Type = PhyloTreeNode.TREE_NODE_TYPE_SAMPLE Then mCollection.Add(node)
            Case GetSubcladeVisitor.NODE_TYPE_TREE
                If node.Type = PhyloTreeNode.TREE_NODE_TYPE_ROOT Or _
                    node.Type = GetSubcladeVisitor.NODE_TYPE_INNER Or _
                    node.Type = GetSubcladeVisitor.NODE_TYPE_TERMINAL _
                Then mCollection.Add(node)
            Case GetSubcladeVisitor.NODE_TYPE_INTERNAL
                If node.Type = PhyloTreeNode.TREE_NODE_TYPE_ROOT Or _
                    node.Type = GetSubcladeVisitor.NODE_TYPE_INNER _
                Then mCollection.Add(node)
            Case GetSubcladeVisitor.NODE_TYPE_ALL
                mCollection.Add(node)
            Case Else
                'Invalid ***
        End Select
        mCollection.Add(node)

    End Sub
End Class


