using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimVelController : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider miSliderElement;
    public Text mitexto;
    public Animator miAnimator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mitexto.text = "Vel: " + Math.Round(miSliderElement.value, 2);
        GetComponent<Animator>().speed = miSliderElement.value;
    }
}
