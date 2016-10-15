using UnityEngine;
using System.Collections;

public class CameraControler : MonoBehaviour
{
	public float mouseXSpeed;
	public float mouseYSpeed;
	public int thirdPersonCamDistance;
	public bool fP;

	void Update()
	{
		transform.Rotate(transform.up * mouseYSpeed * Input.GetAxis("Mouse X"));
		transform.FindChild("Cam").Rotate(-Vector3.right * mouseXSpeed * Input.GetAxis("Mouse Y"));
		if(Input.GetButtonDown("Fire2"))
		{
			ToggleViewMode();
		}
	}

	void ToggleViewMode()
	{
		if (fP)
		{
			GetComponentInChildren<Camera>().transform.localPosition = Vector3.forward * -thirdPersonCamDistance;
			fP = false;
		}
		else
		{
			GetComponentInChildren<Camera>().transform.localPosition = Vector3.zero;
			fP = true;
		}
	}
}
