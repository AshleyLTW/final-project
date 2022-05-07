using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;

    private Vector3 moveDirection;
    public float gravityScale;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // move in direction that player is facing
        float yStore = moveDirection.y;
        moveDirection = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        // normalize to stop player from zoooooming when they hit two arrow keys at once
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        // allow jumping only if player is touching ground -- no double jumps
        if (controller.isGrounded) {
            moveDirection.y = 0f;

            if (Input.GetButtonDown("Jump")) {
                moveDirection.y = jumpForce;
            }
        }
  

        // gravity
        moveDirection.y = moveDirection.y + Physics.gravity.y * gravityScale * Time.deltaTime;

        // update character controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}
