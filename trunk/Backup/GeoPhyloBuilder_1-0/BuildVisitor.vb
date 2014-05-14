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
Imports system.io

Imports System.Runtime.InteropServices

<ComClass(BuildVisitor.ClassId, BuildVisitor.InterfaceId, BuildVisitor.EventsId)> _
Public Class BuildVisitor
    Implements Visitor, Observed

    Private m_observer As Observer

    Private m_pFeatureClass As IFeatureClass
    Private m_UseNewOrigin As Boolean
    Private m_OriginX As Double
    Private m_OriginY As Double
    Private m_MultipleZ As Double
    Private m_MaxDepth As Double
    Private m_UseDistance As Boolean
    Private m_DropToZ As Double
    Private m_File As StreamWriter




#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "8aea045b-941e-4504-8cfd-8cacc9afe05d"
    Public Const InterfaceId As String = "69b8a3d2-db0a-4a8d-a34d-55927b816957"
    Public Const EventsId As String = "cff94024-298d-4b9b-a1fc-57fe75fc0752"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
        m_UseNewOrigin = False
        m_UseDistance = False
    End Sub


    Protected Overrides Sub finalize()
        m_pFeatureClass = Nothing
    End Sub

    Public Property Observer() As Observer
        Get
            Return (m_observer)
        End Get
        Set(ByVal value As Observer)
            m_observer = value
        End Set
    End Property

    Public Property Writer() As StreamWriter
        Get
            Return m_File
        End Get
        Set(ByVal value As StreamWriter)
            m_File = value
        End Set
    End Property
    Public Property DropToZ() As Double
        Get
            Return m_DropToZ
        End Get
        Set(ByVal value As Double)
            m_DropToZ = value
        End Set
    End Property
    Public Property UseNewOrigin() As Boolean
        Get
            Return m_UseNewOrigin
        End Get
        Set(ByVal value As Boolean)
            m_UseNewOrigin = value
        End Set
    End Property
    Public Property UseDistance() As Boolean
        Get
            Return m_UseDistance
        End Get
        Set(ByVal value As Boolean)
            m_UseDistance = value
        End Set
    End Property
    Public Property OriginX() As Double
        Get
            Return m_OriginX
        End Get
        Set(ByVal value As Double)
            m_OriginX = value
        End Set
    End Property

    Public Property OriginY() As Double
        Get
            Return m_OriginY
        End Get
        Set(ByVal value As Double)
            m_OriginY = value
        End Set
    End Property
    Public Property MultipleZ() As Double
        Get
            Return m_MultipleZ
        End Get
        Set(ByVal value As Double)
            m_MultipleZ = value
        End Set
    End Property


    Public Property FeatureClass() As IFeatureClass
        Get
            Return m_pFeatureClass
        End Get
        Set(ByVal value As IFeatureClass)
            m_pFeatureClass = value
        End Set
    End Property
    Public Property MaxDepth() As Double
        Get
            Return m_MaxDepth
        End Get
        Set(ByVal value As Double)
            m_MaxDepth = value
        End Set
    End Property

    Public Overridable Sub visit(ByVal node As PhyloTreeNode) Implements Visitor.visit

    End Sub

    Public Sub sendMessage(ByVal message As String) Implements Observed.sendMessage
        If Not Observer Is Nothing Then
            Observer.updateMessage(message)

        End If
    End Sub
End Class


