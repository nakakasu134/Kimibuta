using System.Collections;  // ← これを追加
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource; // 再生する音楽
    [SerializeField] private float delay = 1f;        // 再生を遅らせる秒数

    void Start()
    {
        StartCoroutine(PlayMusicWithDelay());
    }

    private IEnumerator PlayMusicWithDelay()
    {
        yield return new WaitForSeconds(delay);
        if (musicSource != null)
            musicSource.Play();
    }
}
