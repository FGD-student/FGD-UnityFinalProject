using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplenationUI : MonoBehaviour
{
    public GameObject ExplenationBox;
    float timerDisplay;
    private Animator Anim;

    void Start()
    {
        ExplenationBox.SetActive(true);
        timerDisplay = 3f;
        Anim = GetComponent<Animator>();
    }

    void Update()
    {
        timerDisplay -= Time.deltaTime;
        if (timerDisplay < 0)
        {
              Anim.SetBool("Inactive", true);
        }
    }
}
