#!/bin/bash

csvFilePath="your_file2.csv"  # �滻Ϊ��Ҫ������ļ�·��
numRows=400

# д��CSV�ļ�����ͷ
echo "��1,��2" > "$csvFilePath"

# ���ɲ�д������
for i in $(seq 1 $numRows); do
  echo "\"����$i\",\"����$i\"" >> "$csvFilePath"
  # ����Ϊÿһ���������
done

echo "CSV�ļ����ɳɹ�: $csvFilePath"
