using System.Collections.Generic;
using UnityEngine;

public class DataReader : MonoBehaviour
{
    [SerializeField] private CSVReader csvReader;

    private int dataNum;

    public CSVReader CsvReader => csvReader;

    public int DataNum => dataNum;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Data[] GetDataArray()
    {
        List<string[]> rawData = csvReader.GetData();
        dataNum = rawData.Count - 1; // Exclude header row
        Data[] dataArray = new Data[dataNum]; // Exclude header row
        for (int i = 0; i < dataNum; i++)
        {
            string[] row = rawData[i];
            Debug.Log(string.Join(", ", row));
            Data data = new Data();
            data.name = row[0];
            data.type1 = row[1];
            data.type2 = row[2];
            data.HP = int.Parse(row[3]);
            data.Attack = int.Parse(row[4]);
            data.Defense = int.Parse(row[5]);
            data.SpAttack = int.Parse(row[6]);
            data.SpDefense = int.Parse(row[7]);
            data.Speed = int.Parse(row[8]);
            dataArray[i] = data;
        }
        return dataArray;
    }
}
