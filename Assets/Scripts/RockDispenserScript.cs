using UnityEngine;
using System.Collections;

public class RockDispenserScript : MonoBehaviour
{
    public GameObject rock;
    public int forceAmt;
    public bool GO;

    void DispenseRock()
    {
        GameObject currentRock = Instantiate(rock, transform.position, Quaternion.identity) as GameObject;
        currentRock.GetComponent<Rigidbody>().AddForce(transform.forward * forceAmt);
    }

    void Update()
    {
        if(GO)
        {
            DispenseRock();
            GO = false;
        }
    }
}
