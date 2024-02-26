using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;

    public Vector3 offset = new Vector3 (5, 0, 0);
    public Vector3 GetDestination()
    {
        return destination.position + offset;
    }
}