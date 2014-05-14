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
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry


Imports System.Runtime.InteropServices
<ComClass(BuildSampleLinksVisitor.ClassId, BuildSampleLinksVisitor.InterfaceId, BuildSampleLinksVisitor.EventsId)> _
Public Class BuildSampleLinksVisitor
    Inherits BuildVisitor
    Private count As Integer

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "f01deb17-0977-4723-b125-42141f7d1a0e"
    Public Const InterfaceId As String = "08e5d828-a727-4176-aa53-3c07596d4e3e"
    Public Const EventsId As String = "54499dd9-eb38-40db-84c1-06587a56efc2"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub
    Public Overrides Sub visit(ByVal node As PhyloTreeNode)
        Dim chd As PhyloTreeNode

        Dim i As Integer
        For i = 0 To node.Children.Count - 1
            chd = node.Children.Item(i)
            If chd.Type <> PhyloTreeNode.TREE_NODE_TYPE_SAMPLE Then
                Continue For
            End If
            Dim toz As Double
            Dim frz As Double

            If chd.Type = PhyloTreeNode.TREE_NODE_TYPE_SAMPLE Then
                toz = 0
            Else
                If UseDistance Then
                    toz = (MaxDepth - chd.DepthFromRoot) * MultipleZ
                Else
                    toz = (MaxDepth - chd.LevelFromRoot) * MultipleZ
                End If
            End If
            If node.Type = PhyloTreeNode.TREE_NODE_TYPE_SAMPLE Then
                frz = 0
            Else
                If UseDistance Then
                    frz = (MaxDepth - node.DepthFromRoot) * MultipleZ
                Else
                    frz = (MaxDepth - node.LevelFromRoot) * MultipleZ
                End If
            End If
            Writer.WriteLine("<Placemark>")
            Writer.WriteLine("<name>" & (node.Name).Trim() & "-" & (chd.Name).Trim() & "</name>")
            Writer.WriteLine("<description>Link from drop nodes to sample nodes</description>")
            Writer.WriteLine("<styleUrl>#sampleLink</styleUrl>")
            Writer.WriteLine("<LineString>")
            Writer.WriteLine("<tessellate>1</tessellate>")
            Writer.WriteLine("<coordinates>")
            Writer.WriteLine(node.X & "," & node.Y & "," & frz & " " & chd.X & "," & chd.Y & "," & toz & "</coordinates>")
            Writer.WriteLine("</LineString>")
            Writer.WriteLine("</Placemark>")
        Next

    End Sub
End Class


