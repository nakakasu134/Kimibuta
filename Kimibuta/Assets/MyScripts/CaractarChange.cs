using UnityEngine;
using System.Collections.Generic;

public class CharacterChange : MonoBehaviour
{
    [SerializeField] private CSVReader csvReader;
    [SerializeField] private SpriteRenderer character;
    [SerializeField] private Sprite[] Poses; // 表示させたいキャラの配列
    [SerializeField] private float triggerCooldown = 0.2f; // クールタイム（秒）
    [SerializeField] private string PoseChangeTag = "PoseChange";

    private List<int[]> csvData; // CSVデータ格納用
    private int csvPointer = 0;  // CSVの現在の行を指す
    private FlagTimer cooldownTimer;
    private int nextIndex;
    private bool onNoteStay = false;

    void Start()
    {
        cooldownTimer = new FlagTimer(triggerCooldown);
        csvPointer = 0;
        nextIndex = 0;
        onNoteStay = false;

        csvReader.ReadCSV();

        // CSVデータの読み込み（文字列→整数）
        var rawData = csvReader.GetData();
        csvData = new List<int[]>();

        foreach (var row in rawData)
        {
            int[] intRow = new int[row.Length];
            for (int i = 0; i < row.Length; i++)
            {
                int.TryParse(row[i], out intRow[i]);
            }
            csvData.Add(intRow);
        }
    }

    void Update()
    {
        cooldownTimer.Update();
    }

    public void NextCharacter()
    {
        if (cooldownTimer.Flag) return; // クールタイム中は無視
        else if (csvData == null || csvData.Count == 0) return;
        Debug.Log("csvPointer:" + csvPointer);

        // CSVから次のキャラ番号を取得
        nextIndex = csvData[csvPointer][0];

        // 範囲外対策
        nextIndex = Mathf.Clamp(nextIndex, 0, Poses.Length - 1);

        // CSVの次の行へ
        csvPointer = (csvPointer + 1) % csvData.Count;
        cooldownTimer.Set();
    }

    public void ChangeCharacter()
    {
        SetSprite(nextIndex);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(PoseChangeTag) && !onNoteStay)
        {
            onNoteStay = true;
            NextCharacter(); // キャラ切り替え
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(PoseChangeTag)) // ノーツから離れたら
        {
            onNoteStay = false;
        }
    }

    private void SetSprite(int index)
    {
        if (index >= 0 && index < Poses.Length)
        {
            character.sprite = Poses[index];
        }
    }
}
