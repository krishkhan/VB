Imports System.IO
Imports System.Text.RegularExpressions

Module Module1
    Private TagLocation As Dictionary(Of String, String)
    Sub Main()


        Dim elementTagLocations As New Dictionary(Of String, Dictionary(Of String, String))

        Dim elementName As String = String.Empty
        Dim taglistString As String = "ETCM,ESCL,EINP,EUNT,ETAG,PIR!"
        Dim locationlistString As String = "120,130;121,142;135,150;123,111;124,456;123,456"

        Dim tagLocationDictionary As New Dictionary(Of String, String)
        getTagLocationCollection(tagLocationDictionary, taglistString, locationlistString)

        elementTagLocations.Add("%%I01PT001", tagLocationDictionary)

        Dim startReading As Boolean = False

        Dim targetDrawingLines As New List(Of String)(File.ReadAllLines("C:\Users\vamsi\Documents\TestFile.txt", System.Text.Encoding.Default))
        Dim currentLine As String = String.Empty
        Dim currentStartTagLine As String = String.Empty
        Dim startUpdatingLocation As Boolean = False
        Dim currentElementKey As String = String.Empty
        '^(([0-9],?)+)$
        Dim updateLine As Integer = 0
        For i As Integer = 0 To targetDrawingLines.Count - 1 Step 1
            currentLine = targetDrawingLines(i)
            Console.WriteLine(currentLine)
            If currentLine.StartsWith(":") Then
                'this is a start tag
                currentStartTagLine = currentLine
                'reset the values
                startUpdatingLocation = False
                currentElementKey = String.Empty
            ElseIf Not startUpdatingLocation Then
                startReading = True
            End If

            If startReading Then
                currentLine = currentLine.TrimEnd(New Char() {";"c})
                Dim subitems() As String = currentLine.Split(New Char() {":"c})
                If Not startUpdatingLocation Then
                    For Each item As String In subitems
                        If elementTagLocations.ContainsKey(item) Then
                            currentElementKey = item
                            startUpdatingLocation = True
                        End If
                    Next
                Else
                    'update the location
                    Dim blockCommentName As String = subitems(3)

                    Dim tagValueDict As Dictionary(Of String, String) = elementTagLocations(currentElementKey)
                    If tagValueDict.ContainsKey(blockCommentName) Then
                        Dim blockCommentLocation As String = tagValueDict(blockCommentName)
                        If Regex.IsMatch(currentLine, "(?<x>[0-9]+),(?<y>[0-9]+)") Then
                            Dim replaceString1 As String = Regex.Replace(currentLine, "(?<x>[0-9]+),(?<y>[0-9]+)", blockCommentLocation)
                        End If
                    End If
                End If 'end of update location
            End If 'end of start reading
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

    'Dim drawingSheetRule As String = DrawingSheetName.Trim
    'Dim columnName As String = DrawingSheetHelper.getDrawingSheetColumnName(drawingSheetRule)
    'Dim validSheetName As String = String.Empty
    'Dim drawingSheetNumber As Integer
    'Dim dbTable As DataTable = TableFactory.TaglistTable.MyTableData
    'Dim tagListTableView As New DataView(TableFactory.TaglistTable.MyTableData)
    '    tagListTableView.Sort = "[" & TableFactory.TypnoColumnName & "] ASC, [" & TableFactory.LoopColumnName & "] ASC, [" & TableFactory.FcsColumnName & "] ASC, [" & TableFactory.PidTagColumnName & "] ASC"
    'Dim rowNum As Integer = 0
    'Dim expression As String = String.Empty
    '    For Each generationRow As GenerationDataSet.GenerationRow In generationTable.Rows
    ''only check loops that will be generated
    '        If Not generationRow.IsForGenerationNull() AndAlso generationRow.ForGeneration Then
    '            expression = String.Format("[LOOP] LIKE '{0}' AND [TYPNO] LIKE '{1}' AND [FCS] LIKE '{2}'", _
    '                                                      generationRow.TypicalName, generationRow.LoopName, generationRow.StationName

    'Dim rows As DataRow() = dbTable.Select(expression)
    '            If rows.Length > 0 Then
    '                If Not columnName.Equals(String.Empty) Then
    '                    validSheetName = rows(0).Item(columnName).ToString()
    '                    validSheetName = DrawingSheetHelper.embeddTagValInDrawingSheetName(drawingSheetRule, validSheetName)
    '                Else
    '                    validSheetName = drawingSheetRule
    '                End If
    '                drawingSheetNumber = CInt(rows(0).Item("DRAWNO"))
    '            End If
    'Dim DrawingWithFCS As New DrawingDetailsWithFCS(drawingSheetNumber, validSheetName, generationRow.StationName)
    '            SelectedlistofdrawingfilewithFcs.Add(DrawingWithFCS)
    '        End If
    '        rowNum += 1
    '    Next
End Module
