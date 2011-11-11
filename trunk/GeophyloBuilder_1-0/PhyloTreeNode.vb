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
<ComClass(PhyloTreeNode.ClassId, PhyloTreeNode.InterfaceId, PhyloTreeNode.EventsId)> _
Public Class PhyloTreeNode
    Public Const TREE_NODE_TYPE_ROOT As Integer = 0
    Public Const TREE_NODE_TYPE_INNER As Integer = 1
    Public Const TREE_NODE_TYPE_TERMINAL As Integer = 2
    Public Const TREE_NODE_TYPE_SAMPLE As Integer = 3
    Public Const TREE_NODE_TYPE_TREE As Integer = 4  '4 = 0 + 1 + 2
    Public Const VISITOR_TYPE_FORE As Integer = 0
    Public Const VISITOR_TYPE_BACK As Integer = 1

    Dim mId As Integer
    Dim mRankId As String
    Dim mName As String
    Dim mType As Integer
    Dim mDistance As Double
    Dim mDepthFromRoot As Double
    Dim mHeightToTip As Double
    Dim mLevelFromRoot As Integer
    Dim mLevelsToTip As Integer
    Dim mParent As PhyloTreeNode
    Dim mChildren As ArrayList
    Dim mAttributes As ArrayList
    Dim mWithDistance As Boolean
    Dim mX As Double
    Dim mY As Double
    Dim mZ As Double
    Dim mMyZ As Double
    Dim mFixedLocation As Boolean
    Dim mLongestDistance As Double
    Dim mLongestPath As ArrayList





#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "6972ca92-2f46-49a5-bec9-090fb4382ba3"
    Public Const InterfaceId As String = "113417fe-e1bb-44e2-bf2e-007d3f87833c"
    Public Const EventsId As String = "4f5e12ff-ab53-4cf4-b5a7-9573ec1aaf29"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
        mChildren = New ArrayList()
        mAttributes = New ArrayList()
        mWithDistance = False
        mFixedLocation = False
        mType = 1
    End Sub
    Public Property LongestDistance() As Double
        Get
            Return mLongestDistance
        End Get
        Set(ByVal value As Double)
            mLongestDistance = value
        End Set
    End Property
    Public Property LongestPath() As ArrayList
        Get
            Return mLongestPath
        End Get
        Set(ByVal value As ArrayList)
            mLongestPath = value
        End Set
    End Property
    Public Property FixedLocation() As Boolean
        Get
            Return mFixedLocation
        End Get
        Set(ByVal value As Boolean)
            mFixedLocation = value
        End Set
    End Property
    Public Property DepthFromRoot() As Double
        Get
            Return mDepthFromRoot
        End Get
        Set(ByVal value As Double)
            mDepthFromRoot = value
        End Set
    End Property

    Public Property HeightToTip() As Double
        Get
            Return mHeightToTip
        End Get
        Set(ByVal value As Double)
            mHeightToTip = value
        End Set
    End Property
    Public Property RankId() As String
        Get
            Return mRankId
        End Get
        Set(ByVal value As String)
            mRankId = value
        End Set
    End Property
    Public Property LevelFromRoot() As Integer
        Get
            Return mLevelFromRoot
        End Get
        Set(ByVal value As Integer)
            mLevelFromRoot = value
        End Set
    End Property


    Public Property LevelToTip() As Integer
        Get
            Return mLevelsToTip
        End Get
        Set(ByVal value As Integer)
            mLevelsToTip = value
        End Set
    End Property

    Public Property IsDistanced() As Boolean
        Get
            Return mWithDistance
        End Get
        Set(ByVal value As Boolean)
            mWithDistance = value
        End Set
    End Property
    Public Property X() As Double
        Get
            Return mX
        End Get
        Set(ByVal value As Double)
            mX = value
        End Set
    End Property
    Public Property Y() As Double
        Get
            Return mY
        End Get
        Set(ByVal value As Double)
            mY = value
        End Set
    End Property
    Public Property Z() As Double
        Get
            Return mZ
        End Get
        Set(ByVal value As Double)
            mZ = value
        End Set
    End Property

    Public Property MyZ() As Double
        Get
            Return mMyZ
        End Get
        Set(ByVal value As Double)
            mMyZ = value
        End Set
    End Property

    Public Property Id() As Integer
        Get
            Return mId
        End Get
        Set(ByVal value As Integer)
            mId = value
        End Set
    End Property
    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal value As String)
            mName = value
        End Set
    End Property
    Public Property Type() As Integer
        Get
            Return mType
        End Get
        Set(ByVal value As Integer)
            mType = value
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

    Public Property Parent() As PhyloTreeNode
        Get
            Return mParent
        End Get
        Set(ByVal value As PhyloTreeNode)
            mParent = value
        End Set
    End Property
    Public Property Children() As ArrayList
        Get
            Return mChildren
        End Get
        Set(ByVal value As ArrayList)
            mChildren = value
        End Set
    End Property
    Public Property Attributes() As ArrayList
        Get
            Return mAttributes
        End Get
        Set(ByVal value As ArrayList)
            mAttributes = value
        End Set
    End Property
    Public Sub VisitMe(ByRef visitor As Visitor, ByVal order As Integer)
        If order = PhyloTreeNode.VISITOR_TYPE_FORE Then
            visitor.visit(Me)
            Dim child As PhyloTreeNode

            For Each child In mChildren
                child.VisitMe(visitor, order)
            Next
        ElseIf order = PhyloTreeNode.VISITOR_TYPE_BACK Then
            Dim child As PhyloTreeNode
            For Each child In mChildren
                child.VisitMe(visitor, order)
            Next
            visitor.visit(Me)
        End If
        
    End Sub
    Public Sub AddChild(ByVal node As PhyloTreeNode)
        mChildren.Add(node)
        node.Parent = Me
    End Sub
    Public Sub RemoveChild(ByVal node As PhyloTreeNode)
        Dim child As PhyloTreeNode
        For Each child In mChildren
            If child.Id = node.Id Then
                mChildren.Remove(node)
                Return
            End If
        Next
    End Sub

    Public Function Find(ByVal key As String, Optional ByVal includeAttributes As Boolean = False) As PhyloTreeNode
        If key Is Nothing Then
            Return Nothing
        End If

        Dim node As PhyloTreeNode
        If mName = key Then
            Return Me
        Else
            If includeAttributes Then
                If mAttributes.Contains(key) Then
                    Return Me
                End If
            End If

            Dim child As PhyloTreeNode

            For Each child In mChildren
                node = child.Find(key, includeAttributes)
                If Not node Is Nothing Then
                    Return node
                End If
            Next
        End If
        Return Nothing
    End Function
    Public Function FindById(ByVal key As String) As PhyloTreeNode
        If key Is Nothing Then
            Return Nothing
        End If

        Dim node As PhyloTreeNode
        If mId = key Then
            Return Me
        Else

            Dim child As PhyloTreeNode

            For Each child In mChildren
                node = child.FindById(key)
                If Not node Is Nothing Then
                    Return node
                End If
            Next
        End If
        Return Nothing
    End Function

    Public Sub FindAll(ByVal key As String, ByRef list As ArrayList, Optional ByVal includeAttributes As Boolean = False)
        If key Is Nothing Then
            Return
        End If

        If mName = key Then
            list.Add(Me)
        Else
            If includeAttributes Then
                If mAttributes.Contains(key) Then
                    list.Add(Me)
                End If
            End If

            Dim child As PhyloTreeNode

            For Each child In mChildren
                child.FindAll(key, list, includeAttributes)
            Next
        End If
        Return
    End Sub

    Public Function GetSubcladeSamples() As Collection

        'Returns a collection of nodes below passed node
        Dim pColl As ICollection
        Dim sampleVisitor As SampleVisitor = New SampleVisitor
        Me.VisitMe(sampleVisitor, PhyloTreeNode.VISITOR_TYPE_FORE)
        pColl = sampleVisitor.SampleCollection

        Return pColl
    End Function

    Public Function GetSubcladeNodes(ByVal NodeType As Integer) As Collection

        'Returns a collection of nodes below passed node
        Dim pColl As ICollection

        If (NodeType > 6 Or NodeType < 0) Then
            MsgBox("Nodetype must be between 0 and 6")
            pColl = New Collection
            Return pColl
        End If


        Dim myGetSubcladeVisitor As GetSubcladeVisitor = New GetSubcladeVisitor
        myGetSubcladeVisitor.NodeType = NodeType
        Me.VisitMe(myGetSubcladeVisitor, PhyloTreeNode.VISITOR_TYPE_FORE)
        pColl = myGetSubcladeVisitor.GetCollection

        Return pColl
    End Function


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        mChildren.Clear()
        mAttributes.Clear()
        mChildren = Nothing
        mAttributes = Nothing
        mParent = Nothing
    End Sub


End Class


