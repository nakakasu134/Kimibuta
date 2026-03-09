using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private GameObject notePrefabL; // 左ノーツのプレハブ
    [SerializeField] private GameObject notePrefabR; // 右ノーツのプレハブ
    [SerializeField] private GameObject TwinNotePrefabL;
    [SerializeField] private GameObject TwinNotePrefabR;
    [SerializeField] private Transform leftSpawn;     // 左ノーツの出現位置
    [SerializeField] private Transform rightSpawn;    // 右ノーツの出現位置
    [SerializeField] private Transform leftTarget;    // 左ノーツの目標位置
    [SerializeField] private Transform rightTarget;   // 右ノーツの目標位置
    [SerializeField] private float moveDuration = 2.0f; // ノーツが移動する時間
    [SerializeField] private CSVReader csvReader;     // CSVデータを読み込むクラス

    private UnityEvent endCallBack = new();
    private bool isActive = false;
    private List<string[]> data = new List<string[]>();
    private int TotalRows = 0;
    private int currentRow = 0;
    private double lastSpawnedTime;
    private Thread BeatThread;
    private bool beatFlag = false;

    void Start()
    {
        csvReader.ReadCSV();
        isActive = false;
    }

    void Update()
    {
        if (beatFlag)
        {
            ReadData();
            beatFlag = false;
        }
    }

    public void StartNote(UnityAction _endCallBack)
    {
        endCallBack.AddListener(_endCallBack);
        lastSpawnedTime = AudioSettings.dspTime;
        data = csvReader.GetData();
        TotalRows = data.Count;
        currentRow = 0;
        beatFlag = true;
        isActive = true;
        //StartCoroutine(SpawnNotesFromCSV());
        BeatThread = new Thread(() =>
        {
            while (isActive)
            {
                CountInBeat();
                Thread.Sleep(1);
            }
        });
        BeatThread.Start();
    }

    IEnumerator SpawnNotesFromCSV()
    {
        var data = csvReader.GetData(); // CSVから譜面データを取得
        Debug.Log("CSV Data Loaded: " + data.Count + " rows");

        for (int i = 0; i < data.Count; i++)
        {
            string[] row = data[i];
            if (row.Length < 2) continue;

            // dataからノーツを生成 <右：１、左：２、両方：３>
            int noteData = int.Parse(row[0]) * 2 + int.Parse(row[1]);

            switch (noteData)
            {
                case 0:
                    break;
                case 1:
                    SpawnSingleNote(false);
                    break;
                case 2:
                    SpawnSingleNote(true);
                    break;
                case 3:
                    SpawnTwinNote();
                    break;
            }


            // BPM（1拍）に合わせて待機
            yield return new WaitForSeconds(BeatManager.beatInterval);
        }

        endCallBack?.Invoke();
    }

    void CountInBeat()
    {
        double audioDeltaTime = AudioSettings.dspTime - lastSpawnedTime;
        if (audioDeltaTime >= BeatManager.beatInterval)
        {
            lastSpawnedTime += BeatManager.beatInterval;
            beatFlag = true;
        }
    }

    void ReadData()
    {
        string[] row = data[currentRow];
        if (row.Length < 2) return;

        // dataからノーツを生成 <右：１、左：２、両方：３>
        int noteData = int.Parse(row[0]) * 2 + int.Parse(row[1]);

        switch (noteData)
        {
            case 0:
                break;
            case 1:
                SpawnSingleNote(false);
                break;
            case 2:
                SpawnSingleNote(true);
                break;
            case 3:
                SpawnTwinNote();
                break;
        }

        currentRow++;
        if (currentRow >= TotalRows)
        {
            isActive = false;
            endCallBack?.Invoke();
            endCallBack.RemoveAllListeners();
            return;
        }
    }

    // ノーツを生成して移動を開始
    void SpawnNote(GameObject notePrefab, Transform spawn, Transform target)
    {
        var note = Instantiate(notePrefab, spawn.position, Quaternion.identity);
        note.GetComponent<NoteMovement>().Initialize(target.position, moveDuration);
    }

    void SpawnSingleNote(bool isLeft)
    {
        GameObject notePrefab = isLeft ? notePrefabL : notePrefabR;
        Transform spawn = isLeft ? leftSpawn : rightSpawn;
        Transform target = isLeft ? leftTarget : rightTarget;
        SpawnNote(notePrefab, spawn, target);
    }

    void SpawnTwinNote()
    {
        SpawnNote(TwinNotePrefabL, leftSpawn, leftTarget);
        SpawnNote(TwinNotePrefabR, rightSpawn, rightTarget);
    }

    private void OnDestroy()
    {
        isActive = false;
        if (BeatThread != null && BeatThread.IsAlive)
        {
            BeatThread.Join();
        }
    }
}
