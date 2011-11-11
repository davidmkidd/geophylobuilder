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
<ComClass(NwkTokenizer.ClassId, NwkTokenizer.InterfaceId, NwkTokenizer.EventsId)> _
Public Class NwkTokenizer
    Dim mStr As String
    Dim mCurPos As Integer
    Dim prevtk As NwkToken
#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "a5b68bf8-6f8a-4ead-bc5f-8a95805d5173"
    Public Const InterfaceId As String = "cc32924b-213a-4952-b193-52a5eeb93f2d"
    Public Const EventsId As String = "73b2ccf5-a610-4a9e-a5a1-3940a6a30522"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
        mStr = ""
        mCurPos = 1
    End Sub



    Public Property TokenString() As String
        Get
            Return mStr
        End Get
        Set(ByVal value As String)
            mStr = value
        End Set
    End Property

    Public Function NextToken() As NwkToken
        Dim MyChar As Char
        Dim tk As NwkToken


        If mCurPos > Len(mStr) Then
            tk = New NwkToken
            tk.Type = NwkToken.TOKEN_TYPE_NULL

            Return tk
        End If

        MyChar = Mid(mStr, mCurPos, 1)
        If (MyChar = " ") Then
            mCurPos = mCurPos + 1
            MyChar = Mid(mStr, mCurPos, 1)
            Do Until MyChar <> " "
                mCurPos = mCurPos + 1
                If mCurPos = Len(mStr) Then
                    tk = New NwkToken
                    tk.Type = NwkToken.TOKEN_TYPE_NULL
                    prevtk = tk
                    Return tk
                End If

                MyChar = Mid(mStr, mCurPos, 1)
            Loop
        End If

        If (MyChar = "(") Then
            tk = New NwkToken
            tk.Type = NwkToken.TOKEN_TYPE_LEFT_PARENTHESIS
            mCurPos = mCurPos + 1
            prevtk = tk
            Return tk
        ElseIf MyChar = ")" Then
            tk = New NwkToken
            tk.Type = NwkToken.TOKEN_TYPE_RIGHT_PARENTHESIS
            mCurPos = mCurPos + 1
            prevtk = tk
            Return tk
        ElseIf MyChar = ":" Then
            tk = New NwkToken
            tk.Type = NwkToken.TOKEN_TYPE_COLON
            mCurPos = mCurPos + 1
            prevtk = tk
            Return tk
        ElseIf MyChar = "," Then
            tk = New NwkToken
            tk.Type = NwkToken.TOKEN_TYPE_COMMA
            mCurPos = mCurPos + 1
            prevtk = tk
            Return tk
        ElseIf MyChar = ";" Then
            tk = New NwkToken
            tk.Type = NwkToken.TOKEN_TYPE_END
            mCurPos = mCurPos + 1
            prevtk = tk
            Return tk
        Else
            Dim s As String
            s = MyChar
            mCurPos = mCurPos + 1
            MyChar = Mid(mStr, mCurPos, 1)
            Do Until MyChar = "(" Or MyChar = ")" Or MyChar = ":" Or MyChar = "," Or mCurPos = Len(mStr)
                s = s + MyChar
                mCurPos = mCurPos + 1
                If mCurPos < Len(mStr) Then
                    MyChar = Mid(mStr, mCurPos, 1)
                End If
            Loop
            s = s.Trim()
            If s <> "" Then
                tk = New NwkToken
                If prevtk.Type = NwkToken.TOKEN_TYPE_COLON Then
                    tk.Type = NwkToken.TOKEN_TYPE_NUMBER
                Else
                    tk.Type = NwkToken.TOKEN_TYPE_STRING
                End If
                tk.Value = s
                prevtk = tk
                Return tk
            Else
                tk = New NwkToken
                tk.Type = NwkToken.TOKEN_TYPE_NULL
                prevtk = tk
                Return tk
            End If
        End If

    End Function
End Class


