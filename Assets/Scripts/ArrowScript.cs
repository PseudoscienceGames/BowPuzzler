using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour
{
    private Vector3 previousPosition;
    public bool isWaitingArrow;
    public bool isSelfTeleportingArrow;
    public bool isFreezingArrow;
    public bool isTeleportingGrabberArrow;
    public bool isTeleportingTargetLocArrow;
    public bool isExplosiveArrow;
    public bool isMineArrow;
    private float forceAmt = 1000f;
    public float freezeColRadius;
    public Transform iceSpike;
    public Transform teleportingGrabberArrow;
    public bool stopped;
    public bool exploded;
    public float explosionForce;
    public float explosionRadius;

    void OnTriggerEnter(Collider other)
    {
        if (stopped && other.GetComponent<EnemyScript>() != null && isMineArrow && !exploded)
        {
            Explode();
            exploded = true;
        }
    }

    void Start()
    {
        previousPosition = transform.position;
        GetComponent<Rigidbody>().useGravity = false;
        if (!isWaitingArrow)
            Launch();
        if (isWaitingArrow)
            BowScript.Instance.AddWaitingArrow(this);

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
                    if (isFreezingArrow)
                        StartCoroutine(Freeze());
                    if (isSelfTeleportingArrow)
                        TeleportPlayer();
                    if (isTeleportingTargetLocArrow)
                        Teleport();
                    if (isExplosiveArrow)
                        Explode();
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

    public void Teleport()
    {
        if(teleportingGrabberArrow.transform.root.GetComponent<Rigidbody>() != null)
            teleportingGrabberArrow.transform.root.position = transform.position;
    }

    public void Launch()
    {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().AddForce(transform.forward * BowScript.Instance.shotForce);
    }

    public void TeleportPlayer()
    {
        PlayerControlScript.Instance.transform.position = transform.position;
    }

    IEnumerator Freeze()
    {
        GetComponent<CapsuleCollider>().radius = 0.1f;
        float scale = 0;
        while (iceSpike.localScale.x < 1)
        {
            scale += Time.deltaTime;
            iceSpike.localScale = Vector3.one * scale;
            yield return null;
        }
        iceSpike.localScale = Vector3.one;
    }

    void Explode()
    {
        Collider[] itemsInRange = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider item in itemsInRange)
        {
            if (item.GetComponent<EnemyScript>() != null)
            {
                item.GetComponent<EnemyScript>().Ragdoll();
            }
        }
        itemsInRange = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider item in itemsInRange)
        {
            if (item.GetComponent<Rigidbody>() != null)
            {
                item.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }
}
