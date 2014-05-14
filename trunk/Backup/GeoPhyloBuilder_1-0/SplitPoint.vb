<ComClass(SplitPoint.ClassId, SplitPoint.InterfaceId, SplitPoint.EventsId)> _
Public Class SplitPoint
    Dim mFromNode As PhyloTreeNode
    Dim mToNode As PhyloTreeNode
    Dim mDistance As Double
    Dim mIsPercentage As Boolean

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "7838b2ce-f256-465c-a52f-ce24aa95f628"
    Public Const InterfaceId As String = "8a357fd3-a58b-4214-b842-286d250ed3ed"
    Public Const EventsId As String = "ecfdefe3-a98d-45c6-973b-027f8af3d29e"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub
    Public Property FromNode() As PhyloTreeNode
        Get
            Return mFromNode
        End Get
        Set(ByVal value As PhyloTreeNode)
            mFromNode = value
        End Set
    End Property

    Public Property ToNode() As PhyloTreeNode
        Get
            Return mToNode
        End Get
        Set(ByVal value As PhyloTreeNode)
            mToNode = value
        End Set
    End Property
    Public Property Distance() As Double
        Get
            Return mDistance
        End Get
        Set(ByVal value As Double)
            mDistance = value
        End Set
    End Property
    Public Property IsPercentage() As Boolean
        Get
            Return mIsPercentage
        End Get
        Set(ByVal value As Boolean)
            mIsPercentage = value
        End Set
    End Property
End Class


