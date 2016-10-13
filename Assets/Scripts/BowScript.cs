using UnityEngine;
using System.Collections;

public class BowScript : MonoBehaviour
{
	public float timeTilShot = 0;
	public int shotForce;
	public GameObject arrowPrefab;

	public static BowScript Instance;
	void Awake() { Instance = this; }

	void Update()
	{
		if (timeTilShot > 0)
			timeTilShot -= Time.deltaTime;
		if (timeTilShot <= 0 && Input.GetButton("Fire1"))
		{
			Fire();
			timeTilShot = 0.2f;
		}
	}

	void Fire()
	{
		Transform camTransform = Camera.main.transform;
		GameObject newArrow = Instantiate(arrowPrefab, (camTransform.forward * 1.15f) + camTransform.position, camTransform.rotation) as GameObject;
	}
}
