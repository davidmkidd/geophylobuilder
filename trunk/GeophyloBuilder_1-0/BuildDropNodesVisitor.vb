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

<ComClass(BuildDropNodesVisitor.ClassId, BuildDropNodesVisitor.InterfaceId, BuildDropNodesVisitor.EventsId)> _
Public Class BuildDropNodesVisitor
    Inherits BuildVisitor

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "f0f365c4-7123-4a1a-a479-025bffaf3d07"
    Public Const InterfaceId As String = "3deca1cb-738f-4a6d-800b-735ce6fd0f87"
    Public Const EventsId As String = "573d5baa-95ff-4805-890f-084a8037ebfc"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub
    Public Overrides Sub visit(ByVal node As PhyloTreeNode)
        If Not FeatureClass Is Nothing Then
            Dim pNodeFeature As IFeature
            Dim pNode As IPoint

            pNodeFeature = FeatureClass.CreateFeature()
            pNode = New Point()
            Dim pZAware As IZAware
            pZAware = pNode
            If pZAware.ZAware = True Then
                pZAware.DropZs()
                pZAware.ZAware = False
            End If

            pZAware.ZAware = True
            pNode.Z = -9999.0#

            With pNode
                .X = node.X
                .Y = node.Y
                .Z = DropToZ
            End With

            pNodeFeature.Shape = pNode

            pNodeFeature.Value(2) = node.Id
            pNodeFeature.Value(3) = node.Name & ":Drop"
            pNodeFeature.Value(4) = node.Type

            pNodeFeature.Store()
            pZAware = Nothing
            pNodeFeature = Nothing
            pNode = Nothing
        ElseIf Not Writer Is Nothing Then

            If node.Type = PhyloTreeNode.TREE_NODE_TYPE_SAMPLE Then
                Return
            End If
            Writer.WriteLine("<Placemark>")
            Writer.WriteLine("<name>" & node.Name & ":Drop" & "</name>")
            Writer.WriteLine("<visibility>1</visibility>")
            Writer.WriteLine("<description>Drop node of " & node.Name & "</description>")
            Writer.WriteLine("<styleUrl>#treeNodeDrop</styleUrl>")
            Writer.WriteLine("<Point>")
            Writer.WriteLine("<altitudeMode>absolute</altitudeMode>")
            
            Writer.WriteLine("<coordinates>" & node.X & "," & node.Y & ",0</coordinates>")
            Writer.WriteLine("</Point>")
            Writer.WriteLine("</Placemark>")
        End If
    End Sub
End Class


