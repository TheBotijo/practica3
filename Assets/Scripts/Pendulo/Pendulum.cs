using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    Quaternion _start, _end;

    [SerializeField, Range(0.0f,360f)]
    private float _angle=90.0f;

    [SerializeField, Range(0.0f, 360f)]
    private float _speed = 2.0f;

    [SerializeField, Range(0.0f, 360f)]
    private float _starttime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _start = PendulumRotation(_angle);
        _end = PendulumRotation(-_angle);
    }

    private void FixedUpdate()
    {
        _starttime += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(_start, _end, (Mathf.Sin(_starttime * _speed + Mathf.PI / 2) + 1.0f) / 2.0f);
    }

    // Update is called once per frame

    void ResetTimer()
    {
        _starttime = 0.0f;
    }

    Quaternion PendulumRotation(float angle)
    {
        var pendulumRotation = transform.rotation;
        var angleZ = pendulumRotation.eulerAngles.z + angle;

        if (angleZ > 180)
            angleZ -= 360;
        else if (angleZ < -180)
            angleZ += 360;

        pendulumRotation.eulerAngles = new Vector3(angleZ, pendulumRotation.eulerAngles.y, pendulumRotation.eulerAngles.z);
        return pendulumRotation;
    }

}
