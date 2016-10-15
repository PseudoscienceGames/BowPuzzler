using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerControlScript : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public Vector3 moveDirection = Vector3.zero;
    public CharacterController controller;
    public float runSpeed;
    public float creepSpeed;

    public static PlayerControlScript Instance;
    void Awake() { Instance = this; }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float speedMod = 1;
        if(Input.GetButton("Creep"))
            speedMod = creepSpeed;
        if (Input.GetButton("Run"))
            speedMod = runSpeed;
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * speed, moveDirection.y, Input.GetAxis("Vertical") * speed * speedMod);
        moveDirection = transform.TransformDirection(moveDirection);
        if (controller.isGrounded)
        {
            moveDirection.y = 0;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
