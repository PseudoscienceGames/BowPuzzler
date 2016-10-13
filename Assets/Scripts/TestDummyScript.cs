using UnityEngine;
using System.Collections;

public class TestDummyScript : MonoBehaviour
{
    public Vector3 moveDirection;

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float runSpeed;
    public float creepSpeed;
    public Vector3 moveTarget;
    public bool move;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (move)
        {
            float speedMod = 1;
            //if (Input.GetButton("Creep"))
            //    speedMod = creepSpeed;
            //if (Input.GetButton("Run"))
            //    speedMod = runSpeed;
            moveDirection = (moveTarget - transform.position).normalized * speed * speedMod;
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.y = 0;
            if (controller.isGrounded)
            {
                moveDirection.y = 0;
                //if (Input.GetButton("Jump"))
                //    moveDirection.y = jumpSpeed;
            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }
}
