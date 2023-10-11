import java.io.FileWriter;
import java.io.IOException;

public class GenerateAndExportCSV {
    public static void main(String[] args) {
        String csvFilePath = "your_file.csv"; // �滻Ϊ��Ҫ������ļ�·��
        int numRows = 40000;

        try (FileWriter fileWriter = new FileWriter(csvFilePath)) {
            // д��CSV�ļ�����ͷ
            fileWriter.append("��1");
            fileWriter.append(",");
            fileWriter.append("��2");
            fileWriter.append("\n");

            // ���ɲ�д������
            for (int i = 1; i <= numRows; i++) {
                fileWriter.append("\"����" + i + "\"");
                fileWriter.append(",");
                fileWriter.append("\"����" + i + "\"");
                fileWriter.append("\n");
                // ����Ϊÿһ���������
            }

            System.out.println("CSV�ļ����ɳɹ���" + csvFilePath);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
