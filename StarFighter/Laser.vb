Public Class Laser

#Region " Private Members "
    'holds the image of the laser
    Private mPbLaser As New PictureBox
    'creates a timer which allows the laser to move
    Private WithEvents tmrLaser As New Timer
#End Region

#Region " Public Properties "
    Public ReadOnly Property PbLaser() As PictureBox
        Get
            Return mPbLaser
        End Get

    End Property

    Public ReadOnly Property Location() As Point
        Get
            Return mPbLaser.Location
        End Get
    End Property
#End Region

#Region " Public Methods "

#End Region

#Region " Private Methods "

    Private Sub Move()
        'sets PbLaser to a variable(pt) 
        'then modifies the the variable(pt) 
        'and returns it to the PbLaser location property
        Dim pt As Point = mPbLaser.Location
        pt.Y -= 20
        mPbLaser.Location = pt

    End Sub


#End Region

#Region " Private Events "
    Private Sub tmrLaser_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrLaser.Tick

        Move()

    End Sub
#End Region

#Region " Constructor "

    Public Sub New(ByVal pt As Point)
        Dim bmp As New Bitmap("images\laser.png")

        mPbLaser.Height = bmp.Height
        mPbLaser.Width = bmp.Width
        mPbLaser.Image = bmp

        mPbLaser.Location = pt

        Me.tmrLaser.Interval = 1
        Me.tmrLaser.Enabled = True
        Me.tmrLaser.Start()

    End Sub

#End Region

End Class
