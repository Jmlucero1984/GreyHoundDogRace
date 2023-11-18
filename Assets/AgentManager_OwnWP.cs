using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



public class AgentManager_OwnWP : MonoBehaviour
{
    public int currentWP = 0;
    private UnityEngine.AI.NavMeshAgent[] navAgents;
    public Transform targetMarker;
    public GameObject[] wps;
    public GameObject[] Participantes;
    public float verticalOffset = 1.0f;
    public static bool larguen = false;
    public static bool largados = false;
    public static bool largar = false;
    [Tooltip("Hola soy un tooltip")]
    [SerializeField] float accuracy = 5.0f;
    private static string _fileName = "";
    //private static string _filePath = "(no file path yet)";
    private static string _folderName = "Logs";
    //float randomVelocity = 0;
    public GameObject banderin;
    public GameObject banderin2;


    void Start()
    {
       
 

    }

    

  
    void Update()

    {
    }

}
