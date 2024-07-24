using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class CSVReader
{
    public static List<Dictionary<string, string>> Read(string filePath)
    {
        List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
        string[] lines = File.ReadAllLines(filePath);

        if (lines.Length > 0)
        {
            string[] headers = lines[0].Split(',');

            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(',');
                Dictionary<string, string> entry = new Dictionary<string, string>();

                for (int j = 0; j < headers.Length; j++)
                {
                    entry[headers[j]] = fields[j];
                }

                list.Add(entry);
            }
        }

        return list;
    }
}
