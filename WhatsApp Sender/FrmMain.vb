Imports System.Threading

Public Class FrmMain
    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        FrmAbout.ShowDialog()
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        TxtMessage.Text = ""
        TxtNumbers.Text = ""
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        FrmWhatsApp.Show()
    End Sub
    Dim SendingTask As Task
    Dim SendingIndex As Integer
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SendingIndex = 0
        Timer1.Enabled = True
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Timer1.Enabled = False
        LblSendingStatus.Text = ""
        ProgressBarSending.Value = 0
    End Sub

    Private Async Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LblSendingStatus.Text = "Sending to " & TxtNumbers.Lines(SendingIndex)
        ProgressBarSending.Maximum = TxtNumbers.Lines.Count
        ProgressBarSending.Value = SendingIndex
        Await FrmWhatsApp.SendMessage(TxtNumbers.Lines(SendingIndex), TxtMessage.Text)

        If SendingIndex < TxtNumbers.Lines.Count - 1 Then
            SendingIndex += 1
        Else
            Timer1.Enabled = False
            LblSendingStatus.Text = ""
            ProgressBarSending.Value = 0
            MsgBox("Done", vbInformation, Application.ProductName)
        End If

    End Sub
End Class
