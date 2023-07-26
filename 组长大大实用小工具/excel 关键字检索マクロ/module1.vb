Option Explicit

Const START_ROW = 20
Const START_ROW_LOG = 2

Const COLOR_RED = -16776961 '��
'****************************************************************
'�����u�b�N���̌����}�N���i�ΏہF�Z���ƃI�u�W�F�N�g�j
'****************************************************************
Sub main()
    Dim cInputKeys As Collection
    Dim rInputKeys As Excel.Range
    Dim likeKey() As String, key() As String
    Dim iRow As Long, iLog As Long
    Dim sPath As String
    Dim lastRow As Long, lastCol As Long
    Dim j As Long, i As Long, m As Long, n As Long, x As Long
    Dim targetSheet As Excel.Worksheet
    Dim targetRange As Variant
    Dim targetValue As String
    Dim targetCell As String
    Dim rowRow As Long
    Dim rowCol As Long
    Dim targetCount As Long
    Dim sp As Excel.Shapes, txt As String
    Dim objectSearchFlag As String
    Dim bFound As Boolean
    
    Dim sht As Excel.Worksheet
    Set sht = ThisWorkbook.Worksheets("�Ώۃu�b�N")
    
    'ONOFF�t���O
    objectSearchFlag = sht.Range("B5").Value
    
    '�����L�[
    Set cInputKeys = New Collection
    For Each rInputKeys In sht.Range("B8:B17")
        If rInputKeys.Value <> "" Then
            cInputKeys.Add rInputKeys.Value
        End If
    Next
    
    '���������̑啶���A�S�p��
    ReDim likeKey(1 To cInputKeys.Count)
    ReDim key(1 To cInputKeys.Count)
    
    Dim k As Integer
    For k = 1 To cInputKeys.Count
        '�Z���p��������
        likeKey(k) = "*" & StrConv(cInputKeys(k), vbWide + vbUpperCase) & "*"
        
        '�I�u�W�F�N�g���p��������
        key(k) = StrConv(cInputKeys(k), vbWide + vbUpperCase)
    Next
    
    '�Ώۃp�X�p�s
    Dim nTotal As Integer
    nTotal = 0
    
    iRow = START_ROW
    While sht.Cells(iRow, 1).Value <> "" Or _
          sht.Cells(iRow, 2).Value <> ""
        nTotal = nTotal + 1
        iRow = iRow + 1
    Wend
    sht.Range("20:" & CStr(iRow)).Font.ColorIndex = xlColorIndexAutomatic
    
    '���ʃV�[�g������
    Dim objSht As Excel.Worksheet
    Set objSht = ThisWorkbook.Worksheets("����")
    objSht.Activate
    objSht.Cells(1, 1).Select
    
    '���ʃV�[�g�p�̍s�J�E���^
    iLog = START_ROW_LOG
    While objSht.Cells(iLog, 1).Value <> ""
        iLog = iLog + 1
    Wend
    
    Dim nYesNo As Integer
    nYesNo = MsgBox("Excel�u�b�N���̌������s���܂��B" & vbLf & _
                    "�Ώۃu�b�N���F" & Format(nTotal, "#,##0") & "�{" & vbLf & vbLf & _
                    "���ʃV�[�g���N���A���܂����H" & vbLf & _
                    "�͂�" & vbTab & "���ʃV�[�g���N���A����" & vbLf & _
                    "������" & vbTab & "�N���A�����ɒǋL����" & vbLf & _
                    "�L�����Z��" & vbTab & "�����𒆎~����", _
                    vbQuestion + vbYesNoCancel, _
                    "�����񌟍�")
    If nYesNo = vbCancel Then Exit Sub
    If nYesNo = vbYes Then
        With objSht
            '�I�[�g�t�B���^������
            If .AutoFilterMode Then .AutoFilterMode = False
            
            '�S���폜
            .Range("2:" & CStr(.UsedRange.Rows.Count)).Delete shift:=xlShiftUp
            
            '���ʃV�[�g�̃^�C�g����ݒ�
            .Cells(1, 1).Value = "�u�b�N��"
            .Cells(1, 2).Value = "�V�[�g��"
            .Cells(1, 3).Value = "��v�ӏ�"
            .Cells(1, 4).Value = "������"
            .Cells(1, 5).Value = "�O���[�v������"
            .Cells(1, 6).Value = "�p�X"
            With .Range(.Cells(1, 1), .Cells(1, 6)).Borders(xlEdgeBottom)
                .LineStyle = XlLineStyle.xlContinuous
                .Weight = XlBorderWeight.xlThin
                .ColorIndex = XlColorIndex.xlColorIndexAutomatic
            End With
        End With
    End If
            
            
    '�J�n�����ݒ�A�I���ɓ����N���A
    sht.Activate
    sht.Range("D11").Value = Date + Time
    sht.Range("D13").Value = Empty
    
    Call SCREEN_UP_CONTROLL(False)
    Application.DisplayAlerts = False
    
    Dim nCount As Integer
    nCount = 0
    
    '�Ώۃu�b�N�������Ȃ�܂Ń��[�v�i�󔒃Z�������݂����烋�[�v�I���j
    iRow = START_ROW
    While sht.Cells(iRow, 1).Value <> "" Or _
          sht.Cells(iRow, 2).Value <> ""
        
        nCount = nCount + 1
        sPath = sht.Cells(iRow, 2).Value
        
        Application.StatusBar = CStr(nCount) & "/" & CStr(nTotal) & " - " & sPath
        
        '�Ώہ��ȊO�̓X�L�b�v����
        If sht.Cells(iRow, 1).Value <> "��" Then GoTo SKIP_THIS_BOOK
        
        '�ǂݎ���p�őΏۃu�b�N���J��
        Dim objBook As Excel.Workbook
        Set objBook = Nothing
        
        On Error Resume Next
        Set objBook = Workbooks.Open(Filename:=sPath, ReadOnly:=True, UpdateLinks:=False)
        On Error GoTo 0
        
        If objBook Is Nothing Then
            sht.Cells(iRow, 2).Font.Color = COLOR_RED
            GoTo SKIP_THIS_BOOK
        End If
        
        '�V�[�g���������J��Ԃ�
        For j = 1 To objBook.Sheets.Count
            Set targetSheet = objBook.Worksheets(j)
            
            objBook.Activate
            targetSheet.Activate
            
            '�Z��������
            targetRange = targetSheet.UsedRange
            targetCount = targetSheet.UsedRange.Count
            rowRow = targetSheet.UsedRange.Row - 1
            rowCol = targetSheet.UsedRange.Column - 1
            
            If targetCount = 1 Then
                targetValue = IIf(IsError(targetRange), "", targetRange)
                targetCell = Trim(targetValue)
                targetCell = Replace(targetCell, vbCr, "")
                targetCell = Replace(targetCell, vbLf, "")
                targetCell = StrConv(StrConv(targetCell, vbWide), vbUpperCase)
                bFound = False
                For k = LBound(likeKey) To UBound(likeKey)
                    If targetCell Like likeKey(k) Then bFound = True: Exit For
                Next
                If bFound = True Then
                    With objSht
                        .Cells(iLog, 1).Value = targetSheet.Parent.Name
                        .Cells(iLog, 2).Value = targetSheet.Name
                        .Cells(iLog, 3).Value = "�s" & 1 + rowRow & " ��" & 1 + rowCol
                        .Cells(iLog, 4).Value = "'" & targetValue
                        .Cells(iLog, 5).Value = "-"
                        .Cells(iLog, 6).Value = sPath
                        .Hyperlinks.Add anchor:=.Cells(iLog, 1), _
                            Address:=sPath & "#'" & targetSheet.Name & "'!" & targetSheet.Cells(1, 1).Address, _
                            TextToDisplay:=targetSheet.Parent.Name
                    End With
                    iLog = iLog + 1
                End If
            Else
                '�ő�s�܂Ń��[�v
                For m = 1 To UBound(targetRange, 1)
                    '�ő�s�܂Ń��[�v
                    For n = 1 To UBound(targetRange, 2)
                        targetValue = IIf(IsError(targetRange(m, n)), "", targetRange(m, n))
                        targetCell = Trim(targetValue)
                        targetCell = Replace(targetCell, vbCr, "")
                        targetCell = Replace(targetCell, vbLf, "")
                        targetCell = StrConv(StrConv(targetCell, vbWide), vbUpperCase)
                        bFound = False
                        For k = LBound(likeKey) To UBound(likeKey)
                            If targetCell Like likeKey(k) Then bFound = True: Exit For
                        Next
                        If bFound = True Then
                            With objSht
                                .Cells(iLog, 1).Value = targetSheet.Parent.Name
                                .Cells(iLog, 2).Value = targetSheet.Name
                                .Cells(iLog, 3).Value = "�s" & m + rowRow & " ��" & n + rowCol
                                .Cells(iLog, 4).Value = "'" & targetValue
                                .Cells(iLog, 5).Value = "-"
                                .Cells(iLog, 6).Value = sPath
                                .Hyperlinks.Add anchor:=.Cells(iLog, 1), _
                                    Address:=sPath & "#'" & targetSheet.Name & "'!" & targetSheet.Cells(m, n).Address, _
                                    TextToDisplay:=targetSheet.Parent.Name
                            End With
                            iLog = iLog + 1
                        End If
                    Next n
                Next m
            End If
            
            '�I�u�W�F�N�g������
            If objectSearchFlag = "ON" Then
                Set sp = targetSheet.Shapes
                For i = 1 To sp.Count
                    '�P��I�u�W�F�N�g�p
                    If (sp.Item(i).Type = msoAutoShape Or sp.Item(i).Type = msoTextBox) Then
                        targetValue = ""
                        On Error Resume Next
                        targetValue = sp.Item(i).DrawingObject.Caption
                        On Error GoTo 0
                        txt = Trim(targetValue)
                        txt = Replace(txt, vbCr, "")
                        txt = Replace(txt, vbLf, "")
                        txt = StrConv(txt, vbWide + vbUpperCase)
                        If txt <> "" Then
                            bFound = False
                            For k = LBound(key) To UBound(key)
                                If InStr(txt, key(k)) > 0 Then bFound = True: Exit For
                            Next
                            If bFound = True Then
                                With objSht
                                    .Cells(iLog, 1).Value = targetSheet.Parent.Name
                                    .Cells(iLog, 2).Value = targetSheet.Name
                                    .Cells(iLog, 3).Value = sp.Item(i).Name
                                    .Cells(iLog, 4).Value = "'" & targetValue
                                    .Cells(iLog, 5).Value = "-"
                                    .Cells(iLog, 6).Value = sPath
                                    .Hyperlinks.Add anchor:=.Cells(iLog, 1), _
                                        Address:=sPath & "#'" & targetSheet.Name & "'!" & sp.Item(i).TopLeftCell.Address, _
                                        TextToDisplay:=targetSheet.Parent.Name
                                End With
                                iLog = iLog + 1
                            End If
                        End If
                    End If
                    
                    '�O���[�v�����Ă���I�u�W�F�N�g�p
                    If (sp.Item(i).Type = msoGroup) Then
                        For x = 1 To sp.Item(i).GroupItems.Count
                            targetValue = ""
                            On Error Resume Next
                            targetValue = sp.Item(i).GroupItems(x).AlternativeText
                            On Error GoTo 0
                            txt = Trim(targetValue)
                            txt = Replace(txt, vbCr, "")
                            txt = Replace(txt, vbLf, "")
                            txt = StrConv(txt, vbWide + vbUpperCase)
                            If txt <> "" Then
                                bFound = False
                                For k = LBound(key) To UBound(key)
                                    If InStr(txt, key(k)) > 0 Then bFound = True: Exit For
                                Next
                                If bFound = True Then
                                    With objSht
                                        .Cells(iLog, 1).Value = targetSheet.Parent.Name
                                        .Cells(iLog, 2).Value = targetSheet.Name
                                        .Cells(iLog, 3).Value = sp.Item(i).Name
                                        .Cells(iLog, 4).Value = "'" & targetValue
                                        .Cells(iLog, 5).Value = sp.Item(i).GroupItems(x).Name
                                        .Cells(iLog, 6).Value = sPath
                                        .Hyperlinks.Add anchor:=.Cells(iLog, 1), _
                                            Address:=sPath & "#'" & targetSheet.Name & "'!" & sp.Item(i).GroupItems(x).TopLeftCell.Address, _
                                            TextToDisplay:=targetSheet.Parent.Name
                                    End With
                                    iLog = iLog + 1
                                End If
                            End If
                        Next x
                    End If
                Next i
            End If
            
        Next j
        
        '�ۑ������ɕ���
        objBook.Saved = True
        objBook.Close
        
        sht.Cells(iRow, 1).Value = "��"

SKIP_THIS_BOOK:
        iRow = iRow + 1
    Wend
    Application.StatusBar = ""
    
    With objSht
        .Parent.Activate
        .Activate
        .Cells.Select
        .Cells.EntireColumn.AutoFit
        .Cells.EntireRow.AutoFit
        .Range("1:1").Select
        .Range("1:1").AutoFilter
        .Range("A1").Select
    End With
    
    '�I�������ݒ�
    sht.Range("D13").Value = Date + Time
    
    Call SCREEN_UP_CONTROLL(True)
    
    Dim nLaps As Long
    nLaps = DateDiff("s", sht.Range("D11").Value, sht.Range("D13").Value)
    
    MsgBox "�I�����܂���" & vbLf & _
            "�������ʁF" & Format(iLog - 2, "#,##0") & "��" & vbLf & _
            "���v���ԁF" & Format(nLaps, "#,##0") & "�b", _
            vbInformation + vbOKOnly, _
            "�����񌟍�"
End Sub

Sub make_Summary()
    Const ORIGINAL_SHEET = "����"
    Const SUMMARY_SHEET = "���ʁi�T�}���j"
    
    Dim bFound As Boolean
    bFound = False
    
    Dim i As Integer
    For i = 1 To Worksheets.Count
        If Worksheets(i).Name = SUMMARY_SHEET Then
            bFound = True
            Exit For
        End If
    Next
    
    If bFound Then
        Dim nYesNo As Integer
        nYesNo = MsgBox("[" & SUMMARY_SHEET & "] �V�[�g�����łɑ��݂��Ă��܂��B" & vbLf & _
                        "�폜���đ��s���Ă���낵���ł����H" & vbLf & _
                        "�͂�" & vbTab & "�폜���đ��s" & vbLf & _
                        "������" & vbTab & "�����𒆒f", _
                        vbQuestion + vbYesNo, SUMMARY_SHEET & "�̍쐬")
        If nYesNo <> vbYes Then Exit Sub
        
        Dim bDisplayAlerts As Boolean
        bDisplayAlerts = Application.DisplayAlerts
        
        Application.DisplayAlerts = False
        
        Worksheets(SUMMARY_SHEET).Delete
        
        Application.DisplayAlerts = bDisplayAlerts
    End If
    
    
    Dim sht As Excel.Worksheet
    Set sht = Worksheets(ORIGINAL_SHEET)
    
    sht.Copy after:=sht
    
    Set sht = ActiveSheet
    sht.Name = SUMMARY_SHEET
    sht.Activate
    
    Dim nRow As Long
    nRow = 2
    
    While sht.Cells(nRow, 1).Value <> ""
        Dim nRowCount As Long
        nRowCount = 0
        While sht.Cells(nRow, 1).Value = sht.Cells(nRow + nRowCount, 1).Value And _
              sht.Cells(nRow, 6).Value = sht.Cells(nRow + nRowCount, 6).Value
            nRowCount = nRowCount + 1
        Wend
        
        sht.Rows(nRow).Insert shift:=XlInsertShiftDirection.xlShiftDown
        sht.Rows(nRow + 1).Copy Destination:=sht.Rows(nRow)
        sht.Cells(nRow, 2).Value = Empty
        sht.Cells(nRow, 3).Value = CStr(nRowCount) & "�ӏ�"
        sht.Cells(nRow, 4).Value = Empty
        sht.Cells(nRow, 5).Value = Empty
        
        With sht.Range(CStr(nRow + 1) & ":" & CStr(nRow + nRowCount))
            .Group
            .Font.Color = &H808080
        End With
        
        nRow = nRow + nRowCount + 1
        DoEvents
    Wend
    
    sht.Outline.ShowLevels RowLevels:=1
    
    MsgBox "�������ʂ̃T�}�����I�����܂���", _
            vbInformation + vbOKOnly, SUMMARY_SHEET & "�̍쐬"
End Sub






