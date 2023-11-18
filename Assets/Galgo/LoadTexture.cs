using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadTexture : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string filePath = "D:/PerroAtigrado_con_ojos_y_lengua.png";
        var textura = LoadPNG(filePath);
        Transform child1 = transform.Find("Cuerpo");
        Debug.Log("EL nombre del objeto es: " + child1.name);
        var name = child1.GetComponent<SkinnedMeshRenderer>().material.mainTexture = textura;
                //var name = child1.GetComponent<SkinnedMeshRenderer>().material.mainTexture = Resources.Load("DalmataTexture") as Texture;
                Debug.Log("El nombre de la texture es: " + name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
}
