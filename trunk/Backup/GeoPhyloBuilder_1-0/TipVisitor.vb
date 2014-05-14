<ComClass(TipVisitor.ClassId, TipVisitor.InterfaceId, TipVisitor.EventsId)> _
Public Class TipVisitor
    Implements Visitor

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "e9bc4fe9-535d-4683-9503-7109bbc6f1d4"
    Public Const InterfaceId As String = "189fd168-f5a4-4e78-9ec8-b3bca1efde27"
    Public Const EventsId As String = "4a51c68e-e81c-4da3-9a38-8368956bd403"
#End Region
    Private mCollection As Collection

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
        mCollection = New Collection
    End Sub

    Public Property TipCollection() As Collection
        Get
            Return mCollection
        End Get
        Set(ByVal value As Collection)
            mCollection = value
        End Set
    End Property

    Public Property Count() As Integer
        Get
            Return mCollection.Count
        End Get
        Set(ByVal value As Integer)
        End Set
    End Property

    Public Sub visit(ByVal node As PhyloTreeNode) Implements Visitor.visit
        If node.Type = PhyloTreeNode.TREE_NODE_TYPE_TERMINAL Then
            mCollection.Add(node.Name)
        End If
    End Sub

End Class


