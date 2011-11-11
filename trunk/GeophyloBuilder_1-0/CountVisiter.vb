<ComClass(CountVisiter.ClassId, CountVisiter.InterfaceId, CountVisiter.EventsId)> _
Public Class CountVisiter
    Implements Visitor
    Private mCount As Integer



#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "50f0be9e-43ca-456b-b697-21cc2d182a16"
    Public Const InterfaceId As String = "6cb2e40d-899c-4279-9fd6-4bdc9598fad8"
    Public Const EventsId As String = "c145ff5c-bb8d-455e-8d37-5fd794fed591"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
        count = 0
    End Sub
    Public Property Count() As Integer
        Get
            Return mCount
        End Get
        Set(ByVal value As Integer)
            mCount = value
        End Set
    End Property

    Public Sub visit(ByVal node As PhyloTreeNode) Implements Visitor.visit
        mCount = mCount + 1
    End Sub
End Class


