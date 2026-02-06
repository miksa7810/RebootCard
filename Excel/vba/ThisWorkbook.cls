VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "ThisWorkbook"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = True
' エクセルを開いたら行う
Private Sub Workbook_Open()
    ' 読み込み候補のVBAモジュール名リストを取得
    sFileName = ThisWorkbook.path & "\vba\auto_input_list.txt"
    Dim sAllText As String
    Dim sLineMax As Integer
    With CreateObject("ADODB.Stream")
        .Charset = "UTF-8"
        .Open
        .LoadFromFile sFileName
        sAllText = .ReadText
        .Close
    End With
    
    sLineText = Split(sAllText, vbCrLf)
    sLineMax = UBound(sLineText)
    
    ' 候補に記載されているVBAモジュールをインポート
    For i = 0 To sLineMax
        With ThisWorkbook.VBProject.VBComponents
            .Remove .Item(Left(sLineText(i), InStrRev(sLineText(i), ".") - 1))
        End With
        ThisWorkbook.VBProject.VBComponents.Import ThisWorkbook.path & "\vba\" & sLineText(i)
    Next
End Sub
