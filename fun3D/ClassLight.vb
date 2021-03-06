Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports System.ComponentModel
<System.Serializable()> _
Public Class ClassLight
    Dim pType As LightType = LightType.Point
    Dim pDiffuse As Color = System.Drawing.Color.LightGray
    Dim pAmbient As Color = System.Drawing.Color.LightGray
    Dim pSpecular As Color = System.Drawing.Color.White
    Public pPosition As New Vector3(-30, -30, -500)
    Public pDirection As New Vector3(0, 0, 0)
    Dim pEnabled As Boolean = True
    Dim naziv As String = "Light1"
    Dim r As Single = 1000
    Public Property Range() As Single
        Get
            Return r
        End Get
        Set(ByVal value As Single)
            r = value
        End Set
    End Property
    <Category("Meta")> _
    Public Property Name() As String
        Get
            Return Me.naziv
        End Get
        Set(ByVal value As String)
            Me.naziv = value
        End Set
    End Property
    <Category("Position")> _
    Public Property Position() As String
        Get
            Return Me.pPosition.X.ToString + ";" + Me.pPosition.Y.ToString + ";" + Me.pPosition.Z.ToString
        End Get
        Set(ByVal value As String)
            Dim lv() As String = value.Split(CChar(";"))
            Me.pPosition = New Vector3(CSng(Val(lv(0))), CSng(Val(lv(1))), CSng(Val(lv(2))))
        End Set
    End Property
    <Category("Position")> _
    Public Property Direction() As String
        Get
            Return Me.pDirection.X.ToString + ";" + Me.pDirection.Y.ToString + ";" + Me.pDirection.Z.ToString
        End Get
        Set(ByVal value As String)
            Dim lv() As String = value.Split(CChar(";"))
            Me.pDirection = New Vector3(CSng(Val(lv(0))), CSng(Val(lv(1))), CSng(Val(lv(2))))
        End Set
    End Property
    <Category("Appearance")> _
    Public Property Type() As LightType
        Get
            Return Me.pType
        End Get
        Set(ByVal value As LightType)
            Me.pType = value
        End Set
    End Property
    <Category("Appearance")> _
    Public Property Ambient() As Color
        Get
            Return Me.pAmbient
        End Get
        Set(ByVal value As Color)
            Me.pAmbient = value
        End Set
    End Property
    <Category("Appearance")> _
    Public Property Diffuse() As Color
        Get
            Return Me.pDiffuse
        End Get
        Set(ByVal value As Color)
            Me.pDiffuse = value
        End Set
    End Property
    <Category("Appearance")> _
    Public Property Specular() As Color
        Get
            Return Me.pSpecular
        End Get
        Set(ByVal value As Color)
            Me.pSpecular = value
        End Set
    End Property
    <Category("Behavior")> _
    Public Property Enabled() As Boolean
        Get
            Return Me.pEnabled
        End Get
        Set(ByVal value As Boolean)
            Me.pEnabled = value
        End Set
    End Property
End Class
