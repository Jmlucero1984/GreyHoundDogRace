using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OndeoBanderaRandom : MonoBehaviour
{
    Animator m_Animator;
    // Start is called before the first frame update
 
    void Start()
    {
        //Get the animator, attached to the GameObject you are intending to animate.
        m_Animator = gameObject.GetComponent<Animator>();
         m_Animator.Play("Ondeo", -1, UnityEngine.Random.Range(0.0f, 1.0f));
        m_Animator.speed = UnityEngine.Random.Range(1.8F, 2.3F);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
