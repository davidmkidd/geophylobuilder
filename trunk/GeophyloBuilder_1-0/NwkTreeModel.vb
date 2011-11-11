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
<ComClass(NwkTreeModel.ClassId, NwkTreeModel.InterfaceId, NwkTreeModel.EventsId)> _
Public Class NwkTreeModel
    Inherits PhyloTreeModel


#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Shadows Const ClassId As String = "c1d9f533-2468-4ab5-bc98-1626fd6061c4"
    Public Shadows Const InterfaceId As String = "f913174f-5611-45b0-b03a-8d5e380f29db"
    Public Shadows Const EventsId As String = "4ddaea59-fd6b-4d76-96e7-043f2f11c7ca"
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



        intMyPos = InStr(FileName, ".")
        If intMyPos > 0 Then
            strExt = Right(FileName, 4)
        End If

        Dim tree As PhyloTree
        tree = New PhyloTree()
        tree.TreeName = "tree 1"

        AddTree("tree 1", tree)
        pInFile = pFileSys.OpenTextFile(FileName, Scripting.IOMode.ForReading)
        strLine = ""
        Do Until Right(strLine, 1) = ";" Or pInFile.AtEndOfStream()
            strLine2 = Trim(pInFile.ReadLine)
            intMyPos = InStrRev(strLine2, ";")
            If intMyPos > 0 Then strLine2 = Left(strLine2, intMyPos)
            strLine = strLine + strLine2
        Loop
        If Right(strLine, 1) <> ";" Then
            Throw New System.Exception("No ';' found as the end of the tree string")
        End If
        strLine = strLine.Trim()

        If strLine = "" Then
            Throw New System.Exception("No tree found")
        End If
        Dim parser As NwkTreeParser = New NwkTreeParser

        tree.Root = parser.parse(strLine)

    End Sub
End Class


