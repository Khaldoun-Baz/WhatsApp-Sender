Imports System.IO
Imports Microsoft.Web.WebView2.Core

Public Class FrmWhatsApp
    Private Async Sub FrmWhatsApp_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            WebView21.InitializeLifetimeService()
            Dim webView2Environment = Await CoreWebView2Environment.CreateAsync(Nothing, Path.GetTempPath() & Application.ProductName)
            Await WebView21.EnsureCoreWebView2Async(webView2Environment)
            If WebView21.CoreWebView2 Is Nothing Then
                Throw New InvalidOperationException("WebView2 is not initialized.")
            End If
            WebView21.Source = New Uri("https://web.whatsapp.com")
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, Application.ProductName)
        End Try
    End Sub

    Private Async Sub WebView21_NavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs) Handles WebView21.NavigationCompleted
        Do
            Application.DoEvents()
            System.Threading.Thread.Sleep(10)
        Loop Until Val(Await WebView21.ExecuteScriptAsync("document.getElementsByClassName('selectable-text').length")) > 0
        Await WebView21.ExecuteScriptAsync(My.Resources.wppconnect_wa)
    End Sub
    Public Async Function SendMessage(ByVal Number As String, ByVal Message As String) As Task(Of Boolean)
        Number = Number.Replace("+", "")
        Number = Number.Replace(" ", "")
        Try
            Await WebView21.ExecuteScriptAsync("WPP.chat.sendTextMessage('" & Number & "@c.us', '" & Message & "', {createChat: true});")
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub WebView21_Click(sender As Object, e As EventArgs) Handles WebView21.Click

    End Sub

    Private Sub FrmWhatsApp_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged

    End Sub

    Private Sub FrmWhatsApp_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub
End Class