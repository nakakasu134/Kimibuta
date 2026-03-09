using UnityEngine;
using UnityEngine.Events;

public class DeplicateTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent triggerEvent;
    public void DeplicateInvoke(int count)
    {
        for (int i = 0; i < count; i++)
        {
            triggerEvent.Invoke();
        }
    }
}
