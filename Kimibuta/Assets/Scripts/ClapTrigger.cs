using UnityEngine;
using UnityEngine.Events;

public class ClapTrigger : MonoBehaviour
{
    [SerializeField] private string key;
    [SerializeField] private UnityEvent onClap;
    public UnityEvent OnClapEvent => onClap;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            OnClap();
        }
    }

    public void OnClap() => onClap.Invoke();
}
