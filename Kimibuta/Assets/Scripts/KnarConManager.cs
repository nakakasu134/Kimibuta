using UnityEngine;
using UnityEngine.Events;

public class KnarConManager : MonoBehaviour
{
    enum GyroType
    {
        roll = 0,
        yaw = 1,
        pitch = 2
    }

    [SerializeField] private SerialReceiver serialReceiver;
    [SerializeField] private UnityEvent onKnarConClapped;
    [SerializeField] private int gyroThreshold = 1000;
    [SerializeField] private GyroType clapGyroType = GyroType.yaw;
    [SerializeField] private bool positiveThreshold = true;
    [SerializeField] private int volumeThreshold = 1500;
    [SerializeField] private float minClapInterval = 0.4f; // Minimum interval between claps in seconds

    private bool isActive = true;
    private FlagTimer clappedFlag;
    private FlagTimer detectedRotationFlag;
    private FlagTimer detectedVolumeFlag;

    public UnityEvent OnKnarConClappedEvent => onKnarConClapped;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            Action();
        }
    }

    private void Action()
    {
        clappedFlag.Update();
        detectedRotationFlag.Update();
        detectedVolumeFlag.Update();
    }

    private void Init()
    {
        isActive = true;
        clappedFlag = new FlagTimer(minClapInterval);
        clappedFlag.AddonSetListener(() => onKnarConClapped.Invoke());
        detectedRotationFlag = new FlagTimer(minClapInterval);
        detectedVolumeFlag = new FlagTimer(minClapInterval);

        if (serialReceiver != null)
        {
            serialReceiver.Init();
            serialReceiver.OnDataReceivedEvent.AddListener(OnDataReceived);
        }
    }

    private void OnDataReceived()
    {
        if (serialReceiver != null && serialReceiver.IsRunning && !clappedFlag.Flag)
        {
            SerialData data = serialReceiver.SerialData;
            //Debug.Log($"Accel: {data.accel}, Gyro: {data.gyro}, Volume: {data.volume}");
            float gyroValue = 0;
            switch (clapGyroType)
            {
                case GyroType.roll:
                    gyroValue = data.gyro.x;
                    break;
                case GyroType.yaw:
                    gyroValue = data.gyro.y;
                    break;
                case GyroType.pitch:
                    gyroValue = data.gyro.z;
                    break;
            }
            bool detectedRotation = positiveThreshold ? gyroValue > gyroThreshold : gyroValue < gyroThreshold;
            bool detectedVolume = data.volume > volumeThreshold;
            if (detectedRotation)
            {
                detectedRotationFlag.Set();
                Debug.Log($"Rotation detected: {gyroValue}");
            }
            if (detectedVolume)
            {
                detectedVolumeFlag.Set();
                Debug.Log($"Volume detected: {data.volume}");
            }
            if (detectedRotationFlag.Flag && detectedVolumeFlag.Flag)
            {
                clappedFlag.Set();
                detectedRotationFlag.Quit();
                detectedVolumeFlag.Quit();
            }
        }
    }
}