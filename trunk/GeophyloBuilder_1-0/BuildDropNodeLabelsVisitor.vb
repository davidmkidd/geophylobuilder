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
<ComClass(BuildDropNodeLabelsVisitor.ClassId, BuildDropNodeLabelsVisitor.InterfaceId, BuildDropNodeLabelsVisitor.EventsId)> _
Public Class BuildDropNodeLabelsVisitor
    Inherits BuildVisitor

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "12d9b0fc-570c-4bb2-990f-d3168bde2814"
    Public Const InterfaceId As String = "074536bc-a501-4f45-994e-5276a24853c6"
    Public Const EventsId As String = "9aef29e4-80cc-4293-8353-410a685f1634"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub
    Public Overrides Sub visit(ByVal node As PhyloTreeNode)
        If node.Type = PhyloTreeNode.TREE_NODE_TYPE_SAMPLE Then
            Return
        End If
        Writer.WriteLine("<Placemark>")
        Writer.WriteLine("<name>" & node.Name & ":Drop" & "</name>")
        Writer.WriteLine("<visibility>1</visibility>")
        Writer.WriteLine("<description>Label of drop node of " & node.Name & "</description>")
        Writer.WriteLine("<styleUrl>#label</styleUrl>")
        Writer.WriteLine("<Point>")
        Writer.WriteLine("<altitudeMode>absolute</altitudeMode>")
        Writer.WriteLine("<coordinates>" & node.X & "," & node.Y & ",0</coordinates>")
        Writer.WriteLine("</Point>")
        Writer.WriteLine("</Placemark>")

    End Sub
End Class


