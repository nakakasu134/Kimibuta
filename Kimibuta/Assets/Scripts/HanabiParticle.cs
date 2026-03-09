using UnityEngine;

public class HanabiParticle : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Vector2 firstVelocity;
    [SerializeField] private Vector2 lastVelocity;
    [SerializeField] private float angularSpeed;
    [SerializeField] private float firstSize;
    [SerializeField] private float lastSize;
    [SerializeField] private float fadeoutTime;
    [SerializeField] private bool playSelf = false;

    private bool isActive;
    private DestroyTimer timer;
    private SpriteRenderer spriteRenderer;
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        if (playSelf)
        {
            Init(time);
            velocity = firstVelocity;
            isActive = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            Action();
        }
    }

    void Action()
    {
        float timeRate = timer.TimeCount / time;

        velocity = firstVelocity * (1 - timeRate) + lastVelocity * timeRate;
        transform.localPosition += new Vector3(velocity.x, velocity.y, 0)* Time.deltaTime;
        transform.Rotate(Vector3.forward, angularSpeed * Time.deltaTime);
        transform.localScale = Vector3.one * (timeRate * (lastSize - firstSize) + firstSize);
        if (fadeoutTime > 0.0f && time - timer.TimeCount < fadeoutTime)
        {
            float r = spriteRenderer.color.r;
            float g = spriteRenderer.color.g;
            float b = spriteRenderer.color.b;
            float a = (time - timer.TimeCount) / fadeoutTime;
            spriteRenderer.color = new Color(r, g, b, a);
        }
    }

    public void Init(float _time)
    {
        playSelf = false;
        isActive = true;
        time = _time;
        if (TryGetComponent<DestroyTimer>(out DestroyTimer _timer))
        {
            timer = _timer;
        }
        else
        {
            timer = gameObject.AddComponent<DestroyTimer>();
        }
        timer.SetAwake(time);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetMovement(Vector2 _firstVelocity, Vector2 _lastVelocity, float _angularSpeed)
    {
        firstVelocity = _firstVelocity;
        lastVelocity = _lastVelocity;
        velocity = firstVelocity;
        angularSpeed = _angularSpeed;
    }

    public void SetSize(float _firstSize, float _lastSize)
    {
        firstSize = _firstSize;
        lastSize = _lastSize;
    }

    public void SetFadeout(float _fadeoutTime)
    {
        fadeoutTime = _fadeoutTime;
    }

    public void SetFadeout()
    {
        fadeoutTime = time;
    }

    public void SetColor(Color _color)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = _color;
        }
    }

    public void SetLayer(string _sortingLayerName, int _orderInLayer)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = _sortingLayerName;
            spriteRenderer.sortingOrder = _orderInLayer;
        }
    }
}