using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public float bpm = 120f; // BPM設定
    public static float beatInterval=1; // 1拍の間隔（秒）
    
    void Awake()
    {
        beatInterval = 60f / bpm; // 1拍の間隔を計算
    }
}
