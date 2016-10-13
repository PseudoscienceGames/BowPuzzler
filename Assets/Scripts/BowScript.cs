using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BowScript : MonoBehaviour
{
	public List<ArrowScript> waitingArrows = new List<ArrowScript>();
	public float timeTilShot = 0;
	public int shotForce;
	public bool teleportingArrowOut = false;
	public GameObject teleportingArrow;
	public List<GameObject> arrowTypes = new List<GameObject>();
	public int arrowType;
	public Text arrowTypeName;

	public static BowScript Instance;
	void Awake() { Instance = this; }

	void Start()
	{
		SwitchArrowType(0);
	}

	void Update()
	{
		if (timeTilShot > 0)
			timeTilShot -= Time.deltaTime;
		if (timeTilShot <= 0 && Input.GetButton("Fire1"))
		{
			Fire();
			timeTilShot = 0.2f;
		}
		if (Input.GetButtonDown("Fire2"))
		{
			LaunchWaitingArrows();
		}

		if (Input.GetAxis("Mouse ScrollWheel") != 0)
		{
			SwitchArrowType((int)Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")));
		}
	}

	void Fire()
	{
		Transform camTransform = Camera.main.transform;
		GameObject newArrow = Instantiate(arrowTypes[arrowType], (camTransform.forward * 1.15f) + camTransform.position, camTransform.rotation) as GameObject;
		if (newArrow.GetComponent<ArrowScript>().isTeleportingGrabberArrow)
		{
			if (teleportingArrowOut)
			{
				newArrow.GetComponent<ArrowScript>().isTeleportingGrabberArrow = false;
				newArrow.GetComponent<ArrowScript>().isTeleportingTargetLocArrow = true;
				teleportingArrowOut = false;
				newArrow.GetComponent<ArrowScript>().teleportingGrabberArrow = teleportingArrow.transform;
			}
			else
			{
				teleportingArrowOut = true;
				teleportingArrow = newArrow;
			}
		}
	}

	void LaunchWaitingArrows()
	{
		foreach (ArrowScript arrow in waitingArrows)
		{
			arrow.Launch();
		}
		waitingArrows.Clear();
	}

	public void AddWaitingArrow(ArrowScript arrow)
	{
		waitingArrows.Add(arrow);
	}

	public void SwitchArrowType(int dir)
	{
		arrowType += dir;
		if (arrowType < 0)
			arrowType = arrowTypes.Count - 1;
		if (arrowType > arrowTypes.Count - 1)
			arrowType = 0;
		arrowTypeName.text = arrowTypes[arrowType].name;

	}
}
