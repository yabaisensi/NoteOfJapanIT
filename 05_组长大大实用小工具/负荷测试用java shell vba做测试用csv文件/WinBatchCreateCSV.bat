@echo off
setlocal enabledelayedexpansion
REM ����Ҫ���ɵ�����
set rows=40000

REM ����CSV�ļ���д�������
echo "Column1","Column2","Column3">output.csv

REM ����������ݲ�д��CSV�ļ�
for /l %%i in (1, 1, %rows%) do (
    set "col1=data1_%%i"
    set "col2=data2_%%i"
    set "col3=data3_%%i"
    echo "!col1!","!col2!","!col3!" >> output.csv
)
pause
endlocal
