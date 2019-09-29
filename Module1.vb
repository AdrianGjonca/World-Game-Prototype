Module Module1
    Dim playerX As Integer = 0
    Dim playerY As Integer = 0
    Dim iron As Integer = 0
    Dim screen(40, 40) As Char
    Dim tile(1024, 1024) As Char
    Dim world(2048, 2048) As Char
    '█
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
        Console.WriteLine(playerX & " " & playerY & " Iron:" & iron)
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
                    If iron > 0 Then
                        tile((playerX - 1) / 2, (playerY - 1) / 2) = tool
                        iron -= 1
                    End If
                End If
            Case Is = "j"
                If tile((playerX - 1) / 2 - 1, (playerY - 1) / 2) <> "█" Then
                    If tile((playerX - 1) / 2 - 1, (playerY - 1) / 2) = tool Then
                        iron += 1
                    End If
                    tile((playerX - 1) / 2 - 1, (playerY - 1) / 2) = " "
                End If
            Case Is = "l"
                If tile((playerX - 1) / 2 + 1, (playerY - 1) / 2) <> "█" Then
                    If tile((playerX - 1) / 2 + 1, (playerY - 1) / 2) = tool Then
                        iron += 1
                    End If
                    tile((playerX - 1) / 2 + 1, (playerY - 1) / 2) = " "
                End If
            Case Is = "i"
                If tile((playerX - 1) / 2, (playerY - 1) / 2 + 1) <> "█" Then
                    If tile((playerX - 1) / 2, (playerY - 1) / 2 + 1) = tool Then
                        iron += 1
                    End If
                    tile((playerX - 1) / 2, (playerY - 1) / 2 + 1) = " "
                End If
            Case Is = "k"
                If tile((playerX - 1) / 2, (playerY - 1) / 2 - 1) <> "█" Then
                    If tile((playerX - 1) / 2, (playerY - 1) / 2 - 1) = tool Then
                        iron += 1
                    End If
                    tile((playerX - 1) / 2, (playerY - 1) / 2 - 1) = " "
                End If
        End Select
    End Sub
    Sub Main()
        WorldGen()
        playerY = 1025
        playerX = 1025
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
        'Stone
        For x As Integer = 10 To 1000 Step 1
            For y As Integer = 10 To 1000 Step 1
                'If Int((2 * Rnd()) + 1) = "1" Then
                tile(x, y) = "▒"
                'End If
            Next
        Next

        'Caves
        For x As Integer = 100 To 900 Step 1
            For y As Integer = 100 To 900 Step 1
                If Int((50 * Rnd()) + 1) = "1" Then
                    Dim ax = 0
                    Dim ay = 0
                    While Int((8 * Rnd()) + 1) <> "1"
                        tile(x + ax, y + ay) = " "
                        If Int((3 * Rnd()) + 1) <> "1" Then
                            tile(x + ax + 1, y + ay) = " "
                        End If
                        If Int((3 * Rnd()) + 1) <> "1" Then
                            tile(x + ax - 1, y + ay) = " "
                        End If
                        If Int((3 * Rnd()) + 1) <> "1" Then
                            tile(x + ax, y + ay + 1) = " "
                        End If
                        If Int((3 * Rnd()) + 1) <> "1" Then
                            tile(x + ax, y + ay - 1) = " "
                        End If

                        If Int((3 * Rnd()) + 1) <> "1" Then
                            ax += 1
                        End If
                        If Int((3 * Rnd()) + 1) <> "1" Then
                            ax -= 1
                        End If
                        If Int((3 * Rnd()) + 1) <> "1" Then
                            ay += 1
                        End If
                        If Int((3 * Rnd()) + 1) <> "1" Then
                            ay -= 1
                        End If
                    End While
                End If
            Next
        Next

        'Ores
        For x As Integer = 100 To 900 Step 1
            For y As Integer = 100 To 900 Step 1
                If Int((800 * Rnd()) + 1) = "3" Then
                    Dim ax = 0
                    Dim ay = 0
                    While Int((8 * Rnd()) + 1) <> "1"
                        tile(x + ax, y + ay) = "▓"
                        If Int((3 * Rnd()) + 1) <> "1" Then
                            tile(x + ax + 1, y + ay) = "▓"
                        End If
                        If Int((3 * Rnd()) + 1) <> "1" Then
                            tile(x + ax - 1, y + ay) = "▓"
                        End If
                        If Int((3 * Rnd()) + 1) <> "1" Then
                            tile(x + ax, y + ay + 1) = "▓"
                        End If
                        If Int((3 * Rnd()) + 1) <> "1" Then
                            tile(x + ax, y + ay - 1) = "▓"
                        End If

                        If Int((3 * Rnd()) + 1) <> "1" Then
                            ax += 1
                        End If
                        If Int((3 * Rnd()) + 1) <> "1" Then
                            ax -= 1
                        End If
                        If Int((3 * Rnd()) + 1) <> "1" Then
                            ay += 1
                        End If
                        If Int((3 * Rnd()) + 1) <> "1" Then
                            ay -= 1
                        End If
                    End While
                End If
            Next
        Next

        'House
        For ax As Integer = 0 To 9
            For ay As Integer = 0 To 9
                tile(508 + ax, 508 + ay) = " "
                If ax = 0 Or ax = 9 Then
                    tile(508 + ax, 508 + ay) = "▓"
                ElseIf ay = 0 And ax <> 5 And ax <> 4 Then
                    tile(508 + ax, 508 + ay) = "▓"
                ElseIf ay = 9 Then
                    tile(508 + ax, 508 + ay) = "▓"
                End If
            Next
        Next

    End Sub
End Module