using UnityEngine;
using UnityEngine.Events;

public class CountDownAndClap : MonoBehaviour
{
    [SerializeField] private CountDownTrigger trigger;
    [SerializeField] private int count;
    [SerializeField] private int needScore;
    [SerializeField] private UnityEvent onClap;

    private bool countUp;
    private bool hasEnoughScore;
    // Start is called before the first frame update
    void Start()
    {
        countUp = false;
        hasEnoughScore = false;
        trigger.Set(count, SetFlag);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClap()
    {
        if (countUp && hasEnoughScore)
            onClap.Invoke();
    }

    private void SetFlag()
    {
        countUp = true;
    }

    public void SendScore(int score)
    {
        if (score >= needScore)
        {
            hasEnoughScore = true;
        }
    }
}
