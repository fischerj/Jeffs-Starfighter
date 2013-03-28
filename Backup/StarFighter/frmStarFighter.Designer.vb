<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStarfighter
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.tmrGameEngine = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblLives = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.pbGameTitle = New System.Windows.Forms.PictureBox
        Me.pbTitleAlien = New System.Windows.Forms.PictureBox
        CType(Me.pbGameTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbTitleAlien, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Lime
        Me.Label1.Location = New System.Drawing.Point(27, 470)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(13, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "0"
        '
        'lblLives
        '
        Me.lblLives.AutoSize = True
        Me.lblLives.ForeColor = System.Drawing.Color.Lime
        Me.lblLives.Location = New System.Drawing.Point(677, 9)
        Me.lblLives.Name = "lblLives"
        Me.lblLives.Size = New System.Drawing.Size(0, 13)
        Me.lblLives.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Lime
        Me.Label2.Location = New System.Drawing.Point(170, 216)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(476, 47)
        Me.Label2.TabIndex = 2
        Me.Label2.Tag = "TitleStuff"
        Me.Label2.Text = "Press 's' to start or 'x' to Quit"
        '
        'pbGameTitle
        '
        Me.pbGameTitle.Location = New System.Drawing.Point(267, 281)
        Me.pbGameTitle.Name = "pbGameTitle"
        Me.pbGameTitle.Size = New System.Drawing.Size(231, 87)
        Me.pbGameTitle.TabIndex = 3
        Me.pbGameTitle.TabStop = False
        Me.pbGameTitle.Tag = "TitleStuff"
        '
        'pbTitleAlien
        '
        Me.pbTitleAlien.Location = New System.Drawing.Point(387, 90)
        Me.pbTitleAlien.Name = "pbTitleAlien"
        Me.pbTitleAlien.Size = New System.Drawing.Size(231, 87)
        Me.pbTitleAlien.TabIndex = 4
        Me.pbTitleAlien.TabStop = False
        Me.pbTitleAlien.Tag = "TitleStuff"
        '
        'frmStarfighter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(728, 492)
        Me.ControlBox = False
        Me.Controls.Add(Me.pbTitleAlien)
        Me.Controls.Add(Me.pbGameTitle)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblLives)
        Me.Controls.Add(Me.Label1)
        Me.KeyPreview = True
        Me.Name = "frmStarfighter"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.pbGameTitle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbTitleAlien, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tmrGameEngine As System.Windows.Forms.Timer
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblLives As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pbGameTitle As System.Windows.Forms.PictureBox
    Friend WithEvents pbTitleAlien As System.Windows.Forms.PictureBox

End Class
