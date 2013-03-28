Public Class frmStarfighter
    Private WithEvents Creator As Creator = Nothing

    Private Sub frmStarfighter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.X Then
            Me.Close()
        ElseIf e.KeyCode = Keys.S Then
            If Creator Is Nothing Then
                Creator = New Creator(Me)
            ElseIf Creator.GameOver = True Then
                Do
                    For Each ctl As Control In Me.Controls
                        If Not TypeOf ctl Is Label Then
                            Me.Controls.Remove(ctl)
                        End If
                    Next
                Loop While Controls.Count > 3
                Creator = New Creator(Me)

            End If
            Me.pbTitleAlien.Visible = False
            Me.pbGameTitle.Visible = False
            Me.BackgroundImage = Nothing
            Me.lblLives.Text = Creator.Ship.Lives.ToString
            Me.Label2.Visible = False
        End If

        If Creator IsNot Nothing Then
            Creator.Ship.Key = e.KeyValue
        End If
    End Sub

    Private Sub frmStarfighter_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        If Creator IsNot Nothing Then
            Creator.Ship.MouseButton = e.Button

        End If

    End Sub

    Private Sub frmStarfighter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Label2.Location = New Point((Me.Width \ 2) - (Me.Label2.Width \ 2), (Me.Height \ 2) - (Me.Label2.Height \ 2))
        Me.Label2.BringToFront()
        Me.BackgroundImage = New Bitmap("images\stars.bmp")
        Me.pbGameTitle.Image = New Bitmap("images\starfighter.jpg")
        Me.pbGameTitle.Height = Me.pbGameTitle.Image.Height
        Me.pbGameTitle.Width = Me.pbGameTitle.Image.Width
        Me.pbGameTitle.Location = New Point((Me.Width \ 2) - (Me.Label2.Width \ 2), (Me.Height \ 3) - (Me.Label2.Height \ 2))
        Me.pbGameTitle.BringToFront()
        Me.pbGameTitle.Visible = True
    End Sub

    'Public WriteOnly Property AddPoints() As String

    '    Set(ByVal value As String)
    '        Me.Label1.Text = value
    '    End Set
    'End Property

    'Public WriteOnly Property Lives() As String
    '    Set(ByVal value As String)
    '        Me.lblLives.Text = value
    '    End Set
    'End Property

    Private Sub Creator_evtEndGame(ByVal GameOver As Boolean) Handles Creator.evtEndGame
        If GameOver = True Then
            'Creator.ClearControls()
            If MessageBox.Show("You have been defeated. You are worthless", "DEFEATED", MessageBoxButtons.YesNo, _
                                                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                Me.Close()
            Else
                Me.Label2.Visible = True
                Me.pbTitleAlien.Visible = True
                Me.pbGameTitle.Visible = True
                Me.BackgroundImage = New Bitmap("images\stars.bmp")
            End If
        ElseIf GameOver = False AndAlso Creator.WinGame = True Then
            Creator.ClearControls()
            If MessageBox.Show("You have defeated the alien invasion. You are victorious", "VICTORIOUS", MessageBoxButtons.YesNo, _
                                                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                Me.Close()
            Else
                Me.Label2.Visible = True
                Me.pbTitleAlien.Visible = True
                Me.pbGameTitle.Visible = True
                Me.BackgroundImage = New Bitmap("images\stars.bmp")
            End If
        
        End If

    End Sub
End Class
