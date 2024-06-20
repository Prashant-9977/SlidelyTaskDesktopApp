Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Enable KeyPreview when the form loads
        Me.KeyPreview = True
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        ' Handle key presses here
        If e.Control AndAlso e.KeyCode = Keys.V Then
            ' Ctrl + V pressed - View Submissions
            ViewSubmissions()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            ' Ctrl + N pressed - Create New Submission
            CreateNewSubmission()
        End If
    End Sub

    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs) Handles btnViewSubmissions.Click
        ' Handle button click for View Submissions
        ViewSubmissions()
    End Sub

    Private Sub btnCreateNewSubmission_Click(sender As Object, e As EventArgs) Handles btnCreateNewSubmission.Click
        ' Handle button click for Create New Submission
        CreateNewSubmission()
    End Sub

    Private Sub ViewSubmissions()
        ' Code to open View Submissions form
        Dim viewForm As New ViewSubmissionsForm()
        viewForm.Show()
    End Sub

    Private Sub CreateNewSubmission()
        ' Code to open Create New Submission form
        Dim createForm As New CreateSubmissionForm()
        createForm.Show()
    End Sub

End Class
