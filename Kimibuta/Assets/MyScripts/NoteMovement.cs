using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    [SerializeField ]private float travelTime;   // 移動にかかる時間
    private Vector3 startPos;
    private Vector3 targetPos;
    private float elapsedTime;  // 経過時間

    public void Initialize(Vector3 target, float moveDuration)
    {
        startPos = transform.position;
        targetPos = target;
        travelTime = moveDuration;
        elapsedTime = 0f;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / travelTime;

        transform.position = startPos + (t * (targetPos - startPos));
    }
}