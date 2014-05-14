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
<ComClass(NwkToken.ClassId, NwkToken.InterfaceId, NwkToken.EventsId)> _
Public Class NwkToken
    Dim mType As Integer
    Dim mValue As String
    Public Const TOKEN_TYPE_RIGHT_PARENTHESIS As Integer = 0
    Public Const TOKEN_TYPE_LEFT_PARENTHESIS As Integer = 1
    Public Const TOKEN_TYPE_COMMA As Integer = 2
    Public Const TOKEN_TYPE_COLON As Integer = 3
    Public Const TOKEN_TYPE_STRING As Integer = 4
    Public Const TOKEN_TYPE_NUMBER As Integer = 5
    Public Const TOKEN_TYPE_END As Integer = 6
    Public Const TOKEN_TYPE_NULL As Integer = 7

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "4581a651-9531-404e-9b03-d93c84e66de3"
    Public Const InterfaceId As String = "17a37408-a349-4d1e-a307-4ca1cca08885"
    Public Const EventsId As String = "26a800e8-fc94-4f30-97ea-3254e95829f6"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

    Public Property Type() As Integer
        Get
            Return mType
        End Get
        Set(ByVal value As Integer)
            mType = value
        End Set
    End Property
    Public Property Value() As String
        Get
            Return mValue
        End Get
        Set(ByVal value As String)
            mValue = value
        End Set
    End Property
End Class


