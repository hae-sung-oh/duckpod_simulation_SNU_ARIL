using UnityEngine;
using System.Collections.Generic;

namespace RosSharp {
    public class TwistManager_ : MonoBehaviour {
        private float linearVelocity, angularVelocity, LinearLimit, AngularLimit, brakeTorque;
        private bool doUpdate;
        public List<WheelCollider> LeftWheels, RightWheels;

        private void Start() {
            brakeTorque = 100f;
            LinearLimit = 1.5f;
            AngularLimit = 1.5f;
            doUpdate = false;
        }

        private void FixedUpdate() {
            //Debug.Log(doUpdate);
            if (doUpdate) {
                accelarate();
                brake();
            }
            //Debug.Log("LinearV: " + linearVelocity + " AngularV: " + angularVelocity);
        }

        private void accelarate() {
            //Debug.Log("Accelarate");
            foreach (WheelCollider wheel in LeftWheels) {
                wheel.brakeTorque = 0;
                wheel.motorTorque = (linearVelocity * 10 - angularVelocity * 100);
            }
            foreach (WheelCollider wheel in RightWheels) {
                wheel.brakeTorque = 0;
                wheel.motorTorque = (linearVelocity * 10 + angularVelocity * 100);
            }
        }

        private void brake() {
            //Debug.Log("Brake");
            foreach (WheelCollider wheel in LeftWheels) {
                wheel.brakeTorque = brakeTorque;
            }
            foreach (WheelCollider wheel in RightWheels) {
                wheel.brakeTorque = brakeTorque;
            }
        }

        public void updateTransform(float _linearVelocity, float _angularVelocity) {
            linearVelocity = makeContolVel(_linearVelocity, linearVelocity, 0.01f);
            angularVelocity = makeContolVel(_angularVelocity, angularVelocity, 0.01f);
            checkLimit();
            doUpdate = true;

            //Debug.Log("Update: " + linearVelocity + ", " + angularVelocity + ", " + isbrake);
        }
        void checkLimit() {
            if (Mathf.Abs(linearVelocity) > LinearLimit)
                linearVelocity = Mathf.Sign(linearVelocity) * LinearLimit;

            if (Mathf.Abs(angularVelocity) > AngularLimit)
                angularVelocity = Mathf.Sign(angularVelocity) * AngularLimit;
        }
        float makeContolVel(float input, float output, float slop) {
            if (input > output)
                return Mathf.Min(input, output + slop);
            else if (input < output)
                return Mathf.Max(input, output - slop);
            else
                return input;
        }
    }
}