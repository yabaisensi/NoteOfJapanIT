::echo off

:traversal
cls
dir /b proDirIni
echo please write the program number that you want to start
set /p snum=

if  "%snum%" == "01"  (
  start "" "C:\myself\01_tool\01_startPro\proDirIni\01_github�ֿ�.lnk"
)

if  "%snum%" == "02"  (
  start "" "C:\myself\01_tool\01_startPro\proDirIni\02_proDirIni  �����ļ��б�.lnk"
)

if  "%snum%" == "03"  (
  start "" "C:\myself\01_tool\01_startPro\proDirIni\03_mian ��������.lnk"
)

if  "%snum%" == "04"  (
  start "" "C:\myself\01_tool\01_startPro\proDirIni\04_backup.bat ������������.lnk"
)

if  "%snum%" == "05"  (
  start "" "C:\myself\01_tool\01_startPro\proDirIni\05_sub1������.lnk"
)

if  "%snum%" == "06"  (
  start "" "C:\myself\01_tool\01_startPro\proDirIni\06_backup.bat sub1������.lnk"
)

if  "%snum%" == "07"  (
  start "" "C:\myself\01_tool\01_startPro\proDirIni\07_sub2������.lnk"
)

if  "%snum%" == "08"  (
  start "" "C:\myself\01_tool\01_startPro\proDirIni\08_backup.bat sub2������.lnk"
)



goto traversal
