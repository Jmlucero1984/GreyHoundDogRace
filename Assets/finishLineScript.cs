using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishLineScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject gameManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
        

        gameManager.GetComponent<GameManager>().finishLineStepped(other);
    }
}
