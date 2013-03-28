Public Class Bomb

#Region " Private Members "
    Private mPbBomb As New PictureBox
    Private WithEvents tmrBomb As New Timer
    Private mSpeed As Integer = 5
#End Region

#Region " Public Properties "
    Public Property Speed() As Integer
        Get
            Return mSpeed
        End Get
        Set(ByVal value As Integer)
            mSpeed = value
        End Set
    End Property

    Public Property PbBomb() As PictureBox
        Get
            Return mPbBomb
        End Get
        Set(ByVal value As PictureBox)
            mPbBomb = value
        End Set
    End Property

    Public Property Location() As Point
        Get
            Return mPbBomb.Location
        End Get
        Set(ByVal value As Point)
            mPbBomb.Location = value
        End Set
    End Property

#End Region

#Region " Public Methods "

#End Region

#Region "Private Methods "
    Private Sub Move()
        Dim pt As Point = mPbBomb.Location
        pt.Y += 5
        mPbBomb.Location = pt
    End Sub
#End Region

#Region " Construction "
    Public Sub New(ByVal pt As Point)
        Dim bmp As New Bitmap("images\bomb.bmp")

        mPbBomb.Height = bmp.Height
        mPbBomb.Width = bmp.Width
        mPbBomb.Image = bmp
        mPbBomb.Location = pt

        Me.tmrBomb.Interval = 1
        Me.tmrBomb.Enabled = True
        Me.tmrBomb.Start()
    End Sub
#End Region

#Region " Private Events "

    Private Sub tmrBullet_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrBomb.Tick
        Move()
    End Sub

#End Region
End Class
