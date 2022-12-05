using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {
    public List<WheelCollider> LeftWheels, RightWheels;
    private float maxLinearVelocity = 3f, maxAngularVelocity = 1.3f, maxBrakeTorque=100f;
    private float m_currentLinear, m_currentAngular, m_currentBrakeTorque;
    public float stepSize;

    void Start() {
        m_currentBrakeTorque = maxBrakeTorque;
        resetVelocity();
    }

    void FixedUpdate() {
        if (Input.anyKey) {
            if (Input.GetKey(KeyCode.W) && m_currentLinear < 1f)
                m_currentLinear += stepSize;
            if (Input.GetKey(KeyCode.S) && m_currentLinear > -1f)
                m_currentLinear -= stepSize;
            if (Input.GetKey(KeyCode.A) && m_currentAngular < 1f)
                m_currentAngular += stepSize;
            if (Input.GetKey(KeyCode.D) && m_currentAngular > -1f)
                m_currentAngular -= stepSize;
            if (Input.GetKey(KeyCode.Space)){
                resetVelocity();
                m_currentBrakeTorque = maxBrakeTorque;
            }
            else
                m_currentBrakeTorque = 0f;
       }

        float Linear = maxLinearVelocity * m_currentLinear;
        float Angular = maxAngularVelocity * m_currentAngular;
        float brake = m_currentBrakeTorque;

        foreach (WheelCollider wheel in LeftWheels) {
            wheel.motorTorque = (Linear * 10 - Angular * 100);
            wheel.brakeTorque = brake;
        }
        foreach (WheelCollider wheel in RightWheels) {
            wheel.motorTorque = (Linear * 10 + Angular * 100);
            wheel.brakeTorque = brake;
        }
    }

    void resetVelocity() {
        m_currentLinear = 0f;
        m_currentAngular = 0f;
    }
}
