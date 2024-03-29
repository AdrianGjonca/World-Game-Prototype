Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Module Dungeons
    Public AincientDungeon = New String(11, 9) {
        {"░", "░", "░", "░", "░", "░", "░", "░", "░", "░"},
        {"░", "♣", "♣", " ", "♣", "♣", " ", "♣", "♣", "░"},
        {"░", "♣", " ", "▲", " ", " ", "▲", " ", "♣", "░"},
        {"░", "♣", " ", " ", "♣", "♣", " ", " ", "♣", "░"},
        {"░", " ", "▲", " ", "♣", "♣", " ", "▲", " ", "░"},
        {"░", "♣", " ", " ", " ", "♣", "♣", " ", "♣", "░"},
        {"░", "♣", " ", "▲", " ", "♣", " ", "▲", " ", "░"},
        {"░", "♣", " ", " ", "♣", "♣", "♣", " ", "♣", "░"},
        {"░", " ", "▲", " ", "♣", "♣", "♣", "♣", "♣", "░"},
        {"░", "♣", " ", "♣", "♣", "♣", "♣", "♣", "♣", "░"},
        {"░", "♣", "♣", "♣", "♣", "♣", "♣", "♣", "♣", "░"},
        {"░", "░", "░", "░", "╬", "╬", "░", "░", "░", "░"}
    }
End Module

Module Module1
    Dim playerX As Integer = 0
    Dim playerY As Integer = 0
    Dim health As Integer = 10
    Dim iron As Integer = 0
    Dim copper As Integer = 0
    Dim wood As Integer = 0
    Dim stone As Integer = 0
    Dim screen(40, 40) As Char
    Dim tileU(5000, 5000) As Char
    Dim tileO(5000, 5000) As Char
    Dim tile(5000, 5000) As Char
    Dim world(10000, 10000) As Char

    Dim handy As Boolean = True
    Public Class entitiy
        Public x As Integer
        Public y As Integer
        Public life As Integer = 0
    End Class

    Dim entitiesO As List(Of entitiy) = New List(Of entitiy)
    Dim entitiesU As List(Of entitiy) = New List(Of entitiy)
    Dim entities As List(Of entitiy) = New List(Of entitiy)

    Dim level As Boolean = True

    '█
    Dim ironT As Char = "▓"
    Dim copperT As Char = "░"
    Dim woodT As Char = "♣"

    Dim spawnerT As Char = "▲"

    Sub Render()
        'Clear Screan
        For x As Integer = 0 To 40
            For y As Integer = 0 To 40
                screen(x, y) = " "
            Next
        Next

        'Process World
        For x As Integer = playerX / 2 - 100 To playerX / 2 + 100
            For y As Integer = playerY / 2 - 100 To playerY / 2 + 100
                world(x * 2, y * 2) = tile(x, y)
                world(x * 2 + 1, y * 2) = tile(x, y)
                world(x * 2, y * 2 + 1) = tile(x, y)
                world(x * 2 + 1, y * 2 + 1) = tile(x, y)
            Next
        Next

        For Each creature As entitiy In entities
            world(creature.x, creature.y) = "║"
            world(creature.x + 1, creature.y) = " "
            world(creature.x, creature.y + 1) = "☻"
            world(creature.x + 1, creature.y + 1) = "*"
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

        If handy Then
            screen(19, 20) = "☺"
            screen(20, 20) = " "
            screen(19, 21) = "║"
            screen(20, 21) = "\"
        Else
            screen(19, 20) = "☺"
            screen(20, 20) = "/"
            screen(19, 21) = "║"
            screen(20, 21) = " "
        End If


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
        Console.WriteLine("╔════════════════════╗")
        Console.WriteLine("║ HP: " & health)
        Console.WriteLine("╠════════════════════╣")
        Console.WriteLine("║Iron: " & iron)
        Console.WriteLine("║Copper: " & copper)
        Console.WriteLine("║Wood: " & wood)
        Console.WriteLine("║Stone: " & stone)
        Console.WriteLine("╠════════════════════╣")
        Console.WriteLine("║1: door  ╬  C:5 I:6 ║")
        Console.WriteLine("║2: table ╥  C:5 I:4 ║")
        Console.WriteLine("╚════════════════════╝")
        Console.WriteLine(playerX & " " & playerY)
        Console.CursorLeft = 0
        Console.CursorTop = 0
    End Sub

    Dim walkthrough As Char() = {"╬", "~"}
    Sub TakeTurn()
        If health < 1 Then
            health = 10
            playerY = 5009
            playerX = 5009
            stone = 0
            copper = 0
            iron = 0
            wood = 0
        End If
        Dim key As Char = Console.ReadKey.KeyChar
        Select Case key
            Case Is = "a"
                If world(playerX - 3, playerY) = " " Or walkthrough.Contains(world(playerX - 3, playerY)) Then
                    playerX -= 2
                End If
            Case Is = "d"
                If world(playerX + 2, playerY) = " " Or walkthrough.Contains(world(playerX + 2, playerY)) Then
                    playerX += 2
                End If
            Case Is = "w"
                If world(playerX, playerY + 2) = " " Or walkthrough.Contains(world(playerX, playerY + 2)) Then
                    playerY += 2
                End If
            Case Is = "s"
                If world(playerX, playerY - 3) = " " Or walkthrough.Contains(world(playerX, playerY - 3)) Then
                    playerY -= 2
                End If
            Case Is = "1"
                If iron >= 6 And copper >= 5 Then
                    tile((playerX - 1) / 2, (playerY - 1) / 2) = "╬"
                    iron -= 6
                    copper -= 5
                End If
            Case Is = "2"
                If iron >= 4 And copper >= 5 Then
                    tile((playerX - 1) / 2, (playerY - 1) / 2) = "╥"
                    iron -= 4
                    copper -= 5
                End If
            Case Is = " "
                If iron > 0 Then
                    tile((playerX - 1) / 2, (playerY - 1) / 2) = ironT
                    iron -= 1
                End If
            Case Is = Constants.vbBack
                If copper > 0 Then
                    tile((playerX - 1) / 2, (playerY - 1) / 2) = copperT
                    copper -= 1
                End If '▒
            Case Is = "]"
                If stone > 0 Then
                    tile((playerX - 1) / 2, (playerY - 1) / 2) = "▒"
                    stone -= 1
                End If
            Case Is = "#"
                If wood > 0 Then
                    tile((playerX - 1) / 2, (playerY - 1) / 2) = woodT
                    wood -= 1
                End If
            Case Is = "j"
                Attack("j")
                If tile((playerX - 1) / 2 - 1, (playerY - 1) / 2) <> "~" Then
                    If tile((playerX - 1) / 2 - 1, (playerY - 1) / 2) = ironT Then
                        iron += 1
                    End If
                    If tile((playerX - 1) / 2 - 1, (playerY - 1) / 2) = copperT Then
                        copper += 1
                    End If
                    If tile((playerX - 1) / 2 - 1, (playerY - 1) / 2) = woodT Then
                        wood += 1
                    End If
                    If tile((playerX - 1) / 2 - 1, (playerY - 1) / 2) = "▒" Then
                        stone += 1
                    End If
                    tile((playerX - 1) / 2 - 1, (playerY - 1) / 2) = " "
                End If
                handy = Not handy
            Case Is = "l"
                Attack("l")
                If tile((playerX - 1) / 2 + 1, (playerY - 1) / 2) <> "~" Then
                    If tile((playerX - 1) / 2 + 1, (playerY - 1) / 2) = ironT Then
                        iron += 1
                    End If
                    If tile((playerX - 1) / 2 + 1, (playerY - 1) / 2) = copperT Then
                        copper += 1
                    End If
                    If tile((playerX - 1) / 2 + 1, (playerY - 1) / 2) = woodT Then
                        wood += 1
                    End If
                    If tile((playerX - 1) / 2 + 1, (playerY - 1) / 2) = "▒" Then
                        stone += 1
                    End If
                    tile((playerX - 1) / 2 + 1, (playerY - 1) / 2) = " "
                End If
                handy = Not handy
            Case Is = "i"
                Attack("i")
                If tile((playerX - 1) / 2, (playerY - 1) / 2 + 1) <> "~" Then
                    If tile((playerX - 1) / 2, (playerY - 1) / 2 + 1) = ironT Then
                        iron += 1
                    End If
                    If tile((playerX - 1) / 2, (playerY - 1) / 2 + 1) = copperT Then
                        copper += 1
                    End If
                    If tile((playerX - 1) / 2, (playerY - 1) / 2 + 1) = woodT Then
                        wood += 1
                    End If
                    If tile((playerX - 1) / 2, (playerY - 1) / 2 + 1) = "▒" Then
                        stone += 1
                    End If
                    tile((playerX - 1) / 2, (playerY - 1) / 2 + 1) = " "
                End If
                handy = Not handy
            Case Is = "k"
                Attack("k")
                If tile((playerX - 1) / 2, (playerY - 1) / 2 - 1) <> "~" Then
                    If tile((playerX - 1) / 2, (playerY - 1) / 2 - 1) = ironT Then
                        iron += 1
                    End If
                    If tile((playerX - 1) / 2, (playerY - 1) / 2 - 1) = copperT Then
                        copper += 1
                    End If
                    If tile((playerX - 1) / 2, (playerY - 1) / 2 - 1) = woodT Then
                        wood += 1
                    End If
                    If tile((playerX - 1) / 2, (playerY - 1) / 2 - 1) = "▒" Then
                        stone += 1
                    End If
                    tile((playerX - 1) / 2, (playerY - 1) / 2 - 1) = " "
                End If
                handy = Not handy
            Case Is = "q"

                handy = Not handy
            Case Is = "e"
                level = Not level
            Case Is = "m"
                Save()
        End Select
    End Sub
    Sub Attack(ByVal direction As Char)
        Dim x1, x2, y1, y2 As Integer
        Select Case direction
            Case Is = "i"
                x1 = -4
                x2 = 2
                y1 = 0
                y2 = 3
            Case Is = "j"
                x1 = -4
                x2 = -2
                y1 = -4
                y2 = 2
            Case Is = "k"
                x1 = -4
                x2 = 2
                y1 = -5
                y2 = -2
            Case Is = "l"
                x1 = 0
                x2 = 3
                y1 = -4
                y2 = 2
        End Select
        Dim remove As List(Of entitiy) = New List(Of entitiy)
        For Each creature As entitiy In entities
            If creature.x > playerX + x1 And creature.x < playerX + x2 Then
                If creature.y > playerY + y1 And creature.y < playerY + y2 Then
                    remove.Add(creature)
                    If Int((3 * Rnd()) + 1) = "1" Then
                        wood += 1
                    End If
                    If Int((4 * Rnd()) + 1) = "1" Then
                        stone += 1
                    End If
                    If Int((12 * Rnd()) + 1) = "1" Then
                        copper += 1
                    End If
                    If Int((15 * Rnd()) + 1) = "1" Then
                        iron += 1
                    End If
                End If
            End If
        Next
        For Each creature As entitiy In remove
            entities.Remove(creature)
        Next
    End Sub
    Sub Main()
b:
        Console.Clear()
        Console.WriteLine("▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓")
        Console.WriteLine("▓ ▓▓▓ ▓▓ ▓▓   ▓▓ ▓▓▓   ▓▓▓  ▓▓▓ ▓▓  ▓  ▓   ▓")
        Console.WriteLine("▓ ▓ ▓ ▓ ▓ ▓ ▓▓ ▓ ▓▓▓ ▓▓ ▓ ▓▓▓▓ ▓ ▓ ▓ ▓ ▓ ▓▓▓")
        Console.WriteLine("▓ ▓ ▓ ▓ ▓ ▓   ▓▓ ▓▓▓ ▓▓ ▓ ▓  ▓   ▓ ▓▓▓ ▓ ▄▄▓")
        Console.WriteLine("▓▓ ▓ ▓▓▓ ▓▓ ▓▓ ▓   ▓   ▓▓▓  ▓▓ ▓ ▓ ▓▓▓ ▓   ▓")
        Console.WriteLine("▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓")
        Console.WriteLine("▓By Adrian Gjonca▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓")
        Console.WriteLine("▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓")
        Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░")
        Console.WriteLine("░1)Continue░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░")
        Console.WriteLine("░2)New Game░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░")
        Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░")
        Console.WriteLine("░3)Monochrome░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░")
        Console.WriteLine("░4)Red 'n Black░░░░░░░░░░░░░░░░░░░░░░░░░░░░░")
        Console.WriteLine("░5)MagenticYellow░░░░░░░░░░░░░░░░░░░░░░░░░░░")
        Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░")
        Console.WriteLine("░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░")
        Dim input As Char = Console.ReadKey.KeyChar
        Select Case input'Load()
            Case Is = "1"
                Randomize()
                Load()
                GoTo c
            Case Is = "2"
                Randomize()
                WorldGen()
                GoTo c
            Case Is = "3"
                Console.ForegroundColor = ConsoleColor.Gray
                Console.BackgroundColor = ConsoleColor.Black
            Case Is = "4"
                Console.ForegroundColor = ConsoleColor.DarkRed
                Console.BackgroundColor = ConsoleColor.Black
            Case Is = "5"
                Console.ForegroundColor = ConsoleColor.Yellow
                Console.BackgroundColor = ConsoleColor.Magenta
        End Select
        GoTo b
c:
        'entities.Add(New entitiy)
        tile = tileU
        playerY = 5009
        playerX = 5009
        tile = tileO
        entities = entitiesO
a:
        '''''Main Loop
        Render()
        TakeTurn()
        Zombify()
        If Not level Then
            tile = tileU
            entities = entitiesU
        Else
            tile = tileO
            entities = entitiesO
        End If
        If Not level Then
            tileU = tile
            entitiesU = entities
        Else
            tileO = tile
            entitiesO = entities
        End If

        ''''''End of Main loop
        GoTo a
    End Sub

    Sub Zombify()
        Dim remove As List(Of entitiy) = New List(Of entitiy)
        For Each creature As entitiy In entities
            ''''Ai Start
            Dim bestX As Integer = 0
            Dim bestY As Integer = 0
            If playerX < creature.x Then
                bestX -= 2
            End If
            If playerX > creature.x Then
                bestX += 2
            End If
            If playerY < creature.y Then
                bestY -= 2
            End If
            If playerY > creature.y Then
                bestY += 2
            End If

            If Math.Abs(playerY - creature.y) < 2 And Math.Abs(playerX - creature.x) < 2 Then
                If tile((playerX - 1) / 2, (playerY - 1) / 2) = " " Then
                    remove.Add(creature)
                    health -= 1
                    Console.Beep()
                End If
            End If

            If tile((creature.x) / 2, (creature.y + bestY) / 2) = " " Or tile((creature.x) / 2, (creature.y + bestY) / 2) = spawnerT Then
                For Each thing As entitiy In entities
                    If creature.y + bestY = thing.y Then
                        If creature.x = thing.x Then
                            GoTo leave
                        End If
                    End If
                Next
                creature.y += bestY
            End If
leave:
            If tile((creature.x + bestX) / 2, (creature.y) / 2) = " " Or tile((creature.x) / 2, (creature.y + bestY) / 2) = spawnerT Then
                For Each thing As entitiy In entities
                    If creature.y = thing.y Then
                        If creature.x + bestX = thing.x Then
                            GoTo leave2
                        End If
                    End If
                Next
                creature.x += bestX
            End If
leave2:
            ''''Ai End
            creature.life += 1
            If creature.life > 500 Then
                remove.Add(creature)
            End If
        Next
        For Each creature As entitiy In remove
            entities.Remove(creature)
        Next
        If Int((20 * Rnd()) + 1) = "1" Then
            For x As Integer = (playerX / 2) - 40 To (playerX / 2) + 40
                For y As Integer = (playerY / 2) - 40 To (playerY / 2) + 40
                    If tile(x, y) = spawnerT Then
                        Dim z As New entitiy
                        z.x = x * 2
                        z.y = y * 2
                        entities.Add(z)
                    End If
                Next
            Next
        End If
    End Sub

    Sub WorldGen()
        Console.Clear()
        Console.WriteLine("---Generating New World---")


        ''''''Caves
        Console.WriteLine("---Underground---")
        Console.WriteLine("Filling in stone...")
        For x As Integer = 0 To 5000
            For y As Integer = 0 To 5000
                tileU(x, y) = "▒"
            Next
        Next

        Console.WriteLine("Generating cave systems...")
        'Caves
        For x As Integer = 100 To 4900 Step 2
            For y As Integer = 100 To 4900 Step 2
                If Int((60 * Rnd()) + 1) = "1" Then
                    Dim ax = 0
                    Dim ay = 0
                    While Int((10 * Rnd()) + 1) <> "1"
                        tileU(x + ax, y + ay) = " "
                        tileU(x + ax + 1, y + ay) = " "
                        tileU(x + ax - 1, y + ay) = " "
                        tileU(x + ax, y + ay + 1) = " "
                        tileU(x + ax, y + ay - 1) = " "

                        Select Case Int((2 * Rnd()) + 1)
                            Case Is = "1"
                                ax += 1
                            Case Else
                                ax -= 1
                        End Select
                        Select Case Int((2 * Rnd()) + 1)
                            Case Is = "1"
                                ay += 1
                            Case Else
                                ay -= 1
                        End Select

                        If Int((20 * Rnd()) + 1) = "1" Then
                            tileU(x, y) = spawnerT
                        End If

                    End While
                End If
            Next
        Next

        Console.WriteLine("Planting ores:")
        Console.WriteLine("of iron...")
        'Ores
        For x As Integer = 100 To 4900 Step 2
            For y As Integer = 100 To 4900 Step 2
                If Int((60 * Rnd()) + 1) = "1" Then
                    Dim ax = 0
                    Dim ay = 0
                    While Int((6 * Rnd()) + 1) <> "1"
                        tileU(x + ax, y + ay) = ironT
                        tileU(x + ax + 1, y + ay) = ironT
                        tileU(x + ax - 1, y + ay) = ironT
                        tileU(x + ax, y + ay + 1) = ironT
                        tileU(x + ax, y + ay - 1) = ironT

                        Select Case Int((2 * Rnd()) + 1)
                            Case Is = "1"
                                ax += 1
                            Case Else
                                ax -= 1
                        End Select
                        Select Case Int((2 * Rnd()) + 1)
                            Case Is = "1"
                                ay += 1
                            Case Else
                                ay -= 1
                        End Select
                    End While
                End If
            Next
        Next
        Console.WriteLine("of copper...")
        For x As Integer = 100 To 4900 Step 2
            For y As Integer = 100 To 4900 Step 2
                If Int((60 * Rnd()) + 1) = "1" Then
                    Dim ax = 0
                    Dim ay = 0
                    While Int((6 * Rnd()) + 1) <> "1"
                        tileU(x + ax, y + ay) = copperT
                        tileU(x + ax + 1, y + ay) = copperT
                        tileU(x + ax - 1, y + ay) = copperT
                        tileU(x + ax, y + ay + 1) = copperT
                        tileU(x + ax, y + ay - 1) = copperT

                        Select Case Int((2 * Rnd()) + 1)
                            Case Is = "1"
                                ax += 1
                            Case Else
                                ax -= 1
                        End Select
                        Select Case Int((2 * Rnd()) + 1)
                            Case Is = "1"
                                ay += 1
                            Case Else
                                ay -= 1
                        End Select
                    End While
                End If
            Next
        Next

        Console.WriteLine("Building house...")
        'House
        For ax As Integer = 0 To 9
            For ay As Integer = 0 To 9
                tileU(2500 + ax, 2500 + ay) = " "
                If ax = 0 Or ax = 9 Then
                    tileU(2500 + ax, 2500 + ay) = "▓"
                ElseIf ay = 0 And ax <> 5 And ax <> 4 Then
                    tileU(2500 + ax, 2500 + ay) = "▓"
                ElseIf ay = 9 Then
                    tileU(2500 + ax, 2500 + ay) = "▓"
                End If
            Next
        Next




        ''''''World
        Console.WriteLine("---Underground---")
        Console.WriteLine("Planting trees...")
        For x As Integer = 0 To 5000
            For y As Integer = 0 To 5000
                tileO(x, y) = woodT
            Next
        Next

        'Clearings
        Console.WriteLine("Deforestation...")
        For x As Integer = 100 To 4900 Step 2
            For y As Integer = 100 To 4900 Step 2
                If Int((30 * Rnd()) + 1) = "1" Then
                    Dim ax = 0
                    Dim ay = 0
                    While Int((20 * Rnd()) + 1) <> "1"
                        tileO(x + ax, y + ay) = " "
                        tileO(x + ax + 1, y + ay) = " "
                        tileO(x + ax - 1, y + ay) = " "
                        tileO(x + ax, y + ay + 1) = " "
                        tileO(x + ax, y + ay - 1) = " "

                        tileO(x + ax + 1, y + ay + 1) = " "
                        tileO(x + ax - 1, y + ay + 1) = " "
                        tileO(x + ax + 1, y + ay - 1) = " "
                        tileO(x + ax - 1, y + ay - 1) = " "

                        tileO(x + ax + 2, y + ay) = " "
                        tileO(x + ax - 2, y + ay) = " "
                        tileO(x + ax, y + ay + 2) = " "
                        tileO(x + ax, y + ay - 2) = " "

                        tileO(x + ax + 2, y + ay + 1) = " "
                        tileO(x + ax - 2, y + ay - 1) = " "
                        tileO(x + ax + 1, y + ay + 2) = " "
                        tileO(x + ax - 1, y + ay - 2) = " "
                        Select Case Int((2 * Rnd()) + 1)
                            Case Is = "1"
                                ax += 1
                            Case Else
                                ax -= 1
                        End Select
                        Select Case Int((2 * Rnd()) + 1)
                            Case Is = "1"
                                ay += 1
                            Case Else
                                ay -= 1
                        End Select
                        If Int((30 * Rnd()) + 1) = "1" Then
                            tileO(x, y) = spawnerT
                        End If

                    End While
                End If
            Next
        Next
        'Lakes
        Console.WriteLine("Lakes...")
        For x As Integer = 100 To 4900 Step 2
            For y As Integer = 100 To 4900 Step 2
                If Int((160 * Rnd()) + 1) = "1" Then
                    Dim ax = 0
                    Dim ay = 0
                    While Int((9 * Rnd()) + 1) <> "1"
                        tileO(x + ax, y + ay) = "~"
                        tileO(x + ax + 1, y + ay) = "~"
                        tileO(x + ax - 1, y + ay) = "~"
                        tileO(x + ax, y + ay + 1) = "~"
                        tileO(x + ax, y + ay - 1) = "~"

                        'tileO(x + ax + 1, y + ay + 1) = "~"
                        'tileO(x + ax - 1, y + ay + 1) = "~"
                        'tileO(x + ax + 1, y + ay - 1) = "~"
                        'tileO(x + ax - 1, y + ay - 1) = "~"

                        'tileO(x + ax + 2, y + ay) = "~"
                        'tileO(x + ax - 2, y + ay) = "~"
                        'tileO(x + ax, y + ay + 2) = "~"
                        'tileO(x + ax, y + ay - 2) = "~"

                        'tileO(x + ax + 2, y + ay + 1) = "~"
                        'tileO(x + ax - 2, y + ay - 1) = "~"
                        'tileO(x + ax + 1, y + ay + 2) = "~"
                        'tileO(x + ax - 1, y + ay - 2) = "~"
                        Select Case Int((2 * Rnd()) + 1)
                            Case Is = "1"
                                ax += 1
                            Case Else
                                ax -= 1
                        End Select
                        Select Case Int((2 * Rnd()) + 1)
                            Case Is = "1"
                                ay += 1
                            Case Else
                                ay -= 1
                        End Select
                    End While
                End If
            Next
        Next



        '''''Structures
        Console.WriteLine("---Structures---")
        Console.WriteLine("Aincient Temples of Zomb...")
        For i As Integer = 0 To 5000
            Dim sx As Integer = Int((4500 * Rnd()) + 100)
            Dim sy As Integer = Int((4500 * Rnd()) + 100)
            For ax As Integer = 0 To 11
                For ay As Integer = 0 To 9
                    tileO(sx + ay, sy + -ax) = Dungeons.AincientDungeon(ax, ay)
                Next
            Next
        Next
        'House
        Console.WriteLine("House...")
        For ax As Integer = 0 To 9
            For ay As Integer = 0 To 9
                tileO(2500 + ax, 2500 + ay) = " "
                If ax = 0 Or ax = 9 Then
                    tileO(2500 + ax, 2500 + ay) = "▓"
                ElseIf ay = 0 And ax <> 5 And ax <> 4 Then
                    tileO(2500 + ax, 2500 + ay) = "▓"
                ElseIf ay = 9 Then
                    tileO(2500 + ax, 2500 + ay) = "▓"
                End If
            Next
        Next
        Console.WriteLine("---Finished---")
        Console.ReadKey()
    End Sub

    Sub Save()
        Dim Folder As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        Dim format As New BinaryFormatter()
        Dim over As String = Folder & "\World_Game_Over.dat"
        Dim under As String = Folder & "\World_Game_Under.dat"

        If File.Exists(over) Then File.Delete(over)
        If File.Exists(under) Then File.Delete(under)

        Dim stream As FileStream = File.Create(over)
        Console.WriteLine("Saving Overworld")
        format.Serialize(stream, tileO)
        stream.Close()
        stream = File.Create(under)
        Console.WriteLine("Saving Underworld")
        format.Serialize(stream, tileU)
        stream.Close()
    End Sub

    Sub Load()
        Dim Folder As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        Dim format As New BinaryFormatter()

        Dim over As String = Folder & "\World_Game_Over.dat"
        Dim under As String = Folder & "\World_Game_Under.dat"

        Dim stream As FileStream = File.OpenRead(over)

        Console.WriteLine("Loading Overworld")
        tileO = format.Deserialize(stream)
        stream.Close()

        stream = File.OpenRead(under)

        Console.WriteLine("Loading Underworld")
        tileU = format.Deserialize(stream)
        stream.Close()
    End Sub
End Module
