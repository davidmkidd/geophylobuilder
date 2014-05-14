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
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.SystemUI
Imports ESRI.ArcGIS.ArcMapUI
Imports System.Runtime.InteropServices
<ComClass(GeoPhyloBuilder.ClassId, GeoPhyloBuilder.InterfaceId, GeoPhyloBuilder.EventsId)> _
Public Class GeoPhyloBuilder
    Implements ICommand


    <DllImport("gdi32.dll")> _
        Private Shared Function DeleteObject(ByVal hObject As IntPtr) As Boolean
    End Function


    Private m_app As IApplication
    Private m_enabled As Boolean
    Private m_bitmap As System.Drawing.Bitmap
    Private m_hBitmap As IntPtr

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "668646f7-707d-4f77-90ca-491526b1932a"
    Public Const InterfaceId As String = "8b230a17-0a80-49b8-b3cb-3a1b9d876c48"
    Public Const EventsId As String = "72c4adb2-0dc1-4070-a9a4-a61faca77e0f"
#End Region
#Region "Component Category Registration"

    <ComRegisterFunction(), ComVisible(False)> _
    Public Shared Sub RegisterFunction(ByVal sKey As String)
        Dim fullKey As String = sKey.Remove(0, 18) & "\Implemented Categories"
        Dim regKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(fullKey, True)
        If Not (regKey Is Nothing) Then
            regKey.CreateSubKey("{B56A7C42-83D4-11D2-A2E9-080009B6F22B}")
        End If
    End Sub

    <ComUnregisterFunction(), ComVisible(False)> _
    Public Shared Sub UnregisterFunction(ByVal sKey As String)
        Dim fullKey As String = sKey.Remove(0, 18) & "\Implemented Categories"
        Dim regKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(fullKey, True)
        If Not (regKey Is Nothing) Then
            regKey.DeleteSubKey("{B56A7C42-83D4-11D2-A2E9-080009B6F22B}")
        End If
    End Sub

#End Region
    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
        Dim res() As String = GetType(GeoPhyloBuilder).Assembly.GetManifestResourceNames()
        If (res.GetLength(0) > 0) Then
            m_bitmap = New System.Drawing.Bitmap(GetType(GeoPhyloBuilder).Assembly.GetManifestResourceStream(res(0)))
            If Not (m_bitmap Is Nothing) Then
                m_bitmap.MakeTransparent(m_bitmap.GetPixel(0, 0))
                m_hBitmap = m_bitmap.GetHbitmap()
            End If
        End If

    End Sub

    Public ReadOnly Property Bitmap() As Integer Implements ICommand.Bitmap

        Get
            Return m_hBitmap.ToInt32()

        End Get
    End Property

    Public ReadOnly Property Caption() As String Implements ICommand.Caption
        Get
            Return "GeoPhyloBuilder"
        End Get
    End Property

    Public ReadOnly Property Category() As String Implements ICommand.Category
        Get
            Return "GeoPhyloBuilder_1.0"
        End Get
    End Property

    Public ReadOnly Property Checked() As Boolean Implements ICommand.Checked
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property Enabled() As Boolean Implements ICommand.Enabled
        Get
            Return m_enabled
        End Get
    End Property

    Public ReadOnly Property HelpContextID() As Integer Implements ICommand.HelpContextID
        Get
            Return 0
        End Get
    End Property

    Public ReadOnly Property HelpFile() As String Implements ICommand.HelpFile
        Get

        End Get
    End Property

    Public ReadOnly Property Message() As String Implements ICommand.Message
        Get
            Return "GeoPhyloBuilder"
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements ICommand.Name
        Get
            Return "GeoPhyloBuilder"
        End Get
    End Property

    Public Sub OnClick() Implements ICommand.OnClick
        'Dim mxDoc As IMxDocument = CType(m_app.Document, IMxDocument)
        'Dim activeView As IActiveView = mxDoc.ActiveView

        'Dim currExtent As IEnvelope = activeView.Extent
        'currExtent.Expand(0.5D, 0.5D, True)
        'activeView.Extent = currExtent
        'activeView.Refresh()

        Dim dlg As DlgMain = New DlgMain
        dlg.ShowDialog()


    End Sub

    Public Sub OnCreate(ByVal hook As Object) Implements ICommand.OnCreate
        If Not (hook Is Nothing) Then
            If TypeOf (hook) Is IMxApplication Then
                m_app = CType(hook, IApplication)
                m_enabled = True
            End If
        End If

    End Sub

    Public ReadOnly Property Tooltip() As String Implements ICommand.Tooltip
        Get
            Return "GeoPhyloBuilder"

        End Get
    End Property
    Protected Overrides Sub Finalize()
        If (m_hBitmap.ToInt32() <> 0) Then
            DeleteObject(m_hBitmap)
        End If
    End Sub
End Class


