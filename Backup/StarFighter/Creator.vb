Public Class Creator

#Region " Private Members "
    Public Delegate Sub CreatorEventHandler(ByVal GameOver As Boolean)

    Private mForm As frmStarfighter
    Private WithEvents mShip As Ship
    Private WithEvents mFlyingSaucer As FlyingSaucer
    Private WithEvents tmrCreator As New Timer
    Private tmpDeadAlien As New List(Of Alien)
    Public Event evtEndGame As CreatorEventHandler
    Private mGameOver As Boolean = False
    Private mLevel As Integer = 1
    Private mWinGame As Boolean = False
#End Region

#Region " Public Properties "
    Public ReadOnly Property Level() As Integer
        Get
            Return mLevel
        End Get
    End Property

    Public ReadOnly Property GameOver() As Boolean
        Get
            Return mGameOver
        End Get

    End Property

    Public ReadOnly Property WinGame() As Boolean
        Get
            Return mWinGame
        End Get
    End Property

    Public Property Form() As frmStarfighter
        Get
            Return mForm
        End Get
        Set(ByVal value As frmStarfighter)
            mForm = value
        End Set
    End Property

    Public Property Ship() As Ship
        Get
            Return mShip
        End Get
        Set(ByVal value As Ship)
            mShip = value
        End Set
    End Property

    Public Property FlyingSaucer() As FlyingSaucer
        Get
            Return mFlyingSaucer
        End Get
        Set(ByVal value As FlyingSaucer)
            mFlyingSaucer = value
        End Set
    End Property

#End Region

#Region " Construction "

    Public Sub New(ByVal frm As Form)
        mForm = frm

        mShip = New Ship(Me, New Point(mForm.Width / 2, mForm.Height - 100))
        mForm.Controls.Add(mShip.PbShip)

        mFlyingSaucer = New FlyingSaucer(Me, New Point(mForm.Width / 2, 0), mLevel)
        mForm.Controls.Add(mFlyingSaucer.PbFlyingSaucer)

        tmrCreator.Interval = 1
        tmrCreator.Enabled = True
        tmrCreator.Start()
    End Sub

#End Region

#Region " Private Events "

    Private Sub tmrCreator_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrCreator.Tick

        'Creates a tmp list which will hold any Laser that wasnt removed 
        Dim tmpLaser As New List(Of Laser)

        'Creates a tmp list which will hold any Rocket that wasnt removed 
        Dim tmpRocket As New List(Of Rocket)

        'Creates a tmp list which will hold any Bomb that wasnt removed 
        Dim tmpBomb As New List(Of Bomb)

        'creates a tmp list which will hold aliens that have been hit by a laser
        Dim tmpAlien As New List(Of Alien)


        'tries to remove any laser that has traveled off the form
        For Each l As Laser In Ship.LaserList
            If l.Location.Y <= 0 Then
                Form.Controls.Remove(l.PbLaser)
                tmpLaser.Add(l)
            End If
        Next

        'tries to remove any Rocket that has traveled off the form
        For Each r As Rocket In Ship.List
            If r.Location.Y <= 0 Then
                Form.Controls.Remove(r.PbRocket)
                tmpRocket.Add(r)
            End If
        Next

        'tries to remove any bomb that has traveled off the form
        For Each a As Alien In FlyingSaucer.List
            For Each b As Bomb In a.List
                If b.Location.Y > Form.Height Then
                    Form.Controls.Remove(b.PbBomb)
                    tmpBomb.Add(b)
                End If
            Next
        Next

        'tries to remove any alien that has been hit by a laser if health = 0
        'if health > 0 then it decrements the health by 1
        'if the laser hits the alien it is removed from the form and added to the tmpLaser list
        For Each l As Laser In Ship.LaserList
            For Each a As Alien In FlyingSaucer.List
                If l.PbLaser.Bounds.IntersectsWith(a.pbAlien.Bounds) Then
                    a.Health = a.Health - 1
                    Form.Controls.Remove(l.PbLaser)
                    tmpLaser.Add(l)
                    If a.Health <= 0 AndAlso a.Alive = True Then
                        a.Blowup()

                        a.Alive = False
                        Form.Label1.Text = Convert.ToInt32(Form.Label1.Text) + a.Points
                    End If
                End If
            Next
        Next

        'if the Rocket hits the alien it is removed from the form and added 
        'to the tmpRocket list
        'calls the Blowup() of the Alien
        For Each r As Rocket In Ship.List
            For Each a As Alien In FlyingSaucer.List
                If a.pbAlien.Bounds.IntersectsWith(New Rectangle _
                                                   (r.PbRocket.Location.X + r.PbRocket.Width / 4, r.PbRocket.Location.Y - r.PbRocket.Height / 4, r.PbRocket.Height / 2, r.PbRocket.Width / 2)) Then
                    'If r.PbRocket.Bounds.IntersectsWith(a.pbAlien.Bounds) Then
                    a.Health = a.Health - 2
                    'tmpRocket.Add(r)
                    If a.Health <= 0 AndAlso a.Alive = True Then
                        a.Alive = False
                        a.Blowup()
                        Form.Label1.Text = Convert.ToInt32(Form.Label1.Text) + a.Points
                    End If
                    'Form.Controls.Remove(a.pbAlien)
                    'tmpAlien.Add(a)
                End If
            Next
        Next

        'if the Bomb hits the Ship it is removed from the form and added to the tmpBomb list
        'tries to remove the Ship when it has been hit by a Bomb
        For Each a As Alien In FlyingSaucer.List
            For Each b As Bomb In a.List
                If b.PbBomb.Bounds.IntersectsWith(Ship.PbShip.Bounds) AndAlso Ship.Alive = True Then
                    Ship.Alive = False
                    Form.Controls.Remove(b.PbBomb)
                    tmpBomb.Add(b)
                    Ship.Blowup()
                End If
            Next
        Next

        'allows the alien to repeat its movement on the form 
        For Each a As Alien In FlyingSaucer.List
            If a.Location.X < -a.pbAlien.Width Then
                a.Location = New Point(Form.Width, a.Location.Y)
            ElseIf a.Location.X >= Form.Width Then
                a.Location = New Point(0, a.Location.Y)
            End If
        Next

        'removes the laser from the tmp list
        For Each l As Laser In tmpLaser
            mShip.LaserList.Remove(l)
        Next

        'removes the Rocket from the tmp list
        For Each r As Rocket In tmpRocket
            mShip.List.Remove(r)
        Next

        'removes the Bomb from the tmp list
        For Each a As Alien In tmpAlien
            For Each b As Bomb In tmpBomb
                a.List.Remove(b)
            Next
        Next

        'removes the Alien from the tmp list
        For Each a As Alien In tmpAlien
            FlyingSaucer.List.Remove(a)
        Next

        'removes pbBomb from form after reaching bottom of form
        'adds instance of bomb to tmpBomb, preparing it for removal
        For Each Da As Alien In tmpDeadAlien
            For Each b As Bomb In Da.List
                If b.Location.Y >= Form.Height Then
                    Form.Controls.Remove(b.PbBomb)
                    tmpBomb.Add(b)
                End If
            Next
        Next
        'removes the bomb from tmpBomb
        For Each a As Alien In tmpDeadAlien
            For Each b As Bomb In tmpBomb
                a.List.Remove(b)
            Next
        Next
        'if aliens bomb count = 0, add alien to tmpAlien to be removed later
        For Each Da As Alien In tmpDeadAlien
            If Da.List.Count = 0 Then
                tmpAlien.Add(Da)
            End If
        Next
        'removes alien from tmpDeadAlien from tmpAlien
        For Each a As Alien In tmpAlien
            tmpDeadAlien.Remove(a)
        Next

        If FlyingSaucer.List.Count = 0 Then
            'MUST CLEAR THE FORM OF ALL CONTROLS EXCEPT SHIP
            ClearControls()
            mLevel = mLevel + 1
            If mLevel = 4 Then
                mGameOver = True
                mWinGame = True
                RaiseEvent evtEndGame(False)
            End If
            If WinGame = False Then
                'CREATE NEW INSTANCE OF FLYINGSAUCER
                mForm.Controls.Remove((mFlyingSaucer.PbFlyingSaucer))
                mFlyingSaucer = New FlyingSaucer(Me, New Point(mForm.Width / 2, 0), mLevel)
                mForm.Controls.Add(mFlyingSaucer.PbFlyingSaucer)
            End If

        End If

    End Sub

#End Region

    Private Sub mFlyingSaucer_evtExplosion(ByVal alien As Object, ByVal e As System.EventArgs) Handles mFlyingSaucer.evtExplosion
        'creates a tmp list which will hold aliens that have been Blowup()
        Form.Controls.Remove(alien.pbAlien)
        'adds the Alien to the tmpDeadAlien list
        tmpDeadAlien.Add(alien)
        FlyingSaucer.List.Remove(alien)
    End Sub

    Private Sub mShip_evtRocketExplosion(ByVal Rocket As Object, ByVal e As System.EventArgs) Handles mShip.evtRocketExplosion
        Form.Controls.Remove(CType(Rocket, Rocket).PbRocket)
        Ship.List.Remove(Rocket)
    End Sub

    Private Sub mShip_evtShipExplosion(ByVal sender As Object, ByVal e As System.EventArgs) Handles mShip.evtShipExplosion

        Form.Controls.Remove(Ship.PbShip)
        Ship.Alive = False
        Ship.Lives = Ship.Lives - 1
        Form.lblLives.Text = Ship.Lives.ToString
        If Ship.Lives > 0 Then
            If MessageBox.Show("you have died.You have " + Ship.Lives.ToString + " lives left") = DialogResult.OK Then
                'Creates a tmp list which will hold any Bomb that wasnt removed 
                Dim tmpBomb As New List(Of Bomb)
                For Each a As Alien In FlyingSaucer.List
                    For Each b As Bomb In a.List
                        If Ship.Alive = False Then
                            Form.Controls.Remove(b.PbBomb)
                            tmpBomb.Add(b)
                        End If
                    Next
                Next
                For Each a As Alien In FlyingSaucer.List
                    For Each b As Bomb In tmpBomb
                        a.List.Remove(b)
                    Next
                Next
                Form.Controls.Add(Ship.PbShip)
                Ship.Alive = True
                Ship.PbShip.Image = Ship.StartImage

            End If
        ElseIf Ship.Lives = 0 Then
            mGameOver = True
            RaiseEvent evtEndGame(True)
        End If
    End Sub

    Public Sub ClearControls()
        Dim i As Integer = 0
        Do
            For Each ctl As Control In Form.Controls
                If Not TypeOf ctl Is Label AndAlso Not Me.Ship.PbShip.Tag = "SHIP" _
                                            AndAlso Not Form.pbGameTitle.Tag = "TitleStuff" _
                                            AndAlso Not Form.pbTitleAlien.Tag = "TitleStuff" Then
                    Form.Controls.Remove(ctl)
                End If
            Next
            i += 1
        Loop While i < 3
    End Sub
End Class
