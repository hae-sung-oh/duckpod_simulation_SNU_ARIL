using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspensionManager : MonoBehaviour {
    // Start is called before the first frame update
    public List<WheelCollider> colliders;
    public List<GameObject> wheels;
    // Update is called once per frame
    void Update() {
        changePos();
    }
    public void changePos() {
        for (int i = 0; i < 6; i++) {
            colliders[i].GetWorldPose(out var position, out var rotation);
            var pos = new Vector3(wheels[i].transform.position.x, position.y, wheels[i].transform.position.z);
            wheels[i].transform.position = pos;
            wheels[i].transform.rotation = rotation;
        }
    }
}
