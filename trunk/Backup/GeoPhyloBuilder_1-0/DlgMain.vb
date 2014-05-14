
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
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.CatalogUI
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.DataSourcesGDB
Imports NexusNet.Nexus

Imports system.io

Imports System.Runtime.InteropServices

Public Class DlgMain
    Implements Observer
    Private m_app As IApplication
    Dim model As PhyloTreeModel
    Dim tree As PhyloTree
    Dim m_ImportType As String
    Dim m_pOutFWS As IFeatureWorkspace
    Dim m_pWSNameShape As String
    Dim m_pWSName As IWorkspaceName
    Dim m_pOutFDataSet As IFeatureDataset
    Dim m_pSpat As ISpatialReference
    ' Import Activate Variables
    Dim bolModelSet As Boolean
    Dim bolLinkset As Boolean
    Dim bolSpatialSet As Boolean

    'Tree Variables
    Dim m_strTreeFile As String
    Dim m_strDataSet As String

    'Spatial Source Variables
    Dim m_pInFClass As IFeatureClass

    'Nodel Link Variables
    Dim m_pNodeLinkTable As ITable

    ' Node Depth variable to set Z domain
    Dim m_dblAllZ As Double
    Dim m_MaxDepth As Double

    Dim intLinkTreeFld As Long
    Dim intInJoinFld As Long
    Dim intLinkSpatialFld As Long
    Dim m_strOutDSName As String
    Dim m_Message As String
    Dim M_progress As Integer

    Dim m_PandLBuffer As Double
    Dim m_PolyBuffer As Double

    Dim m_Writer As StreamWriter

    Public Property ProgressMessage() As String
        Get
            Return m_Message
        End Get
        Set(ByVal value As String)
            m_Message = value
        End Set
    End Property
    Public Property ProgressValue() As Integer
        Get
            Return M_progress
        End Get
        Set(ByVal value As Integer)
            M_progress = value
        End Set
    End Property

    Public Property Application() As IApplication
        Get
            Return m_app
        End Get
        Set(ByVal value As IApplication)
            m_app = value
        End Set
    End Property

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            Me.Enabled = False

            BuildGeoPhyloTree()

            MsgBox("Geophylogeny successfully built.")

            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Enabled = True
        Catch ex As Exception
            'ProgressMessage = ex.Source & ":" & ex.Message
            'Me.DialogResult = System.Windows.Forms.DialogResult.OK
            MsgBox(ex.Message)
            prgBar.Visible = False
            lstReport.Items.Add(ex.Message)
            Me.Enabled = True


            Return

        End Try
    End Sub
    
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub


    Private Sub butOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butOpen.Click
        Dim intPos As Long

        Try

            'Clear objects
            cmbTreeName.Items.Clear()

            txtModelFile.Text = ""
            tvwTree.Nodes.Clear()

            Dim ext As String

            dlgOpenFile.DefaultExt = "*.nex"
            dlgOpenFile.Title = "Select Tree"
            dlgOpenFile.Filter = "All supported files |*.nwk;*.tre|" & _
                "Newick files (*.nwk)|*.nwk|" & _
                "Tree files (*.tre)|*.tre"

            'dlgOpenFile.Filter = "All supported files |*.nex;*.nwk;*.tre|" & _
            '"Nexus files (*.nex)|*.nex|" & _
            '"Newick files (*.nwk)|*.nwk|" & _
            '"Tree files (*.tre)|*.tre"

            dlgOpenFile.FileName = ""
            dlgOpenFile.ShowDialog()
            If dlgOpenFile.FileName = "" Then Exit Sub
            Dim m_strTreeFile As String

            m_strTreeFile = dlgOpenFile.FileName
            intPos = InStr(m_strTreeFile, ".")

            txtModelFile.Text = m_strTreeFile

            ext = Microsoft.VisualBasic.Right(m_strTreeFile, 3)
            If ext = "nwk" Or ext = "tre" Then
                model = New NwkTreeModel()
                model.FileName = m_strTreeFile
            ElseIf ext = "nex" Then
                model = New NexTreeModel()
                model.FileName = m_strTreeFile
            End If

            model.Parse()
            Dim names As ArrayList
            names = model.GetTreeNames()

            cmbTreeName.Items.Clear()
            Dim nm As String
            For Each nm In names
                cmbTreeName.Items.Add(nm)
            Next
            If cmbTreeName.Items.Count > 0 Then
                cmbTreeName.SelectedIndex = 0

            End If



        Catch ex As Exception
            'MsgBox(ex.Message)
            MsgBox("Not a valid tree file. Please check file formating")
            txtModelFile.Clear()
            tree = Nothing
        End Try
    End Sub

    Private Sub cmbTreeName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTreeName.SelectedIndexChanged
        Try
            Dim nm As String
            nm = cmbTreeName.SelectedItem.ToString
            tree = model.GetTree(nm)
            If tree Is Nothing Then
                Return
            End If
            tvwTree.Nodes.Clear()

            Dim visitor As TreeViewVisitor
            visitor = New TreeViewVisitor
            visitor.TreeView = tvwTree
            tree.VisitMe(visitor, PhyloTreeNode.VISITOR_TYPE_FORE)
            tvwTree.ExpandAll()

            lblZ.Visible = True
            cmbZ.Visible = True
            cmbZ.Items.Clear()

            If tree.IsDistanced = True Then
                cmbZ.Items.Add("Distance")
            End If
            cmbZ.Items.Add("Level")
            cmbZ.SelectedIndex = 0
            butSetRoot.Enabled = False

        Catch ex As Exception
            MsgBox(ex.Message)
            tree = Nothing
        End Try
    End Sub

    Private Sub tvwTree_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwTree.AfterSelect
        Dim id As String
        id = e.Node.Name
        Dim nd As PhyloTreeNode
        nd = tree.Find(id)

        If nd Is Nothing Then
            Return
        End If


        If nd.IsDistanced() Then
            lblDistance.Text = nd.Distance
        Else
            lblDistance.Text = "No distance"
        End If

        chkFixLocation.Checked = nd.FixedLocation
        txtFixedX.Text = nd.X
        txtFixedY.Text = nd.Y
        txtFixedX.Enabled = nd.FixedLocation
        txtFixedY.Enabled = nd.FixedLocation

        butSetRoot.Enabled = nd.Type <> PhyloTreeNode.TREE_NODE_TYPE_ROOT


    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetSpatial.Click
        On Error GoTo Error_Handler

        Dim pGxDialog As IGxDialog
        Dim pGDBFilter As IGxObjectFilter
        Dim pFilterCol As IGxObjectFilterCollection
        Dim pEnumGxObj As IEnumGxObject
        Dim pGXObject As IGxObject

        pGDBFilter = New GxFilterFeatureClasses
        pGxDialog = New GxDialog
        pFilterCol = pGxDialog

        pFilterCol.AddFilter(pGDBFilter, True)

        pGxDialog.Title = "Select Location Data"
        pGxDialog.AllowMultiSelect = False

        pEnumGxObj = New GxObjectArray()

        Dim aoInitialize As IAoInitialize
        aoInitialize = New AoInitializeClass()
        If (aoInitialize.IsProductCodeAvailable(esriLicenseProductCode.esriLicenseProductCodeArcInfo) = esriLicenseStatus.esriLicenseAvailable) Then
            aoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeArcInfo)
        End If


        pGxDialog.DoModalOpen(0, pEnumGxObj)
        If pEnumGxObj Is Nothing Then
            MsgBox("Failed to open feature class")
        End If

        pGXObject = pEnumGxObj.Next
        If pGXObject Is Nothing Then
            MsgBox("Failed to get the feature class")
            Exit Sub
        End If

        Dim m_pInFClassName As IFeatureClassName
        Dim m_pInDataSetName As IDatasetName
        Dim m_pInWSName As IWorkspaceName
        Dim m_pInWSFactory As IWorkspaceFactory
        Dim m_pInWS As IWorkspace
        Dim m_pInFWS As IFeatureWorkspace
        'MsgBox("Before geting feature class name")
        m_pInFClassName = pGXObject.InternalObjectName
        'MsgBox("After geting feature class name")
        'MsgBox("Before geting datasetname")
        m_pInDataSetName = m_pInFClassName
        'MsgBox("after geting datasetname")
        'MsgBox("Before geting workspace name")
        m_pInWSName = m_pInDataSetName.WorkspaceName
        'MsgBox("after geting workspace name")
        'MsgBox("Before geting workspace factory")
        m_pInWSFactory = m_pInWSName.WorkspaceFactory
        'MsgBox("after geting workspace factory")
        If m_pInWSFactory Is Nothing Then
            MsgBox("Failed to create Factory")
        End If

        m_pInWS = m_pInWSFactory.OpenFromFile(m_pInWSName.PathName, 0)
        If m_pInWS Is Nothing Then
            MsgBox("Failed to create workspace")
        End If
        m_pInFWS = m_pInWS
        m_pInFClass = m_pInFWS.OpenFeatureClass(m_pInDataSetName.Name)
        If m_pInFClass Is Nothing Then
            MsgBox("Failed to create Feature class")
        End If
        txtInWS.Text = m_pInWSName.PathName
        txtInFclass.Text = m_pInFClass.AliasName
        'txtGDBOut.Text = m_pInWSName.PathName
        m_pWSName = m_pInWSName
        cboInJoinField.Items.Clear()
        AddFieldstoCboBox(m_pInFClass, cboInJoinField, "All")

        Dim pInGeoDataSet As IGeoDataset
        pInGeoDataSet = m_pInFClass
        m_pSpat = pInGeoDataSet.SpatialReference
        If (TypeOf m_pSpat Is IProjectedCoordinateSystem) Then
            If cboObsTipPos.FindStringExact("MCP Centroid") = -1 Then cboObsTipPos.Items.Add("MCP Centroid")
            If cboIntPos.FindStringExact("MCP Centroid") = -1 Then cboIntPos.Items.Add("MCP Centroid")
            If cboIntPos.FindStringExact("DAVA Centroid") = -1 Then cboIntPos.Items.Add("DAVA Centroid")
            ' Default Envelope / 10000
            m_PandLBuffer = Math.Max(pInGeoDataSet.Extent.Envelope.XMax - pInGeoDataSet.Extent.Envelope.XMin, _
                pInGeoDataSet.Extent.Envelope.YMax - pInGeoDataSet.Extent.Envelope.YMin) / 10000
            txtPandLBuffer.Text = m_PandLBuffer

        Else
            cboObsTipPos.Items.Remove("MCP Centroid")
            cboIntPos.Items.Remove("MCP Centroid")
            cboIntPos.Items.Remove("DAVA Centroid")
            'txtCladeBuffDist.Text = 0
            'txtDAVAThreshold.Text = 50
        End If


Exit_Here:
        Exit Sub
Error_Handler:
        MsgBox("Error: " & Err.Number & Err.Description)
        Resume Exit_Here
    End Sub

    Private Sub cmdGetNodeTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetNodeTable.Click
        On Error GoTo Error_Handler

        ' Select the table that will join the phylogenetic model file to the spatial location file
        Dim pGxDialog As IGxDialog
        Dim pGDBFilter As IGxObjectFilter
        Dim pFilterCol As IGxObjectFilterCollection
        Dim pEnumGxObj As IEnumGxObject
        Dim pGXObject As IGxObject
        Dim pName As IName
        Dim pField As IField
        Dim intFields As Long
        Dim i As Long


        pGDBFilter = New GxFilterTables 'GxFilterWorkspaces
        pGxDialog = New GxDialog
        pFilterCol = pGxDialog

        pFilterCol.AddFilter(pGDBFilter, True)

        pGxDialog.Title = "Select Link File..."
        pGxDialog.AllowMultiSelect = False
        Dim aoInitialize As IAoInitialize
        aoInitialize = New AoInitializeClass()
        If (aoInitialize.IsProductCodeAvailable(esriLicenseProductCode.esriLicenseProductCodeArcInfo) = esriLicenseStatus.esriLicenseAvailable) Then
            aoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeArcInfo)
        End If
        pGxDialog.DoModalOpen(0, pEnumGxObj)

        pGXObject = pEnumGxObj.Next
        If pGXObject Is Nothing Then Exit Sub



        Dim m_pNodeLinkClass As IClass
        Dim m_pNodeLinkTableName As ITableName

        m_pNodeLinkTableName = pGXObject.InternalObjectName
        pName = m_pNodeLinkTableName
        m_pNodeLinkTable = pName.Open
        intFields = m_pNodeLinkTable.Fields.FieldCount
        cboNodeTreeJoinFld.Items.Clear()
        cboNodeSpatialJoinFld.Items.Clear()
        For i = 0 To intFields - 1
            If m_pNodeLinkTable.Fields.Field(i).Type = esriFieldType.esriFieldTypeString Then cboNodeTreeJoinFld.Items.Add(m_pNodeLinkTable.Fields.Field(i).Name)
            cboNodeSpatialJoinFld.Items.Add(m_pNodeLinkTable.Fields.Field(i).Name)
        Next
        Dim m_pNodeLinkDataset As IDataset

        m_pNodeLinkDataset = m_pNodeLinkTable
        txtNodeTableName.Text = m_pNodeLinkDataset.Name
        cboNodeTreeJoinFld.SelectedIndex = 0
        cboNodeSpatialJoinFld.SelectedIndex = 0

        pGXObject = Nothing
        pEnumGxObj = Nothing


Exit_Here:
        Exit Sub
Error_Handler:
        MsgBox("Error: " & Err.Number & Err.Description)
        Resume Exit_Here
    End Sub
    Public Sub AddFieldstoCboBox(ByVal pDataSet As IDataset, ByVal myCombo As ComboBox, Optional ByVal myType As String = "All")

        ' Procedure adds fields from given data source to a given combo box

        Dim pTable As ITable
        Dim pFields As IFields
        Dim i As Long

        pTable = pDataSet
        pFields = pTable.Fields

        For i = 1 To pFields.FieldCount

            Select Case myType

                Case "All"
                    myCombo.Items.Add(pFields.Field(i - 1).Name)

                Case "String"
                    If pFields.Field(i - 1).Type = esriFieldType.esriFieldTypeString Then _
                        myCombo.Items.Add(pFields.Field(i - 1).Name)


            End Select

        Next i

        myCombo.SelectedIndex = 0
    End Sub


    Private Sub chkNodeLink_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNodeLink.CheckedChanged

        If chkNodeLink.Checked = False Then
            cmdGetNodeTable.Enabled = False
            cboNodeTreeJoinFld.Enabled = False
            cboNodeSpatialJoinFld.Enabled = False
        Else
            cmdGetNodeTable.Enabled = True
            cboNodeTreeJoinFld.Enabled = True
            cboNodeSpatialJoinFld.Enabled = True
        End If

    End Sub

    Private Sub chkDropLine_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDropLine.CheckedChanged
        If chkDropLine.Checked Then
            txtDropZ.Enabled = True
        Else
            txtDropZ.Enabled = False
        End If
    End Sub

    Private Sub optDropZ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtDropZ.Enabled = True
    End Sub

    Private Sub optDropSurf_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtDropZ.Enabled = False
    End Sub

    Private Sub cmdDropSurf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MsgBox("DropZ Surface is not yet available")
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        'cboBrVert.Items.Add("Distance")
        cboBrVert.Items.Add("Number")
        cboBrVert.SelectedIndex = 0

        'cboBrShape Items
        cboBrShape.Items.Add("Triangular")
        cboBrShape.Items.Add("Rectangular")
        cboBrShape.SelectedIndex = 0

        'cboObsTipPos Items
        cboObsTipPos.Items.Add("Envelope Centroid")
        cboObsTipPos.Items.Add("Mean Position")
        cboObsTipPos.SelectedIndex = 0

        cboIntPos.Items.Add("Envelope Centroid")
        cboIntPos.Items.Add("Mean Position")
        cboIntPos.SelectedIndex = 0

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmdGDBOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGDBOut.Click
        Dim pGxDialog As IGxDialog
        Dim pGDBFilter As IGxObjectFilter
        Dim pFilterCol As IGxObjectFilterCollection
        Dim pEnumGxObj As IEnumGxObject
        Dim pGXObject As IGxObject

        pGxDialog = New GxDialog
        pFilterCol = pGxDialog
        If cmbImportType.SelectedIndex = 0 Then
            pGDBFilter = New GxFilterPersonalGeodatabases
            pGxDialog.Title = "Select Geodatabase"
        ElseIf cmbImportType.SelectedIndex = 1 Then
            pGDBFilter = New GxFilterBasicTypes
            pGxDialog.Title = "Select Folder"
        End If

        If cmbImportType.SelectedIndex <> 2 Then
            pFilterCol.AddFilter(pGDBFilter, True)

            pGxDialog.AllowMultiSelect = False

            Dim aoInitialize As IAoInitialize
            aoInitialize = New AoInitializeClass()
            If (aoInitialize.IsProductCodeAvailable(esriLicenseProductCode.esriLicenseProductCodeArcInfo) = esriLicenseStatus.esriLicenseAvailable) Then
                aoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeArcInfo)
            End If

            pGxDialog.DoModalOpen(0, pEnumGxObj)

            pGXObject = pEnumGxObj.Next
            If pGXObject Is Nothing Then Exit Sub
            If m_ImportType = "Personal Geodatabase" Then
                m_pWSName = pGXObject.InternalObjectName
                txtGDBOut.Text = m_pWSName.PathName
            Else
                m_pWSNameShape = pGXObject.FullName
                txtGDBOut.Text = m_pWSNameShape
            End If
        Else
            SaveFileDialog.ShowDialog()
            txtGDBOut.Text = SaveFileDialog.FileName
            If Not m_Writer Is Nothing Then
                m_Writer = Nothing
            End If


        End If

    End Sub
    Public Sub CheckData()

        If tree Is Nothing Then
            Throw New System.Exception("No phylogenetic tree selected.")
        End If

        If m_pInFClass Is Nothing Then
            Throw New System.Exception("No feature class specified for sample locations.")
        End If

        If m_pWSName Is Nothing And m_pWSNameShape Is Nothing Then
            Throw New System.Exception("No workspace specified for output.")
        End If

        If txtDataName.Text.Length = 0 Then
            Throw New System.Exception("No output dataset name set.")
        End If

        'MsgBox(cboInJoinField.SelectedItem.ToString())
        intInJoinFld = m_pInFClass.Fields.FindField(cboInJoinField.SelectedItem.ToString())

        If chkNodeLink.Checked Then
            If m_pNodeLinkTable Is Nothing Then
                Throw New System.Exception("No link table specified for sample locations.")
            End If
            intLinkTreeFld = m_pNodeLinkTable.Fields.FindField(cboNodeTreeJoinFld.SelectedItem.ToString())
            intLinkSpatialFld = m_pNodeLinkTable.Fields.FindField(cboNodeSpatialJoinFld.SelectedItem.ToString())
            If Not m_pNodeLinkTable.Fields.Field(intLinkTreeFld).Type = esriFieldType.esriFieldTypeString Then
                Throw New System.Exception("Link Table join field must be of type 'string'")
            End If

            If Not m_pInFClass.Fields.Field(intInJoinFld).Type = _
                m_pNodeLinkTable.Fields.Field(intLinkSpatialFld).Type Then
                Throw New System.Exception("Link Table and Spatial Class join fields must of the same type")
            End If
        Else
            If Not m_pInFClass.Fields.Field(intInJoinFld).Type = esriFieldType.esriFieldTypeString Then
                Throw New System.Exception("Feature class join field must be of type 'string'.")
            End If
        End If

    End Sub
  

    Public Sub GetLocations()

        ' Calculates location of tips

        Dim start_id As Integer
        Dim countVisitor As CountVisiter = New CountVisiter

        tree.VisitMe(countVisitor, PhyloTreeNode.VISITOR_TYPE_FORE)


        Dim tipVisitor As TipVisitor = New TipVisitor
        tree.VisitMe(tipVisitor, PhyloTreeNode.VISITOR_TYPE_FORE)
        'Debug.Print(tipVisitor.Count)

        Dim tipColl As Collection
        tipColl = tipVisitor.TipCollection

        start_id = countVisitor.Count + 1

        Dim TipCount As Integer
        Dim i As Integer
        Dim pPoint As IPoint
        Dim pPoly As IPolygon
        Dim pArea As IArea
        Dim pEnv As IEnvelope
        Dim node As PhyloTreeNode
        Dim newNode As PhyloTreeNode
        Dim maxDepth = m_MaxDepth
        Dim pTCursor As ICursor
        Dim pTQueryFilter As IQueryFilter

        Dim pRow As IRow
        Dim treeFldId As Integer
        Dim spatialFldId As Integer

        Dim pFCursor As IFeatureCursor
        Dim pFQueryFilter As IQueryFilter
        Dim pFeature As IFeature
        Dim fType As Integer
        Dim fldIndex As Integer
        Dim shpFldIndex As Integer
        Dim value As String
        'Dim pDataStats As IDataStatistics = New DataStatistics
        'Dim PUniqCursor As ICursor
        'Dim pEnumVar As System.Collections.IEnumerator, value As Object


        ' Get join field of feature class
        fldIndex = m_pInFClass.Fields.FindField(cboInJoinField.SelectedItem.ToString())
        shpFldIndex = m_pInFClass.Fields.FindField("Shape")

        ' Get shape type
        fType = m_pInFClass.ShapeType


        If Not chkNodeLink.Checked Then

            For i = 1 To tipColl.Count

                TipCount = 0

                value = tipColl.Item(i)
                node = tree.Find(value, True)
                'Debug.Print("value - " & value)
                'Debug.Print("n Child after get - " & node.Children.Count)

                ' This is a temporary fix for the node dupliction problem
                If node.Children.Count > 0 Then node.Children.Clear()

                'Get features, calculate point locations and add OBSERVATION NODES to phylotree

                pFQueryFilter = New QueryFilter
                pFQueryFilter.WhereClause = m_pInFClass.Fields.Field(fldIndex).Name & " = '" & value & "'"
                'pFQueryFilter.WhereClause = m_pInFClass.Fields.Field(fldIndex).Name & " = " & value
                pFCursor = m_pInFClass.Search(pFQueryFilter, True)
                pFeature = pFCursor.NextFeature

                Do Until pFeature Is Nothing

                    TipCount = TipCount + 1

                    Select Case fType

                        Case esriGeometryType.esriGeometryPoint
                            pPoint = pFeature.Shape
                            'Debug.Print(pFQueryFilter.WhereClause)
                            'Debug.Print(pFeature.Value(3))
                            'Debug.Print(pPoint.X & ", " & pPoint.Y & ", " & pPoint.Z)

                        Case esriGeometryType.esriGeometryPolyline
                            pEnv = pFeature.Shape.Envelope
                            pArea = pEnv
                            pPoint = pArea.Centroid

                        Case esriGeometryType.esriGeometryPolygon
                            pEnv = pFeature.Shape.Envelope
                            pArea = pEnv
                            pPoint = pArea.Centroid

                        Case Else

                    End Select

                    newNode = New PhyloTreeNode
                    newNode.Type = PhyloTreeNode.TREE_NODE_TYPE_SAMPLE
                    'lstReport.Items.Add("before setting disdistanced: " & organism)
                    newNode.IsDistanced = node.IsDistanced
                    'lstReport.Items.Add("after setting disdistanced: " & organism)
                    newNode.Id = start_id

                    start_id = start_id + 1

                    newNode.Name = value & ":" & pFeature.Value(fldIndex) & ":" & CStr(TipCount)

                    newNode.X = pPoint.X
                    newNode.Y = pPoint.Y

                    newNode.IsDistanced = node.IsDistanced
                    newNode.Distance = maxDepth - node.DepthFromRoot
                    newNode.DepthFromRoot = node.DepthFromRoot
                    newNode.LevelFromRoot = node.LevelFromRoot
                    If cmbZ.SelectedItem.ToString() = "Distance" Then
                        newNode.Z = newNode.DepthFromRoot
                    Else
                        newNode.Z = newNode.LevelFromRoot
                    End If

                    node.AddChild(newNode)
                    'lstReport.Items.Add("after adding new node: " & organism)
                    newNode = Nothing
                    pPoint = Nothing
                    pPoly = Nothing
                    pArea = Nothing
                    pEnv = Nothing
                    pFeature = Nothing
                    pFeature = pFCursor.NextFeature
                Loop


                'Debug.Print("n Child before get - " & node.Children.Count)
                pFCursor = Nothing
                pFQueryFilter = Nothing

            Next

        Else

            'Get the id's of the Tree and Spatial join fields in the Link table
            treeFldId = m_pNodeLinkTable.FindField(cboNodeTreeJoinFld.SelectedItem.ToString())
            spatialFldId = m_pNodeLinkTable.FindField(cboNodeSpatialJoinFld.SelectedItem.ToString())

            For i = 1 To tipColl.Count

                value = tipColl.Item(i)
                node = tree.Find(value, True)

                If node.Children.Count > 0 Then node.Children.Clear() 'temporary fix for node duplication

                'Debug.Print("value - " & value)
                ' Get link table rows where value is found
                pTQueryFilter = New QueryFilter
                pTQueryFilter.WhereClause = m_pNodeLinkTable.Fields.Field(treeFldId).Name & " = '" & value & "'"
                pTCursor = m_pNodeLinkTable.Search(pTQueryFilter, True)

                pRow = pTCursor.NextRow()

                TipCount = 0

                Do Until pRow Is Nothing

                    'Set up feature filter for link table rows
                    'Get features, calculate point locations and add OBSERVATION NODES to phylotree
                    TipCount = TipCount + 1

                    pFQueryFilter = New QueryFilter
                    If pRow.Fields.Field(spatialFldId).Type = esriFieldType.esriFieldTypeString Then
                        pFQueryFilter.WhereClause = m_pInFClass.Fields.Field(fldIndex).Name & " = '" & pRow.Value(spatialFldId) & "'"
                    Else
                        pFQueryFilter.WhereClause = m_pInFClass.Fields.Field(fldIndex).Name & " = " & pRow.Value(spatialFldId)
                    End If


                    pFCursor = m_pInFClass.Search(pFQueryFilter, True)
                    pFeature = pFCursor.NextFeature


                    Select Case fType

                        Case esriGeometryType.esriGeometryPoint
                            pPoint = pFeature.Shape
                            'Debug.Print(pFQueryFilter.WhereClause)
                            'Debug.Print(pFeature.Value(3))
                            'Debug.Print(pPoint.X & ", " & pPoint.Y & ", " & pPoint.Z)

                        Case esriGeometryType.esriGeometryPolyline
                            pEnv = pFeature.Shape.Envelope
                            pArea = pEnv
                            pPoint = pArea.Centroid

                        Case esriGeometryType.esriGeometryPolygon
                            pEnv = pFeature.Shape.Envelope
                            pArea = pEnv
                            pPoint = pArea.Centroid

                        Case Else

                    End Select

                    newNode = New PhyloTreeNode
                    newNode.Type = PhyloTreeNode.TREE_NODE_TYPE_SAMPLE
                    'lstReport.Items.Add("before setting disdistanced: " & organism)
                    newNode.IsDistanced = node.IsDistanced
                    'lstReport.Items.Add("after setting disdistanced: " & organism)
                    newNode.Id = start_id

                    start_id = start_id + 1
                    newNode.Name = node.Name & ":" & pRow.Value(spatialFldId) & ":" & CStr(TipCount)
                    newNode.X = pPoint.X
                    newNode.Y = pPoint.Y

                    newNode.IsDistanced = node.IsDistanced
                    newNode.Distance = maxDepth - node.DepthFromRoot
                    newNode.DepthFromRoot = node.DepthFromRoot
                    newNode.LevelFromRoot = node.LevelFromRoot
                    If cmbZ.SelectedItem.ToString() = "Distance" Then
                        newNode.Z = newNode.DepthFromRoot
                    Else
                        newNode.Z = newNode.LevelFromRoot
                    End If

                    node.AddChild(newNode)
                    'lstReport.Items.Add("after adding new node: " & organism)
                    newNode = Nothing
                    pPoint = Nothing
                    pPoly = Nothing
                    pArea = Nothing
                    pEnv = Nothing
                    pFeature = Nothing
                    pFCursor = Nothing
                    pFQueryFilter = Nothing

                    pRow = pTCursor.NextRow()

                Loop  'Do Until pRow Is Nothing

                pTCursor = Nothing
                node = Nothing

            Next  'Do Until value Is Nothing

            'pEnumVar = Nothing
            'PUniqCursor = Nothing

        End If

        tipColl = Nothing
        countVisitor = Nothing

    End Sub
    Public Sub GetZ()
        Dim visitor As DepthCalculator = New DepthCalculator
        tree.VisitMe(visitor, PhyloTreeNode.VISITOR_TYPE_FORE)

        Dim visitor2 As HeightCalculator = New HeightCalculator
        tree.VisitMe(visitor2, PhyloTreeNode.VISITOR_TYPE_BACK)
    End Sub
    Public Sub InitWorkspace()
        'If cmbImportType.SelectedItem = "KML(Google Earth)" Then
        'Return
        'End If
        Dim pInGeoDataSet As IGeoDataset

        Dim dblZMulti As Double

        m_dblAllZ = m_MaxDepth
        pInGeoDataSet = m_pInFClass
        m_pSpat = pInGeoDataSet.SpatialReference


        dblZMulti = CDbl(txtZMulti.Text)

        If dblZMulti <> 0 Then
            m_pSpat.SetZDomain(-9999, m_dblAllZ * dblZMulti)
        Else
            m_pSpat.SetZDomain(-9999, m_dblAllZ)
        End If
        Dim pOutWSF As IWorkspaceFactory
        Dim pOutWS As IWorkspace

        m_strOutDSName = txtDataName.Text

        If m_ImportType = "Personal Geodatabase" Then

            pOutWSF = New AccessWorkspaceFactory
            pOutWS = pOutWSF.OpenFromFile(txtGDBOut.Text, 0)
            'Dim pWorkspaceFactory As IWorkspaceFactory = New AccessWorkspaceFactory
            'pOutWS = pWorkspaceFactory.OpenFromFile(txtGDBOut.Text, 0)
            'Dim pOutWS2 As ESRI.ArcGIS.Geodatabase.IWorkspaceFactory = pOutWS

            m_pOutFWS = pOutWS

            Try
                m_pOutFDataSet = m_pOutFWS.OpenFeatureDataset(m_strOutDSName)

                'm_pOutFDataSet = m_pOutFWS.CreateFeatureDataset(m_strOutDSName, m_pSpat)
                If Not m_pOutFDataSet Is Nothing Then
                    Dim rst As Microsoft.VisualBasic.MsgBoxResult
                    rst = MsgBox("Dataset '" & m_strOutDSName & "' already exists! Do you want to overite it?", MsgBoxStyle.YesNo)
                    If rst = MsgBoxResult.Yes Then
                        m_pOutFDataSet.Delete()
                        m_pOutFDataSet = Nothing
                    Else
                        m_pOutFDataSet = Nothing
                        Throw New System.Exception("Change dataset name and try again!")
                    End If

                    m_pOutFDataSet = m_pOutFWS.CreateFeatureDataset(m_strOutDSName, m_pSpat)
                End If
            Catch ex As Exception
                If ex.Message.IndexOf("not found") > -1 Then
                    m_pOutFDataSet = m_pOutFWS.CreateFeatureDataset(m_strOutDSName, m_pSpat)
                Else
                    Throw ex
                End If
            End Try


        Else
            pOutWSF = New ShapefileWorkspaceFactory
            Dim path As String
            path = m_pWSNameShape & "\" & m_strOutDSName
            If System.IO.Directory.Exists(path) Then
                Dim rst As Microsoft.VisualBasic.MsgBoxResult
                rst = MsgBox("Folder '" & path & "' already exists! Do you want to overite it?", MsgBoxStyle.YesNo)
                If rst = MsgBoxResult.Yes Then
                    System.IO.Directory.Delete(path, True)
                Else
                    Throw New System.Exception("Change dataset name and try again!")
                End If
            End If
            System.IO.Directory.CreateDirectory(path)
            pOutWS = pOutWSF.OpenFromFile(path, 0)
            m_pOutFWS = pOutWS
        End If

    End Sub
    Public Sub BuildGeoPhyloTree()


        Dim tip_obs_method As Integer
        Dim int_node_method As Integer
        Dim stretch As Boolean
        Dim pMCPFClass As IFeatureClass
        Dim pDavaFClass As IFeatureClass
        Dim enclose_method As Integer
        pMCPFClass = Nothing
        pDavaFClass = Nothing

        ' Set up progress bar 
        Dim steps As Integer
        Dim perstep As Integer
        steps = 11
        perstep = 10
        Dim sleeptime As Integer = 200
        prgBar.Maximum = steps * perstep
        prgBar.Minimum = 0
        prgBar.Step = perstep
        prgBar.Value = 0
        prgBar.Visible = True


        lstReport.Items.Add("Checking data")
        prgBar.Value = prgBar.Value + perstep
        lstReport.Update()

        System.Threading.Thread.Sleep(sleeptime)
        CheckData()

        lstReport.Items.Add("Calculating Z")
        prgBar.Value = prgBar.Value + perstep
        lstReport.Update()
        System.Threading.Thread.Sleep(sleeptime)
        GetZ()

        If tree.IsDistanced And chkLinear.Checked = False Then
            m_MaxDepth = tree.GetDepth(cmbZ.SelectedItem.ToString() = "Distance")
        Else
            m_MaxDepth = tree.GetDepth(False)
        End If


        lstReport.Items.Add("Getting location")
        lstReport.Update()
        prgBar.Value = prgBar.Value + perstep
        System.Threading.Thread.Sleep(sleeptime)

        GetLocations()

        Select cboObsTipPos.Text
            Case "Envelope Centroid"
                tip_obs_method = XYCalculator.NODE_TYPE_ENVELOPE
            Case "Mean Position"
                tip_obs_method = XYCalculator.NODE_TYPE_MEAN
            Case "MCP Centroid"
                tip_obs_method = XYCalculator.NODE_TYPE_MCP_CENTROID
            Case Else
                MsgBox("Not Valid Method")
                Exit Sub
        End Select

        Select Case cboIntPos.Text
            Case "Envelope Centroid"
                int_node_method = XYCalculator.NODE_TYPE_ENVELOPE
            Case "Mean Position"
                int_node_method = XYCalculator.NODE_TYPE_MEAN
            Case "MCP Centroid"
                int_node_method = XYCalculator.NODE_TYPE_MCP_CENTROID
            Case "DAVA Centroid"
                int_node_method = XYCalculator.NODE_TYPE_DAVA_CENTROID
            Case Else
                MsgBox("Not Valid Method")
                Exit Sub
        End Select

        Select Case cboEnclose.Text
            Case "Midpoint"
                enclose_method = XYCalculator.ENCLOSE_METHOD_MIDPOINT
            Case "Encloser"
                enclose_method = XYCalculator.ENCLOSE_METHOD_ENCLOSER
            Case "Enclosed"
                enclose_method = XYCalculator.ENCLOSE_METHOD_ENCLOSED
        End Select


        If chkLinear.Checked = True Then stretch = True

        lstReport.Items.Add("Initiating output workspace")
        lstReport.Update()
        prgBar.Value = prgBar.Value + perstep
        System.Threading.Thread.Sleep(sleeptime)
        InitWorkspace()

        'MCP and DAVA polygons
        If int_node_method = XYCalculator.NODE_TYPE_MCP_CENTROID Or int_node_method = XYCalculator.NODE_TYPE_DAVA_CENTROID Or _
            tip_obs_method = XYCalculator.NODE_TYPE_MCP_CENTROID Then
            lstReport.Items.Add("Building polygons")
            lstReport.Update()
            prgBar.Value = prgBar.Value + perstep
            System.Threading.Thread.Sleep(sleeptime)
            If int_node_method = XYCalculator.NODE_TYPE_DAVA_CENTROID Then
                BuildCladePolygons(True)
                pDavaFClass = m_pOutFWS.OpenFeatureClass(m_strOutDSName + "_dava")
            Else
                BuildCladePolygons(False)
            End If
            pMCPFClass = m_pOutFWS.OpenFeatureClass(m_strOutDSName + "_mcp")
            else
        End If

        'CalculateYX
        lstReport.Items.Add("Calculating XY")
        lstReport.Update()
        prgBar.Value = prgBar.Value + perstep
        System.Threading.Thread.Sleep(sleeptime)
        CalculateXY(tip_obs_method, int_node_method, pMCPFClass, pDavaFClass, enclose_method)

        lstReport.Items.Add("Building nodes")
        lstReport.Update()
        prgBar.Value = prgBar.Value + perstep
        System.Threading.Thread.Sleep(sleeptime)
        BuildNodes(chkTipFan.Checked, stretch)

        lstReport.Items.Add("Building branches")
        lstReport.Update()
        prgBar.Value = prgBar.Value + perstep
        System.Threading.Thread.Sleep(sleeptime)
        BuildBranches(chkTipFan.Checked)

        'MCP and DAVA polygons
        If int_node_method = XYCalculator.NODE_TYPE_MCP_CENTROID Or _
            tip_obs_method = XYCalculator.NODE_TYPE_MCP_CENTROID Then
            lstReport.Items.Add("Adding Z to MCP")
            lstReport.Update()
            prgBar.Value = prgBar.Value + perstep
            System.Threading.Thread.Sleep(sleeptime)
            AddZtoPolyFClass(pMCPFClass)
        End If

        If int_node_method = XYCalculator.NODE_TYPE_DAVA_CENTROID Then
            lstReport.Items.Add("Adding Z to DAVA")
            lstReport.Update()
            prgBar.Value = prgBar.Value + perstep
            System.Threading.Thread.Sleep(sleeptime)
            AddZtoPolyFClass(pDavaFClass)
        End If

        If chkDropLine.Checked Then
            lstReport.Items.Add("Building drop lines")
            lstReport.Update()
            prgBar.Value = prgBar.Value + perstep
            System.Threading.Thread.Sleep(sleeptime)
            BuildDropBranches()
        End If


        'If chkNetwork.Checked Then
        '    lstReport.Items.Add("Building network")
        '    lstReport.Update()
        '    prgBar.Value = prgBar.Value + perstep
        '    System.Threading.Thread.Sleep(sleeptime)
        '    'BuildNetwork()
        'End If


        lstReport.Items.Add("Finished!")
        lstReport.Update()
        prgBar.Value = prgBar.Maximum
        System.Threading.Thread.Sleep(sleeptime)


    End Sub
    Public Sub CalculateXY(ByVal TipObsNodeMethod As Integer, ByVal IntNodeMethod As Integer, _
                           Optional ByVal mcpFClass As IFeatureClass = Nothing, Optional ByVal davaFClass As IFeatureClass = Nothing, _
                           Optional ByVal EncloseMethod As Integer = XYCalculator.ENCLOSE_METHOD_MIDPOINT)
        Dim xyzCal As XYCalculator = New XYCalculator
        xyzCal.Geocoodinate = Not (TypeOf m_pSpat Is IProjectedCoordinateSystem)
        xyzCal.TipObsNodeMethod = TipObsNodeMethod
        xyzCal.IntNodeMethod = IntNodeMethod
        xyzCal.EncloseMethod = EncloseMethod
        xyzCal.MCPFClass = mcpFClass
        xyzCal.DavaFClass = davaFClass
        tree.VisitMe(xyzCal, PhyloTreeNode.VISITOR_TYPE_BACK)
    End Sub
    Public Function CreateFeatureClass(ByVal nodeFeatName As String, ByVal pNodeFields As IFields)
        If m_pOutFWS Is Nothing Then
            Throw New Exception("No output feature class factory")
        End If
        Dim pFClass As IFeatureClass
        If m_ImportType = "Personal Geodatabase" Then
            pFClass = m_pOutFDataSet.CreateFeatureClass( _
                nodeFeatName, pNodeFields, Nothing, _
                Nothing, esriFeatureType.esriFTSimple, "Shape", Nothing)
        Else
            pFClass = m_pOutFWS.CreateFeatureClass( _
                            nodeFeatName, pNodeFields, Nothing, _
                            Nothing, esriFeatureType.esriFTSimple, "Shape", Nothing)
        End If

        Return pFClass
    End Function
    Public Sub AddZtoPolyFClass(ByVal pPolyFClass As IFeatureClass)
        Dim myAddZ As AddZtoPolyVisitor = New AddZtoPolyVisitor
        myAddZ.PolyFClass = pPolyFClass
        tree.VisitMe(myAddZ, PhyloTreeNode.VISITOR_TYPE_FORE)
    End Sub

    Public Sub BuildNodes(ByVal TipFan As Boolean, ByVal LinearStretch As Boolean)

        Dim pNodeFields As IFields
        Dim pNodeFClass As IFeatureClass


        'Create Node Feature Class
        lstReport.Items.Add("Getting node fields")
        pNodeFields = CreateNodeFields(m_pSpat)
        If pNodeFields Is Nothing Then
            Throw New Exception("Failed to create Fields")
        End If
        lstReport.Items.Add("Creating node feature class")
        Dim pCLSID As UID
        pCLSID = New UID


        pCLSID.Value = "esriGeoDatabase.Feature"
        Dim nodeFeatName As String
        nodeFeatName = m_strOutDSName & "_node"
        ProgressMessage = nodeFeatName
        Dim i As Integer
        For i = 0 To pNodeFields.FieldCount - 1
            lstReport.Items.Add("Field " & Str(i + 1) & ":" & pNodeFields.Field(i).Name)
        Next
        pNodeFClass = CreateFeatureClass(nodeFeatName, pNodeFields)

        lstReport.Items.Add("Creating BuildNodesVisitor")
        Dim buildNodeVisitor As BuildNodesVisitor = New BuildNodesVisitor
        lstReport.Items.Add("Setting B feature class handle for BuildNodesVisitor")
        buildNodeVisitor.FeatureClass = pNodeFClass

        If tree.IsDistanced Then buildNodeVisitor.UseDistance = (cmbZ.SelectedItem.ToString() = "Distance")

        If LinearStretch = True Then buildNodeVisitor.LinearStretch = True

        buildNodeVisitor.TipFan = CDbl(txtTipFanDepth.Text)
        buildNodeVisitor.MultipleZ = CDbl(txtZMulti.Text)
        buildNodeVisitor.MaxDepth = m_MaxDepth

        lstReport.Items.Add("Visiting the tree")
        tree.VisitMe(buildNodeVisitor, PhyloTreeNode.VISITOR_TYPE_FORE)
        pNodeFields = Nothing
        pNodeFClass = Nothing
        'End If
    End Sub
    Public Sub BuildSampleNodes()

        If cmbImportType.SelectedItem = "KML(Google Earth)" Then
            m_Writer.WriteLine("<Folder>")
            m_Writer.WriteLine("<name>Sample Nodes</name>")
            m_Writer.WriteLine("<description>Sample Nodes</description>")


            Dim buildSampleNodeVisitor As BuildSampleNodesVisitor = New BuildSampleNodesVisitor
            lstReport.Items.Add("Setting BuildSampleNodesVisitor")
            buildSampleNodeVisitor.Writer = m_Writer
            'buildSampleNodeVisitor.UseNewOrigin = chkSpatRoot.Checked
            If tree.IsDistanced Then
                buildSampleNodeVisitor.UseDistance = (cmbZ.SelectedItem.ToString() = "Distance")
            End If
            'If chkSpatRoot.Checked Then
            '    buildSampleNodeVisitor.OriginX = CDbl(txtRootX.Text)
            '    buildSampleNodeVisitor.OriginY = CDbl(txtRootY.Text)
            'End If
            buildSampleNodeVisitor.MultipleZ = CDbl(txtZMulti.Text)
            buildSampleNodeVisitor.MaxDepth = m_MaxDepth
            lstReport.Items.Add("Visiting the tree")
            tree.VisitMe(buildSampleNodeVisitor, PhyloTreeNode.VISITOR_TYPE_FORE)
            m_Writer.WriteLine("</Folder>")
            buildSampleNodeVisitor = Nothing

        End If
    End Sub
    Public Sub BuildSampleLinks()

        m_Writer.WriteLine("<Folder>")
        m_Writer.WriteLine("<name>Sample Links</name>")
        m_Writer.WriteLine("<description>Links from drop nodes to sample nodes</description>")

        lstReport.Items.Add("Creating BuildSampleLinksVisitor")
        Dim buildSampleLinksVisitor As BuildSampleLinksVisitor = New BuildSampleLinksVisitor
        lstReport.Items.Add("Setting feature class handle for BuildSampleLinksVisitor")
        buildSampleLinksVisitor.Writer = m_Writer
        'buildSampleLinksVisitor.UseNewOrigin = chkSpatRoot.Checked
        If tree.IsDistanced Then
            buildSampleLinksVisitor.UseDistance = (cmbZ.SelectedItem.ToString() = "Distance")
        End If

        buildSampleLinksVisitor.MultipleZ = CDbl(txtZMulti.Text)
        buildSampleLinksVisitor.MaxDepth = m_MaxDepth
        lstReport.Items.Add("Building sample links")
        tree.VisitMe(buildSampleLinksVisitor, PhyloTreeNode.VISITOR_TYPE_FORE)
        m_Writer.WriteLine("</Folder>")
        buildSampleLinksVisitor = Nothing


    End Sub
    Public Sub BuildBranches(ByVal TipFan As Boolean)

        If cmbImportType.SelectedItem = "KML(Google Earth)" Then
            m_Writer.WriteLine("<Folder>")
            m_Writer.WriteLine("<name>Tree Braches</name>")
            m_Writer.WriteLine("<description>Tree Nodes</description>")


            lstReport.Items.Add("Creating BuildLinesVisitor")
            Dim buildLineVisitor As BuildBranchesVisitor = New BuildBranchesVisitor

            lstReport.Items.Add("Setting feature class handle for BuildLinesVisitor")
            buildLineVisitor.Writer = m_Writer
            'buildLineVisitor.UseNewOrigin = chkSpatRoot.Checked
            If tree.IsDistanced Then
                buildLineVisitor.UseDistance = (cmbZ.SelectedItem.ToString() = "Distance")
            End If
            'If chkSpatRoot.Checked Then
            '    buildLineVisitor.OriginX = CDbl(txtRootX.Text)
            '    buildLineVisitor.OriginY = CDbl(txtRootY.Text)
            'End If
            buildLineVisitor.MultipleZ = CDbl(txtZMulti.Text)
            buildLineVisitor.MaxDepth = m_MaxDepth
            buildLineVisitor.VertexShape = cboBrShape.Text
            lstReport.Items.Add("Building branches")
            tree.VisitMe(buildLineVisitor, PhyloTreeNode.VISITOR_TYPE_FORE)
            m_Writer.WriteLine("</Folder>")
            buildLineVisitor = Nothing

            BuildSampleLinks()

        Else
            Dim pLineFields As IFields
            Dim pLineFClass As IFeatureClass

            'Create Line Feature Class
            lstReport.Items.Add("Geting Line fields")
            pLineFields = CreateBranchFields(m_pSpat)
            If pLineFields Is Nothing Then
                Throw New Exception("Failed to create Fields")
            End If
            lstReport.Items.Add("Creating Line feature class")
            Dim pCLSID As UID
            pCLSID = New UID
            pCLSID.Value = "esriGeoDatabase.Feature"
            Dim LineFeatName As String
            LineFeatName = m_strOutDSName & "_branch"
            ProgressMessage = LineFeatName

            Dim i As Integer
            For i = 0 To pLineFields.FieldCount - 1
                lstReport.Items.Add("Field " & Str(i + 1) & ":" & pLineFields.Field(i).Name)
            Next

            pLineFClass = CreateFeatureClass(LineFeatName, pLineFields)

            lstReport.Items.Add("Creating BuildLinesVisitor")
            Dim buildLineVisitor As BuildBranchesVisitor = New BuildBranchesVisitor
            buildLineVisitor.VertexType = cboBrVert.SelectedItem
            buildLineVisitor.VertexNo = CDbl(txtBrVert.Text)
            buildLineVisitor.VertexShape = cboBrShape.SelectedItem
            buildLineVisitor.VertexXDepth = CDbl(txtXBrDepth.Text)
            buildLineVisitor.TipFan = chkTipFan.Checked

            lstReport.Items.Add("Setting feature class handle for BuildLinesVisitor")
            buildLineVisitor.FeatureClass = pLineFClass
            'buildLineVisitor.UseNewOrigin = chkSpatRoot.Checked
            If tree.IsDistanced Then
                buildLineVisitor.UseDistance = (cmbZ.SelectedItem.ToString() = "Distance")
            End If
            'If chkSpatRoot.Checked Then
            '    buildLineVisitor.OriginX = CDbl(txtRootX.Text)
            '    buildLineVisitor.OriginY = CDbl(txtRootY.Text)
            'End If
            buildLineVisitor.MultipleZ = CDbl(txtZMulti.Text)
            buildLineVisitor.MaxDepth = m_MaxDepth
            lstReport.Items.Add("Building branches")
            tree.VisitMe(buildLineVisitor, PhyloTreeNode.VISITOR_TYPE_FORE)
            pLineFields = Nothing
            pLineFClass = Nothing
        End If
    End Sub

    Public Sub BuildNodesLabel()
        m_Writer.WriteLine("<Folder>")
        m_Writer.WriteLine("<name>Labels</name>")
        m_Writer.WriteLine("<description>Labels of Tree Nodes</description>")

        Dim buildNodeLabelsVisitor As BuildNodeLabelsVisitor = New BuildNodeLabelsVisitor
        lstReport.Items.Add("Setting B feature class handle for BuildNodeLabelsVisitor")
        buildNodeLabelsVisitor.Writer = m_Writer
        'buildNodeLabelsVisitor.UseNewOrigin = chkSpatRoot.Checked
        If tree.IsDistanced Then
            buildNodeLabelsVisitor.UseDistance = (cmbZ.SelectedItem.ToString() = "Distance")
        End If
        'If chkSpatRoot.Checked Then
        '    buildNodeLabelsVisitor.OriginX = CDbl(txtRootX.Text)
        '    buildNodeLabelsVisitor.OriginY = CDbl(txtRootY.Text)
        'End If
        buildNodeLabelsVisitor.MultipleZ = CDbl(txtZMulti.Text)
        buildNodeLabelsVisitor.MaxDepth = m_MaxDepth
        lstReport.Items.Add("Visiting the tree")
        tree.VisitMe(buildNodeLabelsVisitor, PhyloTreeNode.VISITOR_TYPE_FORE)
        m_Writer.WriteLine("</Folder>")
        buildNodeLabelsVisitor = Nothing
    End Sub
   
    Public Sub BuildDropBranches()
   
        Dim pLineFields As IFields
        Dim pLineFClass As IFeatureClass

        'Create Line Feature Class
        lstReport.Items.Add("Geting Line fields")
        pLineFields = CreateDropLineFields(m_pSpat)
        If pLineFields Is Nothing Then
            Throw New Exception("Failed to create Fields")
        End If
        lstReport.Items.Add("Creating Line feature class")
        Dim pCLSID As UID
        pCLSID = New UID

        pCLSID.Value = "esriGeoDatabase.Feature"
        Dim LineFeatName As String
        LineFeatName = m_strOutDSName & "_dropline"
        ProgressMessage = LineFeatName


        'Dim i As Integer
        'For i = 0 To pLineFields.FieldCount - 1
        'lstReport.Items.Add("Field " & Str(i + 1) & ":" & pLineFields.Field(i).Name)
        'Next

        pLineFClass = CreateFeatureClass(LineFeatName, pLineFields)

        lstReport.Items.Add("Creating BuildDropLinesVisitor")
        Dim buildDropLineVisitor As BuildDropLinesVisitor = New BuildDropLinesVisitor
        lstReport.Items.Add("Setting B feature class handle for uildLinesVisitor")
        buildDropLineVisitor.FeatureClass = pLineFClass
        'buildDropLineVisitor.UseNewOrigin = chkSpatRoot.Checked
        buildDropLineVisitor.DropToZ = CDbl(txtDropZ.Text)
        If tree.IsDistanced Then
            buildDropLineVisitor.UseDistance = (cmbZ.SelectedItem.ToString() = "Distance")
        End If
        'If chkSpatRoot.Checked Then
        '    buildDropLineVisitor.OriginX = CDbl(txtRootX.Text)
        '    buildDropLineVisitor.OriginY = CDbl(txtRootY.Text)
        'End If
        buildDropLineVisitor.MultipleZ = CDbl(txtZMulti.Text)
        buildDropLineVisitor.MaxDepth = m_MaxDepth
        lstReport.Items.Add("Building branches")
        tree.VisitMe(buildDropLineVisitor, PhyloTreeNode.VISITOR_TYPE_FORE)
        pLineFields = Nothing
        pLineFClass = Nothing

    End Sub
    Public Sub BuildCladePolygons(ByVal DAVA As Boolean)

        Dim pPolyFields As IFields
        Dim pPolyFClass As IFeatureClass

        'Create Polygon Feature Class
        lstReport.Items.Add("Geting polygon clade fields")
        pPolyFields = CreateMCPFields(m_pSpat)


        If pPolyFields Is Nothing Then
            Throw New Exception("Failed to create Fields")
        End If
        lstReport.Items.Add("Creating polygon clade feature class")

        Dim pCLSID As UID
        pCLSID = New UID

        pCLSID.Value = "esriGeoDatabase.Feature"
        Dim LineFeatName As String
        LineFeatName = m_strOutDSName & "_mcp"
        ProgressMessage = LineFeatName

        Dim i As Integer
        For i = 0 To pPolyFields.FieldCount - 1
            lstReport.Items.Add("Field " & Str(i + 1) & ":" & pPolyFields.Field(i).Name)
        Next

        pPolyFClass = CreateFeatureClass(LineFeatName, pPolyFields)

        lstReport.Items.Add("Creating BuildCladePolygonsVisitor")
        Dim cladePolygonsVisitor As BuildCladePolygonsVisitor = New BuildCladePolygonsVisitor
        lstReport.Items.Add("Setting B feature class handle for BuildLinesVisitor")
        cladePolygonsVisitor.FeatureClass = pPolyFClass
        cladePolygonsVisitor.PandLBufferSize = txtPandLBuffer.Text
        cladePolygonsVisitor.PolyBufferSize = txtCladeBuffDist.Text

        If tree.IsDistanced Then
            cladePolygonsVisitor.UseDistance = (cmbZ.SelectedItem.ToString() = "Distance")
        End If

        cladePolygonsVisitor.MultipleZ = CDbl(txtZMulti.Text)
        cladePolygonsVisitor.MaxDepth = m_MaxDepth

        lstReport.Items.Add("Building clades polygons")
        tree.VisitMe(cladePolygonsVisitor, PhyloTreeNode.VISITOR_TYPE_FORE)

        If DAVA Then
            Dim pDavaPolyFClass As IFeatureClass
            pPolyFields = CreateDAVAFields(m_pSpat)
            pDavaPolyFClass = CreateFeatureClass(m_strOutDSName & "_dava", pPolyFields)
            lstReport.Items.Add("Creating DAVAVisitor")
            Dim DAVAVisitor As DAVAVisitor = New DAVAVisitor
            DAVAVisitor.DAVA_Threshold = CDbl(txtDAVAThreshold.Text)
            DAVAVisitor.NodePolyFeatureClass = pPolyFClass
            DAVAVisitor.DavaPolyFeatureClass = pDavaPolyFClass
            tree.VisitMe(DAVAVisitor, PhyloTreeNode.VISITOR_TYPE_FORE)
            DAVAVisitor = Nothing
        End If

        pPolyFields = Nothing
        pPolyFClass = Nothing
        cladePolygonsVisitor = Nothing

    End Sub

    Public Sub buildDAVApolygons(ByVal pNodePolygonFeatureClass As IFeatureClass)
        Dim pPolyFields As IFields
        Dim pPolyFClass As IFeatureClass

        'Create Polygon Feature Class
        lstReport.Items.Add("Geting polygon clade fields")
        pPolyFields = CreateDAVAFields(m_pSpat)
        If pPolyFields Is Nothing Then
            Throw New Exception("Failed to create Fields")
        End If
        lstReport.Items.Add("Creating polygon clade feature class")
        Dim pCLSID As UID
        pCLSID = New UID

        pCLSID.Value = "esriGeoDatabase.Feature"
        Dim LineFeatName As String
        LineFeatName = m_strOutDSName & "_dava"
        ProgressMessage = LineFeatName

        Dim i As Integer
        For i = 0 To pPolyFields.FieldCount - 1
            lstReport.Items.Add("Field " & Str(i + 1) & ":" & pPolyFields.Field(i).Name)
        Next

        pPolyFClass = CreateFeatureClass(LineFeatName, pPolyFields)

        lstReport.Items.Add("Creating DavaVisitor")
        Dim davaPolygonsVisitor As DAVAVisitor = New DAVAVisitor
        lstReport.Items.Add("Setting B feature class handle for uildLinesVisitor")
        davaPolygonsVisitor.FeatureClass = pPolyFClass
        davaPolygonsVisitor.DAVA_Threshold = txtDAVAThreshold.Text
        davaPolygonsVisitor.NodePolyFeatureClass = pNodePolygonFeatureClass


        'If tree.IsDistanced Then DAVAVisitor.UseDistance = (cmbZ.SelectedItem.ToString() = "Distance")
        'cladePolygonsVisitor.MultipleZ = CDbl(txtZMulti.Text)
        'cladePolygonsVisitor.MaxDepth = m_MaxDepth

        lstReport.Items.Add("Building DAVA polygons")
        tree.VisitMe(davaPolygonsVisitor, PhyloTreeNode.VISITOR_TYPE_FORE)

        pPolyFields = Nothing
        pPolyFClass = Nothing
        davaPolygonsVisitor = Nothing
    End Sub
    Public Sub BuildNetwork()

    End Sub
    Private Function CreateField(ByVal fieldName As String, ByVal fieldType As Integer, ByVal fieldLength As Integer) As IField

        lstReport.Items.Add("creating field: " & fieldName)
        Dim pField As IField
        pField = New Field
        Dim pFieldEdit As IFieldEdit
        pFieldEdit = pField

        With pFieldEdit
            .Name_2 = fieldName
            .Type_2 = fieldType
            If (fieldLength > 0) Then
                .Length_2 = fieldLength
            End If
        End With

        Return pField
    End Function
    Public Function CreateNodeFields(ByVal pSpatRef As ISpatialReference) As IFields

        Dim pGeomDef As IGeometryDef
        Dim pGeomDefEdit As IGeometryDefEdit
        Dim pFieldEdit As IFieldEdit
        Dim pFields As IFields
        Dim pField As IField
        Dim pFieldsEdit As IFieldsEdit

        pFields = New Fields
        pFieldsEdit = pFields

        '1. OID field
        pField = CreateField("OID", esriFieldType.esriFieldTypeOID, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        '2. Shape Field
        pField = CreateField("Shape", esriFieldType.esriFieldTypeGeometry, 0)
        pFieldEdit = pField
        pGeomDef = New GeometryDef
        pGeomDefEdit = pGeomDef
        With pGeomDefEdit
            .GeometryType_2 = esriGeometryType.esriGeometryPoint
            .HasM_2 = False
            .HasZ_2 = True
            .SpatialReference_2 = pSpatRef
        End With
        pFieldEdit.GeometryDef_2 = pGeomDef
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing
        pGeomDef = Nothing
        pGeomDefEdit = Nothing

        '3. NodeID
        pField = CreateField("NodeID", esriFieldType.esriFieldTypeInteger, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        '4. Node Name
        pField = CreateField("NodeName", esriFieldType.esriFieldTypeString, 255)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        '5. Node Type
        pField = CreateField("NodeType", esriFieldType.esriFieldTypeInteger, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        '6. Node Level from Root
        pField = CreateField("nLRoot", esriFieldType.esriFieldTypeInteger, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        '7. Level to Tip (levels to deepest descendant tip)
        pField = CreateField("nLTip", esriFieldType.esriFieldTypeInteger, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        '8. Distance
        pField = CreateField("d", esriFieldType.esriFieldTypeDouble, 16)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        '9. Node Distance From Root
        pField = CreateField("dRoot", esriFieldType.esriFieldTypeDouble, 16)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        '10. Distance to deepest descendant Tip (distance t0 deepest  tip)
        pField = CreateField("dTip", esriFieldType.esriFieldTypeDouble, 16)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        '11. DisplayZ
        pField = CreateField("displayZ", esriFieldType.esriFieldTypeDouble, 16)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        '12. Rank-based id
        pField = CreateField("RankId", esriFieldType.esriFieldTypeString, 1000)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        Return pFields


    End Function

    Public Function CreateBranchFields(ByVal pSpatRef As ISpatialReference) As IFields

        Dim pGeomDef As IGeometryDef
        Dim pGeomDefEdit As IGeometryDefEdit
        Dim pFieldEdit As IFieldEdit
        Dim pFields As IFields
        Dim pField As IField
        Dim pFieldsEdit As IFieldsEdit

        pFields = New Fields
        pFieldsEdit = pFields

        'OID field
        pField = CreateField("OID", esriFieldType.esriFieldTypeOID, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        'Shape field
        pField = CreateField("Shape", esriFieldType.esriFieldTypeGeometry, 0)
        pFieldEdit = pField
        pGeomDef = New GeometryDef
        pGeomDefEdit = pGeomDef
        With pGeomDefEdit
            .GeometryType_2 = esriGeometryType.esriGeometryPolyline
            .HasM_2 = False
            .HasZ_2 = True
            .SpatialReference_2 = pSpatRef
        End With

        pFieldEdit.GeometryDef_2 = pGeomDef
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing
        pGeomDef = Nothing
        pGeomDefEdit = Nothing

        'BranchID field
        pField = CreateField("BranchID", esriFieldType.esriFieldTypeInteger, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        'BranchName field
        pField = CreateField("BranchName", esriFieldType.esriFieldTypeString, 255)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        'From Node
        pField = CreateField("pNodeID", esriFieldType.esriFieldTypeInteger, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        'To Node
        pField = CreateField("cNodeID", esriFieldType.esriFieldTypeInteger, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        'BranchType field
        pField = CreateField("BranchType", esriFieldType.esriFieldTypeInteger, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        'PhyloLength
        pField = CreateField("PhyloLength", esriFieldType.esriFieldTypeDouble, 16)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        'GeoLength
        pField = CreateField("GeoLength", esriFieldType.esriFieldTypeDouble, 16)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        'from rank-based id
        pField = CreateField("pRankId", esriFieldType.esriFieldTypeString, 1000)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        'to rank-based id
        pField = CreateField("cRankId", esriFieldType.esriFieldTypeString, 1000)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        Return pFields


    End Function
    Public Function CreateMCPFields(ByVal pSpatRef As ISpatialReference) As IFields

        Dim pGeomDef As IGeometryDef
        Dim pGeomDefEdit As IGeometryDefEdit
        Dim pFieldEdit As IFieldEdit
        Dim pFields As IFields
        Dim pField As IField
        Dim pFieldsEdit As IFieldsEdit

        pFields = New Fields
        pFieldsEdit = pFields


        'OID field

        pField = CreateField("OID", esriFieldType.esriFieldTypeOID, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        pField = CreateField("Shape", esriFieldType.esriFieldTypeGeometry, 0)
        pFieldEdit = pField
        pGeomDef = New GeometryDef
        pGeomDefEdit = pGeomDef
        With pGeomDefEdit
            .GeometryType_2 = esriGeometryType.esriGeometryPolygon
            .HasM_2 = False
            .HasZ_2 = True
            .SpatialReference_2 = pSpatRef
        End With

        pFieldEdit.GeometryDef_2 = pGeomDef
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing
        pGeomDef = Nothing
        pGeomDefEdit = Nothing

        'NodePolyID field
        pField = CreateField("PolyID", esriFieldType.esriFieldTypeInteger, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        'NodeID field
        pField = CreateField("NodeID", esriFieldType.esriFieldTypeInteger, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        'NodePolyName field
        pField = CreateField("NodeName", esriFieldType.esriFieldTypeString, 255)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        Return pFields


    End Function
    Public Function CreateDAVAFields(ByVal pSpatRef As ISpatialReference) As IFields

        Dim pGeomDef As IGeometryDef
        Dim pGeomDefEdit As IGeometryDefEdit
        Dim pFieldEdit As IFieldEdit
        Dim pFields As IFields
        Dim pField As IField
        Dim pFieldsEdit As IFieldsEdit

        pFields = New Fields
        pFieldsEdit = pFields

        'OID field

        pField = CreateField("OID", esriFieldType.esriFieldTypeOID, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        pField = CreateField("Shape", esriFieldType.esriFieldTypeGeometry, 0)
        pFieldEdit = pField
        pGeomDef = New GeometryDef
        pGeomDefEdit = pGeomDef
        With pGeomDefEdit
            .GeometryType_2 = esriGeometryType.esriGeometryPolygon
            .HasM_2 = False
            .HasZ_2 = True
            .SpatialReference_2 = pSpatRef
        End With

        pFieldEdit.GeometryDef_2 = pGeomDef
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing
        pGeomDef = Nothing
        pGeomDefEdit = Nothing

        'NodePloyID field
        pField = CreateField("PolyID", esriFieldType.esriFieldTypeInteger, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        'Parent NodeID field
        pField = CreateField("NodeID", esriFieldType.esriFieldTypeInteger, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        'Parent NodeName field
        pField = CreateField("NodeName", esriFieldType.esriFieldTypeString, 255)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        'Child NodeID field
        pField = CreateField("cNodeID", esriFieldType.esriFieldTypeInteger, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        'Child NodeName field
        pField = CreateField("cNodeName", esriFieldType.esriFieldTypeString, 255)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        'PolyType
        pField = CreateField("PolyType", esriFieldType.esriFieldTypeInteger, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        'Overlap field
        pField = CreateField("Overlap", esriFieldType.esriFieldTypeDouble, 16)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing


        Return pFields


    End Function

    Public Function CreateDropLineFields(ByVal pSpatRef As ISpatialReference) As IFields

        Dim pGeomDef As IGeometryDef
        Dim pGeomDefEdit As IGeometryDefEdit
        Dim pFieldEdit As IFieldEdit
        Dim pFields As IFields
        Dim pField As IField
        Dim pFieldsEdit As IFieldsEdit

        pFields = New Fields
        pFieldsEdit = pFields

        'OID field
        pField = CreateField("OID", esriFieldType.esriFieldTypeOID, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        pField = CreateField("Shape", esriFieldType.esriFieldTypeGeometry, 0)
        pFieldEdit = pField
        pGeomDef = New GeometryDef
        pGeomDefEdit = pGeomDef
        With pGeomDefEdit
            .GeometryType_2 = esriGeometryType.esriGeometryPolyline
            .HasM_2 = False
            .HasZ_2 = True
            .SpatialReference_2 = pSpatRef
        End With

        pFieldEdit.GeometryDef_2 = pGeomDef
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing
        pGeomDef = Nothing
        pGeomDefEdit = Nothing

        'DropLineID field
        pField = CreateField("DLineID", esriFieldType.esriFieldTypeInteger, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        'DropLineName field
        pField = CreateField("DLineName", esriFieldType.esriFieldTypeString, 255)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        'DropLineType
        pField = CreateField("DLineType", esriFieldType.esriFieldTypeInteger, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        'From Node
        pField = CreateField("NodeID", esriFieldType.esriFieldTypeInteger, 8)
        pFieldEdit = pField
        pFieldsEdit.AddField(pField)
        pFieldEdit = Nothing
        pField = Nothing

        Return pFields


    End Function
    Private Sub DlgMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbImportType.Items.Add("Personal Geodatabase")
        cmbImportType.Items.Add("Shapefile")
        cmbImportType.SelectedIndex = 0
        m_ImportType = "Personal Geodatabase"

        cboEnclose.Items.Add("Midpoint")
        cboEnclose.Items.Add("Encloser")
        cboEnclose.Items.Add("Enclosed")
        cboEnclose.SelectedIndex = 0

        Me.CenterToScreen()

    End Sub

    Private Sub prgTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim s As String
        Dim ps As String
        ps = ProgressMessage()

        If lstReport.Items.Count > 0 Then
            s = lstReport.Items(lstReport.Items.Count - 1).ToString
            If (s <> ProgressMessage()) Then
                lstReport.Items.Add(ps)
            End If
        Else
            lstReport.Items.Add(ps)
        End If
        prgBar.Value = ProgressValue
    End Sub

    Private Sub txtDataName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDataName.TextChanged

    End Sub

    Private Sub Label23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label23.Click

    End Sub

    Private Sub cmbImportType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbImportType.SelectedIndexChanged
        m_ImportType = cmbImportType.SelectedItem.ToString
        'lblDatasetName.Visible = (m_ImportType = "Personal Geodatabse")
        'txtDataName.Visible = (m_ImportType = "Personal Geodatabse")
        chkNetwork.Visible = (m_ImportType = "Personal Geodatabse")
        txtGDBOut.Text = ""
        lblDatasetName.Visible = (m_ImportType <> "KML(Google Earth)")
        txtDataName.Visible = (m_ImportType <> "KML(Google Earth)")


    End Sub

    Public Sub ShowTree(ByVal name As String)
        Try
            tree = model.GetTree(name)
            If tree Is Nothing Then
                Return
            End If
            tvwTree.Nodes.Clear()

            Dim visitor As TreeViewVisitor
            visitor = New TreeViewVisitor
            visitor.TreeView = tvwTree
            tree.VisitMe(visitor, PhyloTreeNode.VISITOR_TYPE_FORE)
            tvwTree.ExpandAll()

            lblZ.Visible = tree.IsDistanced
            cmbZ.Visible = tree.IsDistanced
            cmbZ.Items.Clear()
            cmbZ.Items.Add("Distance")
            cmbZ.Items.Add("Level")
            cmbZ.SelectedIndex = 0
            butSetRoot.Enabled = False

        Catch ex As Exception
            MsgBox(ex.Message)
            tree = Nothing
        End Try
    End Sub
    Private Sub butSetRoot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butSetRoot.Click
        Dim id As String
        id = tvwTree.SelectedNode.Name
        Dim nd As PhyloTreeNode
        nd = tree.Find(id)

        If nd Is Nothing Then
            Return
        End If
        tree.SetRoot(nd)
        ShowTree(tree.TreeName)

    End Sub


    Private Sub chkFixLocation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFixLocation.CheckedChanged
        Dim id As String
        id = tvwTree.SelectedNode.Name

        Dim nd As PhyloTreeNode
        nd = tree.Find(id)

        If nd Is Nothing Then
            Return
        End If

        nd.FixedLocation = chkFixLocation.Checked
        txtFixedX.Enabled = chkFixLocation.Checked
        txtFixedY.Enabled = chkFixLocation.Checked

    End Sub

    Private Sub txtFixedX_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFixedX.TextChanged
        Dim id As String
        id = tvwTree.SelectedNode.Name

        Dim nd As PhyloTreeNode
        nd = tree.Find(id)

        If nd Is Nothing Then
            Return
        End If
        If txtFixedX.Text = "" Then
            Return
        End If

        Try
            nd.X = CDbl(txtFixedX.Text)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtFixedY_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFixedY.TextChanged
        Dim id As String
        id = tvwTree.SelectedNode.Name

        Dim nd As PhyloTreeNode
        nd = tree.Find(id)

        If nd Is Nothing Then
            Return
        End If

        If txtFixedY.Text = "" Then
            Return
        End If

        Try
            nd.Y = CDbl(txtFixedY.Text)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Public Sub UpdateMessage(ByVal message As String) Implements Observer.UpdateMessage
        lstReport.Items.Add(message)

    End Sub

    Private Sub cboBrShape_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBrShape.SelectedValueChanged

        If cboBrShape.Text = "Rectangular" Then
            lblXBrDepth.Enabled = True
            txtXBrDepth.Enabled = True
        Else
            lblXBrDepth.Enabled = False
            txtXBrDepth.Enabled = False
        End If



    End Sub

    Private Sub butSetMidpoint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butSetMidpoint.Click
        Dim longestPathVisitor As LongestPathVisitor = New LongestPathVisitor
        Dim midPoint As SplitPoint

        tree.VisitMe(longestPathVisitor, PhyloTreeNode.VISITOR_TYPE_BACK)
        midPoint = tree.MidPoint
        tree.SetRoot(midPoint)
        ShowTree(tree.TreeName)

    End Sub

    Private Sub txtXBrDepth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtXBrDepth.TextChanged
        If IsNumeric(txtXBrDepth.Text) Then
            If CDbl(txtXBrDepth.Text) > 100 Or CDbl(txtXBrDepth.Text) < 0 Then MsgBox("Cross branch depth must be between 0 and 100")
            If CDbl(txtXBrDepth.Text) > 100 Then txtXBrDepth.Text = "100"
            If CDbl(txtXBrDepth.Text) < 0 Then txtXBrDepth.Text = "0"
        Else
            MsgBox("Cross branch depth must be between 0 and 100")
            txtXBrDepth.Text = 0
        End If

    End Sub

    Private Sub chkTipFan_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTipFan.CheckStateChanged

        If chkTipFan.Checked = True Then
            txtTipFanDepth.Enabled = True
            lblTipFanDepth.Enabled = True
        Else
            txtTipFanDepth.Enabled = False
            lblTipFanDepth.Enabled = False
        End If

    End Sub

    Private Sub txtTipFanDepth_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTipFanDepth.TextChanged
        If IsNumeric(txtTipFanDepth.Text) Then
            If CDbl(txtTipFanDepth.Text) > 100 Or CDbl(txtTipFanDepth.Text) < 0 Then MsgBox("Fan Tip depth must be between 0 and 100%")
            If CDbl(txtTipFanDepth.Text) > 100 Then txtTipFanDepth.Text = "100"
            If CDbl(txtTipFanDepth.Text) < 0 Then txtTipFanDepth.Text = "0"
        Else
            MsgBox("Fan Tip branch depth must be between 0 and 100")
            txtTipFanDepth.Text = 50
        End If
    End Sub

    Private Sub cmbZ_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbZ.SelectedValueChanged
        If cmbZ.Text = "Distance" Then
            chkLinear.Checked = False
            chkLinear.Enabled = False
        Else
            chkLinear.Checked = True
            chkLinear.Enabled = True
        End If

    End Sub

    Private Sub cboObsTipPos_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboObsTipPos.SelectedValueChanged

        If cboObsTipPos.SelectedItem = "MCP Centroid" Then
            txtCladeBuffDist.Enabled = True
            lblCladeBuffDist.Enabled = True
            txtPandLBuffer.Enabled = True
            lblPandLBuffer.Enabled = True
            cboEnclose.Enabled = True
            lblEnclose.Enabled = True
        Else
            txtCladeBuffDist.Enabled = False
            lblCladeBuffDist.Enabled = False
            txtPandLBuffer.Enabled = False
            lblPandLBuffer.Enabled = False
            cboEnclose.Enabled = False
            lblEnclose.Enabled = False
        End If

    End Sub

    Private Sub cboIntPos_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboIntPos.SelectedValueChanged

        If (cboIntPos.SelectedItem = "MCP Centroid" Or cboIntPos.SelectedItem = "DAVA Centroid") Then
            txtCladeBuffDist.Enabled = True
            lblCladeBuffDist.Enabled = True
            txtPandLBuffer.Enabled = True
            lblPandLBuffer.Enabled = True
            cboEnclose.Enabled = True
            lblEnclose.Enabled = True
        Else
            txtCladeBuffDist.Enabled = False
            lblCladeBuffDist.Enabled = False
            txtPandLBuffer.Enabled = False
            lblPandLBuffer.Enabled = False
            cboEnclose.Enabled = False
            lblEnclose.Enabled = False
        End If

        If (cboIntPos.SelectedItem = "DAVA Centroid") Then
            txtDAVAThreshold.Enabled = True
            lblDAVAThreshold.Enabled = True
        Else
            txtDAVAThreshold.Enabled = False
            lblDAVAThreshold.Enabled = False
        End If

    End Sub


    Private Sub txtCladeBuffDist_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCladeBuffDist.Validated
        ErrorProvider1.SetError(txtCladeBuffDist, "")
    End Sub

    Private Sub txtCladeBuffDist_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtCladeBuffDist.Validating
        ' Postitive numeric
        If Not IsNumeric(txtCladeBuffDist.Text) Then
            e.Cancel = True
            txtCladeBuffDist.Select(0, txtCladeBuffDist.TextLength)
            ErrorProvider1.SetError(txtCladeBuffDist, "Must be a positive number")
        Else
            If txtCladeBuffDist.Text < 0 Then
                e.Cancel = True
                txtCladeBuffDist.Select(0, txtCladeBuffDist.TextLength)
                ErrorProvider1.SetError(txtCladeBuffDist, "Must be a positive number")
            End If
        End If

    End Sub

    Private Sub txtPandLBuffer_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPandLBuffer.Validated
        ErrorProvider1.SetError(txtPandLBuffer, "")
    End Sub

    Private Sub txtPandLBuffer_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPandLBuffer.Validating
        If Not IsNumeric(txtPandLBuffer.Text) Then
            e.Cancel = True
            txtPandLBuffer.Select(0, txtPandLBuffer.TextLength)
            ErrorProvider1.SetError(txtPandLBuffer, "Must be a positive number")
        Else
            If txtPandLBuffer.Text < 0 Then
                e.Cancel = True
                txtPandLBuffer.Select(0, txtPandLBuffer.TextLength)
                ErrorProvider1.SetError(txtPandLBuffer, "Must be a positive number")
            End If
        End If
    End Sub

    Private Sub txtDAVAThreshold_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDAVAThreshold.Validated
        ErrorProvider1.SetError(txtDAVAThreshold, "")
    End Sub

    Private Sub txtDAVAThreshold_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtDAVAThreshold.Validating
        If Not IsNumeric(txtDAVAThreshold.Text) Then
            e.Cancel = True
            txtDAVAThreshold.Select(0, txtDAVAThreshold.TextLength)
            ErrorProvider1.SetError(txtDAVAThreshold, "Must be a number between 0 and 100")
        Else
            If txtDAVAThreshold.Text < 0 Or txtPandLBuffer.Text > 100 Then
                e.Cancel = True
                txtDAVAThreshold.Select(0, txtDAVAThreshold.TextLength)
                ErrorProvider1.SetError(txtDAVAThreshold, "Must be a number between 0 and 100")
            End If
        End If
    End Sub

    Private Sub txtDropZ_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDropZ.Validated
        ErrorProvider1.SetError(txtDropZ, "")
    End Sub

    Private Sub txtDropZ_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtDropZ.Validating
        If Not IsNumeric(txtDropZ.Text) Then
            e.Cancel = True
            txtDropZ.Select(0, txtDropZ.TextLength)
            ErrorProvider1.SetError(txtDropZ, "Must be a number")
        End If
    End Sub

    Private Sub txtBrVert_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBrVert.Validated
        ErrorProvider1.SetError(txtBrVert, "")
    End Sub

    Private Sub txtBrVert_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBrVert.Validating
        If Not IsNumeric(txtBrVert.Text) Then
            e.Cancel = True
            txtBrVert.Select(0, txtBrVert.TextLength)
            ErrorProvider1.SetError(txtBrVert, "Must be a positive integer greater than 1")
        Else
            Try
                Dim result = Integer.Parse(txtBrVert.Text)
            Catch ex As Exception
                e.Cancel = True
                txtBrVert.Select(0, txtBrVert.TextLength)
                ErrorProvider1.SetError(txtBrVert, "Must be a positive integer greater than 1")
            End Try
            If txtBrVert.Text < 1 Then
                e.Cancel = True
                txtBrVert.Select(0, txtBrVert.TextLength)
                ErrorProvider1.SetError(txtBrVert, "Must be a positive integer greater than 1")
            End If
        End If
    End Sub

End Class