using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private NoteSpawner noteSpawner;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private tapNote leftTapNote;
    [SerializeField] private tapNote rightTapNote;
    [SerializeField] private ScoreCounter scoreCounter;
    [SerializeField] private int scorePerHit = 100;
    [SerializeField] private CountDownAndClap[]countDownAndClaps;
    [SerializeField] private float startMusicDelay = 1.0f;
    [SerializeField] private float startSpawnDelay = 2.0f;
    [SerializeField] private float endDelay = 2.0f;
    [SerializeField] private UnityEvent EndEvent;

    private int score = 0;
    private FlagTimer musicTimer;
    private FlagTimer SpawnTimer;
    private FlagTimer endTimer;

    // Start is called before the first frame update
    void Start()
    {
        musicTimer = new FlagTimer(startMusicDelay);
        if (musicSource != null)
            musicTimer.AddOnTimeUpListener(() => musicSource.Play());
        musicTimer.Set();
        SpawnTimer = new FlagTimer(startSpawnDelay);
        SpawnTimer.AddOnTimeUpListener(() => noteSpawner.StartNote(EndNote));
        SpawnTimer.Set();
        leftTapNote.OnHit.AddListener(AddScore);
        rightTapNote.OnHit.AddListener(AddScore);
    }

    // Update is called once per frame
    void Update()
    {
        musicTimer.Update();
        SpawnTimer.Update();
        endTimer?.Update();
    }

    public void EndNote()
    {
        endTimer = new FlagTimer(endDelay);
        endTimer.AddOnTimeUpListener(() => EndEvent.Invoke());
        endTimer.Set();
    }

    void AddScore()
    {
        score += scorePerHit;
        Debug.Log("スコア: " + score);
        scoreCounter?.SetScore(score);
        foreach(var countDownAndClap in countDownAndClaps)
        {
            countDownAndClap.SendScore(score);
        }
    }
}
