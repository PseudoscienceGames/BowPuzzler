using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour
{
    private Vector3 previousPosition;
    private float forceAmt = 1000f;
    public bool stopped;

    void Start()
    {
        previousPosition = transform.position;
        GetComponent<Rigidbody>().useGravity = false;
        Launch();

    }

    void Update()
    {
        if (!stopped)
        {
            Ray ray = new Ray(previousPosition, transform.position - previousPosition);
            Debug.DrawLine(previousPosition, transform.position, Color.red, Mathf.Infinity);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit) && hit.transform != transform)
            {
                if (Vector3.Distance(previousPosition, hit.point) <= Vector3.Distance(previousPosition, transform.position))
                {
                    GetComponent<Rigidbody>().isKinematic = true;
                    transform.position = hit.point;
                    transform.parent = hit.transform;
                    Vector3 newScale = hit.transform.localScale;
                    transform.localScale = new Vector3(1 / newScale.x, 1 / newScale.y, 1 / newScale.z);
                    GetComponent<Collider>().enabled = true;
                    if (hit.transform.root.GetComponent<EnemyScript>() != null)
                    {
                        hit.transform.root.GetComponent<EnemyScript>().Ragdoll();
                    }
                    if (hit.transform.GetComponent<Rigidbody>() != null)
                    {
                        hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * forceAmt, hit.point);
                    }
                    stopped = true;
                    Destroy(GetComponent<Rigidbody>());
                    Destroy(this);

                }
            }
            

            previousPosition = transform.position;
            transform.LookAt(transform.position + GetComponent<Rigidbody>().velocity);
        }
    }

	public void Launch()
	{
		GetComponent<Rigidbody>().useGravity = true;
		GetComponent<Rigidbody>().AddForce(transform.forward * BowScript.Instance.shotForce);
	}
}
