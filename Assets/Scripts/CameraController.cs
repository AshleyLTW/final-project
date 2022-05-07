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

    // Start is called before the first frame update
    void Start()
    {
        if (!useOffsetValues) {
            offset = target.position - transform.position;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // rotate player based on mouse
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        // vertical applied to x and horizontal applied to y
        target.Rotate(-vertical, horizontal, 0);

        // rotate camera around player (and mouse) + camera follows player around
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = target.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);

        transform.position = target.position - rotation * offset;
        transform.LookAt(target);
    }
}
