using UnityEngine;
using System.Collections;

public class CameraControler : MonoBehaviour
{
	public float mouseXSpeed;
	public float mouseYSpeed;
	public float uhhhh;
	public float minSpeed;
	public float maxSpeed;
	public float secToMaxSpeed;
	public float timer;
	public float minToRamp;

	void Update()
	{
		transform.Rotate(transform.up * mouseYSpeed * Input.GetAxis("Mouse X"));
		transform.FindChild("Cam").Rotate(-Vector3.right * mouseXSpeed * Input.GetAxis("Mouse Y"));


		//if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
		//{
		//	timer += Time.deltaTime / secToMaxSpeed;
		//	float x = Input.GetAxis("Mouse X") * Mathf.Abs(Input.GetAxis("Mouse X")) * Time.deltaTime;
		//	float y = Input.GetAxis("Mouse Y") * Mathf.Abs(Input.GetAxis("Mouse Y")) * Time.deltaTime;
		//	if(Mathf.Abs(Input.GetAxis("Mouse X")) > minToRamp)
		//		x = Mathf.Lerp(x, maxSpeed * Time.deltaTime, timer);
		//	if(Mathf.Abs(Input.GetAxis("Mouse Y")) > minToRamp)
		//		y = Mathf.Lerp(y, maxSpeed * Time.deltaTime, timer);
		//	Debug.Log(x / Time.deltaTime + "  " + y / Time.deltaTime);
		//	transform.Rotate(transform.up * mouseYSpeed * x);
		//	transform.FindChild("Cam").Rotate(-Vector3.right * mouseXSpeed * y);
		//}
		//else
		//	timer = 0;
	}
}
