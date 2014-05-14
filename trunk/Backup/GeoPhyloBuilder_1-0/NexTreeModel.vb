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
<ComClass(NexTreeModel.ClassId, NexTreeModel.InterfaceId, NexTreeModel.EventsId)> _
Public Class NexTreeModel
    Inherits PhyloTreeModel

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Shadows Const ClassId As String = "589c5a0f-f032-4c10-878e-ae63b27d952c"
    Public Shadows Const InterfaceId As String = "d04f5d5a-a2e5-4c11-ab08-3b1284f3b1dc"
    Public Shadows Const EventsId As String = "b5fdf405-ab92-4730-b7df-2ca9e28d542c"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub
    Public Overrides Sub Parse()
        MyBase.Parse()


        Dim st As Stack
        st = New Stack

        Dim pFileSys As New Scripting.FileSystemObject
        Dim pInFile As Scripting.TextStream
        Dim strExt As String
        Dim intMyPos As Integer

        Dim strLine As String
        Dim strLine2 As String
        'Dim strMyLine As String



        intMyPos = InStr(FileName, ".")
        If intMyPos > 0 Then
            strExt = Right(FileName, 4)
        End If
        pInFile = pFileSys.OpenTextFile(FileName, Scripting.IOMode.ForReading)

        Dim p1 As Integer
        Dim p2 As Integer
        Dim ended As Boolean
        ended = False
        Dim began As Boolean
        began = False


        strLine = ""
        Do Until ended Or pInFile.AtEndOfStream()

            strLine2 = Trim(pInFile.ReadLine)

            p1 = InStr(strLine2, "TREE ")
            If (p1 > 0) Then
                If Not began Then
                    Throw New System.Exception("NEX Tree syntex: no 'begin trees;'")
                    Return
                End If
                strLine = ""
                p2 = InStr(strLine2, "=")
                If p2 = 0 Then
                    Throw New System.Exception("NEX Tree syntex: no '='")
                    Return
                End If
                Dim treeName As String
                treeName = strLine2.Substring(p1 + 4, p2 - p1 - 5)
                treeName = treeName.Trim()

                strLine = strLine2.Substring(p2).Trim()
                While Right(strLine, 1) <> ";" And Not pInFile.AtEndOfStream()
                    strLine2 = Trim(pInFile.ReadLine)
                    strLine = strLine + strLine2
                End While
                If Right(strLine, 1) <> ";" Then
                    Throw New System.Exception("No ';' found as the end of the tree string")
                End If
                strLine = strLine.Trim()

                If strLine = "" Then
                    Throw New System.Exception("Empty tree")
                End If
                Dim parser As NwkTreeParser = New NwkTreeParser
                Dim tree As PhyloTree = New PhyloTree

                Try
                    tree.TreeName = treeName
                    tree.Root = parser.parse(strLine)
                    AddTree(treeName, tree)
                    tree = Nothing
                Catch ex As Exception
                    MsgBox(ex.Message)
                    Return
                End Try
            Else
                p1 = InStrRev(strLine2, "begin trees;")
                If (p1 > 0) Then
                    began = True
                Else
                    p1 = InStrRev(strLine2, "end;")
                    If (p1 > 0) And began Then
                        ended = True
                    End If
                End If
            End If

        Loop


    End Sub
End Class


