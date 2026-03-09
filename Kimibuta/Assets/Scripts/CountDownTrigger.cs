using UnityEngine;
using UnityEngine.Events;

public class CountDownTrigger : MonoBehaviour
{
    [SerializeField] private int count;
    [SerializeField] private UnityEvent onCountUp;

    private int counter=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Set(int _count,UnityAction action)
    {
        count=_count;
        onCountUp.AddListener(action);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountDown()
    {
        counter++;
        if(counter>=count)onCountUp.Invoke();
    }
}
