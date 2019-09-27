Module Module1
    Dim playerX As Integer = 0
    Dim playerY As Integer = 0
    Dim screen(40, 40) As Char
    Dim world(1024, 1024) As Char
    Sub Render()
        'Clear Screan
        For x As Integer = 0 To 40
            For y As Integer = 0 To 40
                screen(x, y) = " "
            Next
        Next

        'Draw World
        For x As Integer = 0 To 1024
            For y As Integer = 0 To 1024
                Dim sx As Integer = x - playerX + 20
                Dim sy As Integer = -y + playerY + 20

                If sx > 0 And sx < 40 And sy > 0 And sy < 40 Then
                    screen(sx, sy) = world(x, y)
                End If

            Next
        Next


        'Draw Player
        '¶⁋
        '<>
        screen(19, 20) = "¶"
        screen(20, 20) = "⁋"
        screen(19, 21) = "<"
        screen(20, 21) = ">"

        'Draw Screen
        Dim data As String = ""
        Console.Clear()
        For y As Integer = 0 To 40
            For x As Integer = 0 To 40
                data &= screen(x, y)
            Next
            data &= vbCrLf
        Next
        Console.WriteLine(data)
        Console.WriteLine(playerX & " " & playerY)
    End Sub
    Sub TakeTurn()
        Dim key As Char = Console.ReadKey.KeyChar
        Select Case key
            Case Is = "a"
                If world(playerX - 2, playerY) = " " Then
                    playerX -= 1
                End If
            Case Is = "d"
                If world(playerX + 1, playerY) = " " Then
                    playerX += 1
                End If
            Case Is = "w"
                If world(playerX, playerY + 1) = " " Then
                    playerY += 1
                End If
            Case Is = "s"
                If world(playerX, playerY - 2) = " " Then
                    playerY -= 1
                End If
        End Select
    End Sub
    Sub Main()
        For x As Integer = 0 To 1024
            For y As Integer = 0 To 1024
                world(x, y) = " "
            Next
        Next
        For x As Integer = 500 To 520
            For y As Integer = 500 To 520
                world(x, y) = "*"
            Next
        Next
        playerY = 490
        playerX = 490
a:
        Render()
        TakeTurn()
        GoTo a
    End Sub

End Module
