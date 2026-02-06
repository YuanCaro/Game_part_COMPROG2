Public Class Form1

    Dim goUp As Boolean = False
    Dim goDown As Boolean = False
    Dim goLeft As Boolean = False
    Dim goRight As Boolean = False
    Dim speed As Integer = 5

    Dim canMove As Boolean = False
    Dim enemySpeed As Integer = 6

    Dim walls As New List(Of PictureBox)
    Dim deathZones As New List(Of PictureBox)

    Dim Red1GoingDown As Boolean = True
    Dim Red2GoingDown As Boolean = False
    Dim Red3GoingRight As Boolean = True
    Dim Red4GoingRight As Boolean = False
    Dim Red5GoingRight As Boolean = True
    Dim Red6GoingRight As Boolean = False
    Dim Cat1GoingDown As Boolean = True

    Dim startX As Integer = 239
    Dim startY As Integer = 374

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        Timer1.Interval = 20
        Timer1.Enabled = True

        walls.Add(PictureBox1)
        walls.Add(PictureBox2)
        walls.Add(PictureBox3)
        walls.Add(PictureBox4)
        walls.Add(PictureBox6)
        walls.Add(PictureBox7)
        walls.Add(PictureBox8)
        walls.Add(PictureBox9)
        walls.Add(PictureBox10)

        deathZones.Add(Red1)
        deathZones.Add(Red2)
        deathZones.Add(Red3)
        deathZones.Add(Red4)
        deathZones.Add(Red5)
        deathZones.Add(Red6)
        deathZones.Add(Cat1)

        PictureBox5.Left = startX
        PictureBox5.Top = startY
    End Sub

    Private Sub StartGame(Optional easyMode As Boolean = False)
        canMove = True
        enemySpeed = If(easyMode, 4, 6)

        PictureBox5.Left = startX
        PictureBox5.Top = startY

        Red1.Top = 137
        Red2.Top = 590
        Red3.Left = 1164
        Red4.Left = 1288
        Red5.Left = 1164
        Red6.Left = 1288
        Cat1.Top = 137

        Red1GoingDown = True
        Red2GoingDown = False
        Red3GoingRight = True
        Red4GoingRight = False
        Red5GoingRight = True
        Red6GoingRight = False
        Cat1GoingDown = True

        goUp = False
        goDown = False
        goLeft = False
        goRight = False

        Timer1.Enabled = True
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        StartGame(False)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        StartGame(True)
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If canMove Then
            If e.KeyCode = Keys.W Then goUp = True
            If e.KeyCode = Keys.S Then goDown = True
            If e.KeyCode = Keys.A Then goLeft = True
            If e.KeyCode = Keys.D Then goRight = True
        End If
    End Sub

    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If canMove Then
            If e.KeyCode = Keys.W Then goUp = False
            If e.KeyCode = Keys.S Then goDown = False
            If e.KeyCode = Keys.A Then goLeft = False
            If e.KeyCode = Keys.D Then goRight = False
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If canMove Then
            MoveVertical(Red1, 137, 590, Red1GoingDown)
            MoveVertical(Red2, 137, 590, Red2GoingDown)
            MoveVertical(Cat1, 137, 478, Cat1GoingDown)
            MoveHorizontal(Red3, 1164, 1288, Red3GoingRight)
            MoveHorizontal(Red4, 1164, 1288, Red4GoingRight)
            MoveHorizontal(Red5, 1164, 1288, Red5GoingRight)
            MoveHorizontal(Red6, 1164, 1288, Red6GoingRight)

            If goUp Then MovePlayer(0, -speed)
            If goDown Then MovePlayer(0, speed)
            If goLeft Then MovePlayer(-speed, 0)
            If goRight Then MovePlayer(speed, 0)
        End If

        If PictureBox5.Bounds.IntersectsWith(Mango.Bounds) Then
            Timer1.Enabled = False
            MessageBox.Show("You win!")
            canMove = False
        End If

        For Each death As PictureBox In deathZones
            If PictureBox5.Bounds.IntersectsWith(death.Bounds) Then
                Timer1.Enabled = False
                MessageBox.Show("You died!")
                canMove = False
                Exit For
            End If
        Next
    End Sub

    Private Sub MoveVertical(enemy As PictureBox, minY As Integer, maxY As Integer, ByRef goingDown As Boolean)
        If goingDown Then
            enemy.Top += enemySpeed
            If enemy.Top >= maxY Then goingDown = False
        Else
            enemy.Top -= enemySpeed
            If enemy.Top <= minY Then goingDown = True
        End If
    End Sub

    Private Sub MoveHorizontal(enemy As PictureBox, minX As Integer, maxX As Integer, ByRef goingRight As Boolean)
        If goingRight Then
            enemy.Left += enemySpeed
            If enemy.Left >= maxX Then goingRight = False
        Else
            enemy.Left -= enemySpeed
            If enemy.Left <= minX Then goingRight = True
        End If
    End Sub


    Private Sub MovePlayer(dx As Integer, dy As Integer)
        PictureBox5.Left += dx
        PictureBox5.Top += dy

        If CollidesWithWalls() Then
            PictureBox5.Left -= dx
            PictureBox5.Top -= dy
        End If

        If PictureBox5.Left < 0 Then PictureBox5.Left = 0
        If PictureBox5.Top < 0 Then PictureBox5.Top = 0
        If PictureBox5.Right > Me.ClientSize.Width Then PictureBox5.Left = Me.ClientSize.Width - PictureBox5.Width
        If PictureBox5.Bottom > Me.ClientSize.Height Then PictureBox5.Top = Me.ClientSize.Height - PictureBox5.Height
    End Sub

    Private Function CollidesWithWalls() As Boolean
        For Each wall As PictureBox In walls
            If PictureBox5.Bounds.IntersectsWith(wall.Bounds) Then Return True
        Next
        Return False
    End Function

End Class
