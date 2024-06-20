Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Public Class CreateSubmissionForm
    Private stopwatch As New Stopwatch()
    Private timer As New Timer()

    Public Sub New()
        InitializeComponent()
        AddHandler timer.Tick, AddressOf TimerTick
        timer.Interval = 1000 

        
        Me.KeyPreview = True
    End Sub

    Private Sub TimerTick(sender As Object, e As EventArgs)
        lblStopwatch.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub

    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs) Handles btnToggleStopwatch.Click
        ToggleStopwatch()
    End Sub

    Private Sub ToggleStopwatch()
        If stopwatch.IsRunning Then
            stopwatch.Stop()
            timer.Stop()
        Else
            stopwatch.Start()
            timer.Start()
        End If
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        SubmitForm() 
    End Sub

    Private Async Sub SubmitForm()
        Dim client As New HttpClient()
        Dim url As String = "http://localhost:3000/submit"
        Dim content As New StringContent($"{{""name"":""{txtName.Text}"",""email"":""{txtEmail.Text}"",""phone"":""{txtPhone.Text}"",""github_link"":""{txtGithub.Text}"",""stopwatch_time"":""{lblStopwatch.Text}""}}", Encoding.UTF8, "application/json")
        Dim response As HttpResponseMessage = Await client.PostAsync(url, content)
        Dim responseString As String = Await response.Content.ReadAsStringAsync()
        MessageBox.Show(responseString)
    End Sub

    Private Sub CreateSubmissionForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
       
        If e.Control AndAlso e.KeyCode = Keys.T Then
            
            ToggleStopwatch()
        ElseIf e.Control AndAlso e.KeyCode = Keys.S Then
            
            SubmitForm()
        End If
    End Sub
End Class
