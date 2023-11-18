using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class midTrackLineScript : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    } 
     void OnTriggerEnter(Collider other){
        //Debug.Log(" #################  MID TRACK LINE ##################");


        gameManager.GetComponent<GameManager>().midTrackLineStepped();
    }
}
