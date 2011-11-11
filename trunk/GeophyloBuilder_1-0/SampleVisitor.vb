<ComClass(SampleVisitor.ClassId, SampleVisitor.InterfaceId, SampleVisitor.EventsId)> _
Public Class SampleVisitor
    Implements Visitor
#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "43220d2f-ee4f-40e3-b2d2-2049a4676edc"
    Public Const InterfaceId As String = "622735ee-40d9-4b4d-aeaa-b1d8a9330f87"
    Public Const EventsId As String = "c2914335-75f4-4d1c-a2f9-e92229af706c"
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

    Public Property SampleCollection() As Collection
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
        If node.Type = PhyloTreeNode.TREE_NODE_TYPE_SAMPLE Then
            mCollection.Add(node)
        End If
    End Sub


End Class


