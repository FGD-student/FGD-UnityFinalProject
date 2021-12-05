using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishEnding : MonoBehaviour
{
    private Animator animation;
    float originalPosition;
    private float floatStrength = 0.3f;
    AudioSource audioSource;

    void Start()
    {
        animation = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
        Playermovementend controller = collision.GetComponent<Playermovementend>();

        if (collision.tag == "PlayerEnd")
        {
            animation.SetTrigger("Ending");
            audioSource.Play();
            Destroy(collision.gameObject);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}
