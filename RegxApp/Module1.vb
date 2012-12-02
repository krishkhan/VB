Imports System.Text.RegularExpressions

Module Module1

    Sub Main()
        Dim values() As String = {"ABD0123", "ABC-", "A123456789ABCDEF"}
        ' Dim pattern As String = "^[A-Z][0-9]"
        'Dim pattern As String = "^((%%)?[A-Z][A-Z0-9_-]*)?$|^%Z\d{16}$"
        Dim pattern As String = "^((%%)?[A-Z][A-Z0-9]*)?$|^%Z\d{16}$"
        For Each value As String In values
            If Regex.IsMatch(value, pattern) Then
                Console.WriteLine("{0} is a valid", value)
            Else
                Console.WriteLine("{0} is invalid", value)

            End If
        Next
    End Sub
End Module

