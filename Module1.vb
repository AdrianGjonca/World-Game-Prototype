Module Module1
    Dim playerX As Integer = 0
    Dim playerY As Integer = 0
    Dim screen(40, 40) As Char
    Dim world(10, 10) As Char
    Sub Render()
        'Clear Screan
        For x As Integer = 0 To 40
            For y As Integer = 0 To 40
                screen(x, y) = " "
            Next
        Next

        'Draw World
        For x As Integer = 0 To 10
            For y As Integer = 0 To 10
                Dim sx As Integer = x - playerX
                Dim sy As Integer = y + playerY

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
                playerX -= 1
            Case Is = "d"
                playerX += 1
            Case Is = "w"
                playerY += 1
            Case Is = "s"
                playerY -= 1
        End Select
    End Sub
    Sub Main()
        For x As Integer = 0 To 10
            For y As Integer = 0 To 10
                world(x, y) = "*"
            Next
        Next
a:
        Render()
        TakeTurn()
        GoTo a
    End Sub

End Module
