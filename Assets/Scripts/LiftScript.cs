using UnityEngine;
using System.Collections;

public class LiftScript : MonoBehaviour
{
    public Transform minHeight;
    public Transform maxHeight;
    public float weight;

    public void UpdateWeight(float deltaWeight)
    {
        weight += deltaWeight;
    }

    public void ChangePos()
    {
        //if()
    }
}
