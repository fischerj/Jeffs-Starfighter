Public Class FlyingSaucer
#Region " Private Members "

    Private mPbFlyingSaucer As New PictureBox
    Private WithEvents tmrFlyingSaucer As New Timer
    Private mCreator As Creator
    Private mlist As New List(Of Alien)
    Private mAlien As Alien
    Public Event evtExplosion As EventHandler
    Private mLevelDeploy As Integer = 5

#End Region

#Region " Public Properties "

    Public Property Location() As Point
        Get
            Return mPbFlyingSaucer.Location
        End Get
        Set(ByVal value As Point)
            mPbFlyingSaucer.Location = value
        End Set
    End Property

    Public Property List() As List(Of Alien)
        Get
            Return mlist
        End Get
        Set(ByVal value As List(Of Alien))
            mlist = value
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

    Public Property PbFlyingSaucer() As PictureBox
        Get
            Return mPbFlyingSaucer
        End Get
        Set(ByVal value As PictureBox)
            mPbFlyingSaucer = value
        End Set
    End Property

#End Region

#Region " Public Methods "

#End Region

#Region "Private Methods "
    Private Sub Move()
        Dim pt As Point = Me.mPbFlyingSaucer.Location
        pt.X += 5

        If pt.X >= Creator.Form.Width Then
            pt.X = 0
        End If
        Me.mPbFlyingSaucer.Location = pt
    End Sub

    Public Sub DeployAlien(ByVal location As Point)
        If mAlien IsNot Nothing Then
            If mAlien.Location.Y < Creator.Form.Height - Me.PbFlyingSaucer.Height - 30 Then
                mAlien = New Alien(Me, location)
                AddHandler mAlien.evtExplosion, AddressOf mAlien_evtExplosion
                mlist.Add(mAlien)
                Creator.Form.Controls.Add(mAlien.pbAlien)
            End If
        Else
            mAlien = New Alien(Me, location)
            AddHandler mAlien.evtExplosion, AddressOf mAlien_evtExplosion
            mlist.Add(mAlien)
            Creator.Form.Controls.Add(mAlien.pbAlien)
        End If
    End Sub
#End Region

#Region " Construction "
    Public Sub New(ByVal c As Creator, ByVal location As Point, ByVal Level As Integer)
        mCreator = c

        If Level = 1 Or 2 Then
            Me.mLevelDeploy = Me.mLevelDeploy * Level
        Else
            Me.mLevelDeploy = Me.mLevelDeploy * 2
        End If

        Dim bmp As New Bitmap("images\flyingsaucer.bmp")
        mPbFlyingSaucer.Height = bmp.Height
        mPbFlyingSaucer.Width = bmp.Width
        mPbFlyingSaucer.Image = bmp

        Me.tmrFlyingSaucer.Interval = 1
        Me.tmrFlyingSaucer.Enabled = True
        Me.tmrFlyingSaucer.Start()
        Me.Location = location

        Dim r As New Random(System.DateTime.Now.Millisecond)
        Dim rnX As Integer
        Dim rnY As Integer
        Dim count As Integer = 0
        Do
            rnX = r.Next(0, Convert.ToInt32(Creator.Form.Width))
            rnY = r.Next(50, Convert.ToInt32(Creator.Form.Height / 2))
            count = count + 1
            DeployAlien(New Point(rnX, rnY))
        Loop While count < mLevelDeploy
    End Sub
#End Region

#Region " Private Events "

#End Region

    Private Sub tmrBullet_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrFlyingSaucer.Tick
        Move()
    End Sub

    Private Sub mAlien_evtExplosion(ByVal alien As Object, ByVal e As System.EventArgs)
        RaiseEvent evtExplosion(alien, Nothing)
    End Sub
End Class
