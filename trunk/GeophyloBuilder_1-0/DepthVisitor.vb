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
<ComClass(DepthVisitor.ClassId, DepthVisitor.InterfaceId, DepthVisitor.EventsId)> _
Public Class DepthVisitor
    Implements Visitor
    Private m_Depth As Double
    Private m_UseDistance As Boolean


#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "5a2bba1f-cd2c-447c-a09e-f93278dc8adf"
    Public Const InterfaceId As String = "426afda2-8edc-4889-9742-ad460e6d240e"
    Public Const EventsId As String = "e50edf34-75be-45be-ab88-ff4fd5b64049"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
        m_Depth = 0
        m_UseDistance = False
    End Sub
    Public Property UseDistance() As Boolean
        Get
            Return m_UseDistance
        End Get
        Set(ByVal value As Boolean)
            m_UseDistance = value
        End Set
    End Property
    Public Property Depth() As Double
        Get
            Return m_Depth
        End Get
        Set(ByVal value As Double)
            m_Depth = value
        End Set
    End Property
    Public Sub visit(ByVal node As PhyloTreeNode) Implements Visitor.visit
        If UseDistance Then
            If node.DepthFromRoot > m_Depth Then
                m_Depth = node.DepthFromRoot
            End If
        Else
            If node.LevelFromRoot > m_Depth Then
                m_Depth = node.LevelFromRoot
            End If
        End If

    End Sub
End Class


