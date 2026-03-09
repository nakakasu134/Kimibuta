using System;
using UnityEngine;
using UnityEngine.Events;

public class tapNote : MonoBehaviour
{
    public enum NoteSide { L, R }
    public NoteSide side;
    [SerializeField] private char key; // 判定キー
    [SerializeField] private bool checkSelf;
    [SerializeField] private UnityEvent onNoteCome = new();
    [SerializeField] private UnityEvent onHit = new UnityEvent();
    [SerializeField]private UnityEvent onMiss = new UnityEvent();
    private GameObject currentNote; // 通過時にDestroyするため判定範囲内のノーツを記憶

    public UnityEvent OnHit => onHit;

    private void Start()
    {

    }

    private void Update()
    {
        if (checkSelf && Input.GetKeyDown((KeyCode)key))
        {
            HitNote();
        }
    }

    public void HitNote()
    {
        if (currentNote != null)
        {
            float distance = Vector3.Distance(transform.position, currentNote.transform.position);
            float successRange = currentNote.GetComponent<CircleCollider2D>().radius;
            if (distance <= successRange)
            {
                OnHit?.Invoke();
            }
            else
            {
                onMiss?.Invoke();
            }

            Destroy(currentNote);
            currentNote = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<NoteMovement>())
        {
            currentNote = other.gameObject;
            onNoteCome?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == currentNote)
        {
            currentNote = null;
        }
    }
}
