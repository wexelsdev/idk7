using UnityEngine;

public class Camera : MonoBehaviour {
    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f; 

    private float x = 0.0f;
    private float y = 0.0f;

    private void Start() {
        if (target != null) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            Vector3 angles = transform.eulerAngles;
            x = angles.y;
            y = angles.x;
        }
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = !Cursor.visible;
        }
    }
    
    private void LateUpdate() {
        if (target != null && Cursor.lockState == CursorLockMode.Locked) {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            Vector3 desiredCameraPos = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
            Vector3 direction = desiredCameraPos - target.position;
            float currentDistance = distance;

            RaycastHit hit;
            if (Physics.Raycast(target.position, direction.normalized, out hit, distance)) {
                currentDistance = hit.distance - 0.1f;
                if (currentDistance < 0.1f) currentDistance = 0.1f;
            }

            Vector3 finalPosition = rotation * new Vector3(0.0f, 0.0f, -currentDistance) + target.position;

            transform.rotation = rotation;
            transform.position = finalPosition;
        }
    }

    private static float ClampAngle(float angle, float min, float max) {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}