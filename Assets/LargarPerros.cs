using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargarPerros : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        var anim = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (anim.IsName("Open")&& anim.normalizedTime>0.2)
        {
            GameManager.largar = true;

            Debug.Log("ABRIO!!!");
          
        }
    }
}
