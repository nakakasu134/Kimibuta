using UnityEngine;

public class SerialData
{
    public Vector3 accel;
    public Vector3 gyro;
    public int volume;

    public SerialData()
    {
        accel = Vector3.zero;
        gyro = Vector3.zero;
        volume = 0;
    }

    public void SetAccel(float x, float y, float z)
    {
        accel = new Vector3(x, y, z);
    }

    public void SetGyro(float x, float y, float z)
    {
        gyro = new Vector3(x, y, z);
    }

    public void SetVolume(int vol)
    {
        volume = vol;
    }
}
