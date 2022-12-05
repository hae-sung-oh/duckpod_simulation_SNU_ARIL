using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp;

public class CarController_ : MonoBehaviour {
    // private TwistManager_ twistManager;
    private float linearVelocity, angularVelocity;
    public float stepSize;
    
    void Start() {
        // twistManager = gameObject.GetComponent<TwistManager_>();
        resetVelocity();
    }

    void Update() {
       if (Input.anyKey) {
            if (Input.GetKey(KeyCode.W))
                linearVelocity += stepSize;
            else if (Input.GetKey(KeyCode.S))
                linearVelocity -= stepSize;
            else if (Input.GetKey(KeyCode.A))
                angularVelocity += stepSize;
            if (Input.GetKey(KeyCode.D))
                angularVelocity -= stepSize;
            else if (Input.GetKey(KeyCode.Space))
                resetVelocity();
            else{}
       }
                

            //Debug.Log("LinearV: " + linearVelocity + " AngularV: " + angularVelocity + " CtrlL: " + controlLinear + " CtrlA: " + controlAngular);
            // twistManager.updateTransform(linearVelocity, angularVelocity);
        //}else {
            //resetVelocity();
        //}
    }
    void resetVelocity() {
        linearVelocity = 0f;
        angularVelocity = 0f;
    }
}
