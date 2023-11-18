using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LiebreWayPoints : MonoBehaviour {

    public int currentWP = 0;
    public GameObject caseta;
    public Slider miSliderElement;
    public Text mitexto;
    //the array of waypoints in order of visiting
    //can be made up of any game objects
    private bool alreadyLaunched = false;
    [Header("Game Manager")]
    [SerializeField] GameManager gameManager;
    public GameObject[] wps;
    private GameObject[] perros;
    [SerializeField] float speed = 40;
    [SerializeField] int rotationSpeed = 5;

    //how close to get to the waypoint to consider having reached it
    [Tooltip("Hola soy un tooltip")]
    [SerializeField] float accuracy = 5.0f;
    public AudioClip clipDisparo;
    public AudioClip startingCrowd;
    public AudioClip shotCrowd;
    private AudioSource audioSourceDisparo;
    private AudioSource audioSourceStartingCrowd;
    private AudioSource audioSourceShotCrowd;
    // Start is called before the first frame update
    void Awake()
    {
        audioSourceDisparo = CreateAudioSource(clipDisparo, false);
        audioSourceStartingCrowd = CreateAudioSource(startingCrowd, false);
        audioSourceShotCrowd = CreateAudioSource(shotCrowd, false);
    }
    private AudioSource CreateAudioSource(AudioClip audioClip, bool startPlayingImmediately)
    {
        GameObject audioSourceGO = new GameObject();
        audioSourceGO.transform.parent = transform;
        audioSourceGO.transform.position = transform.position;
        AudioSource newAudioSource =
        audioSourceGO.AddComponent<AudioSource>();
        newAudioSource.clip = audioClip;
        if (startPlayingImmediately)
            newAudioSource.Play();
        return newAudioSource;
    }
    void Start()
    {
        audioSourceStartingCrowd.Play();
        perros = GameObject.FindGameObjectsWithTag("Perro");
    }
    
         
   
    void Update()
    {
        if (!audioSourceStartingCrowd.isPlaying)
        {
            audioSourceStartingCrowd.Play();
        }
        if (wps.Length == 0) return;
        if (Vector3.Distance(wps[currentWP].transform.position, transform.position) < accuracy)
        {
            if (wps[currentWP].name == "WP_lateral_202" && !alreadyLaunched)
            {
                alreadyLaunched = true;
                Debug.Log("YAAA!!!!!");
                gameManager.startCrono();
                audioSourceShotCrowd.Stop();
                audioSourceDisparo.Play();
                audioSourceShotCrowd.Play();
                caseta.GetComponent<Animator>().SetTrigger("Abrir");
                foreach (var perro in perros)
                {
                    perro.GetComponent<Animator>().SetTrigger("largada");
                }
            }
            
                /*  if (wps[currentWP].name == "WP_lateral_0")
            {
                AgentManager.largados = true;
            }*/
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
        mitexto.text = "Vel: " + Math.Round(miSliderElement.value,2);
         if (ActionsOnScreen.firstPosition != null)
        {
            var positionLiebre = transform.position;
            var vectorLiebrePerro = positionLiebre - ActionsOnScreen.firstPosition.transform.position;
            var distancia = (float)Math.Round(vectorLiebrePerro.magnitude, 4);
            speed=40+(20-distancia)/3;
      

        }
        
       

    }
}