using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalgoWaypoints : MonoBehaviour {

    public int currentWP = 0;

    //the array of waypoints in order of visiting
    //can be made up of any game objects
    public GameObject[] wps;

    int speed = 20;
    int rotationSpeed = 2;

    //how close to get to the waypoint to consider having reached it
    float accuracy = 5.0f;

    void Update () 
    {
        if(wps.Length == 0) return;
        if(Vector3.Distance(wps[currentWP].transform.position, 
            transform.position) < accuracy)
        {
            currentWP++;
            if(currentWP >= wps.Length)
            {
                currentWP = 0;
            }   
        }
        
        //rotate towards target
        Vector3 direction = wps[currentWP].transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        transform.Translate(0, 0, Time.deltaTime * speed);
    }
}