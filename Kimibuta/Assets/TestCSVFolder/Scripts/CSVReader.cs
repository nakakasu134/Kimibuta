using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    [SerializeField] private TextAsset csvFile;
    private List<string[]> data = new List<string[]>();

    public void ReadCSV()
    {
        string[] lines = csvFile.text.Split(new char[] { '\n' });
        foreach (string line in lines)
        {
            string[] row = line.Split(new char[] { ',' });
            data.Add(row);
        }
    }

    public List<string[]> GetData()
    {
        return data;
    }
}