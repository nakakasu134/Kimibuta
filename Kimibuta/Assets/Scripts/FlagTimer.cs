using UnityEngine;
using UnityEngine.Events;

public class FlagTimer
{
    private float limit;
    private float time;
    private bool flag;
    private bool countinuous;
    
    private UnityEvent onFlag = new();
    private UnityEvent onSet = new();
    private UnityEvent onTimeUp = new();

    public float TimeCount => time;
    public bool Flag => flag;
    public UnityEvent OnTimeUp => onTimeUp;

    public FlagTimer(float _limit)
    {
        limit = _limit;
        time = 0;
        flag = false;
        countinuous = false;
        onFlag = new();
        onSet = new();
    }

    public void SetContinuous(bool _countinuous)
    {
        countinuous = _countinuous;
    }

    public void AddOnFlagListener(UnityAction action)
    {
        onFlag.AddListener(action);
    }

    public void AddonSetListener(UnityAction action)
    {
        onSet.AddListener(action);
    }

    public void AddOnTimeUpListener(UnityAction action)
    {
        onTimeUp.AddListener(action);
    }

    public void Set()
    {
        flag = true;
        onSet.Invoke();
    }

    public void Quit()
    {
        flag = false;
        time = 0f;
        onTimeUp.Invoke();
    }

    public void Update()
    {
        if (flag)
        {
            onFlag.Invoke();
            time += Time.deltaTime;
            if (time >= limit)
            {
                Quit();
                if (countinuous)
                {
                    Set();
                }
            }
        }
    }
}
