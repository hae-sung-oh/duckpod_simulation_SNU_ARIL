using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDraw : MonoBehaviour {
    public float offset = 0.01f;
    public LayerMask layerMask;
    public List<GameObject> circle;
    private RaycastHit info;
    public GameObject Route;
    public List<Vector2> RouteInput;
    public float speed;
    public int height;
    private int RouteIndex;

    void Start() {
        RouteIndex = 0;
        transform.position = new Vector3(RouteInput[RouteIndex].x, height, RouteInput[RouteIndex].y);
    }
    void Update() {
        if (RouteIndex < RouteInput.Count) {
            MoveRoute();
        }
    }

    public void MoveRoute() {
        transform.position = Vector3.MoveTowards(transform.position, make_vec3(RouteInput[RouteIndex]), speed);
        if (Physics.Raycast(transform.position, new Vector3(0f, -1f, 0f), out info, Mathf.Infinity, layerMask)) {
            Debug.DrawRay(transform.position, new Vector3(0f, -1f, 0f) * info.distance, Color.green);
            make_instant(circle[0], info);
        } else {
            Debug.DrawRay(transform.position, new Vector3(0f, -1f, 0f) * 1000f, Color.red);
        }
        if (transform.position == make_vec3(RouteInput[RouteIndex])) {
            RouteIndex++;
            make_instant(circle[1], info);
        }
    }

    void make_instant(GameObject circle, RaycastHit info) {
        var instant = Instantiate(circle, info.point + info.normal * offset, circle.transform.rotation);
        instant.transform.parent = Route.transform;
    }

    Vector3 make_vec3(Vector2 input) {
        return new Vector3(input.x, height, input.y);
    }
}
