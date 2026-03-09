using UnityEngine;
using System.IO.Ports;
using System.Threading;
using UnityEngine.Events;

public class SerialReceiver : MonoBehaviour
{
    [SerializeField] private string portName = "COM3"; // Set your port name here
    [SerializeField] private int baudRate = 115200; // Set your baud rate here

    private SerialPort serialPort;
    private Thread readThread;
    private SerialData serialData = new();
    private UnityEvent onDataReceived;
    private bool isRunning = false;
    private string message = "";
    private bool newDataReceived = false;

    public SerialData SerialData => serialData;
    public UnityEvent OnDataReceivedEvent => onDataReceived;
    public bool IsRunning => isRunning;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (newDataReceived)
        {
            OnDataReceived();
        }
    }

    public void Init()
    {
        if (onDataReceived == null)
        {
            onDataReceived = new UnityEvent();
        }
        newDataReceived = false;
        OpenSerialPort();
    }
    private void OpenSerialPort()
    {
        if (serialPort == null || !serialPort.IsOpen)
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.Open();
            isRunning = true;
            readThread = new Thread(ReadSerialData);
            readThread.Start();
            Debug.Log("Serial port opened.");
        }
        else if (serialPort.IsOpen)
        {
            Debug.Log("Serial port is already open.");
        }
        else
        {
            Debug.LogError("Failed to open serial port.");
        }
    }

    private void CloseSerialPort()
    {
        isRunning = false;
        if (readThread != null && readThread.IsAlive)
        {
            readThread.Join();
        }
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
            serialPort.Dispose();
            Debug.Log("Serial port closed.");
        }
    }

    private void ReadSerialData()
    {
        while (isRunning && serialPort != null && serialPort.IsOpen)
        {
            try
            {
                message = serialPort.ReadLine();
                newDataReceived = true;
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error reading serial data: " + e.Message);
            }
        }
    }

    private void OnDataReceived()
    {
        string[] datas = message.Split(
            new string[] { "\t" }, System.StringSplitOptions.None);
        if (datas.Length > 0)
        {
            string[] data = datas[0].Split(',');
            if (data.Length >= 7)
            {
                if (float.TryParse(data[0], out float ax) &&
                    float.TryParse(data[1], out float ay) &&
                    float.TryParse(data[2], out float az) &&
                    float.TryParse(data[3], out float gx) &&
                    float.TryParse(data[4], out float gy) &&
                    float.TryParse(data[5], out float gz) &&
                    int.TryParse(data[6], out int vol))
                {
                    serialData.SetAccel(ax, ay, az);
                    serialData.SetGyro(gx, gy, gz);
                    serialData.SetVolume(vol);
                    onDataReceived?.Invoke();
                }
                else
                {
                    Debug.LogWarning("Failed to parse serial data: " + message);
                }
            }
            else
            {
                Debug.LogWarning("Unexpected data length: " + message);
            }
            newDataReceived = false;
        }
    }

    private void OnDestroy()
    {
        CloseSerialPort();
    }
}
