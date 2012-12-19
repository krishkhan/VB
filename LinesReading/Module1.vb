Imports System.IO
Imports System.Text.RegularExpressions

Module Module1

    Private xTopPointCorrectionValue As Integer
    Private yTopPointCorrectionValue As Integer

    Sub Main()
        'Dim connectionInfoValue As String = String.Empty
        'connectionInfoValue = Regex.Replace(connectionInfoValue, _
        '                                                "(?<x>[0-9]+),(?<y>[0-9]+)", _
        '                                                New MatchEvaluator(AddressOf correctWiringInfoMatchEvaluator))


        Dim targetDrawingLines As New List(Of String)(File.ReadAllLines("C:\Users\vamsi\Documents\TestFile.txt", System.Text.Encoding.Default))
        '^(([0-9],?)+)$
        Dim updateLine As Integer = 0
        For i As Integer = targetDrawingLines.Count - 1 To 1 Step -1
            If Regex.IsMatch(targetDrawingLines(i), "^10:GBCM:") Then
                If Regex.IsMatch(targetDrawingLines(i), "(?<x>[0-9]+),(?<y>[0-9]+)") Then

                    Dim replaceString1 As String = Regex.Replace(targetDrawingLines(i), "(?<x>[0-9]+),(?<y>[0-9]+)", "122,133")
                    Dim replaceString2 As String = Regex.Replace(targetDrawingLines(i), "(?<x>[0-9]+),(?<y>[0-9]+)", "2,3")
                    Dim replaceString3 As String = Regex.Replace(targetDrawingLines(i), "(?<x>[0-9]+),(?<y>[0-9]+)", "2342,3789")

                    Console.WriteLine("Matched = {0} as {1}", targetDrawingLines(i), replaceString1)
                    Console.WriteLine("Matched = {0} as {1}", targetDrawingLines(i), replaceString2)
                    Console.WriteLine("Matched = {0} as {1}", targetDrawingLines(i), replaceString3)

                End If
            End If
        Next

    End Sub

    Private Function correctWiringInfoMatchEvaluator(ByVal m As Match) As String
        Return getCorrectedXandY(m, xTopPointCorrectionValue, yTopPointCorrectionValue)
    End Function

    Private Function getCorrectedXandY(ByVal m As Match, ByVal xTopPointCorrection As Integer, ByVal yTopPointCorrection As Integer) As String
        Dim xPoint As Integer = CInt(m.Groups("x").Value)
        Dim yPoint As Integer = CInt(m.Groups("y").Value)

        Dim xPointNew As Integer = xPoint + xTopPointCorrection
        Dim yPointNew As Integer = yPoint + yTopPointCorrection

        Dim currentLocationNew As String = CStr(xPointNew) & "," & CStr(yPointNew)

        Return currentLocationNew
    End Function
End Module
