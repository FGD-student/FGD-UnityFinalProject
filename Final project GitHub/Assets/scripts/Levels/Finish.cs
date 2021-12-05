using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private Animator animation;
    public AudioClip collectedClip;

    void Start()
    {
        animation = GetComponent<Animator>();        

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Playermovement controller = collision.GetComponent<Playermovement>();

        if (collision.tag == "Player")
        {
            animation.SetTrigger("Change");
            controller.PlaySound(collectedClip);
        }
    }

    public void LoadNewScene()
    {
        GameManager.MyInstance.Finish();
    }
}
