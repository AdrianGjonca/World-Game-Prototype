Module Module1
    Dim playerX As Integer = 0
    Dim playerY As Integer = 0
    Dim screen(40, 40) As Char
    Dim tile(1024, 1024) As Char
    Dim world(2048, 2048) As Char

    Dim tool As Char = "▓"

    Sub Render()
        'Clear Screan
        For x As Integer = 0 To 40
            For y As Integer = 0 To 40
                screen(x, y) = " "
            Next
        Next

        'Process World
        For x As Integer = 0 To 1023
            For y As Integer = 0 To 1023
                world(x * 2, y * 2) = tile(x, y)
                world(x * 2 + 1, y * 2) = tile(x, y)
                world(x * 2, y * 2 + 1) = tile(x, y)
                world(x * 2 + 1, y * 2 + 1) = tile(x, y)
            Next
        Next

        'Draw World
        For x As Integer = playerX - 40 To playerX + 40
            For y As Integer = playerY - 40 To playerY + 40
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
                If world(playerX - 3, playerY) = " " Then
                    playerX -= 2
                End If
            Case Is = "d"
                If world(playerX + 2, playerY) = " " Then
                    playerX += 2
                End If
            Case Is = "w"
                If world(playerX, playerY + 2) = " " Then
                    playerY += 2
                End If
            Case Is = "s"
                If world(playerX, playerY - 3) = " " Then
                    playerY -= 2
                End If
            Case Is = " "
                If tile((playerX - 1) / 2, (playerY - 1) / 2) = " " Then
                    tile((playerX - 1) / 2, (playerY - 1) / 2) = tool
                End If
            Case Is = "j"
                tile((playerX - 1) / 2 - 1, (playerY - 1) / 2) = " "
            Case Is = "l"
                tile((playerX - 1) / 2 + 1, (playerY - 1) / 2) = " "
            Case Is = "i"
                tile((playerX - 1) / 2, (playerY - 1) / 2 + 1) = " "
            Case Is = "k"
                tile((playerX - 1) / 2, (playerY - 1) / 2 - 1) = " "
        End Select
    End Sub
    Sub Main()
        WorldGen()
        playerY = 491
        playerX = 491
a:
        Render()
        TakeTurn()
        GoTo a
    End Sub

    Sub WorldGen()
        For x As Integer = 0 To 1024
            For y As Integer = 0 To 1024
                tile(x, y) = " "
            Next
        Next
        For x As Integer = 10 To 1000 Step 1
            For y As Integer = 10 To 1000 Step 1
                If Int((2 * Rnd()) + 1) = "1" Then
                    tile(x, y) = "▲"
                End If
            Next
        Next
    End Sub
End Module
