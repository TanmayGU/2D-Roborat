using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedColliders = new List<Collider2D>();
    public List<Collider2D> goodColliders = new List<Collider2D>();

    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" /*&& GameObject.FindGameObjectWithTag("Player").health != 0*/)
        {
            detectedColliders.Add(collision);
        }
        //else if (collision.tag == "KnightEnemy")
        //{
        //    goodColliders.Add(collision);
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision);
    }

}
