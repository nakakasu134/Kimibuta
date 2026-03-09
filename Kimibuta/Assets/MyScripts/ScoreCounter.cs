using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [System.Serializable]
    class Rank
    {
        public GameObject ui;
        public int maxScore;

        public void SetActive(bool bl)
        {
            ui.SetActive(bl);
        }
    }
    int score = 0;
    [SerializeField] TextMeshProUGUI textComponent;
    [SerializeField] Rank[] ranks;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (textComponent == null) this.textComponent = GetComponent<TextMeshProUGUI>();
        ShowScore();
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void SetScore(int _score)
    {
        score = _score;
        ShowScore();
    }

    public void AddScore()
    {
        score += 100;
        ShowScore();
    }

    void ShowScore()
    {
        if (this.textComponent == null) return;
        this.textComponent.text = score.ToString();
        Debug.Log(score);
        if (score <= ranks[0].maxScore)
            ranks[0].SetActive(true);
        else for (int i = 1; i < ranks.Length; i++)
            {
                bool isActive = score > ranks[i - 1].maxScore && score <= ranks[i].maxScore;
                ranks[i].SetActive(isActive);
                if (isActive) ranks[i - 1].SetActive(false);
            }
    }

}
