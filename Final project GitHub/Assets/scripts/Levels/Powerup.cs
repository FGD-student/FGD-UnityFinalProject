using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    float originalPosition;
    private float floatStrength = 0.3f;

    private Animator animation;
    public AudioClip collectedClip;

    void Start()
    {
        this.originalPosition = this.transform.position.y;
    }

    void Update()
    {
       transform.position = new Vector3(transform.position.x,
           originalPosition + ((float)Mathf.Sin(Time.time) * floatStrength),
           transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Playermovement controller = collision.GetComponent<Playermovement>();

        if (collision.tag == "Player")
        {
            Collected();
            controller.PlaySound(collectedClip);

        }
    }

    public void Collected()
    {
        Destroy(this.gameObject);
    }
}





