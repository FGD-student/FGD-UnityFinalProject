using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animation;

    void Start()
    {
        animation = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        animation.SetTrigger("OpenDoor");
    }
}
