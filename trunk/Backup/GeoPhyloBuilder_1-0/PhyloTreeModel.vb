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
<ComClass(PhyloTreeModel.ClassId, PhyloTreeModel.InterfaceId, PhyloTreeModel.EventsId)> _
Public Class PhyloTreeModel
    Dim mTrees As Hashtable
    Dim mFile As String
    Dim mModelName As String
#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "f5340d4f-e65b-4f57-a2dd-141580502803"
    Public Const InterfaceId As String = "0bb31620-2a68-4be8-9d2e-1ccc39a8662c"
    Public Const EventsId As String = "c8b228bd-c035-4cbb-98d7-a88f301f7aa1"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
        mTrees = New Hashtable()
    End Sub

    Public Property FileName() As String
        Get
            Return mFile
        End Get
        Set(ByVal value As String)
            mFile = value
        End Set
    End Property
    Public Property Trees() As Hashtable
        Get
            Return mTrees
        End Get
        Set(ByVal value As Hashtable)
            mTrees = value
        End Set
    End Property
    Public Property ModelName() As String
        Get
            Return mModelName
        End Get
        Set(ByVal value As String)
            mModelName = value
        End Set
    End Property
    Public Function GetTreeCount() As Integer
        If mTrees Is Nothing Then
            Return 0
        Else
            Return mTrees.Count()
        End If
    End Function
    Public Function GetTreeNames() As ArrayList
        Dim names As ArrayList
        names = New ArrayList()
        Dim s As PhyloTree
        For Each s In mTrees.Values()
            names.Add(s.TreeName)
        Next

        Return names
    End Function

    Public Function GetTree(ByVal name As String) As PhyloTree
        Return mTrees.Item(name)
    End Function
    Public Sub AddTree(ByVal name As String, ByVal tree As PhyloTree)
        mTrees.Add(name, tree)
    End Sub

    Public Overridable Sub Parse()

    End Sub
End Class


