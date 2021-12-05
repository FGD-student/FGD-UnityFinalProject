using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private Animator playerAnim;
    public GameObject Door;
    public AudioClip collectedKeyClip;
    public AudioClip DoorClip;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Playermovement controller = collision.GetComponent<Playermovement>();

        if (collision.tag == "Player")
        {
            Collected();
            Door.GetComponent<Door>().OpenDoor();
            controller.PlaySound(collectedKeyClip);
            controller.PlaySound(DoorClip);
        }
    }

    public void Collected()
    {
        Destroy(this.gameObject);
        Debug.Log("Collected key");
    }    
}
