using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrintsCreator : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Prefab;
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PistaNavMesh")
        {
            
            SpawnDecal(Prefab);
            
        }
       // Debug.Log("HAN PISADO");
    }
    // Update is called once per frame
    void Update()
    {
      
    }
    private void SpawnDecal(GameObject prefab)
    {
        //we want to cast a ray(line) from the player to the ground
       // Vector3 from = this.transform.position;
       
         
            GameObject decal = Instantiate(prefab);
        decal.transform.position = this.transform.position;
            //turn the footprint to match the direction the player is facing
            decal.transform.Rotate(Vector3.up, this.transform.eulerAngles.y);
        
    }
}
