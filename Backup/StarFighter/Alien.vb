Public Class Alien

#Region " Private Members "
    Private mAlive As Boolean = True
    Private mHealth As Integer = 0
    Private mPoints As Integer = 0
    Private mPbAlien As New PictureBox
    Private WithEvents tmrAlien As New Timer
    Private mParent As FlyingSaucer
    Private mBomb As Bomb = Nothing
    Private mList As New List(Of Bomb)
    Private mSpeed As Integer = 1
    Private mDirection As Integer = 0
    Private mExplosion As Image
    Public Event evtExplosion As EventHandler
    Private WithEvents tmrExplosion As New Timer

#End Region

#Region " Public Properties "
    Public Property Alive() As Boolean
        Get
            Return mAlive
        End Get
        Set(ByVal value As Boolean)
            mAlive = value
        End Set
    End Property

    Public ReadOnly Property Points() As Integer
        Get
            Return mPoints
        End Get
    End Property

    Public Property Health() As Integer
        Get
            Return mHealth
        End Get
        Set(ByVal value As Integer)
            mHealth = value
        End Set
    End Property

    Public Property Direction() As Integer
        Get
            Return mDirection
        End Get
        Set(ByVal value As Integer)
            mDirection = value
        End Set
    End Property

    Public Property Speed() As Integer
        Get
            Return mSpeed
        End Get
        Set(ByVal value As Integer)
            mSpeed = value
        End Set
    End Property

    Public ReadOnly Property List() As List(Of Bomb)
        Get
            Return mList
        End Get
    End Property

    Public ReadOnly Property pbAlien() As PictureBox
        Get
            Return mPbAlien
        End Get
    End Property

    Public Property Location() As Point
        Get
            Return mPbAlien.Location
        End Get
        Set(ByVal value As Point)
            mPbAlien.Location = value
        End Set
    End Property

#End Region

#Region " Public Methods "
    Public Sub Blowup()
        Me.mPbAlien.Image = mExplosion
        tmrAlien.Stop()
        tmrExplosion.Interval = 1750
        tmrExplosion.Enabled = True
        tmrExplosion.Start()
    End Sub
#End Region

#Region "Private Methods "

    Private Sub Move()
        Dim pt As Point = mPbAlien.Location

        pt.X += mSpeed
        'pt.Y += 1

        mPbAlien.Location = pt

        If mParent.Creator.Ship.Location.X >= Me.Location.X AndAlso _
            mParent.Creator.Ship.Location.X <= Me.Location.X + Me.pbAlien.Width Then
            DeployBomb()
        End If

        If mParent.Creator.Ship.Location.X + mParent.Creator.Ship.PbShip.Width >= Me.Location.X AndAlso _
            mParent.Creator.Ship.Location.X + mParent.Creator.Ship.PbShip.Width < Me.Location.X + Me.pbAlien.Width Then

            DeployBomb()
        End If


    End Sub

    Public Sub DeployBomb()
        If mBomb IsNot Nothing And mParent.Creator.Ship.Alive = True Then
            If mBomb.Location.Y > Me.Location.Y + Me.pbAlien.Height + 75 Then
                mBomb = New Bomb(New Point(mParent.Creator.Ship.Location.X + mParent.Creator.Ship.PbShip.Width / 2, Me.Location.Y + Me.pbAlien.Height))
                mList.Add(mBomb)
                mParent.Creator.Form.Controls.Add(mBomb.PbBomb)
            End If
        ElseIf mParent.Creator.Ship.Alive = True Then
            mBomb = New Bomb(New Point(mParent.Creator.Ship.Location.X + mParent.Creator.Ship.PbShip.Width / 2, Me.Location.Y + Me.pbAlien.Height))
            mList.Add(mBomb)
            mParent.Creator.Form.Controls.Add(mBomb.PbBomb)
        End If
    End Sub

#End Region

#Region " Construction "
    Public Sub New(ByVal parent As FlyingSaucer, ByVal pt As Point)
        mParent = parent
        Dim bmp As New Bitmap("images\Alien1.jpg")
        Dim bmp1 As New Bitmap("images\NewAlien.png")
        Dim RandomClass As New Random(System.DateTime.Now.Millisecond)
        Dim RandomNumber As Integer
        RandomNumber = RandomClass.Next(1, 5)
        If RandomNumber <= 3 Then
            mPbAlien.Height = bmp.Height
            mPbAlien.Width = bmp.Width
            mPbAlien.Image = bmp
            mPoints = 10
            mHealth = 2
        Else
            mPbAlien.Height = bmp1.Height
            mPbAlien.Width = bmp1.Width
            mPbAlien.Image = bmp1
            mPoints = 15
            mHealth = 4
        End If
        mPbAlien.Location = pt

        Me.tmrAlien.Interval = 1
        Me.tmrAlien.Enabled = True
        Me.tmrAlien.Start()

        Do
            RandomNumber = RandomClass.Next(-5, 5)
        Loop While RandomNumber = 0
        mSpeed = RandomNumber

        Dim bmpExplosion As New Bitmap("images\Explode.gif")
        mExplosion = bmpExplosion


        'mPbAlien.BorderStyle = BorderStyle.Fixed3D
        'mPbAlien.Image = bmp
    End Sub
#End Region

#Region " Private Events "

    Private Sub tmrAlien_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrAlien.Tick
        Move()
    End Sub

    Private Sub tmrExplosion_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrExplosion.Tick
        RaiseEvent evtExplosion(Me, Nothing)
        Me.tmrExplosion.Stop()

    End Sub
#End Region

End Class
