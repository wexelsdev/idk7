using System;
using UnityEngine;

public class Player : MonoBehaviour {
    public float force = 10f;
    public Rigidbody rb;
    public Transform cam;

    public float Drag {
        get => rb.drag;
        set => rb.drag = value;
    }

    private void Start() {
        Drag = 6f;
    }

    private void Update() {
        Vector3 move = Vector3.zero;
        if (cam != null) {
            Vector3 forward = cam.forward;
            Vector3 right = cam.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            if (Input.GetKey(KeyCode.W)) move += forward;
            if (Input.GetKey(KeyCode.S)) move -= forward;
            if (Input.GetKey(KeyCode.A)) move -= right;
            if (Input.GetKey(KeyCode.D)) move += right;
        } else {
            if (Input.GetKey(KeyCode.W)) move += transform.forward;
            if (Input.GetKey(KeyCode.S)) move -= transform.forward;
            if (Input.GetKey(KeyCode.A)) move -= transform.right;
            if (Input.GetKey(KeyCode.D)) move += transform.right;
        }

        if (move != Vector3.zero) {
            rb.AddForce(move.normalized * force, ForceMode.Force);
        }
    }
}
