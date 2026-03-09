using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Transform parent;
    [SerializeField] private Vector2 minPosition;
    [SerializeField] private Vector2 maxPosition;
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;

    float lastPositionX;
    float lastPositionY;

    // Start is called before the first frame update
    void Start()
    {
        lastPositionX = 0.0f;
        lastPositionY = 0.0f;
    }

    public void Spawn()
    {
        float index = Random.Range(0, prefabs.Length);
        GameObject prefab = prefabs[(int)index];
        GameObject instance = Instantiate(prefab, parent);
        AdjustPosition(instance.transform);
        float size = Random.Range(minSize, maxSize);
        instance.transform.localScale = Vector3.one * size;
    }

    void AdjustPosition(Transform instance)
    {
        lastPositionX = AdjustRandom(lastPositionX);
        lastPositionY = AdjustRandom(lastPositionY);
        float x=minPosition.x+lastPositionX*(maxPosition.x-minPosition.x);
        float y=minPosition.y+lastPositionY*(maxPosition.y-minPosition.y);
        instance.position = new Vector3(x, y, 0);
    }

    float AdjustRandom(float lastPosition)
    {
        float x = Random.Range(0f, 1f);
        float x1 = 1 - x;
        float a = 1;
        float b = -10 * a / 3;
        float c = (23 * a - 3) / 6;
        float d = 1 - a - b - c;
        float f = x * x * (x * (x * (x * a + b) + c) + d);
        float g = 1 - x1 * x1 * (x1 * (x1 * (x1 * a + b) + c) + d);
        float y = g + lastPosition * (f - g);
        return y;
    }
}
