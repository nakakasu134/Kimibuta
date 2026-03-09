using UnityEngine;
using UnityEngine.Events;

public class EnterTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent onEnterKeyDown;
    [SerializeField] private UnityEvent onEnterKeyDownWithShift;

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Return)) return;
        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
        {
            onEnterKeyDownWithShift.Invoke();
        }
        else
        {
            onEnterKeyDown.Invoke();
        }
    }
}
