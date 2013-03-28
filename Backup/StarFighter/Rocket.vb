Public Class Rocket

#Region " Private Members "
    Private mPbRocket As New PictureBox
    Private WithEvents tmrRocket As New Timer
    Private mExplosion As Image
    Private mStartImage As Image
    Private WithEvents tmrExplosion As New Timer
    Public Event evtRocketExplosion As EventHandler
#End Region

#Region " Public Properties "
    Public Property StartImage() As Image
        Get
            Return mStartImage
        End Get
        Set(ByVal value As Image)
            mStartImage = value
        End Set
    End Property

    Public ReadOnly Property PbRocket() As PictureBox
        Get
            Return mPbRocket
        End Get
    End Property

    Public ReadOnly Property Location() As Point
        Get
            Return mPbRocket.Location
        End Get
    End Property

#End Region

#Region " Public Methods "


#End Region

#Region "Private Methods "
    Private Sub Blowup()
        tmrRocket.Stop()

        PbRocket.Location = New Point(PbRocket.Location.X - mExplosion.Width \ 2, PbRocket.Location.Y)

        Me.PbRocket.Image = mExplosion
        mPbRocket.Height = mExplosion.Height
        mPbRocket.Width = mExplosion.Width
        tmrExplosion.Interval = 1450
        tmrExplosion.Enabled = True
        tmrExplosion.Start()
    End Sub

    Private Sub Move()
        Dim pt As Point = mPbRocket.Location
        pt.Y -= 10
        mPbRocket.Location = pt

        'If pt.Y <= 175 Then
        '    Blowup()
        'End If

    End Sub

#End Region

#Region " Construction "
    Public Sub New(ByVal pt As Point)
        Dim bmp As New Bitmap("images\rocket.bmp")
        mStartImage = bmp
        Dim bmp1 As New Bitmap("images\Explode2.gif")
        mExplosion = bmp1
        mPbRocket.Height = bmp.Height
        mPbRocket.Width = bmp.Width
        mPbRocket.Image = mStartImage

        mPbRocket.Location = pt

        Me.tmrRocket.Interval = 1
        Me.tmrRocket.Enabled = True
        Me.tmrRocket.Start()
    End Sub
#End Region

#Region " Private Events "

    Private Sub tmrBullet_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrRocket.Tick
        Move()


    End Sub

#End Region

    Private Sub tmrExplosion_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrExplosion.Tick
        If Me.tmrExplosion.Enabled = True Then
            Me.tmrExplosion.Enabled = False
            Me.tmrExplosion.Stop()
            RaiseEvent evtRocketExplosion(Me, Nothing)
        End If

    End Sub
End Class
