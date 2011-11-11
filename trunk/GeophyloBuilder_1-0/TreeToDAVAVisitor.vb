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
<ComClass(TreeToDAVAVisitor.ClassId, TreeToDAVAVisitor.InterfaceId, TreeToDAVAVisitor.EventsId)> _
Public Class TreeToDAVAVisitor
    Inherits BuildVisitor
    Private m_OutName As String
    Private m_outFWS As IFeatureWorkspace
#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "734f7859-7d0e-416c-9c93-60845dd64e08"
    Public Const InterfaceId As String = "a2c0b7dd-cb2a-46b4-bc7f-719020701184"
    Public Const EventsId As String = "e00da1fe-ea45-49d5-86a8-0f6230067c82"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub


    Public Property OutName() As String
        Get
            Return m_OutName
        End Get
        Set(ByVal value As String)
            m_OutName = value
        End Set
    End Property
    Public Property OutFWS() As IFeatureWorkspace
        Get
            Return m_outFWS
        End Get
        Set(ByVal value As IFeatureWorkspace)
            m_outFWS = value
        End Set
    End Property

    Public Overrides Sub visit(ByVal node As PhyloTreeNode)
        'Update X,Y of node, branch and droplines to centroid of DAVA model
        Dim pNodeFClass As IFeatureClass
        Dim pBrFClass As IFeatureClass
        Dim pDAVAFClass As IFeatureClass
        Dim pDlFClass As IFeatureClass
        Dim pQueryFilter As IQueryFilter
        Dim pNodeFCursor As IFeatureCursor
        Dim pBrFCursor As IFeatureCursor
        Dim pDavaFCursor As IFeatureCursor
        Dim pDlFCursor As IFeatureCursor
        Dim pPolygon As IArea
        Dim pPolyline As IPolyline
        Dim pPoint As IPoint
        Dim pNewPoint As IPoint
        Dim pNodeFeature As IFeature
        Dim pBrFeature As IFeature
        Dim pDavaFeature As IFeature
        Dim pDlFeature As IFeature

        If node.Type <> 3 Then
            'If node.type <> 3 then move node, Branch fromNode and dropline
            pNodeFClass = Me.OutFWS.OpenFeatureClass(Me.OutName + "_node")
            pDAVAFClass = Me.OutFWS.OpenFeatureClass(Me.OutName + "_dava")
            pBrFClass = Me.OutFWS.OpenFeatureClass(Me.OutName + "_branch")
            pDlFClass = Me.OutFWS.OpenFeatureClass(Me.OutName + "_dropline")

            pQueryFilter = New QueryFilter
            pQueryFilter.WhereClause = "nodeID = " + CStr(node.Id)
            pNodeFCursor = pNodeFClass.Search(pQueryFilter, Nothing)

            pQueryFilter.WhereClause = "nodeID = " + CStr(node.Id) + " AND (Type = 1 OR Type = 3)"
            pDavaFCursor = pDAVAFClass.Search(pQueryFilter, Nothing)

            pDavaFeature = pDavaFCursor.NextFeature
            If pDavaFeature Is Nothing Then MsgBox("No DAVA feature!)")
            pPolygon = pDavaFeature.Shape

            pQueryFilter.WhereClause = "FromNodeID = " + CStr(node.Id)
            pBrFCursor = pBrFClass.Search(pQueryFilter, Nothing)

            pBrFeature = pBrFCursor.NextFeature

            Do While Not pBrFeature Is Nothing
                ' Only supporting simple branches
                pPolyline = pBrFeature.Shape
                pPolyline.FromPoint.X = pPolygon.Centroid.X
                pPolyline.FromPoint.Y = pPolygon.Centroid.Y
                pBrFeature.Shape = pPolyline
                pBrFeature.Store()
                pBrFeature = pBrFCursor.NextFeature
            Loop


            pPoint = pPolygon.Centroid
            pNodeFeature = pNodeFCursor.NextFeature
            pNewPoint = pNodeFeature.Shape
            pNewPoint.X = pPoint.X
            pNewPoint.Y = pPoint.Y
            pNodeFeature.Shape = pNewPoint
            pNodeFeature.Store()

            pQueryFilter.WhereClause = "NodeID = " + CStr(node.Id)
            pDlFCursor = pDlFClass.Search(pQueryFilter, Nothing)
            pDlFeature = pDlFCursor.NextFeature
            While Not pDlFeature Is Nothing
                pPolyline = pDlFeature.Shape
                pPolyline.FromPoint.Y = pPoint.X
                pPolyline.FromPoint.Y = pPoint.Y
                pDlFeature.Shape = pPolyline
                pDlFeature.Store()
            End While

        End If




    End Sub
End Class


