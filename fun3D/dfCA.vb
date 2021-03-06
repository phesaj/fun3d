Imports System.Windows.Forms

Public Class dfCA
    Public CA As New ClassCA(cf3D.device, 6, 6, 3)
    Dim saveCA As New ClassCA(cf3D.device)
    Dim paintC As Boolean = True
    Dim rlist As New List(Of Rectangle)
    Dim matrixRefresh As Boolean = True
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        With Me.CA
            .nOfLevels = Me.saveCA.nOfLevels
            .matrices = Me.saveCA.matrices
            .maxBC = Me.saveCA.maxBC
            .minBC = Me.saveCA.minBC
            .toLive = Me.saveCA.toLive
            .xFields = Me.saveCA.xFields
            .yFields = Me.saveCA.yFields
        End With
        Me.CA.createLevels()
        Me.CA.refreshBuffer(cf3D.device)
        CA.refreshBuffer(cf3D.device)
        cf3D.Refresh()
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dfCA_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.matrixRefresh = False
        NUDC.Value = CA.yFields
        NUDR.Value = CA.xFields
        NUDL.Value = CA.nOfLevels
        Me.NUDMax.Value = CA.maxBC
        Me.NUDMin.Value = CA.minBC
        Me.NUDLive.Value = CA.toLive


        With Me.saveCA
            .xFields = Me.CA.xFields
            .yFields = Me.CA.yFields
            .nOfLevels = Me.CA.nOfLevels
            .matrices = Me.CA.matrices
            .maxBC = Me.CA.maxBC
            .minBC = Me.CA.minBC
            .toLive = Me.CA.toLive
        End With
        Me.matrixRefresh = True
    End Sub

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim r As Rectangle
            For Each r In rlist
                If r.Contains(e.Location) Then
                    Me.paintC = CBool(Not CA.matrices(0)(rlist.IndexOf(r)) And 1)
                    CA.matrices(0)(rlist.IndexOf(r)) = CByte(Not CA.matrices(0)(rlist.IndexOf(r)) And 1)
                End If
            Next
        End If
    End Sub

    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim r As Rectangle
            For Each r In rlist
                If r.Contains(e.Location) Then
                    If paintC Then
                        CA.matrices(0)(rlist.IndexOf(r)) = 1
                    Else
                        CA.matrices(0)(rlist.IndexOf(r)) = 0
                    End If
                End If
            Next
            Me.PictureBox1.Refresh()
        End If
    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        CA.createLevels()
        CA.refreshBuffer(cf3D.device)
        cf3D.Refresh()
        Me.PictureBox1.Refresh()
    End Sub


    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
        rlist.Clear()
        Dim h, w As Single
        h = CSng((e.ClipRectangle.Height - 4) / CA.yFields)
        w = CSng((e.ClipRectangle.Width - 4) / CA.xFields)
        Dim t, u As Integer
        u = CA.matrices(0).Count
        For t = 0 To u - 1
            rlist.Add(New Rectangle(CInt((t - Int(t / CA.xFields) * CA.xFields) * w + 2), CInt(Int(t / CA.xFields) * h + 2), CInt(w - 2), CInt(h - 2)))
            If CA.matrices(0)(t) > 0 Then
                e.Graphics.FillRectangle(Brushes.Black, rlist(t))
            Else
                e.Graphics.DrawRectangle(Pens.Black, rlist(t))
            End If
        Next
    End Sub

    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NUDR.ValueChanged, NUDC.ValueChanged, NUDL.ValueChanged
        If sender Is NUDC And Me.matrixRefresh Then
            CA.rows = CInt(NUDC.Value)
            CA.nOfLevels = CInt(NUDL.Value)
        ElseIf sender Is NUDR And Me.matrixRefresh Then
            CA.columns = CInt(NUDR.Value)
            CA.nOfLevels = CInt(NUDL.Value)
        ElseIf sender Is NUDL And Me.matrixRefresh Then
            CA.nOfLevels = CInt(NUDL.Value)
            CA.createLevels()
            CA.refreshBuffer(cf3D.device)
            cf3D.Refresh()
        Else
            CA.createLevels()
            CA.refreshBuffer(cf3D.device)
            cf3D.Refresh()
        End If
        If tfOsobine.Visible Then
            tfOsobine.PropertyGridUV.SelectedObject = mf.Scena.SelectedObject
            tfOsobine.PropertyGridUV.Invalidate()
        End If
        cf3D.Refresh()
        Me.PictureBox1.Refresh()
    End Sub

    Private Sub NumericUpDown3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NUDLive.ValueChanged, NUDMin.ValueChanged, NUDMax.ValueChanged
        If Me.matrixRefresh Then
            CA.toLive = CByte(Me.NUDLive.Value)
            CA.minBC = CByte(Me.NUDMin.Value)
            CA.maxBC = CByte(Me.NUDMax.Value)
            CA.createLevels()
            CA.refreshBuffer(cf3D.device)
            cf3D.Refresh()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CA.generateEmptyMatrix()
        CA.createLevels()
        CA.refreshBuffer(cf3D.device)
        cf3D.Refresh()
        Me.PictureBox1.Refresh()
    End Sub

    Private Sub dfCA_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.PictureBox1.Refresh()
    End Sub

    Private Sub dfCA_ResizeBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin
        Me.Opacity = 0.8
    End Sub

    Private Sub dfCA_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        Me.Opacity = 1
        mf.xdfca = Me.Left
        mf.ydfca = Me.Top
    End Sub

    Private Sub BISO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BISO.Click
        CA.createISOMesh(cf3D.device)
        CA.drawISO = Not CA.drawISO
        cf3D.Refresh()
    End Sub
End Class
