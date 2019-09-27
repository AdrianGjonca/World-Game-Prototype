Module Module1
    Dim screen(40, 40) As Char
    Sub Render()
        Dim data As String = ""
        Console.Clear()
        For y As Integer = 0 To 40
            For x As Integer = 0 To 40
                data &= "*"
                'Console.Write(screen(x, y))
            Next
            data &= vbCrLf

        Next
        Console.WriteLine(data)
    End Sub
    Sub Main()
a:
        Render()
        Console.ReadKey()
        GoTo a
    End Sub

End Module
