using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualBasic;
using Aspose.Cells;

namespace Task5_PersonsFaker.Helpers
{
    public class CsvStreamer
    {
        public static List<List<string>> ReadData(string path)
        {
            List<List<string>> csvData = new List<List<string>>();

            using (Stream file = new FileStream(path, FileMode.Open))
            {
                TextFieldParser parser = new TextFieldParser(file);
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(new string[] { ";" });
                while (!parser.EndOfData)
                {
                    csvData.Add(parser.ReadFields().ToList());
                }
            }

            return csvData;
        }

        public static void WriteData(List<List<string>> data, string path)
        {
            var workbook = new Workbook();
            var worksheet = workbook.Worksheets[0];


            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data[i].Count; j++)
                {
                    //Console.WriteLine($"{(char)('A' + j)}{i + 1}");
                    worksheet.Cells[$"{(char)('A' + j)}{(i + 1)}"].PutValue(data[i][j]);
                }
            }

            workbook.Save(path, SaveFormat.Csv);
        }
    }
}
