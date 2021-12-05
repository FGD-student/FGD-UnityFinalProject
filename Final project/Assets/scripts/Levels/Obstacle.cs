using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] protected float damage = 1;

    //audio
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.tag == "Player")
        {
            Collision();
            audioSource.Play();

        }
    }

    public void Collision()
    {
        Playermovement.MyInstance.PlayerHit(damage);
    }
}
