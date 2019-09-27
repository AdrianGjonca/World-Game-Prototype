Module Module1
    Dim playerX As Integer = 0
    Dim playerY As Integer = 0
    Dim screen(40, 40) As Char
    Dim world(1024, 1024) As Char

    Dim tool As Char = "▓"

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
            Case Is = "j"
                If world(playerX - 3, playerY) = " " Then
                    world(playerX - 1 - 2, playerY + 1 - 2) = tool
                    world(playerX - 2, playerY + 1 - 2) = tool
                    world(playerX - 1 - 2, playerY + 2 - 2) = tool
                    world(playerX - 2, playerY + 2 - 2) = tool
                End If
            Case Is = "l"
                If world(playerX + 2, playerY) = " " Then

                End If
            Case Is = "i"
                If world(playerX, playerY + 2) = " " Then
                    world(playerX - 1, playerY + 1) = tool
                    world(playerX, playerY + 1) = tool
                    world(playerX - 1, playerY + 2) = tool
                    world(playerX, playerY + 2) = tool
                End If
            Case Is = "k"
                If world(playerX, playerY - 3) = " " Then
                    world(playerX - 1, playerY + 1 - 4) = tool
                    world(playerX, playerY + 1 - 4) = tool
                    world(playerX - 1, playerY + 2 - 4) = tool
                    world(playerX, playerY + 2 - 4) = tool
                End If
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
                world(x, y) = " "

            Next
        Next
        For x As Integer = 10 To 1000 Step 2
            For y As Integer = 10 To 1000 Step 2
                If Int((6 * Rnd()) + 1) = "3" Then
                    world(x, y) = "▲"
                    world(x + 1, y) = "▲"
                    world(x, y + 1) = "▲"
                    world(x + 1, y + 1) = "▲"
                End If
            Next
        Next
        For x As Integer = 500 To 521
            For y As Integer = 500 To 521
                world(x, y) = "▓"
            Next
        Next
    End Sub
End Module
