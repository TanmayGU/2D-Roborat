using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedColliders = new List<Collider2D>();
    public List<string> collisionTag = new List<string>();
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
        //collisionTag.Add(collision.tag);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision);
    }

}
