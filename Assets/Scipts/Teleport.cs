using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;

    public Vector3 offset = new Vector3 (0, 4, 0);
    public Transform GetDestination()
    {
        return destination;
    }
}