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

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DlgMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DlgMain))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.OK_Button = New System.Windows.Forms.Button
        Me.butOpen = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbTreeName = New System.Windows.Forms.ComboBox
        Me.txtModelFile = New System.Windows.Forms.TextBox
        Me.tvwTree = New System.Windows.Forms.TreeView
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblDistance = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.butSetMidpoint = New System.Windows.Forms.Button
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtFixedY = New System.Windows.Forms.TextBox
        Me.txtFixedX = New System.Windows.Forms.TextBox
        Me.chkFixLocation = New System.Windows.Forms.CheckBox
        Me.butSetRoot = New System.Windows.Forms.Button
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.chkLinear = New System.Windows.Forms.CheckBox
        Me.txtTipFanDepth = New System.Windows.Forms.TextBox
        Me.lblTipFanDepth = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.Label36 = New System.Windows.Forms.Label
        Me.Label35 = New System.Windows.Forms.Label
        Me.cboIntPos = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cboObsTipPos = New System.Windows.Forms.ComboBox
        Me.chkTipFan = New System.Windows.Forms.CheckBox
        Me.txtXBrDepth = New System.Windows.Forms.TextBox
        Me.lblXBrDepth = New System.Windows.Forms.Label
        Me.lblBrShape = New System.Windows.Forms.Label
        Me.cboBrShape = New System.Windows.Forms.ComboBox
        Me.LblBrVert = New System.Windows.Forms.Label
        Me.cboBrVert = New System.Windows.Forms.ComboBox
        Me.txtBrVert = New System.Windows.Forms.TextBox
        Me.lblZ = New System.Windows.Forms.Label
        Me.cmbZ = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtZMulti = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.cboInJoinField = New System.Windows.Forms.ComboBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.cboNodeSpatialJoinFld = New System.Windows.Forms.ComboBox
        Me.cmdGetSpatial = New System.Windows.Forms.Button
        Me.txtInWS = New System.Windows.Forms.TextBox
        Me.cmdGetNodeTable = New System.Windows.Forms.Button
        Me.txtNodeTableName = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtInFclass = New System.Windows.Forms.TextBox
        Me.cboNodeTreeJoinFld = New System.Windows.Forms.ComboBox
        Me.chkNodeLink = New System.Windows.Forms.CheckBox
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.lblEnclose = New System.Windows.Forms.Label
        Me.cboEnclose = New System.Windows.Forms.ComboBox
        Me.lblDAVAThreshold = New System.Windows.Forms.Label
        Me.txtDAVAThreshold = New System.Windows.Forms.TextBox
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.lblPandLBuffer = New System.Windows.Forms.Label
        Me.txtPandLBuffer = New System.Windows.Forms.TextBox
        Me.lblCladeBuffDist = New System.Windows.Forms.Label
        Me.txtDropZ = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.chkDropLine = New System.Windows.Forms.CheckBox
        Me.txtCladeBuffDist = New System.Windows.Forms.TextBox
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.cmbImportType = New System.Windows.Forms.ComboBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.lstReport = New System.Windows.Forms.ListBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.chkNetwork = New System.Windows.Forms.CheckBox
        Me.lblDatasetName = New System.Windows.Forms.Label
        Me.prgBar = New System.Windows.Forms.ProgressBar
        Me.txtDataName = New System.Windows.Forms.TextBox
        Me.txtGDBOut = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.cmdGDBOut = New System.Windows.Forms.Button
        Me.dlgOpenFile = New System.Windows.Forms.OpenFileDialog
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog
        Me.Label9 = New System.Windows.Forms.Label
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.CheckBox2 = New System.Windows.Forms.CheckBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.ComboBox2 = New System.Windows.Forms.ComboBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.ComboBox3 = New System.Windows.Forms.ComboBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.ComboBox4 = New System.Windows.Forms.ComboBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.ComboBox5 = New System.Windows.Forms.ComboBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.ComboBox6 = New System.Windows.Forms.ComboBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.TextBox4 = New System.Windows.Forms.TextBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.TextBox5 = New System.Windows.Forms.TextBox
        Me.Label31 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.TextBox6 = New System.Windows.Forms.TextBox
        Me.ComboBox7 = New System.Windows.Forms.ComboBox
        Me.CheckBox3 = New System.Windows.Forms.CheckBox
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(506, 478)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Close"
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'butOpen
        '
        Me.butOpen.Location = New System.Drawing.Point(503, 36)
        Me.butOpen.Name = "butOpen"
        Me.butOpen.Size = New System.Drawing.Size(76, 22)
        Me.butOpen.TabIndex = 1
        Me.butOpen.Text = "Open"
        Me.butOpen.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Tree Name"
        '
        'cmbTreeName
        '
        Me.cmbTreeName.FormattingEnabled = True
        Me.cmbTreeName.Location = New System.Drawing.Point(99, 65)
        Me.cmbTreeName.Name = "cmbTreeName"
        Me.cmbTreeName.Size = New System.Drawing.Size(384, 21)
        Me.cmbTreeName.TabIndex = 3
        '
        'txtModelFile
        '
        Me.txtModelFile.Location = New System.Drawing.Point(99, 38)
        Me.txtModelFile.Name = "txtModelFile"
        Me.txtModelFile.ReadOnly = True
        Me.txtModelFile.Size = New System.Drawing.Size(384, 20)
        Me.txtModelFile.TabIndex = 4
        '
        'tvwTree
        '
        Me.tvwTree.Location = New System.Drawing.Point(99, 106)
        Me.tvwTree.Name = "tvwTree"
        Me.tvwTree.Size = New System.Drawing.Size(384, 287)
        Me.tvwTree.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, -11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Label2"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(70, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(23, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "File"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(500, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Branch Distance"
        '
        'lblDistance
        '
        Me.lblDistance.AutoSize = True
        Me.lblDistance.Location = New System.Drawing.Point(500, 139)
        Me.lblDistance.Name = "lblDistance"
        Me.lblDistance.Size = New System.Drawing.Size(13, 13)
        Me.lblDistance.TabIndex = 9
        Me.lblDistance.Text = "0"
        Me.lblDistance.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(64, 106)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Tree"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(636, 445)
        Me.TabControl1.TabIndex = 11
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.butSetMidpoint)
        Me.TabPage1.Controls.Add(Me.Label24)
        Me.TabPage1.Controls.Add(Me.Label21)
        Me.TabPage1.Controls.Add(Me.txtFixedY)
        Me.TabPage1.Controls.Add(Me.txtFixedX)
        Me.TabPage1.Controls.Add(Me.chkFixLocation)
        Me.TabPage1.Controls.Add(Me.butSetRoot)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.cmbTreeName)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.butOpen)
        Me.TabPage1.Controls.Add(Me.txtModelFile)
        Me.TabPage1.Controls.Add(Me.lblDistance)
        Me.TabPage1.Controls.Add(Me.tvwTree)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(628, 419)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Tree Model"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'butSetMidpoint
        '
        Me.butSetMidpoint.Location = New System.Drawing.Point(503, 338)
        Me.butSetMidpoint.Name = "butSetMidpoint"
        Me.butSetMidpoint.Size = New System.Drawing.Size(100, 20)
        Me.butSetMidpoint.TabIndex = 17
        Me.butSetMidpoint.Text = "Midpoint Root"
        Me.butSetMidpoint.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(500, 272)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(14, 13)
        Me.Label24.TabIndex = 16
        Me.Label24.Text = "Y"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(500, 246)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(14, 13)
        Me.Label21.TabIndex = 15
        Me.Label21.Text = "X"
        '
        'txtFixedY
        '
        Me.txtFixedY.Enabled = False
        Me.txtFixedY.Location = New System.Drawing.Point(519, 269)
        Me.txtFixedY.Name = "txtFixedY"
        Me.txtFixedY.Size = New System.Drawing.Size(84, 20)
        Me.txtFixedY.TabIndex = 14
        '
        'txtFixedX
        '
        Me.txtFixedX.Enabled = False
        Me.txtFixedX.Location = New System.Drawing.Point(519, 243)
        Me.txtFixedX.Name = "txtFixedX"
        Me.txtFixedX.Size = New System.Drawing.Size(84, 20)
        Me.txtFixedX.TabIndex = 13
        '
        'chkFixLocation
        '
        Me.chkFixLocation.AutoSize = True
        Me.chkFixLocation.Location = New System.Drawing.Point(503, 220)
        Me.chkFixLocation.Name = "chkFixLocation"
        Me.chkFixLocation.Size = New System.Drawing.Size(83, 17)
        Me.chkFixLocation.TabIndex = 12
        Me.chkFixLocation.Text = "Fix Location"
        Me.chkFixLocation.UseVisualStyleBackColor = True
        '
        'butSetRoot
        '
        Me.butSetRoot.Location = New System.Drawing.Point(503, 318)
        Me.butSetRoot.Name = "butSetRoot"
        Me.butSetRoot.Size = New System.Drawing.Size(100, 20)
        Me.butSetRoot.TabIndex = 11
        Me.butSetRoot.Text = "Set Root"
        Me.butSetRoot.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.chkLinear)
        Me.TabPage2.Controls.Add(Me.txtTipFanDepth)
        Me.TabPage2.Controls.Add(Me.lblTipFanDepth)
        Me.TabPage2.Controls.Add(Me.Label38)
        Me.TabPage2.Controls.Add(Me.Label39)
        Me.TabPage2.Controls.Add(Me.Label37)
        Me.TabPage2.Controls.Add(Me.Label36)
        Me.TabPage2.Controls.Add(Me.Label35)
        Me.TabPage2.Controls.Add(Me.cboIntPos)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.cboObsTipPos)
        Me.TabPage2.Controls.Add(Me.chkTipFan)
        Me.TabPage2.Controls.Add(Me.txtXBrDepth)
        Me.TabPage2.Controls.Add(Me.lblXBrDepth)
        Me.TabPage2.Controls.Add(Me.lblBrShape)
        Me.TabPage2.Controls.Add(Me.cboBrShape)
        Me.TabPage2.Controls.Add(Me.LblBrVert)
        Me.TabPage2.Controls.Add(Me.cboBrVert)
        Me.TabPage2.Controls.Add(Me.txtBrVert)
        Me.TabPage2.Controls.Add(Me.lblZ)
        Me.TabPage2.Controls.Add(Me.cmbZ)
        Me.TabPage2.Controls.Add(Me.Label13)
        Me.TabPage2.Controls.Add(Me.txtZMulti)
        Me.TabPage2.Controls.Add(Me.Label15)
        Me.TabPage2.Controls.Add(Me.cboInJoinField)
        Me.TabPage2.Controls.Add(Me.Label14)
        Me.TabPage2.Controls.Add(Me.cboNodeSpatialJoinFld)
        Me.TabPage2.Controls.Add(Me.cmdGetSpatial)
        Me.TabPage2.Controls.Add(Me.txtInWS)
        Me.TabPage2.Controls.Add(Me.cmdGetNodeTable)
        Me.TabPage2.Controls.Add(Me.txtNodeTableName)
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.txtInFclass)
        Me.TabPage2.Controls.Add(Me.cboNodeTreeJoinFld)
        Me.TabPage2.Controls.Add(Me.chkNodeLink)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(628, 419)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Tree Attributes"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'chkLinear
        '
        Me.chkLinear.AutoSize = True
        Me.chkLinear.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkLinear.Enabled = False
        Me.chkLinear.Location = New System.Drawing.Point(463, 318)
        Me.chkLinear.Name = "chkLinear"
        Me.chkLinear.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkLinear.Size = New System.Drawing.Size(92, 17)
        Me.chkLinear.TabIndex = 53
        Me.chkLinear.Text = "Linear Stretch"
        Me.chkLinear.UseVisualStyleBackColor = True
        '
        'txtTipFanDepth
        '
        Me.txtTipFanDepth.Enabled = False
        Me.txtTipFanDepth.Location = New System.Drawing.Point(546, 157)
        Me.txtTipFanDepth.Name = "txtTipFanDepth"
        Me.txtTipFanDepth.Size = New System.Drawing.Size(51, 20)
        Me.txtTipFanDepth.TabIndex = 52
        Me.txtTipFanDepth.Text = "0"
        '
        'lblTipFanDepth
        '
        Me.lblTipFanDepth.AutoSize = True
        Me.lblTipFanDepth.Enabled = False
        Me.lblTipFanDepth.Location = New System.Drawing.Point(493, 160)
        Me.lblTipFanDepth.Name = "lblTipFanDepth"
        Me.lblTipFanDepth.Size = New System.Drawing.Size(47, 13)
        Me.lblTipFanDepth.TabIndex = 51
        Me.lblTipFanDepth.Text = "Depth %"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(404, 259)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(81, 13)
        Me.Label38.TabIndex = 50
        Me.Label38.Text = "Z Positioning"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(25, 31)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(90, 13)
        Me.Label39.TabIndex = 49
        Me.Label39.Text = "Spatial Source"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(404, 29)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(77, 13)
        Me.Label37.TabIndex = 47
        Me.Label37.Text = "Branch Path"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(24, 278)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(103, 13)
        Me.Label36.TabIndex = 46
        Me.Label36.Text = "Node Positioning"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(68, 339)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(42, 13)
        Me.Label35.TabIndex = 45
        Me.Label35.Text = "Internal"
        '
        'cboIntPos
        '
        Me.cboIntPos.FormattingEnabled = True
        Me.cboIntPos.Location = New System.Drawing.Point(116, 336)
        Me.cboIntPos.Name = "cboIntPos"
        Me.cboIntPos.Size = New System.Drawing.Size(183, 21)
        Me.cboIntPos.TabIndex = 44
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(60, 312)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(22, 13)
        Me.Label5.TabIndex = 43
        Me.Label5.Text = "Tip"
        '
        'cboObsTipPos
        '
        Me.cboObsTipPos.FormattingEnabled = True
        Me.cboObsTipPos.Location = New System.Drawing.Point(116, 309)
        Me.cboObsTipPos.Name = "cboObsTipPos"
        Me.cboObsTipPos.Size = New System.Drawing.Size(184, 21)
        Me.cboObsTipPos.TabIndex = 42
        '
        'chkTipFan
        '
        Me.chkTipFan.AutoSize = True
        Me.chkTipFan.Location = New System.Drawing.Point(425, 159)
        Me.chkTipFan.Name = "chkTipFan"
        Me.chkTipFan.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkTipFan.Size = New System.Drawing.Size(62, 17)
        Me.chkTipFan.TabIndex = 40
        Me.chkTipFan.Text = "Tip Fan"
        Me.chkTipFan.UseVisualStyleBackColor = True
        '
        'txtXBrDepth
        '
        Me.txtXBrDepth.Enabled = False
        Me.txtXBrDepth.Location = New System.Drawing.Point(546, 126)
        Me.txtXBrDepth.Name = "txtXBrDepth"
        Me.txtXBrDepth.Size = New System.Drawing.Size(51, 20)
        Me.txtXBrDepth.TabIndex = 39
        Me.txtXBrDepth.Text = "0"
        '
        'lblXBrDepth
        '
        Me.lblXBrDepth.AutoSize = True
        Me.lblXBrDepth.Enabled = False
        Me.lblXBrDepth.Location = New System.Drawing.Point(427, 129)
        Me.lblXBrDepth.Name = "lblXBrDepth"
        Me.lblXBrDepth.Size = New System.Drawing.Size(113, 13)
        Me.lblXBrDepth.TabIndex = 38
        Me.lblXBrDepth.Text = "Cross Branch Depth %"
        '
        'lblBrShape
        '
        Me.lblBrShape.AutoSize = True
        Me.lblBrShape.Location = New System.Drawing.Point(411, 63)
        Me.lblBrShape.Name = "lblBrShape"
        Me.lblBrShape.Size = New System.Drawing.Size(38, 13)
        Me.lblBrShape.TabIndex = 37
        Me.lblBrShape.Text = "Shape"
        '
        'cboBrShape
        '
        Me.cboBrShape.FormattingEnabled = True
        Me.cboBrShape.Location = New System.Drawing.Point(455, 60)
        Me.cboBrShape.Name = "cboBrShape"
        Me.cboBrShape.Size = New System.Drawing.Size(142, 21)
        Me.cboBrShape.TabIndex = 36
        '
        'LblBrVert
        '
        Me.LblBrVert.AutoSize = True
        Me.LblBrVert.Location = New System.Drawing.Point(396, 96)
        Me.LblBrVert.Name = "LblBrVert"
        Me.LblBrVert.Size = New System.Drawing.Size(54, 13)
        Me.LblBrVert.TabIndex = 35
        Me.LblBrVert.Text = "n Vertices"
        '
        'cboBrVert
        '
        Me.cboBrVert.FormattingEnabled = True
        Me.cboBrVert.Location = New System.Drawing.Point(455, 92)
        Me.cboBrVert.Name = "cboBrVert"
        Me.cboBrVert.Size = New System.Drawing.Size(85, 21)
        Me.cboBrVert.TabIndex = 34
        '
        'txtBrVert
        '
        Me.txtBrVert.Location = New System.Drawing.Point(546, 93)
        Me.txtBrVert.Name = "txtBrVert"
        Me.txtBrVert.Size = New System.Drawing.Size(51, 20)
        Me.txtBrVert.TabIndex = 32
        Me.txtBrVert.Text = "10"
        '
        'lblZ
        '
        Me.lblZ.AutoSize = True
        Me.lblZ.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.lblZ.Location = New System.Drawing.Point(406, 293)
        Me.lblZ.Name = "lblZ"
        Me.lblZ.Size = New System.Drawing.Size(51, 13)
        Me.lblZ.TabIndex = 31
        Me.lblZ.Text = "Z Source"
        '
        'cmbZ
        '
        Me.cmbZ.FormattingEnabled = True
        Me.cmbZ.Location = New System.Drawing.Point(463, 291)
        Me.cmbZ.Name = "cmbZ"
        Me.cmbZ.Size = New System.Drawing.Size(134, 21)
        Me.cmbZ.TabIndex = 30
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(399, 344)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(58, 13)
        Me.Label13.TabIndex = 29
        Me.Label13.Text = "Z Multiplier"
        '
        'txtZMulti
        '
        Me.txtZMulti.Location = New System.Drawing.Point(463, 341)
        Me.txtZMulti.Name = "txtZMulti"
        Me.txtZMulti.Size = New System.Drawing.Size(63, 20)
        Me.txtZMulti.TabIndex = 28
        Me.txtZMulti.Text = "1"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(59, 112)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(51, 13)
        Me.Label15.TabIndex = 19
        Me.Label15.Text = "Join Field"
        '
        'cboInJoinField
        '
        Me.cboInJoinField.FormattingEnabled = True
        Me.cboInJoinField.Location = New System.Drawing.Point(116, 109)
        Me.cboInJoinField.Name = "cboInJoinField"
        Me.cboInJoinField.Size = New System.Drawing.Size(183, 21)
        Me.cboInJoinField.TabIndex = 18
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(24, 223)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(86, 13)
        Me.Label14.TabIndex = 17
        Me.Label14.Text = "Spatial Join Field"
        '
        'cboNodeSpatialJoinFld
        '
        Me.cboNodeSpatialJoinFld.Enabled = False
        Me.cboNodeSpatialJoinFld.FormattingEnabled = True
        Me.cboNodeSpatialJoinFld.Location = New System.Drawing.Point(116, 220)
        Me.cboNodeSpatialJoinFld.Name = "cboNodeSpatialJoinFld"
        Me.cboNodeSpatialJoinFld.Size = New System.Drawing.Size(183, 21)
        Me.cboNodeSpatialJoinFld.TabIndex = 16
        '
        'cmdGetSpatial
        '
        Me.cmdGetSpatial.Location = New System.Drawing.Point(304, 57)
        Me.cmdGetSpatial.Name = "cmdGetSpatial"
        Me.cmdGetSpatial.Size = New System.Drawing.Size(47, 22)
        Me.cmdGetSpatial.TabIndex = 15
        Me.cmdGetSpatial.Text = "Open"
        Me.cmdGetSpatial.UseVisualStyleBackColor = True
        '
        'txtInWS
        '
        Me.txtInWS.Location = New System.Drawing.Point(116, 57)
        Me.txtInWS.Name = "txtInWS"
        Me.txtInWS.ReadOnly = True
        Me.txtInWS.Size = New System.Drawing.Size(182, 20)
        Me.txtInWS.TabIndex = 14
        '
        'cmdGetNodeTable
        '
        Me.cmdGetNodeTable.Enabled = False
        Me.cmdGetNodeTable.Location = New System.Drawing.Point(305, 169)
        Me.cmdGetNodeTable.Name = "cmdGetNodeTable"
        Me.cmdGetNodeTable.Size = New System.Drawing.Size(47, 22)
        Me.cmdGetNodeTable.TabIndex = 13
        Me.cmdGetNodeTable.Text = "Open"
        Me.cmdGetNodeTable.UseVisualStyleBackColor = True
        '
        'txtNodeTableName
        '
        Me.txtNodeTableName.Location = New System.Drawing.Point(116, 169)
        Me.txtNodeTableName.Name = "txtNodeTableName"
        Me.txtNodeTableName.ReadOnly = True
        Me.txtNodeTableName.Size = New System.Drawing.Size(183, 20)
        Me.txtNodeTableName.TabIndex = 12
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(27, 197)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(83, 13)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Model Join Field"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(40, 86)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(71, 13)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "Feature Class"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(44, 60)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(67, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Data Source"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(53, 172)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 13)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Link Table"
        '
        'txtInFclass
        '
        Me.txtInFclass.Location = New System.Drawing.Point(116, 83)
        Me.txtInFclass.Name = "txtInFclass"
        Me.txtInFclass.ReadOnly = True
        Me.txtInFclass.Size = New System.Drawing.Size(183, 20)
        Me.txtInFclass.TabIndex = 3
        '
        'cboNodeTreeJoinFld
        '
        Me.cboNodeTreeJoinFld.Enabled = False
        Me.cboNodeTreeJoinFld.FormattingEnabled = True
        Me.cboNodeTreeJoinFld.Location = New System.Drawing.Point(116, 194)
        Me.cboNodeTreeJoinFld.Name = "cboNodeTreeJoinFld"
        Me.cboNodeTreeJoinFld.Size = New System.Drawing.Size(183, 21)
        Me.cboNodeTreeJoinFld.TabIndex = 2
        '
        'chkNodeLink
        '
        Me.chkNodeLink.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkNodeLink.AutoSize = True
        Me.chkNodeLink.Location = New System.Drawing.Point(116, 141)
        Me.chkNodeLink.Name = "chkNodeLink"
        Me.chkNodeLink.Size = New System.Drawing.Size(140, 17)
        Me.chkNodeLink.TabIndex = 0
        Me.chkNodeLink.Text = "Node-Spatial Link Table"
        Me.chkNodeLink.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.lblEnclose)
        Me.TabPage3.Controls.Add(Me.cboEnclose)
        Me.TabPage3.Controls.Add(Me.lblDAVAThreshold)
        Me.TabPage3.Controls.Add(Me.txtDAVAThreshold)
        Me.TabPage3.Controls.Add(Me.Label40)
        Me.TabPage3.Controls.Add(Me.Label20)
        Me.TabPage3.Controls.Add(Me.Label16)
        Me.TabPage3.Controls.Add(Me.lblPandLBuffer)
        Me.TabPage3.Controls.Add(Me.txtPandLBuffer)
        Me.TabPage3.Controls.Add(Me.lblCladeBuffDist)
        Me.TabPage3.Controls.Add(Me.txtDropZ)
        Me.TabPage3.Controls.Add(Me.Label17)
        Me.TabPage3.Controls.Add(Me.chkDropLine)
        Me.TabPage3.Controls.Add(Me.txtCladeBuffDist)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(628, 419)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Options"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'lblEnclose
        '
        Me.lblEnclose.AutoSize = True
        Me.lblEnclose.Location = New System.Drawing.Point(118, 139)
        Me.lblEnclose.Name = "lblEnclose"
        Me.lblEnclose.Size = New System.Drawing.Size(54, 13)
        Me.lblEnclose.TabIndex = 27
        Me.lblEnclose.Text = "Enclosure"
        '
        'cboEnclose
        '
        Me.cboEnclose.FormattingEnabled = True
        Me.cboEnclose.Location = New System.Drawing.Point(187, 136)
        Me.cboEnclose.Name = "cboEnclose"
        Me.cboEnclose.Size = New System.Drawing.Size(117, 21)
        Me.cboEnclose.TabIndex = 26
        '
        'lblDAVAThreshold
        '
        Me.lblDAVAThreshold.AutoSize = True
        Me.lblDAVAThreshold.Enabled = False
        Me.lblDAVAThreshold.Location = New System.Drawing.Point(72, 221)
        Me.lblDAVAThreshold.Name = "lblDAVAThreshold"
        Me.lblDAVAThreshold.Size = New System.Drawing.Size(100, 13)
        Me.lblDAVAThreshold.TabIndex = 25
        Me.lblDAVAThreshold.Text = "Range % Threshold"
        '
        'txtDAVAThreshold
        '
        Me.txtDAVAThreshold.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDAVAThreshold.Enabled = False
        Me.txtDAVAThreshold.Location = New System.Drawing.Point(187, 218)
        Me.txtDAVAThreshold.Name = "txtDAVAThreshold"
        Me.txtDAVAThreshold.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtDAVAThreshold.Size = New System.Drawing.Size(52, 20)
        Me.txtDAVAThreshold.TabIndex = 24
        Me.txtDAVAThreshold.Text = "50"
        Me.txtDAVAThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label40.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(23, 124)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(80, 13)
        Me.Label40.TabIndex = 23
        Me.Label40.Text = "Area Objects"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(23, 27)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(98, 13)
        Me.Label20.TabIndex = 22
        Me.Label20.Text = "Graphic Objects"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(23, 263)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(401, 13)
        Me.Label16.TabIndex = 20
        Me.Label16.Text = "Area Objects are only supported for projected coordinate systems and point featur" & _
            "es"
        '
        'lblPandLBuffer
        '
        Me.lblPandLBuffer.AutoSize = True
        Me.lblPandLBuffer.Enabled = False
        Me.lblPandLBuffer.Location = New System.Drawing.Point(66, 192)
        Me.lblPandLBuffer.Name = "lblPandLBuffer"
        Me.lblPandLBuffer.Size = New System.Drawing.Size(106, 13)
        Me.lblPandLBuffer.TabIndex = 19
        Me.lblPandLBuffer.Text = "Point and Line Buffer"
        '
        'txtPandLBuffer
        '
        Me.txtPandLBuffer.Enabled = False
        Me.txtPandLBuffer.Location = New System.Drawing.Point(187, 189)
        Me.txtPandLBuffer.Name = "txtPandLBuffer"
        Me.txtPandLBuffer.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtPandLBuffer.Size = New System.Drawing.Size(117, 20)
        Me.txtPandLBuffer.TabIndex = 18
        Me.txtPandLBuffer.Text = "0"
        Me.txtPandLBuffer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblCladeBuffDist
        '
        Me.lblCladeBuffDist.AutoSize = True
        Me.lblCladeBuffDist.Enabled = False
        Me.lblCladeBuffDist.Location = New System.Drawing.Point(96, 166)
        Me.lblCladeBuffDist.Name = "lblCladeBuffDist"
        Me.lblCladeBuffDist.Size = New System.Drawing.Size(76, 13)
        Me.lblCladeBuffDist.TabIndex = 15
        Me.lblCladeBuffDist.Text = "Polygon Buffer"
        '
        'txtDropZ
        '
        Me.txtDropZ.Location = New System.Drawing.Point(355, 61)
        Me.txtDropZ.Name = "txtDropZ"
        Me.txtDropZ.Size = New System.Drawing.Size(52, 20)
        Me.txtDropZ.TabIndex = 11
        Me.txtDropZ.Text = "0"
        '
        'Label17
        '
        Me.Label17.AutoEllipsis = True
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(283, 64)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(52, 13)
        Me.Label17.TabIndex = 9
        Me.Label17.Text = "Drop to Z"
        '
        'chkDropLine
        '
        Me.chkDropLine.AutoSize = True
        Me.chkDropLine.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkDropLine.Checked = True
        Me.chkDropLine.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDropLine.Location = New System.Drawing.Point(124, 64)
        Me.chkDropLine.Name = "chkDropLine"
        Me.chkDropLine.Size = New System.Drawing.Size(77, 17)
        Me.chkDropLine.TabIndex = 6
        Me.chkDropLine.Text = "Drop Lines"
        Me.chkDropLine.UseVisualStyleBackColor = True
        '
        'txtCladeBuffDist
        '
        Me.txtCladeBuffDist.Enabled = False
        Me.txtCladeBuffDist.Location = New System.Drawing.Point(187, 163)
        Me.txtCladeBuffDist.Name = "txtCladeBuffDist"
        Me.txtCladeBuffDist.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtCladeBuffDist.Size = New System.Drawing.Size(52, 20)
        Me.txtCladeBuffDist.TabIndex = 4
        Me.txtCladeBuffDist.Text = "0"
        Me.txtCladeBuffDist.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.cmbImportType)
        Me.TabPage4.Controls.Add(Me.Label23)
        Me.TabPage4.Controls.Add(Me.lstReport)
        Me.TabPage4.Controls.Add(Me.Label22)
        Me.TabPage4.Controls.Add(Me.chkNetwork)
        Me.TabPage4.Controls.Add(Me.lblDatasetName)
        Me.TabPage4.Controls.Add(Me.prgBar)
        Me.TabPage4.Controls.Add(Me.txtDataName)
        Me.TabPage4.Controls.Add(Me.txtGDBOut)
        Me.TabPage4.Controls.Add(Me.Label19)
        Me.TabPage4.Controls.Add(Me.cmdGDBOut)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(628, 419)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Build"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'cmbImportType
        '
        Me.cmbImportType.FormattingEnabled = True
        Me.cmbImportType.Location = New System.Drawing.Point(102, 29)
        Me.cmbImportType.Name = "cmbImportType"
        Me.cmbImportType.Size = New System.Drawing.Size(179, 21)
        Me.cmbImportType.TabIndex = 11
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(33, 32)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(63, 13)
        Me.Label23.TabIndex = 10
        Me.Label23.Text = "Import Type"
        '
        'lstReport
        '
        Me.lstReport.FormattingEnabled = True
        Me.lstReport.Location = New System.Drawing.Point(102, 153)
        Me.lstReport.Name = "lstReport"
        Me.lstReport.Size = New System.Drawing.Size(363, 173)
        Me.lstReport.TabIndex = 9
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(13, 154)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(83, 13)
        Me.Label22.TabIndex = 8
        Me.Label22.Text = "Progress Report"
        '
        'chkNetwork
        '
        Me.chkNetwork.AutoSize = True
        Me.chkNetwork.Location = New System.Drawing.Point(102, 111)
        Me.chkNetwork.Name = "chkNetwork"
        Me.chkNetwork.Size = New System.Drawing.Size(98, 17)
        Me.chkNetwork.TabIndex = 7
        Me.chkNetwork.Text = "Build Network?"
        Me.chkNetwork.UseVisualStyleBackColor = True
        '
        'lblDatasetName
        '
        Me.lblDatasetName.AutoSize = True
        Me.lblDatasetName.Location = New System.Drawing.Point(21, 88)
        Me.lblDatasetName.Name = "lblDatasetName"
        Me.lblDatasetName.Size = New System.Drawing.Size(75, 13)
        Me.lblDatasetName.TabIndex = 6
        Me.lblDatasetName.Text = "Dataset Name"
        '
        'prgBar
        '
        Me.prgBar.Location = New System.Drawing.Point(102, 326)
        Me.prgBar.Name = "prgBar"
        Me.prgBar.Size = New System.Drawing.Size(363, 10)
        Me.prgBar.TabIndex = 4
        Me.prgBar.Visible = False
        '
        'txtDataName
        '
        Me.txtDataName.Location = New System.Drawing.Point(102, 85)
        Me.txtDataName.Name = "txtDataName"
        Me.txtDataName.Size = New System.Drawing.Size(173, 20)
        Me.txtDataName.TabIndex = 3
        '
        'txtGDBOut
        '
        Me.txtGDBOut.Location = New System.Drawing.Point(102, 58)
        Me.txtGDBOut.Name = "txtGDBOut"
        Me.txtGDBOut.ReadOnly = True
        Me.txtGDBOut.Size = New System.Drawing.Size(289, 20)
        Me.txtGDBOut.TabIndex = 2
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(48, 62)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(48, 13)
        Me.Label19.TabIndex = 1
        Me.Label19.Text = "Import to"
        '
        'cmdGDBOut
        '
        Me.cmdGDBOut.Location = New System.Drawing.Point(393, 57)
        Me.cmdGDBOut.Name = "cmdGDBOut"
        Me.cmdGDBOut.Size = New System.Drawing.Size(72, 22)
        Me.cmdGDBOut.TabIndex = 0
        Me.cmdGDBOut.Text = "Open"
        Me.cmdGDBOut.UseVisualStyleBackColor = True
        '
        'SaveFileDialog
        '
        Me.SaveFileDialog.Filter = "KML files|*.kml"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(89, 140)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(50, 13)
        Me.Label9.TabIndex = 43
        Me.Label9.Text = "Obs - Tip"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(145, 137)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(139, 21)
        Me.ComboBox1.TabIndex = 42
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBox1.Location = New System.Drawing.Point(125, 174)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(160, 17)
        Me.CheckBox1.TabIndex = 41
        Me.CheckBox1.Text = "Proportial to Branch Lengths"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(389, 164)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CheckBox2.Size = New System.Drawing.Size(90, 17)
        Me.CheckBox2.TabIndex = 40
        Me.CheckBox2.Text = "Tip - 1 to Obs"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Enabled = False
        Me.TextBox1.Location = New System.Drawing.Point(466, 115)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(51, 20)
        Me.TextBox1.TabIndex = 39
        Me.TextBox1.Text = "0"
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Enabled = False
        Me.Label10.Location = New System.Drawing.Point(347, 118)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(113, 13)
        Me.Label10.TabIndex = 38
        Me.Label10.Text = "Cross Branch Depth %"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(309, 61)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(75, 13)
        Me.Label25.TabIndex = 37
        Me.Label25.Text = "Branch Shape"
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(397, 58)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(120, 21)
        Me.ComboBox2.TabIndex = 36
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(309, 88)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(82, 13)
        Me.Label26.TabIndex = 35
        Me.Label26.Text = "Branch Vertices"
        '
        'ComboBox3
        '
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Location = New System.Drawing.Point(397, 84)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(63, 21)
        Me.ComboBox3.TabIndex = 34
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(466, 85)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(51, 20)
        Me.TextBox2.TabIndex = 32
        Me.TextBox2.Text = "10"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(125, 346)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(14, 13)
        Me.Label27.TabIndex = 31
        Me.Label27.Text = "Z"
        '
        'ComboBox4
        '
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Location = New System.Drawing.Point(146, 343)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(139, 21)
        Me.ComboBox4.TabIndex = 30
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(82, 318)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(58, 13)
        Me.Label28.TabIndex = 29
        Me.Label28.Text = "Z Multiplier"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(145, 315)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(63, 20)
        Me.TextBox3.TabIndex = 28
        Me.TextBox3.Text = "1"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(88, 84)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(51, 13)
        Me.Label29.TabIndex = 19
        Me.Label29.Text = "Join Field"
        '
        'ComboBox5
        '
        Me.ComboBox5.FormattingEnabled = True
        Me.ComboBox5.Location = New System.Drawing.Point(145, 81)
        Me.ComboBox5.Name = "ComboBox5"
        Me.ComboBox5.Size = New System.Drawing.Size(139, 21)
        Me.ComboBox5.TabIndex = 18
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(53, 272)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(86, 13)
        Me.Label30.TabIndex = 17
        Me.Label30.Text = "Spatial Join Field"
        '
        'ComboBox6
        '
        Me.ComboBox6.Enabled = False
        Me.ComboBox6.FormattingEnabled = True
        Me.ComboBox6.Location = New System.Drawing.Point(145, 269)
        Me.ComboBox6.Name = "ComboBox6"
        Me.ComboBox6.Size = New System.Drawing.Size(139, 21)
        Me.ComboBox6.TabIndex = 16
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(432, 29)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(47, 22)
        Me.Button1.TabIndex = 15
        Me.Button1.Text = "Open"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(145, 29)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(286, 20)
        Me.TextBox4.TabIndex = 14
        '
        'Button2
        '
        Me.Button2.Enabled = False
        Me.Button2.Location = New System.Drawing.Point(432, 216)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(47, 22)
        Me.Button2.TabIndex = 13
        Me.Button2.Text = "Open"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBox5
        '
        Me.TextBox5.Enabled = False
        Me.TextBox5.Location = New System.Drawing.Point(145, 218)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(286, 20)
        Me.TextBox5.TabIndex = 12
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(56, 246)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(83, 13)
        Me.Label31.TabIndex = 10
        Me.Label31.Text = "Model Join Field"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(68, 58)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(71, 13)
        Me.Label32.TabIndex = 9
        Me.Label32.Text = "Feature Class"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(72, 32)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(67, 13)
        Me.Label33.TabIndex = 6
        Me.Label33.Text = "Spatial Class"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(82, 221)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(57, 13)
        Me.Label34.TabIndex = 5
        Me.Label34.Text = "Link Table"
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(145, 55)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(139, 20)
        Me.TextBox6.TabIndex = 3
        '
        'ComboBox7
        '
        Me.ComboBox7.Enabled = False
        Me.ComboBox7.FormattingEnabled = True
        Me.ComboBox7.Location = New System.Drawing.Point(145, 243)
        Me.ComboBox7.Name = "ComboBox7"
        Me.ComboBox7.Size = New System.Drawing.Size(139, 21)
        Me.ComboBox7.TabIndex = 2
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(145, 197)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(140, 17)
        Me.CheckBox3.TabIndex = 0
        Me.CheckBox3.Text = "Node-Spatial Link Table"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'DlgMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(664, 519)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DlgMain"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "GeoPhyloBuilder 1.1.1"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents butOpen As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbTreeName As System.Windows.Forms.ComboBox
    Friend WithEvents txtModelFile As System.Windows.Forms.TextBox
    Friend WithEvents tvwTree As System.Windows.Forms.TreeView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblDistance As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents dlgOpenFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents chkDropLine As System.Windows.Forms.CheckBox
    Friend WithEvents txtCladeBuffDist As System.Windows.Forms.TextBox
    Friend WithEvents txtDropZ As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lblCladeBuffDist As System.Windows.Forms.Label
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents txtDataName As System.Windows.Forms.TextBox
    Friend WithEvents txtGDBOut As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cmdGDBOut As System.Windows.Forms.Button
    Friend WithEvents prgBar As System.Windows.Forms.ProgressBar
    Friend WithEvents lblDatasetName As System.Windows.Forms.Label
    Friend WithEvents chkNetwork As System.Windows.Forms.CheckBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents lstReport As System.Windows.Forms.ListBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents cmbImportType As System.Windows.Forms.ComboBox
    Friend WithEvents butSetRoot As System.Windows.Forms.Button
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtFixedY As System.Windows.Forms.TextBox
    Friend WithEvents txtFixedX As System.Windows.Forms.TextBox
    Friend WithEvents chkFixLocation As System.Windows.Forms.CheckBox
    Friend WithEvents SaveFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents butSetMidpoint As System.Windows.Forms.Button
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents chkTipFan As System.Windows.Forms.CheckBox
    Friend WithEvents txtXBrDepth As System.Windows.Forms.TextBox
    Friend WithEvents lblXBrDepth As System.Windows.Forms.Label
    Friend WithEvents lblBrShape As System.Windows.Forms.Label
    Friend WithEvents cboBrShape As System.Windows.Forms.ComboBox
    Friend WithEvents LblBrVert As System.Windows.Forms.Label
    Friend WithEvents cboBrVert As System.Windows.Forms.ComboBox
    Friend WithEvents txtBrVert As System.Windows.Forms.TextBox
    Friend WithEvents lblZ As System.Windows.Forms.Label
    Friend WithEvents cmbZ As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtZMulti As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cboInJoinField As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cboNodeSpatialJoinFld As System.Windows.Forms.ComboBox
    Friend WithEvents cmdGetSpatial As System.Windows.Forms.Button
    Friend WithEvents txtInWS As System.Windows.Forms.TextBox
    Friend WithEvents cmdGetNodeTable As System.Windows.Forms.Button
    Friend WithEvents txtNodeTableName As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtInFclass As System.Windows.Forms.TextBox
    Friend WithEvents cboNodeTreeJoinFld As System.Windows.Forms.ComboBox
    Friend WithEvents chkNodeLink As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboObsTipPos As System.Windows.Forms.ComboBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents cboIntPos As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents ComboBox3 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents ComboBox4 As System.Windows.Forms.ComboBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents ComboBox5 As System.Windows.Forms.ComboBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents ComboBox6 As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox7 As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents txtTipFanDepth As System.Windows.Forms.TextBox
    Friend WithEvents lblTipFanDepth As System.Windows.Forms.Label
    Friend WithEvents chkLinear As System.Windows.Forms.CheckBox
    Friend WithEvents lblPandLBuffer As System.Windows.Forms.Label
    Friend WithEvents txtPandLBuffer As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblDAVAThreshold As System.Windows.Forms.Label
    Friend WithEvents txtDAVAThreshold As System.Windows.Forms.TextBox
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents lblEnclose As System.Windows.Forms.Label
    Friend WithEvents cboEnclose As System.Windows.Forms.ComboBox

End Class
