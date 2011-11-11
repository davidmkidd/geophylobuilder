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
<ComClass(XYCalculator.ClassId, XYCalculator.InterfaceId, XYCalculator.EventsId)> _
Public Class XYCalculator
    Implements Visitor

    Public Const NODE_TYPE_ENVELOPE As Integer = 0
    Public Const NODE_TYPE_MEAN As Integer = 1
    Public Const NODE_TYPE_MCP_CENTROID As Integer = 2
    Public Const NODE_TYPE_DAVA_CENTROID As Integer = 3

    Public Const ENCLOSE_METHOD_MIDPOINT As Integer = 0
    Public Const ENCLOSE_METHOD_ENCLOSED As Integer = 1
    Public Const ENCLOSE_METHOD_ENCLOSER As Integer = 2

    Private m_UseNewOrigin As Boolean
    Private m_OriginX As Double
    Private m_OriginY As Double
    Private m_Geocoordinate As Boolean
    Private m_TipObsNodeMethod As Integer
    Private m_IntNodeMethod As Integer
    Private m_Enclose As Integer
    Private m_DAVA_FClass As IFeatureClass
    Private m_MCP_FClass As IFeatureClass

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "5160e9e6-f0b3-4b14-8df2-6d21958ed034"
    Public Const InterfaceId As String = "451026ad-b734-45c1-8dbd-f83dab17f638"
    Public Const EventsId As String = "303c5491-9540-4cdb-86c4-52753ae60175"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
        m_Geocoordinate = False
        m_DAVA_FClass = Nothing
        m_MCP_FClass = Nothing
    End Sub
    Public Property DavaFClass() As IFeatureClass
        Get
            Return m_DAVA_FClass
        End Get
        Set(ByVal value As IFeatureClass)
            m_DAVA_FClass = value
        End Set
    End Property

    Public Property MCPFClass() As IFeatureClass
        Get
            Return m_MCP_FClass
        End Get
        Set(ByVal value As IFeatureClass)
            m_MCP_FClass = value
        End Set
    End Property

    Public Property Geocoodinate() As Boolean
        Get
            Return m_Geocoordinate
        End Get
        Set(ByVal value As Boolean)
            m_Geocoordinate = value
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
    Public Property TipObsNodeMethod() As Integer
        Get
            Return m_TipObsNodeMethod
        End Get
        Set(ByVal value As Integer)
            m_TipObsNodeMethod = value
        End Set
    End Property
    Public Property IntNodeMethod() As Integer
        Get
            Return m_IntNodeMethod
        End Get
        Set(ByVal value As Integer)
            m_IntNodeMethod = value
        End Set
    End Property

    Public Property EncloseMethod() As Integer
        Get
            Return m_Enclose
        End Get
        Set(ByVal value As Integer)
            m_Enclose = value
        End Set
    End Property

    Public Function GetPolyPoint(ByVal node As PhyloTreeNode, ByVal source As String) As IPoint

        ' returns the XY of MCPs and DAVA
        Dim pPoint As IPoint = New Point
        'MCP and DAVA variables
        Dim pQFilter As IQueryFilter
        Dim pFCursor As IFeatureCursor
        Dim pFeature As IFeature
        Dim pArea As IArea
        Dim child As PhyloTreeNode
        Dim pChildF1 As IFeature
        Dim pChildF2 As IFeature
        Dim enclosed As Integer = 0
        Dim pChd1Area As IArea
        Dim pChd2Area As IArea
        Dim pTopoOp As ITopologicalOperator
        Dim newpoly As IPolygon

        pQFilter = New QueryFilter

        If node.Type = PhyloTreeNode.TREE_NODE_TYPE_INNER Or node.Type = PhyloTreeNode.TREE_NODE_TYPE_ROOT Then

            'Now need to know if children overlap
            child = node.Children(0)
            pQFilter.WhereClause = "NodeID = " + CStr(child.Id)
            pFCursor = m_MCP_FClass.Search(pQFilter, Nothing)
            pChildF1 = pFCursor.NextFeature
            pChd1Area = pChildF1.Shape

            child = node.Children(1)
            pQFilter.WhereClause = "NodeID = " + CStr(child.Id)
            pFCursor = m_MCP_FClass.Search(pQFilter, Nothing)
            pChildF2 = pFCursor.NextFeature
            pChd2Area = pChildF2.Shape

            'Does one enclose the other

            pTopoOp = pChildF1.Shape
            pTopoOp.Simplify()
            newpoly = pTopoOp.Difference(pChildF2.Shape)
            If newpoly.IsEmpty Then
                enclosed = 1
            Else
                pTopoOp = pChildF2.Shape
                pTopoOp.Simplify()
                newpoly = pTopoOp.Difference(pChildF2.Shape)
                If newpoly.IsEmpty Then enclosed = 2
            End If

            If enclosed > 0 Then

                Select Case Me.m_Enclose
                    Case XYCalculator.ENCLOSE_METHOD_MIDPOINT
                        pPoint.X = (pChd1Area.Centroid.X + pChd2Area.Centroid.X) / 2
                        pPoint.Y = (pChd1Area.Centroid.Y + pChd2Area.Centroid.Y) / 2

                    Case XYCalculator.ENCLOSE_METHOD_ENCLOSER
                        If enclosed = 1 Then
                            pPoint.X = pChd2Area.Centroid.X
                            pPoint.Y = pChd2Area.Centroid.Y
                        Else
                            pPoint.X = pChd1Area.Centroid.X
                            pPoint.Y = pChd1Area.Centroid.Y
                        End If

                    Case XYCalculator.ENCLOSE_METHOD_ENCLOSED
                        If enclosed = 1 Then
                            pPoint.X = pChd1Area.Centroid.X
                            pPoint.Y = pChd1Area.Centroid.Y
                        Else
                            pPoint.X = pChd2Area.Centroid.X
                            pPoint.Y = pChd2Area.Centroid.Y
                        End If
                End Select

                Return pPoint

            End If

        End If

        'Not enclosed or inner or root
        If source = "MCP" Then
            pQFilter.WhereClause = "NodeID = " + CStr(node.Id)
            pFCursor = Me.m_MCP_FClass.Search(pQFilter, False)
            pFeature = pFCursor.NextFeature
            pArea = pFeature.Shape
            pPoint.X = pArea.Centroid.X
            pPoint.Y = pArea.Centroid.Y
        ElseIf source = "DAVA" Then
            pQFilter.WhereClause = "NodeID = " + CStr(node.Id) + " AND (PolyType <= 1 OR PolyType = 3)"
            pFCursor = Me.m_DAVA_FClass.Search(pQFilter, False)
            pFeature = pFCursor.NextFeature
            pArea = pFeature.Shape
            pPoint.X = pArea.Centroid.X
            pPoint.Y = pArea.Centroid.Y
        End If

        Return pPoint

    End Function


    Public Sub visit(ByVal node As PhyloTreeNode) Implements Visitor.visit

        Dim pPoint As IPoint

        If node.Type = PhyloTreeNode.TREE_NODE_TYPE_SAMPLE Then Return

        ' Calculates the location of tree nodes
        'Use Fixed location if set
        If node.FixedLocation Then
            Return
        End If

        'If MCP
        If (Me.TipObsNodeMethod = NODE_TYPE_MCP_CENTROID And node.Type = PhyloTreeNode.TREE_NODE_TYPE_TERMINAL) Or _
            (Me.IntNodeMethod = NODE_TYPE_MCP_CENTROID And (node.Type = PhyloTreeNode.TREE_NODE_TYPE_INNER Or _
            node.Type = PhyloTreeNode.TREE_NODE_TYPE_ROOT)) Then
            pPoint = Me.GetPolyPoint(node, "MCP")
            node.X = pPoint.X
            node.Y = pPoint.Y
            Return
        End If

        'If DAVA
        If (Me.IntNodeMethod = NODE_TYPE_DAVA_CENTROID And (node.Type = PhyloTreeNode.TREE_NODE_TYPE_INNER Or _
            node.Type = PhyloTreeNode.TREE_NODE_TYPE_ROOT)) Then
            pPoint = Me.GetPolyPoint(node, "DAVA")
            node.X = pPoint.X
            node.Y = pPoint.Y
            Return
        End If

        Dim i As Integer
        Dim x As Double
        Dim y As Double
        Dim nc As Integer
        Dim dSum As Double
        Dim xArray() As Double
        Dim xTransArray() As Double
        Dim xTransArray2() As Double
        Dim yArray() As Double
        Dim dArray() As Double
        Dim yArray2() As Double
        Dim dArray2() As Double

        nc = node.Children.Count

        ReDim xArray(nc - 1)
        ReDim yArray(nc - 1)
        ReDim dArray(nc - 1)
        ReDim yArray2(nc - 1)
        ReDim dArray2(nc - 1)
        ReDim xTransArray(nc - 1)
        ReDim xTransArray2(nc - 1)

        'Calculates Location of nodes
        x = 0
        y = 0
        dSum = 0
        Dim child As PhyloTreeNode

        'Debug.Print("NODE: " & node.Name)

        ' Get x,y,z for childern into arrays
        If nc > 0 Then
            For i = 0 To nc - 1
                child = node.Children.Item(i)
                xArray(i) = child.X
                yArray(i) = child.Y
                If node.IsDistanced And m_TipObsNodeMethod = XYCalculator.NODE_TYPE_MEAN Then
                    If child.Type = 3 Then
                        dArray(i) = 1
                    Else
                        dArray(i) = child.Distance
                    End If
                Else
                    dArray(i) = 1
                End If
            Next

            'Debug.Print(Nothing)
            'Debug.Print("LOCATION")
            'For i = 0 To nc - 1
            'Debug.Print("Name = " & node.Name & "  x = " & xArray(i) & _
            '"  y = " & yArray(i) & "  d = " & dArray(i))
            'Next

            If (Not Geocoodinate) Then
                'Planar Coordinate System

                ' Spatial Envelope
                If (node.Type = 2 And m_TipObsNodeMethod = XYCalculator.NODE_TYPE_ENVELOPE) Or _
                    (node.Type < 2 And m_IntNodeMethod = XYCalculator.NODE_TYPE_ENVELOPE) Then
                    Array.Sort(xArray)
                    Array.Sort(yArray)
                    x = xArray(0) + ((xArray(nc - 1) - xArray(0)) / 2)
                    y = yArray(0) + ((yArray(nc - 1) - yArray(0)) / 2)

                Else
                    'Spatial Mean
                    'Calculate sum of distances
                    For i = 0 To nc - 1
                        dSum = dSum + dArray(i)
                    Next

                    'Calculate weighted point
                    For i = 0 To nc - 1
                        x = xArray(i) * (dArray(i) / dSum)
                        y = yArray(i) * (dArray(i) / dSum)
                    Next
                End If

            Else
                'Geographic datum

                Dim maxx As Double = 0
                Dim startx As Double
                Dim starti As Integer
                Dim j As Integer
                Dim tmpx As Double
                Dim tmpy As Double
                Dim tmpd As Double

                xTransArray = xArray

                ' Sort arrays min to max x
                For i = 0 To nc - 2
                    For j = i + 1 To nc - 1
                        If xTransArray(i) > xTransArray(j) Then
                            tmpx = xTransArray(i)
                            tmpy = yArray(i)
                            tmpd = dArray(i)
                            xTransArray(i) = xTransArray(j)
                            yArray(i) = yArray(j)
                            dArray(i) = dArray(j)
                            xTransArray(j) = tmpx
                            yArray(j) = tmpy
                            dArray(j) = tmpd
                        End If
                    Next j
                Next i

                'Debug.Print(Nothing)
                'Debug.Print("SORTED")
                'For i = 0 To nc - 1
                'Debug.Print("Name = " & node.Name & "  x = " & xTransArray(i) & _
                '"  y = " & yArray(i) & "  d = " & dArray(i))
                'Next

                'Find end of largest gap
                starti = 0
                maxx = 0

                For i = 0 To nc - 1
                    If i = nc - 1 Then
                        j = 0
                    Else
                        j = i + 1
                    End If

                    If (xTransArray(j) >= 0 And xTransArray(i) >= 0) Then
                        If j <> 0 Then
                            tmpx = xTransArray(j) - xTransArray(i)
                        Else
                            tmpx = 180 - xTransArray(j) + 180 + xTransArray(i)
                        End If

                    ElseIf (xTransArray(j) < 0 And xTransArray(i) >= 0) Then
                        ' can only occurs when j = 0
                        tmpx = (180 - xTransArray(i)) + (180 + xTransArray(j))

                    ElseIf (xTransArray(j) >= 0 And xTransArray(i) < 0) Then
                        If j <> 0 Then
                            tmpx = xTransArray(j) - xTransArray(i)
                        Else
                            tmpx = (180 - xTransArray(j)) + (180 - Math.Abs(xTransArray(i)))
                        End If

                    Else  '(xTransArray(j) < 0 And xTransArray(i) < 0)
                        If j <> 0 Then
                            tmpx = Math.Abs(xTransArray(i)) - Math.Abs(xTransArray(j))
                        Else
                            tmpx = Math.Abs(xTransArray(i)) + 180 + (180 - Math.Abs(xTransArray(j)))
                        End If

                    End If

                    If tmpx > maxx Then
                        maxx = tmpx
                        starti = j
                    End If
                Next i

                startx = xTransArray(starti)

                'Debug.Print(Nothing)
                'Debug.Print("LARGEST GAP")
                'Debug.Print("max dist: " & maxx & "  starti: " & starti & "  startx: " & startx)


                'Order geographically from startx upwards
                j = starti - 1
                For i = 0 To nc - 1
                    j = j + 1
                    If j > nc - 1 Then j = 0
                    xTransArray2(i) = xTransArray(j)
                    yArray2(i) = yArray(j)
                    dArray2(i) = dArray(j)
                Next
                yArray = yArray2
                dArray = dArray2


                'Debug.Print(Nothing)
                'Debug.Print("GEOGRAPHICAL ORDER")
                'For i = 0 To nc - 1
                'Debug.Print("Name = " & node.Name & "  x = " & xTransArray2(i) & _
                '"  y = " & yArray(i) & "  d = " & dArray(i))
                'Next


                'Calculate difference from startx
                xTransArray(0) = 0
                For i = 0 To nc - 2
                    If (xTransArray2(i + 1) >= 0 And xTransArray2(i) >= 0) Then
                        xTransArray(i + 1) = xTransArray(i) + xTransArray2(i + 1) - xTransArray2(i)
                    ElseIf (xTransArray2(i + 1) < 0 And xTransArray2(i) >= 0) Then
                        xTransArray(i + 1) = xTransArray(i) + (180 - xTransArray2(i)) + (180 + xTransArray2(i + 1))
                    ElseIf (xTransArray2(i + 1) >= 0 And xTransArray2(i) < 0) Then
                        xTransArray(i + 1) = xTransArray(i) + xTransArray2(i + 1) - xTransArray2(i)
                    Else
                        '(xTransArray2(j) < 0 And xTransArray2(i) < 0)
                        xTransArray(i + 1) = xTransArray(i) + -(xTransArray2(i)) + xTransArray2(i + 1)
                    End If
                Next


                'Debug.Print(Nothing)
                'Debug.Print("DISTANCE FROM startx")
                'For i = 0 To nc - 1
                'Debug.Print("Name = " & node.Name & "  x = " & xTransArray(i) & _
                '"  y = " & yArray(i) & "  d = " & dArray(i))
                'Next

                If (node.Type = 2 And m_TipObsNodeMethod = XYCalculator.NODE_TYPE_ENVELOPE) Or _
    (node.Type < 2 And m_IntNodeMethod = XYCalculator.NODE_TYPE_ENVELOPE) Then

                    'Array.Sort(xTransArray)
                    Array.Sort(yArray)
                    x = xArray(nc - 1) / 2
                    y = yArray(0) + ((yArray(nc - 1) - yArray(0)) / 2)
                Else
                    'Calculate sum of node distances between nodes
                    dSum = 0
                    For i = 0 To nc - 1
                        dSum = dSum + dArray(i)
                    Next

                    ' Average weighted distance from starti
                    tmpx = xTransArray(0) * dArray(0)
                    For i = 1 To nc - 1
                        tmpx = tmpx + (xTransArray(i) * dArray(i))
                    Next

                    x = tmpx / dSum

                    'And  Y
                    y = yArray(0) * dArray(0)
                    For i = 1 To nc - 1
                        y = y + yArray(i) * dArray(i)
                    Next
                    y = y / dSum
                End If

                'Debug.Print(Nothing)
                'Debug.Print("DIFF FROM STARTX: " & x)

                'Retransform x
                x = x + startx

                If x > 180 Then
                    x = -360 + x
                End If

                xArray = Nothing
                yArray = Nothing
                dArray = Nothing
                xTransArray = Nothing
                xTransArray2 = Nothing
            End If

            node.X = x
            node.Y = y

            'Debug.Print(Nothing)
            'Debug.Print("Node x: " & x & "  Node y: " & y)
            'Debug.Print(Nothing)
            'Debug.Print(Nothing)

        Else

            If node.Type <> PhyloTreeNode.TREE_NODE_TYPE_SAMPLE Then
                Throw New System.Exception("No samples found for " + node.Name)
            End If

        End If

    End Sub


End Class


