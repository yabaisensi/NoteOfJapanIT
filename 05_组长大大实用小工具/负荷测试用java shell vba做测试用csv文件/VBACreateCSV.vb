Sub GenerateAndExportCSV()
    Dim ws As Worksheet
    Dim i As Long
    Dim numRows As Long
    Dim filePath As String
    
    ' ����Ҫ������ݵĹ�����
    Set ws = ActiveWorkbook.Sheets("Sheet1")
    
    ' ����Ҫ���ɵ�����
    numRows = 40000
    
    ' ����CSV�ļ�����·��
    filePath = "C:\Temp\lib\Test_data2.csv" ' �滻Ϊ����Ҫ������ļ�·��
    
    ' ��չ����������
    ws.UsedRange.Clear
    
    ' �ڵ�һ�д�����ͷ
    ws.Cells(1, 1).Value = "��1"
    ws.Cells(1, 2).Value = "��2"
    ' ����Ϊÿһ����ӱ�ͷ
    
    ' �ӵڶ��п�ʼ��������
    For i = 2 To numRows + 1
        ws.Cells(i, 1).Value = Chr(34) & ���� & i - 1 & Chr(34)
        ws.Cells(i, 2).Value = Chr(34) & ���� & i - 1 & Chr(34)
        ' ����Ϊÿһ���������
    Next i
    
    ' ���湤��������ΪCSV�ļ�
    'ws.SaveAs filePath, xlCSV
    
    ' �رչ�����
    'ws.Parent.Close False
End Sub
