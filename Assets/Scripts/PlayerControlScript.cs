using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerControlScript : MonoBehaviour
{
	public Vector3 previousPosition;

	public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public Vector3 moveDirection = Vector3.zero;
    public CharacterController controller;
    public float runSpeed;
    public float creepSpeed;
	public float airMoveSpeed;

    public static PlayerControlScript Instance;
    void Awake() { Instance = this; }

    void Start()
    {
        controller = GetComponent<CharacterController>();
		previousPosition = transform.position;
	}

    void Update()
    {
		float speedMod = 1;
        if(Input.GetButton("Creep"))
            speedMod = creepSpeed;
        if (Input.GetButton("Run"))
            speedMod = runSpeed;

		if (controller.isGrounded)
		{
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed * speedMod;
			if (Input.GetButton("Jump"))
			{
				Input.ResetInputAxes();
				moveDirection = controller.velocity;
				moveDirection.y = jumpSpeed;
			}
			moveDirection.y -= gravity * Time.deltaTime;
			controller.Move(moveDirection * Time.deltaTime);

		}
		else
		{
			Vector3 airMoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			airMoveDirection = transform.TransformDirection(airMoveDirection);
			airMoveDirection *= airMoveSpeed;
			moveDirection.y -= gravity * Time.deltaTime;
			controller.Move((moveDirection + airMoveDirection) * Time.deltaTime);
		}
		Debug.DrawLine(previousPosition, transform.position, Color.red, Mathf.Infinity);
		previousPosition = transform.position;
	}
}
