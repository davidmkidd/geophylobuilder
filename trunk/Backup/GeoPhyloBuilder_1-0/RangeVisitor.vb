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

<ComClass(RangeVisitor.ClassId, RangeVisitor.InterfaceId, RangeVisitor.EventsId)> _
Public Class RangeVisitor
    Implements Visitor
    Private minx As Double
    Private miny As Double
    Private maxx As Double
    Private maxy As Double



#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "cec3f71b-e464-405c-a78e-5c580cde0d66"
    Public Const InterfaceId As String = "251318c2-506a-45ad-bc00-efbeb7970c6e"
    Public Const EventsId As String = "7eec8ea6-db10-4dd4-84c9-d04594d13f4c"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
        minx = 0
        maxx = 0
        miny = 0
        maxy = 0
    End Sub
    Public Property Min_X() As Double
        Get
            Return minx
        End Get
        Set(ByVal value As Double)
            minx = value
        End Set
    End Property
    Public Property Max_X() As Double
        Get
            Return maxx
        End Get
        Set(ByVal value As Double)
            maxx = value
        End Set
    End Property
    Public Property Min_Y() As Double
        Get
            Return miny
        End Get
        Set(ByVal value As Double)
            miny = value
        End Set
    End Property
    Public Property Max_Y() As Double
        Get
            Return maxy
        End Get
        Set(ByVal value As Double)
            maxy = value
        End Set
    End Property

    Public Overridable Sub visit(ByVal node As PhyloTreeNode) Implements Visitor.visit
        If minx = 0 Then
            minx = node.X
        Else
            If minx > node.X Then
                minx = node.X
            End If
        End If

        If miny = 0 Then
            miny = node.Y
        Else
            If miny > node.Y Then
                miny = node.Y
            End If
        End If

        If maxx = 0 Then
            maxx = node.X
        Else
            If maxx < node.X Then
                maxx = node.X
            End If
        End If

        If maxy = 0 Then
            maxy = node.Y
        Else
            If maxy < node.Y Then
                maxy = node.Y
            End If
        End If
    End Sub
End Class


