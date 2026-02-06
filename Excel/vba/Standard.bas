Attribute VB_Name = "Standard"
Const LABEL_ROW As Integer = 4
Const DROPDOWN_ROW As Integer = 5
Const DATA_ROW As Integer = 6
Const RULE_ROW As Integer = 2
Const RULE_COLUMN As Integer = 4

Sub UpdateFormula(ByVal Target As Range)
    sCellName = Cells(DROPDOWN_ROW, Target.Column).Value
    If sCellName <> "" Then
        ' 名前が設定されている場合リストがあるか確認
        sIsNames = False
        For Each Data In ActiveWorkbook.Names
            If sCellName = Data.name Then
                sIsNames = True
                Sheets("output").Cells(Target.Row, Target.Column).Value = "=IF(ISBLANK(" & ActiveSheet.name & "!" & Target.Address & "),-1,MATCH(" & ActiveSheet.name & "!" & Target.Address & "," & Data.name & ", 0) - 1)"
            End If
        Next
        If sIsNames = False Then
            Sheets("output").Cells(Target.Row, Target.Column).Value = "=IF(ISBLANK(" & ActiveSheet.name & "!" & Target.Address & "),-1," & ActiveSheet.name & "!" & Target.Address & ")"
        End If
    Else
        Sheets("output").Cells(Target.Row, Target.Column).Value = "=IF(ISBLANK(" & ActiveSheet.name & "!" & Target.Address & "),-1," & ActiveSheet.name & "!" & Target.Address & ")"
    End If
End Sub

' ルールに応じてrefシートを更新
Sub UpdateRef()

End Sub

' ルールに応じてドロップダウンを設定
Sub SetDropDown()
    sDataNum = Cells(Rows.Count, "A").End(xlUp).Row
    sColumn = Cells(LABEL_ROW, Columns.Count).End(xlToLeft).Column
    For i = 1 To sColumn
        sCellName = Cells(DROPDOWN_ROW, i).Value
        If sCellName <> "" Then
            ' 名前が設定されている場合リストがあるか確認
            sIsNames = False
            For Each Data In ActiveWorkbook.Names
                If sCellName = Data.name Then
                    sIsNames = True
                End If
            Next
            ' 範囲指定してデータ設定
            For j = 1 To sDataNum
                With Cells(j, i).Validation
                    .Delete
                    If sIsNames = True Then
                        .Add Type:=xlValidateList, _
                            Operator:=xlEqual, _
                            Formula1:="=" & sCellName
                    End If
                End With
            Next
        End If
    Next
End Sub

' ルールをロード
Sub LoadRule()
    Call SelectRule
    Call UpdateRef
    Call SetDropDown
End Sub

' ルールを選択
Sub SelectRule()
    ' フォルダを選ぶ
    Dim sFolderPath As String
    With Application.FileDialog(msoFileDialogFolderPicker)
        .InitialFileName = ThisWorkbook.path & "\output"
        ' フォルダが選ばれたらフォルダ名を登録
        If .Show = -1 Then
            sFolderPath = .SelectedItems(1)
            sFolderName = Mid(sFolderPath, InStrRev(sFolderPath, "\") + 1)
            Cells(RULE_ROW, RULE_COLUMN).Value = sFolderName
        End If
    End With
End Sub

' データ読み込み
Sub LoadData()
    ' テキストを開く
    sFileName = ThisWorkbook.path & "\output\" & Cells(RULE_ROW, RULE_COLUMN) & "\" & Worksheets(1).name & ".txt"
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
            Cells(i + DATA_ROW, j + 1).Value = sCellText(j)
        Next
    Next
End Sub

' 出力
Sub Output()
    Dim sRow As Integer
    Dim sColumn As Integer
    
    ' テキストを開く
    sFileName = ThisWorkbook.path & "\output\" & Cells(RULE_ROW, RULE_COLUMN) & "\" & Worksheets(1).name & ".txt"
    With CreateObject("ADODB.Stream")
        .Charset = "UTF-8"
        .Open
        .LineSeparator = -1
        
        ' カード名・入力欄の端
        sRow = DATA_ROW
        sColumn = Cells(LABEL_ROW, Columns.Count).End(xlToLeft).Column
        
        ' データのある行まで出力
        Do While Cells(sRow, 2).Value <> ""
            Dim sRowText As String
            sRowText = ""
            For i = 1 To sColumn
                If i <> 1 Then
                    sRowText = sRowText & ","
                End If
                sRowText = sRowText & Cells(sRow, i).Value
            Next i
            .WriteText sRowText, 1
            sRow = sRow + 1
        Loop
        .SaveToFile sFileName, 2
        .Close
    End With
End Sub
