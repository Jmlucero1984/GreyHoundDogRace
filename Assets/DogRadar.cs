using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DogRadar : MonoBehaviour
{
    List<GameObject> perros;
    public TextMeshProUGUI textMesh;
    Dictionary<string, float[]> coordPolares = new();

    private float timer = 0.0f;
    private float waitTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        perros = GetComponentInParent<GameManager>().Participantes;
        //textMesh = GameObject.Find("TextoRadar").GetComponent<TextMesh>();

         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            timer = 0;
            foreach (var actual in perros)
            {
                var actualGO=actual.GetComponent<AIControl>();
                actualGO.front=null;
                actualGO.right=null;
                actualGO.left=null;
                foreach (var another in perros)
                {
                    if(actual!=another){
                        float dist = Vector3.Distance(actual.transform.position, another.transform.position);
                        Vector3 targetDir = another.transform.position - actual.transform.position;
                        float angle = Vector3.Angle(targetDir, actual.transform.forward );
                        if((angle>330 || angle <30)&& dist<2 ){
                            actualGO.front=another;
                        }
                        if((angle>30 && angle <150)&& dist<2 ){
                            actualGO.right=another;
                        }
                         if((angle>210 && angle <330)&& dist<2 ){
                            actualGO.left=another;
                        }
                        //coordPolares[item.name] = new float[] { angle, dist };
                        
                    }


                }

            }/*
            textMesh.text = "";
            foreach (var item in coordPolares)
            {
                textMesh.text += "Perro name: " + item.Key + " - " + item.Value[0] + " | " + item.Value[1] + "\n";
            }*/
        }
         
    }
}
