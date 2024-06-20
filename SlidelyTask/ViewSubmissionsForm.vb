Imports System.Net.Http
Imports Newtonsoft.Json

Public Class ViewSubmissionsForm
    Private submissions As List(Of Submission)
    Private currentIndex As Integer = 0

    Private Async Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Await LoadSubmissions()
        If submissions.Count > 0 Then
            DisplaySubmission(currentIndex)
        End If

        ' Enable KeyPreview for the form to capture keyboard events
        Me.KeyPreview = True
    End Sub

    Private Async Function LoadSubmissions() As Task
        submissions = New List(Of Submission)()

        Using client As New HttpClient()
            Dim index As Integer = 0
            While True
                Dim url As String = $"http://localhost:3000/read?index={index}"
                Dim response As HttpResponseMessage = Await client.GetAsync(url)

                If response.StatusCode = System.Net.HttpStatusCode.NotFound Then
                    Exit While
                End If

                Dim responseString As String = Await response.Content.ReadAsStringAsync()
                Dim submission As Submission = JsonConvert.DeserializeObject(Of Submission)(responseString)
                submissions.Add(submission)
                index += 1
            End While
        End Using
    End Function

    Private Sub DisplaySubmission(index As Integer)
        Dim submission = submissions(index)
        txtName.Text = submission.Name
        txtEmail.Text = submission.Email
        txtPhone.Text = submission.Phone
        txtGitHub.Text = submission.GitHubLink
        lblStopwatch.Text = submission.StopwatchTime
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        NavigatePrevious()
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        NavigateNext()
    End Sub

    Private Sub NavigatePrevious()
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplaySubmission(currentIndex)
        End If
    End Sub

    Private Sub NavigateNext()
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplaySubmission(currentIndex)
        End If
    End Sub

    Private Sub ViewSubmissionsForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        ' Handle key presses here
        If e.Control AndAlso e.KeyCode = Keys.P Then
            ' Ctrl + P pressed - Navigate to previous submission
            NavigatePrevious()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            ' Ctrl + N pressed - Navigate to next submission
            NavigateNext()
        End If
    End Sub
End Class

Public Class Submission
    Public Property Name As String
    Public Property Email As String
    Public Property Phone As String
    Public Property GitHubLink As String
    Public Property StopwatchTime As String
End Class
