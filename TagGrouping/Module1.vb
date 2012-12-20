﻿Imports System.IO
Imports System.Text.RegularExpressions

Module Module1
    Private TagLocation As Dictionary(Of String, String)
    Sub Main()


        Dim elementTagLocations As New Dictionary(Of String, Dictionary(Of String, String))

        Dim elementName As String = String.Empty
        Dim taglistString As String = "EBLK,DCLK,ABCD"
        Dim locationlistString As String = "120,130;121,142;135,150"

        Dim tagLocationDictionary As New Dictionary(Of String, String)
        getTagLocationCollection(tagLocationDictionary, taglistString, locationlistString)

        elementTagLocations.Add("element1", tagLocationDictionary)

        Dim startReading As Boolean = False

        Dim targetDrawingLines As New List(Of String)(File.ReadAllLines("C:\Users\vamsi\Documents\TestFile.txt", System.Text.Encoding.Default))
        Dim currentLine As String = String.Empty
        Dim currentStartTagLine As String = String.Empty
        Dim startUpdatingLocation As Boolean = False
        Dim currrentElementKey As String = String.Empty
        '^(([0-9],?)+)$
        Dim updateLine As Integer = 0
        For i As Integer = 0 To targetDrawingLines.Count - 1 Step 1
            currentLine = targetDrawingLines(i)
            If currentLine.StartsWith(":") Then
                'this is a start tag
                currentStartTagLine = currentLine
            Else
                startReading = True
            End If

            If startReading Then
                currentLine = currentLine.TrimEnd(New Char() {";"c})
                Dim subitems() As String = currentLine.Split(New Char() {":"c})
                For Each item As String In subitems
                    If elementTagLocations.ContainsKey(item) Then

                    End If
                Next

            End If

        Next

    End Sub

    Private Function getTagLocationCollection(ByRef tagLocationDictionary As Dictionary(Of String, String), _
                                              ByVal taglistString As String, _
                                              ByVal locationlistString As String) As Boolean
        Dim isSuccess As Boolean = False

        Dim tagList() As String = taglistString.Split(New Char() {","c})

        Dim locationList() As String = locationlistString.Split(New Char() {";"c})

        If (tagList.Length = locationList.Length) Then
            For index As Integer = 0 To tagList.Length - 1
                tagLocationDictionary.Add(tagList(index), locationList(index))
            Next

        End If




        Return isSuccess
    End Function
End Module