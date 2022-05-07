using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // target is the player
    public Transform target;
    public Vector3 offset;
    public bool useOffsetValues;
    public float rotateSpeed;
    // pivot allows us to rotate camera up/down using mouse while rotating camera & player left/right using mouse
    public Transform pivot;
    public float maxViewAngle;
    public float minViewAngle; // negative value

    // Start is called before the first frame update
    void Start()
    {
        if (!useOffsetValues) {
            offset = target.position - transform.position;
        }

        // move pivot to player and set the player as pivot's parent so it follows the player around
        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        // hide cursor once game starts
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    // Use LateUpdate to make sure that the player updates before the camera
    void LateUpdate()
    {
        // rotate player based on mouse
        // vertical applied to x and horizontal applied to y
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(-vertical, 0, 0);

        // limit up/down camera angle
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f) {
            pivot.rotation = Quaternion.Euler(45f, 0, 0);
        } else if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle) {
            pivot.rotation = Quaternion.Euler(315f, 0, 0);
        }

        // rotate camera around player (and mouse) + camera follows player around
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);

        transform.position = target.position - rotation * offset;
        // stop camera from clipping through the world
        if (transform.position.y < target.position.y) {
            transform.position = new Vector3 (transform.position.x, target.position.y, transform.position.z);
        }

        transform.LookAt(target);
    }
}
