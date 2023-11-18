using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraTravelling : MonoBehaviour
{

    public int currentWP = 0;
    public GameObject caseta;
    public Slider miSliderElement;
    public GameObject puntero;
  
    //the array of waypoints in order of visiting
    //can be made up of any game objects
    public GameObject[] wps;
     
    [SerializeField] float speed = 40;
    [SerializeField] int rotationSpeed = 5;

    //how close to get to the waypoint to consider having reached it
    [Tooltip("Hola soy un tooltip")]
    [SerializeField] float accuracy = 5.0f;
    
    // Start is called before the first frame update
    void Awake()
    {
        
    }
 
    void Start()
    {
        
        
    }



    void Update()
    {
        if (wps.Length == 0) return;
        if (Vector3.Distance(wps[currentWP].transform.position,
            transform.position) < accuracy)
        {
           
            currentWP++;

            if (currentWP >= wps.Length)
            {
                currentWP = 0;
            }
        }

        //rotate towards target
        Vector3 direction = wps[currentWP].transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        transform.Translate(0, 0, Time.deltaTime * speed * miSliderElement.value);
        var angle=puntero.transform.localEulerAngles.y;
        //Debug.Log("PUNTERO ANGLE: "+angle);
        speed=40+(angle-285)/3;
        
         
      
        
    }
}



