Sub GenerateAndExportCSV()
    Dim ws As Worksheet
    Dim i As Long
    Dim numRows As Long
    Dim filePath As String
    
    ' ����Ҫ������ݵĹ�����
    Set ws = ActiveWorkbook.Sheets("Sheet1")
    
    ' ����Ҫ���ɵ�����
    numRows = 4000
    
    ' ����CSV�ļ�����·��
    filePath = "C:\Temp\Test_data2.csv" ' �滻Ϊ����Ҫ������ļ�·��
    fileExportPath = "C:\Temp\final_data.csv"
    ' ��չ����������
    ws.UsedRange.Clear
    
    ' �ڵ�һ�д�����ͷ
    ws.Cells(1, 1).Value = "��1"
    ws.Cells(1, 2).Value = "��2"
    ' ����Ϊÿһ����ӱ�ͷ
    
    ' �ӵڶ��п�ʼ��������
    For i = 2 To numRows + 1
        ws.Cells(i, 1).Value = "����" & i - 1
        ws.Cells(i, 2).Value = "����" & i - 1
        ' ����Ϊÿһ���������
    Next i
    
    ' ���湤��������ΪCSV�ļ�
    ws.SaveAs filePath, xlCSV
    
        ' �رչ�����
    ws.Parent.Close False
    
    ' ����powershell�����csv�ļ����ֶζ�����˫����
    Set wShell = CreateObject("WScript.Shell")
    Set wShellResult = wShell.Exec("powershell Import-Csv -Encoding Default " & filePath & " | export-csv " & fileExportPath & " -NoTypeInformation -Encoding Default ; del " & filePath)
 
    'Change to StdErr.ReadAll to read an error response
    'MsgBox wShellResult.StdOut.ReadAll
End Sub

