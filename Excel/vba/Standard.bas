Attribute VB_Name = "Standard"
Const START_ROW As Integer = 6
Const RULE_ROW As Integer = 1
Const RULE_COLUMN As Integer = 4

Sub LoadRule()
    ' フォルダを選ぶ
    Dim folderPath As String
    With Application.FileDialog(msoFileDialogFolderPicker)
        .InitialFileName = ThisWorkbook.Path & "\output"
        ' フォルダが選ばれたらフォルダ名を登録
        If .Show = -1 Then
            folderPath = .SelectedItems(1)
            folderName = Mid(folderPath, InStrRev(folderPath, "\") + 1)
            Cells(RULE_ROW, RULE_COLUMN).Value = folderName
        End If
    End With
End Sub

Sub LoadData()
    ' テキストを開く
    sFileName = ThisWorkbook.Path & "\output\" & Cells(RULE_ROW, RULE_COLUMN) & "\" & Worksheets(1).Name & ".txt"
    Dim sAllText As String
    Dim sLineMax As Integer
    Dim sCellMax As Integer
    With CreateObject("ADODB.Stream")
        .Charset = "UTF-8"
        .Open
        .LoadFromFile sFileName
        sAllText = .ReadText
        .Close
    End With
    
    sLineText = Split(sAllText, vbCrLf)
    sLineMax = UBound(sLineText)
    
    ' カンマごとにデータを分けて書き込み
    For i = 0 To sLineMax
        sCellText = Split(sLineText(i), ",")
        sCellMax = UBound(sCellText)
        For j = 0 To sCellMax
            Cells(i + START_ROW, j + 1).Value = sCellText(j)
        Next
    Next
End Sub

Sub Output()
    Dim Row As Integer
    Dim Column As Integer
    
    ' テキストを開く
    sFileName = ThisWorkbook.Path & "\output\" & Cells(RULE_ROW, RULE_COLUMN) & "\" & Worksheets(1).Name & ".txt"
    With CreateObject("ADODB.Stream")
        .Charset = "UTF-8"
        .Open
        .LineSeparator = -1
        
        ' カード名・入力欄の端
        Row = START_ROW
        Column = Cells(3, Columns.Count).End(xlToLeft).Column
        
        ' データのある行まで出力
        Do While Cells(Row, 2).Value <> ""
            Dim row_text As String
            row_text = ""
            For i = 1 To Column
                If i <> 1 Then
                    row_text = row_text & ","
                End If
                row_text = row_text & Cells(Row, i).Value
            Next i
            .WriteText row_text, 1
            Row = Row + 1
        Loop
        .SaveToFile sFileName, 2
        .Close
    End With
End Sub
