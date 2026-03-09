using UnityEngine;
using UnityEngine.Events;

public class TimeTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent triggerEvent;
    [SerializeField] private float time;
    [SerializeField] private bool playOnAwake = false;
    [SerializeField] private bool loop = false;

    private FlagTimer timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = new FlagTimer(time);
        timer.AddOnTimeUpListener(() => triggerEvent.Invoke());
        if (playOnAwake) timer.Set();
        timer.SetContinuous(loop);
    }

    // Update is called once per frame
    void Update()
    {
        timer.Update();
    }

    public void Set()
    {
        timer.Set();
    }
}
