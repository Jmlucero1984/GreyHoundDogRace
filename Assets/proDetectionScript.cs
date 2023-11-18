using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proDetectionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
        void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(transform.name=="Dalmata"){
                //GameObject child1 = transform.gameObject;
                Debug.Log("-----"+transform.name+"-----");
                Debug.Log("X: "+transform.position.x);
                Debug.Log("Y: "+transform.position.y);
                Debug.Log("Z: "+transform.position.z);
            }
        }
      
    }
}
