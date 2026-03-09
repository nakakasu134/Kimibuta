using UnityEngine;
using UnityEngine.Events;

public class DualKnarCon : MonoBehaviour
{
    [SerializeField] private ClapTrigger KnarConR;
    [SerializeField] private ClapTrigger KnarConL;
    [SerializeField] private UnityEvent onKnarConClapped;
    [SerializeField] private UnityEvent onBothKnarConClapped;
    [SerializeField] private float timeRange;

    private FlagTimer clappedFlag;
    private FlagTimer bothClappedFlag;

    // Start is called before the first frame update
    void Start()
    {
        KnarConR.OnClapEvent.AddListener(OnKnarConClapped);
        KnarConL.OnClapEvent.AddListener(OnKnarConClapped);
        clappedFlag = new FlagTimer(timeRange);
        clappedFlag.AddonSetListener(onKnarConClapped.Invoke);
        bothClappedFlag = new FlagTimer(timeRange);
        bothClappedFlag.AddonSetListener(onBothKnarConClapped.Invoke);
    }

    // Update is called once per frame
    void Update()
    {
        clappedFlag.Update();
        bothClappedFlag.Update();
    }

    public void OnKnarConClapped()
    {
        if (!clappedFlag.Flag)
        {
            clappedFlag.Set();
        }
        else if (!bothClappedFlag.Flag)
        {
            bothClappedFlag.Set();
        }
    }
}