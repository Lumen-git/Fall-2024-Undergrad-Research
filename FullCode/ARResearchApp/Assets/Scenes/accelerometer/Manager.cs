using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    [SerializeField] GameObject accelRodx;
    [SerializeField] GameObject accelRody;
    [SerializeField] GameObject accelRodz;

    [SerializeField] GameObject linaccelRodx;
    [SerializeField] GameObject linaccelRody;
    [SerializeField] GameObject linaccelRodz;

    [SerializeField] GameObject gravRodx;
    [SerializeField] GameObject gravRody;
    [SerializeField] GameObject gravRodz;

    [SerializeField] Toggle normalToggle;
    
    void Start()
    {
        InputSystem.EnableDevice(Accelerometer.current);
        InputSystem.EnableDevice(GravitySensor.current);
        InputSystem.EnableDevice(LinearAccelerationSensor.current);

    }

    
    void Update()
    {

        Vector3 gravity = GravitySensor.current.gravity.ReadValue();
        Vector3 linAcceleration = LinearAccelerationSensor.current.acceleration.ReadValue();
        Vector3 acceleration = Accelerometer.current.acceleration.ReadValue();

        if (normalToggle.isOn){
            acceleration = Vector3.Normalize(acceleration);
            linAcceleration = Vector3.Normalize(linAcceleration);
            gravity = Vector3.Normalize(gravity);
        }

        

        accelRodx.transform.localScale = new Vector3(1f, 1f, acceleration.x);
        accelRody.transform.localScale = new Vector3(1f, 1f, acceleration.y);
        accelRodz.transform.localScale = new Vector3(1f, 1f, acceleration.z);

        linaccelRodx.transform.localScale = new Vector3(1f, 1f, linAcceleration.x);
        linaccelRody.transform.localScale = new Vector3(1f, 1f, linAcceleration.y);
        linaccelRodz.transform.localScale = new Vector3(1f, 1f, linAcceleration.z);

        gravRodx.transform.localScale = new Vector3(1f, 1f, gravity.x);
        gravRody.transform.localScale = new Vector3(1f, 1f, gravity.y);
        gravRodz.transform.localScale = new Vector3(1f, 1f, gravity.z);

    }
}
