using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBarGliding : MonoBehaviour
{
    public static UIBarGliding instance { get; private set; }

    public Image maskGliding;
    float originalSize;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalSize = maskGliding.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {
        maskGliding.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
