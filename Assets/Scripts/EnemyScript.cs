using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour
{
	private Transform[] allChildren;
	public List<Rigidbody> myRigidbodies = new List<Rigidbody>();
	public Dictionary<Rigidbody, Vector3> velocities = new Dictionary<Rigidbody, Vector3>();

	void Start()
	{
		allChildren = GetComponentsInChildren<Transform>();
		foreach(Transform child in allChildren)
		{
			if(child.GetComponent<Rigidbody>())
			{
				myRigidbodies.Add(child.GetComponent<Rigidbody>());
				child.GetComponent<Rigidbody>().isKinematic = true;
			}
		}
	}
	public void Ragdoll()
	{
        Destroy(GetComponent<TestDummyScript>());
        Destroy(GetComponent<CharacterController>());
        if(GetComponent<Animator>() != null)
    		Destroy(GetComponent<Animator>());
        if (GetComponent<Animation>() != null)
            Destroy(GetComponent<Animation>());
        foreach (Transform child in allChildren)
		{
			if(child.GetComponent<Rigidbody>())
			{
				child.GetComponent<Rigidbody>().isKinematic = false;
			}
		}
	}

    public void Unragdoll()
    {
        foreach (Transform child in allChildren)
        {
            if (child.GetComponent<Rigidbody>())
            {
                //Debug.Log("test2");
                child.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
}

