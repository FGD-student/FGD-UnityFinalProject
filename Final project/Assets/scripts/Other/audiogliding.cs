using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiogliding : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip glideSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
             playMyAudio(glideSound);
        }
        else if (Input.GetKeyUp(KeyCode.Space) || Playermovement.timeLeftGliding < 0 || Playermovement.isGrounded)
        {
            stopMyAudio(glideSound);
        }
    }

    public void playMyAudio(AudioClip cliptoPlay)
    {
        audioSource.clip = cliptoPlay;
        audioSource.Play();
    }

    public void stopMyAudio(AudioClip cliptoPlay)
    {
        audioSource.Stop();
    }
}
