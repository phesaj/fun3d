﻿Imports Microsoft.DirectX.Direct3D
Public Class ucTransform
    Public Property transformObject As IFun3DObject
    Public WithEvents b As Binding
    Private Sub ucTransform_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' CLEAR BINDINGS
        Me.TBarRX.DataBindings.Clear()
        Me.TBarRY.DataBindings.Clear()
        Me.TBarRZ.DataBindings.Clear()
        Me.TBRX.DataBindings.Clear()
        Me.TBRY.DataBindings.Clear()
        Me.TBRZ.DataBindings.Clear()
        Me.TBTX.DataBindings.Clear()
        Me.TBTY.DataBindings.Clear()
        Me.TBTZ.DataBindings.Clear()
        Me.TBSX.DataBindings.Clear()
        Me.TBSY.DataBindings.Clear()
        Me.TBSZ.DataBindings.Clear()
        ' ROTATE BINDINGS
        b = New Binding("Value", Me.transformObject.transform, "rx")
        b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
        Me.TBarRX.DataBindings.Add(b)
        b = New Binding("Value", Me.transformObject.transform, "ry")
        b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
        Me.TBarRY.DataBindings.Add(b)
        b = New Binding("Value", Me.transformObject.transform, "rz")
        b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
        Me.TBarRZ.DataBindings.Add(b)
        b = New Binding("Text", Me.TBarRX, "Value")
        b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
        Me.TBRX.DataBindings.Add(b)
        b = New Binding("Text", Me.TBarRY, "Value")
        b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
        Me.TBRY.DataBindings.Add(b)
        b = New Binding("Text", Me.TBarRZ, "Value")
        b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
        Me.TBRZ.DataBindings.Add(b)
        ' TRANSLATE BINDINGS
        b = New Binding("Text", Me.transformObject.transform, "tx")
        b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
        Me.TBTX.DataBindings.Add(b)
        b = New Binding("Text", Me.transformObject.transform, "ty")
        b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
        Me.TBTY.DataBindings.Add(b)
        b = New Binding("Text", Me.transformObject.transform, "tz")
        b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
        Me.TBTZ.DataBindings.Add(b)
        ' SCALE BINDINGS
        b = New Binding("Text", Me.transformObject.transform, "sx")
        b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
        Me.TBSX.DataBindings.Add(b)
        b = New Binding("Text", Me.transformObject.transform, "sy")
        b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
        Me.TBSY.DataBindings.Add(b)
        b = New Binding("Text", Me.transformObject.transform, "sz")
        b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
        Me.TBSZ.DataBindings.Add(b)

    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b.Format
        Try
            Me.transformObject.tgeom = Me.transformObject.transform.getTransformedGeometry(Me.transformObject.geom)
        Catch ex As Exception
            ' Console.WriteLine("Object has no geometry, using refreshBuffer function")
            ' TODO Convert mesh to geometry for classes that inherits ClassFun3DObject
            Try
                Dim o As Object = Me.transformObject
                Dim p() As Object = {Nothing}
                Dim mi As System.Reflection.MethodInfo = mf.Scena.SelectedObject.GetType().GetMethod("refreshBuffer")
                If mi.GetParameters().Length > 0 Then
                    mi.Invoke(o, p)
                Else
                    mi.Invoke(o, Nothing)
                End If
            Catch exx As Exception
                Console.WriteLine(exx.Message)
            End Try
        End Try
        cf3D.Invalidate()
    End Sub
End Class
