using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PonerBanderines : MonoBehaviour
{
    public GameObject banderin;
    public GameObject banderin2;
    private GameObject[] wps;
    public Vector3 destinoBanderas;
    private GameObject elegido;

    // Start is called before the first frame update

    void Start()
    {

        wps = GetComponentInParent<GameManager>().wps;
        
        
        elegido = GameObject.Find("Dalmata");
        banderin = GetComponentInParent<GameManager>().banderin;
        banderin2 = GetComponentInParent<GameManager>().banderin2;

    }

    // Update is called once per frame
    void Update()
    {

        destinoBanderas = GetComponentInParent<GameManager>().destino;
        GameObject bandera = Instantiate(banderin);
            GameObject bandera2 = Instantiate(banderin2);
            bandera.transform.SetParent(GameObject.Find("PistaNavMesh").transform);
            bandera2.transform.SetParent(GameObject.Find("Banderines").transform);

        bandera.transform.position = destinoBanderas;
            float xBias = UnityEngine.Random.Range(-0.5f, 0.5f);
            Vector3 offsetDestination = destinoBanderas;



            offsetDestination.x -= 2 * MathF.Cos(wps[elegido.GetComponent<AIControl>().currentWP].transform.eulerAngles.y / (180F / 3.141592F));
            offsetDestination.z += 2 * MathF.Sin(wps[elegido.GetComponent<AIControl>().currentWP].transform.eulerAngles.y / (180F / 3.141592F));
            bandera2.transform.position = offsetDestination;

        
    }
}
