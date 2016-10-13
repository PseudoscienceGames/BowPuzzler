using UnityEngine;
using System.Collections;

public class WaitingArrowScript : MonoBehaviour
{
    public int shotForce;
    private Vector3 previousPosition;
    public float myRotation;
    public Transform graphics;
    public bool launch;
    public bool isWaitingArrow;
    public bool isTeleportingArrow;

    void Start()
    {
        previousPosition = transform.position;
        GetComponent<Rigidbody>().useGravity = false;
        if (!isWaitingArrow)
            Launch();
    }

    void Update()
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
                GetComponent<Collider>().enabled = true;
                Destroy(this);
                Destroy(GetComponent<Rigidbody>());
            }
        }

        previousPosition = transform.position;
        transform.LookAt(transform.position + GetComponent<Rigidbody>().velocity);
        graphics.Rotate(Vector3.forward * myRotation * Time.deltaTime);

        if (launch)
        {
            Launch();
            launch = false;
        }
    }

    public void Launch()
    {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().AddForce(transform.forward * shotForce);
    }
}
