Imports System.Text.RegularExpressions

Module Module1

    

    Sub Main()

        Dim dictionary As New Dictionary(Of String, tagNameInformation)
        Calculate(dictionary)


        Dim pair As KeyValuePair(Of String, tagNameInformation)
        For Each pair In dictionary
            Console.WriteLine("{0} with {1} ,{2}", pair.Key, pair.Value.definitionName, pair.Value.elementName)
        Next


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

    Sub Calculate(ByRef dictionary As Dictionary(Of String, tagNameInformation))

        Dim tag As New tagNameInformation
        tag.definitionName = "Human"
        tag.elementName = "carbon"
        tag.typicalName = "vamson"
        tag.loopName = "Marriage"



        dictionary.Add("Vamsi1", tag)
        tag.definitionName = "Human1"
        tag.elementName = "carbon1"
        tag.typicalName = "vamson1"
        tag.loopName = "Marriage1"

        dictionary.Add("home", tag)


    End Sub

End Module

Module Utility
    Public Structure tagNameInformation
        Public typicalName As String
        Public loopName As String
        Public elementName As String
        Public definitionName As String
        Public tagName As String
    End Structure
End Module

'            expression = String.Format("[LOOP_NAME] LIKE '{0}' AND [TYPNO] LIKE '{1}'", tagInfo.loopName, 'tagInfo.typicalName)
            'Dim rows As DataRow() = dbTable.Select(expression)