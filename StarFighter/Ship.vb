Public Class Ship

#Region " Private Members "
    Private mLives As Integer = 3
    Private mPbShip As New PictureBox
    Private WithEvents mRocket As Rocket = Nothing
    Private mLaser As Laser = Nothing
    Private mCreator As Creator
    Private mList As New List(Of Rocket)
    Private mLaserList As New List(Of Laser)
    Private mAlive As Boolean = True
    Private mExplosion As Image
    Private mStartImage As Image
    Public Event evtShipExplosion As EventHandler
    Private WithEvents tmrExplosion As New Timer
    Public Event evtRocketExplosion As EventHandler

#End Region

#Region " Public Properties "
    Public Property Lives() As Integer
        Get
            Return mLives
        End Get
        Set(ByVal value As Integer)
            mLives = value
        End Set
    End Property

    Public Property StartImage() As Image
        Get
            Return mStartImage
        End Get
        Set(ByVal value As Image)
            mStartImage = value
        End Set
    End Property

    Public Property Alive() As Boolean
        Get
            Return mAlive
        End Get
        Set(ByVal value As Boolean)
            mAlive = value
        End Set
    End Property

    Public Property Location() As Point
        Get
            Return mPbShip.Location
        End Get
        Set(ByVal value As Point)
            mPbShip.Location = value
        End Set
    End Property

    Public Property List() As List(Of Rocket)
        Get
            Return mList
        End Get
        Set(ByVal value As List(Of Rocket))
            mList = value
        End Set
    End Property

    Public Property LaserList() As List(Of Laser)
        Get
            Return mLaserList
        End Get
        Set(ByVal value As List(Of Laser))
            mLaserList = value
        End Set
    End Property

    Public Property PbShip() As PictureBox
        Get
            Return mPbShip
        End Get
        Set(ByVal value As PictureBox)
            mPbShip = value
        End Set
    End Property

    Public WriteOnly Property Key() As Integer
        Set(ByVal value As Integer)
            If mAlive = True Then
                If value = 37 Then
                    Dim pt As New Point(PbShip.Location.X, PbShip.Location.Y)
                    pt.X -= 10
                    If pt.X > 0 Then
                        PbShip.Location = pt
                    End If
                ElseIf value = 39 Then
                    Dim pt As New Point(PbShip.Location.X, PbShip.Location.Y)
                    pt.X += 10
                    If pt.X < Creator.Form.Width - PbShip.Width Then
                        PbShip.Location = pt
                    End If
                ElseIf value = 38 Then
                    Dim pt As New Point(PbShip.Location.X, PbShip.Location.Y)
                    pt.Y -= 10
                    If pt.Y > Convert.ToInt32(Creator.Form.Height / 2) Then
                        PbShip.Location = pt
                    End If
                ElseIf value = 40 Then
                    Dim pt As New Point(PbShip.Location.X, PbShip.Location.Y)
                    pt.Y += 10
                    If pt.Y < Creator.Form.Height - (PbShip.Height * 1.5) Then
                        PbShip.Location = pt
                    End If
                End If
            End If
        End Set
    End Property

    Public WriteOnly Property MouseButton() As Integer
        Set(ByVal value As Integer)
            If value = Windows.Forms.MouseButtons.Left Then
                Shoot()
            ElseIf value = Windows.Forms.MouseButtons.Right Then
                ShootLaser()
            End If
        End Set
    End Property

    Public Property Creator() As Creator
        Get
            Return mCreator
        End Get
        Set(ByVal value As Creator)
            mCreator = value
        End Set
    End Property

#End Region

#Region " Public Methods "

    Public Sub Blowup()
        Me.mPbShip.Image = mExplosion
        tmrExplosion.Interval = 1750
        tmrExplosion.Enabled = True
        tmrExplosion.Start()
    End Sub

#End Region

#Region "Private Methods "

    Private Sub Shoot()
        If mRocket IsNot Nothing And Alive = True Then
            If mRocket.Location.Y < Creator.Form.Height - Me.PbShip.Height - 30 Then
                mRocket = New Rocket(Me.PbShip.Location)
                mList.Add(mRocket)
                Creator.Form.Controls.Add(mRocket.PbRocket)
                AddHandler mRocket.evtRocketExplosion, AddressOf mRocket_evtRocketExplosion
            End If
        ElseIf Alive = True Then
            mRocket = New Rocket(Me.PbShip.Location)
            mList.Add(mRocket)
            Creator.Form.Controls.Add(mRocket.PbRocket)
            AddHandler mRocket.evtRocketExplosion, AddressOf mRocket_evtRocketExplosion
        End If
    End Sub

    Private Sub ShootLaser()
        If mLaser IsNot Nothing And Alive = True Then
            If mLaser.Location.Y < Creator.Form.Height - Me.PbShip.Height - 30 Then
                mLaser = New Laser(Me.PbShip.Location)
                mLaserList.Add(mLaser)
                Creator.Form.Controls.Add(mLaser.PbLaser)
            End If
        ElseIf Alive = True Then
            mLaser = New Laser(Me.PbShip.Location)
            mLaserList.Add(mLaser)
            Creator.Form.Controls.Add(mLaser.PbLaser)
        End If
    End Sub

#End Region

#Region " Construction "
    Public Sub New(ByVal c As Creator, ByVal location As Point)
        mCreator = c
        Me.Location = location
        Dim bmp As New Bitmap("images\player.bmp")
        mStartImage = bmp
        Dim bmpExplosion As New Bitmap("images\Explode.gif")
        mExplosion = bmpExplosion

        PbShip.Width = bmp.Width
        PbShip.Height = bmp.Height
        PbShip.Image = bmp
        PbShip.Tag = "SHIP"
    End Sub
#End Region

#Region " Private Events "
    Private Sub tmrExplosion_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrExplosion.Tick



        If Me.tmrExplosion.Enabled = True Then
            Me.tmrExplosion.Enabled = False
            Me.tmrExplosion.Stop()
            RaiseEvent evtShipExplosion(Me, Nothing)
        End If

        

    End Sub

    Private Sub mRocket_evtRocketExplosion(ByVal Rocket As Object, ByVal e As System.EventArgs)
        RaiseEvent evtRocketExplosion(Rocket, Nothing)
    End Sub
#End Region



End Class
