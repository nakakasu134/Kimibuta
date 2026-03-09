using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private DataReader dataReader;
    [SerializeField] private TextMeshProUGUI displayName;
    [SerializeField] private TextMeshProUGUI displayType1;
    [SerializeField] private TextMeshProUGUI displayType2;
    [SerializeField] private TextMeshProUGUI displayHP;
    [SerializeField] private TextMeshProUGUI displayAttack;
    [SerializeField] private TextMeshProUGUI displayDefense;
    [SerializeField] private TextMeshProUGUI displaySpAttack;
    [SerializeField] private TextMeshProUGUI displaySpDefense;
    [SerializeField] private TextMeshProUGUI displaySpeed;
    [SerializeField] private TextMeshProUGUI displayTotal;

    private Data[] dataArray;
    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        dataReader.CsvReader.ReadCSV();
        dataArray = dataReader.GetDataArray();
        currentIndex = 0;
        Display();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Display()
    {
        if (dataArray.Length == 0) return;
        Data currentData = dataArray[currentIndex];
        displayName.text = $"{currentData.name}";
        displayType1.text = $"{currentData.type1}";
        displayType2.text = $"{currentData.type2}";
        displayHP.text = $"HP: {currentData.HP}";
        displayAttack.text = $"Ç±Ç§Ç∞Ç´: {currentData.Attack}";
        displayDefense.text = $"Ç⁄Ç§Ç¨ÇÂ: {currentData.Defense}";
        displaySpAttack.text = $"Ç∆Ç≠Ç±Ç§: {currentData.SpAttack}";
        displaySpDefense.text = $"Ç∆Ç≠Ç⁄Ç§: {currentData.SpDefense}";
        displaySpeed.text = $"Ç∑ÇŒÇ‚Ç≥: {currentData.Speed}";
        int total = currentData.Total;
        displayTotal.text = $"çáåv: {total}";
    }
    public void OnNextButton()
    {
        if (dataArray.Length == 0) return;
        currentIndex = (currentIndex + 1) % dataArray.Length;
        Display();
    }
    public void OnPreviousButton()
    {
        if (dataArray.Length == 0) return;
        currentIndex = (currentIndex - 1 + dataArray.Length) % dataArray.Length;
        Display();
    }
}