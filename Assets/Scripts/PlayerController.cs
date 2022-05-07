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
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
        
        // allow jumping only if player is touching ground -- no double jumps
        if (controller.isGrounded) {
            // moveDirection.y = 0f;

            if (Input.GetButtonDown("Jump")) {
                moveDirection.y = jumpForce;
            }
        }
  

        // gravity
        moveDirection.y = moveDirection.y + Physics.gravity.y * gravityScale;

        // update character controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}
