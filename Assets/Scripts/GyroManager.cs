using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class GyroManager : MonoBehaviour
{
    public Text angleText;

    Gyroscope gyro;

    void Start()
    {
        if(Permission.HasUserAuthorizedPermission(Permission.FineLocation) == false)
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }

        // 만일, 자이로스코프를 사용가능한 기기라면...
        if(SystemInfo.supportsGyroscope)
        {
            // 자이로스코프를 활성화한다.
            gyro = Input.gyro;
            gyro.enabled = true;
        }

        // 화면 꺼짐 방지
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Update()
    {
        //UseGyro();
        UseAcceleration();
    }

    // 자이로 스코프를 이용한 회전 방식
    void UseGyro()
    {
        Vector3 gyroAngle = gyro.attitude.eulerAngles;
        string gyroValue = string.Format("x축: {0:f3} \r\ny축: {1:f3} \r\nz축: {2:f3}", gyroAngle.x, gyroAngle.y, gyroAngle.z);

        angleText.text = gyroValue;

        transform.rotation = gyro.attitude * new Quaternion(1, 0, 0, 0);
    }
    
    // 가속도계를 이용한 회전 방식
    void UseAcceleration()
    {
        Vector3 accel = Input.acceleration;
        string accelValue = string.Format("x축: {0:f3} \r\ny축: {1:f3} \r\nz축: {2:f3}", accel.x, accel.y, accel.z);

        angleText.text = accelValue;

        transform.rotation = Quaternion.Euler(accel);
    }
}
