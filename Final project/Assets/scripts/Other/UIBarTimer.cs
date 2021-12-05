using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBarTimer : MonoBehaviour
{
    public static UIBarTimer instance { get; private set; }

    public Image maskTimer;
    float originalSize;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalSize = maskTimer.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {
        maskTimer.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
