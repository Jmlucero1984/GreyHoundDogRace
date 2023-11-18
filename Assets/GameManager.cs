using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public GameObject myPrefab;
    [SerializeField] GameObject[] startPositions_;
    public GameObject liebreObj;
    public GameObject actionsOnScreen;
    public GameObject travellingCamera;
    private bool finished=false;
    [SerializeField] Material firstLapGlass;
    [SerializeField] Material secondLapGlass;
    [Header("LAP 1 INFO")]
    [SerializeField] TextMeshPro[] firstLapNumbs;
    private int indexOfLEDnumbers_Lap=0;
     [Header("LAP 2 INFO")]
    [SerializeField] TextMeshPro[] secondLapNumbs;
  
    [Header("POINTS TABLE")]

    [SerializeField] GameObject[] participantsInfo;
    [SerializeField] int[] puntos= new int[] {10,7,5,4,3,2,1};
    //private bool boolFinishLineStepped=false;
    [Space(10)]
     [Header("OVERALL")]
    [SerializeField] TextMeshPro[] lap_times;
    private int lap_index=0;
    [SerializeField] TextMeshPro[] lap_vels;
    [SerializeField] TextMeshPro winner_slot;
    [SerializeField] TextMeshPro bestPoints_slot;


    [Space(10)]
    [Header("SOUNDS")]
   
    [SerializeField]  AudioClip finishShouts;
    [SerializeField]  int startTimeAudio=0;
    private AudioSource audioSourceFinishShouts;

   
    private int alldogs=6;
    public bool boolStartCrono=false;
 
    private bool boolMidTrackLineStepped=false;
    private bool boolSlowMotionZone=false;
    private int stepped=0;
    private int timeFinishedTransition=4000;
    [Header("WAYPOINTS")]
    public int currentWP = 0;
    private UnityEngine.AI.NavMeshAgent[] navAgents;
    public Transform targetMarker;
    public GameObject[] wps;
    public List<GameObject> Participantes;
    public List<GameObject> Iconos;
    public float verticalOffset = 1.0f;
    public static bool larguen = false;
    public static bool largados = false;
    public static bool largar = false;
    [Tooltip("Hola soy un tooltip")]
    [SerializeField] float accuracy = 5.0f;
    private static string _fileName = "";
    //private static string _filePath = "(no file path yet)";
    private static string _folderName = "Logs";
    float randomVelocity = 0;
    public GameObject banderin;
    public GameObject banderin2;
    public Vector3 destino;
    private bool secureBoolStopSlowMotion=false;
    private Color yellow = new Color(1f, 1f, 0.0f, 1.0f);
    private Color green = new Color(0f, 1f, 0f, 1.0f);
    private bool evaluatOrderToLine=false;
    private List<string> alreadypass=new List<string>();
    private float raceTimer=0;
    private int raceTimerInterval=3;
    private int winnerNumeral=0;
    private bool secureFinalStats=false;
    private string winnerName="";
    [ContextMenu("Do Something")]
    void DoSomething()
    {
        Debug.Log("Perform operation");
    }
     
    List<Perro> perros;
    // Start is called before the first frame update
    void  Awake()
    {     
        audioSourceFinishShouts = CreateAudioSource(finishShouts, false);
        firstLapGlass.SetColor("_Color", Color.black);
        secondLapGlass.SetColor("_Color", Color.black);
         
        Debug.Log("Hola aca andamos con los pibes todo bien");
        Debug.Log("LA cantidad ee elementtos son: " + startPositions_.Length);
        XmlDocument document = new XmlDocument();
        document.Load("Assets/dogs_data.xml");
        XmlElement element = document.DocumentElement;
         perros = parseXmlFile(document);
        foreach (var perro in perros)
        {
            string mensaje = "";



            mensaje += perro.Name + " --- ";
            string skinValue;
            perro.Rutas.TryGetValue("Skin", out skinValue);
            mensaje += skinValue + " --- ";
            string iconValue;
            perro.Rutas.TryGetValue("Icon", out iconValue);
            mensaje += iconValue + " --- ";
            Debug.Log(mensaje);
            //Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }

        int cantidad = Mathf.Min(startPositions_.Length, perros.Count);
        Participantes = new List<GameObject>();
       
        for (int i = 0; i < cantidad; i++)
        {
            string filePath;
            perros[i].Rutas.TryGetValue("Skin", out filePath);
            perros[i].startNumber=i+1;
            var mainTextura = LoadPNG(filePath);
            string filePathBump;
            perros[i].Rutas.TryGetValue("Normal", out filePathBump);
            var bumpTextura = LoadPNG(filePathBump);
            GameObject galgoGO = Instantiate(myPrefab);
            galgoGO.GetComponent<AIControl>().numeroDePartida=i+1;
            Material mat = new Material(Shader.Find("Legacy Shaders/Bumped Diffuse"));
            mat.name = perros[i].Name + "_material";
            mat.mainTexture = mainTextura;
            mat.SetTexture("_BumpMap", bumpTextura);
            galgoGO.transform.Find("Cabeza").GetComponent<SkinnedMeshRenderer>().material = mat;
            galgoGO.transform.Find("Cuerpo").GetComponent<SkinnedMeshRenderer>().material = mat;
            galgoGO.transform.Find("Colita").GetComponent<SkinnedMeshRenderer>().material = mat;
            galgoGO.transform.Find("Pies").GetComponent<SkinnedMeshRenderer>().material = mat;
            galgoGO.transform.Find("Orejitas").GetComponent<SkinnedMeshRenderer>().material = mat;
            // Debug.Log("EL MATERIAL ES: " + galgoGO.transform.Find("Cabeza").GetComponent<SkinnedMeshRenderer>().material.shader.name);

           
            galgoGO.transform.position = startPositions_[i].transform.position;
            galgoGO.name = perros[i].Name;
            participantsInfo[i].GetComponent<slotPoints>().setParticipantName(perros[i].Name);
            
            galgoGO.GetComponent<AIControl>().liebre = liebreObj; //GameObject.Find("liebre");

            string iconFilePath;
            perros[i].Rutas.TryGetValue("Icon", out iconFilePath);
            var iconTextura = LoadPNG(iconFilePath);
            Participantes.Add(galgoGO);
            Iconos[i].GetComponent<RawImage>().texture = iconTextura;

           

        }
        //gameObject.AddComponent<PonerBanderines>();
        
        navAgents = FindObjectsOfType(typeof(UnityEngine.AI.NavMeshAgent)) as UnityEngine.AI.NavMeshAgent[];
        larguen = true;

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

    public void finishLineStepped(Collider other){

 
        if(boolMidTrackLineStepped==true){
        
            boolMidTrackLineStepped=false;
          
             
        }
        if(stepped==2){
            GetComponent<ActionsOnScreen>().stillRacing=false;
        }
    }
    public void midTrackLineStepped(){
        if(boolMidTrackLineStepped==false){
            boolMidTrackLineStepped=true;
            stepped+=1;
        }
        
        if(stepped==2) {
            boolSlowMotionZone=true;
        }

    }

    public delegate void callBack(); 
    public callBack playFinalShout;
   
     private void playFinalShoutSound() 
     {
         audioSourceFinishShouts.Play();
     }
    public void corridorZone(){
        evaluatOrderToLine=true;
        if(boolSlowMotionZone==true){

             playFinalShout=playFinalShoutSound;
            GetComponent<SlowMotion>().StartSlowMotion2(playFinalShout);
            //audioSourceFinishShouts.time = startTimeAudio;
       
            
            finished=true;

        }

    }

    public List<Perro> GetDogList()
    {
        return perros;
    }

    public List<GameObject> GetIconos ()
    {
        return Iconos;
    }
    public List<GameObject> GetParticipantes ()
    {
        return Participantes;
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
    public void startCrono(){
        boolStartCrono=true;
       



    }


    // Update is called once per frame
    void Update()
    {

        
       if(evaluatOrderToLine==true){
        //Debug.Log("EVALUANDO evaluatOrderToLine EN STEPPED: "+stepped);
        //sDebug.Log("indexOfLEDnumbers_Lap: "+indexOfLEDnumbers_Lap);
        
       
        Dictionary<GameObject,float> detected = new Dictionary<GameObject,float> ();
        foreach(var dog in Participantes){
            if(dog.transform.position.z<0 && !alreadypass.Contains(dog.name)){
                if(stepped==1){ 
                    if(lap_index==0){getFirstLapPartials();}
                    firstLapGlass.SetColor("_Color", green);}
                if(stepped==2){ 
                    if(lap_index==1&& boolStartCrono==true){
                        getSecondLapPartials();
                        }
                    secondLapGlass.SetColor("_Color", green);}
                alreadypass.Add(dog.name);
                detected.Add(dog,dog.transform.position.z);
            }
        }
        if(detected.Count>0){
        var sortedDict = from entry in detected orderby entry.Value ascending select entry;
         foreach(var kvp in sortedDict){
            var numeral = kvp.Key.GetComponent<AIControl>().numeroDePartida;
               // Debug.Log("COLLIDER: "+numeral);
                if(stepped==1){
                firstLapNumbs[indexOfLEDnumbers_Lap].text=""+numeral;
                participantsInfo[numeral-1].GetComponent<slotPoints>().numeral=numeral;
                participantsInfo[numeral-1].GetComponent<slotPoints>().setL1Points(puntos[indexOfLEDnumbers_Lap]);}
                if(stepped==2){
                if(indexOfLEDnumbers_Lap==0) {
                    
                    winnerNumeral=numeral;
                    winnerName=participantsInfo[numeral-1].GetComponent<slotPoints>().participantName;
                    Debug.Log("EL PRIMERO ES:  # "+winnerNumeral+" "+winnerName);

                }
                secondLapNumbs[indexOfLEDnumbers_Lap].text=""+numeral;
                participantsInfo[numeral-1].GetComponent<slotPoints>().setL2Points(puntos[indexOfLEDnumbers_Lap]);}

                indexOfLEDnumbers_Lap++;         
        }
        }
        if(alreadypass.Count==7){
            Debug.Log("SE DETUVO evaluatOrderToLine en STEPPED: "+stepped);
            evaluatOrderToLine=false;
            alreadypass=new List<string>();
            indexOfLEDnumbers_Lap=0;
            Debug.Log("indexOfLEDnumbers_Lap: "+indexOfLEDnumbers_Lap);
            if(stepped==2) {
                getFinalStats();
            }

        }
       }
       
    }

    private void getFirstLapPartials(){
        double vel=550.0*3.6*(1/raceTimer);
        lap_vels[lap_index].text=String.Format("{00:F2}", vel)+" km/h";
        lap_index=1;
        raceTimer=0;


    }
    private void getSecondLapPartials(){
        double vel=500.0*3.6*(1/raceTimer);
        lap_vels[lap_index].text=String.Format("{00:F2}", vel)+" km/h";
        boolStartCrono=false;
    }
    private void getFinalStats(){
        string name="";
        int numeral=0;
        int points=0;
        Debug.Log("------GET FINALS-------");
        foreach(var i in participantsInfo){
            Debug.Log("------FORECHEANDO-------");
            var el =i.GetComponent<slotPoints>();
            var p=el.TotalPoints;
            if(p>points){
                points=p;
                Debug.Log("-------------");
                Debug.Log("Points: "+points);
                numeral=el.numeral;
                name=el.participantName;
                Debug.Log("name: "+name);
                Debug.Log("-------------");
            }

        }
        winner_slot.text="# "+winnerNumeral+"  "+winnerName;
        bestPoints_slot.text="# "+numeral+"  "+name;
        
    }
    private void FixedUpdate()
    {
        if(boolStartCrono==true){
             raceTimer+=Time.deltaTime;
             raceTimerInterval--;
             if(raceTimerInterval<1){
                lap_times[lap_index].text=""+String.Format("{00:F2}", raceTimer)+" seg";
                raceTimerInterval=3;
             }

        }
        updateRace();
        if(finished==true){
            timeFinishedTransition--;
           // Debug.Log("TIME FINISHED: "+timeFinishedTransition);
            if(secureBoolStopSlowMotion==false && timeFinishedTransition<3800){
                secureBoolStopSlowMotion=true;
                
                GetComponent<SlowMotion>().StopSlowMotion();
                
                foreach (UnityEngine.AI.NavMeshAgent agent in navAgents){
                agent.GetComponent<Animator>().SetBool("compitiendo",false);

                }
                
            }
            if(timeFinishedTransition<3700){
                foreach (UnityEngine.AI.NavMeshAgent agent in navAgents){
                 
                if(agent.GetComponent<AIControl>().baseSpeed>2.0f){
                    agent.GetComponent<AIControl>().baseSpeed-=0.04f;
                };
                }
            }
            if(timeFinishedTransition==1) {
                finished=false;
            }

        }
       
        updateTravellingCamera();
        //updateBars();
    }

    void updateTravellingCamera()
    { if (ActionsOnScreen.firstPosition != null) {
            Vector3 anguloCam = travellingCamera.transform.GetChild(0).transform.localRotation.eulerAngles;
            travellingCamera.transform.GetChild(1).transform.LookAt(ActionsOnScreen.firstPosition.transform.position);
            float angle = (travellingCamera.transform.GetChild(0).transform.localRotation.eulerAngles.y - travellingCamera.transform.GetChild(1).transform.localRotation.eulerAngles.y);
            //Debug.Log("ANGULO: " + angle);

            anguloCam.y -= (angle*1.5F) * Time.fixedDeltaTime;
            //eulerRotCamera.y += angle*Time.deltaTime;
            travellingCamera.transform.GetChild(0).transform.localRotation= Quaternion.Euler(anguloCam);


        }
    }

   
    List<Perro> parseXmlFile(XmlDocument document)
    {
        //string totVal = "";
        string ruta = "";
        List<Perro> dogList = new();
        
 
        string xmlPathPattern = "//perros";
        XmlNode myroute = document.SelectSingleNode("//ruta");
        ruta = myroute.InnerText;
        XmlNodeList myNodeList = document.SelectNodes(xmlPathPattern).Item(0).SelectNodes("perro");
        Debug.Log("LA CANTIDAD DE ITEMS SON: " + myNodeList.Count);
        foreach (XmlNode node in myNodeList)
        {
            Dictionary<string,string> rutas = new();
            Dictionary<string, string> features = new();
            string name = "";
            

            XmlNode dogName = node.FirstChild;
            name = dogName.InnerXml;
            rutas.Add("Skin", ruta + name + "/" + node.SelectSingleNode("skin").InnerText);
            rutas.Add("Icon", ruta + name + "/" + node.SelectSingleNode("icon").InnerText);
            rutas.Add("Normal", ruta  + name + "/" + node.SelectSingleNode("normal").InnerText);
            rutas.Add("Specular", ruta  + name + "/" + node.SelectSingleNode("specular").InnerText);
            features.Add("Force",node.SelectSingleNode("force").InnerText);
            dogList.Add(new Perro(name, rutas, features));

            //totVal = "Nombre: " + dogName.InnerXml + "\n Piel: " + dogSkin.InnerXml + "\n\n";
            //Debug.Log( totVal);
            
        }
        return dogList;
    }




    void updateRace()
    {
          
        if (largar)
        {
            Debug.Log("LARGAR ES TRUE");
            foreach (var participante in navAgents)

            {
                participante.destination = wps[0].transform.position;
                participante.GetComponent<Animator>().SetBool("largada",true);
                Debug.Log("Largue usted perro : " + participante.name);

            }
            largar = false;
            GetComponent<ActionsOnScreen>().fadeInStart = true;
        }
        if (larguen)
        {
            foreach (var participante in navAgents)
            {
               


                if (Vector3.Distance(wps[participante.GetComponent<AIControl>().currentWP].transform.position, participante.transform.position) < accuracy)
                {
                    participante.GetComponent<AIControl>().currentWP++;
                    if (participante.GetComponent<AIControl>().currentWP >= wps.Length) participante.GetComponent<AIControl>().currentWP = 0;
                     destino = wps[participante.GetComponent<AIControl>().currentWP].transform.position;
                   

                    participante.destination = destino;
                }
                if (wps.Length == 0) return;


            }

            UpdateVelocities();
        }

    }

    private static string TimeStamp()
    {
        return DateTime.Now.ToString("yyyy/MM/dd") + "," +
        DateTime.Now.ToString("HH:mm:ss");
    }
    void UpdateTargets(Vector3 targetPosition)
    {
        foreach (UnityEngine.AI.NavMeshAgent agent in navAgents)
        {
            agent.destination = targetPosition;
        }
    }
    void UpdateVelocities()
    {
         if(Input.GetKeyDown(KeyCode.D))
        {
            foreach (UnityEngine.AI.NavMeshAgent agent in navAgents)
        {
            agent.GetComponent<AIControl>().baseSpeed=2.0f;
            agent.GetComponent<Animator>().SetBool("compitiendo",false);
/*      
            randomVelocity = UnityEngine.Random.Range(-0.2f, 0.2f);
            float velocidadActual = agent.GetComponent<UnityEngine.AI.NavMeshAgent>().speed;
            velocidadActual += randomVelocity;
            if (velocidadActual < 15.5F) velocidadActual = 15.5F;
            if (velocidadActual > 16.5F) velocidadActual = 16.5F;

            agent.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = velocidadActual;
*/

        }
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            
        }
        
    }
  


}
public static class QuaternionExtensions
{
    public static Quaternion Diff(this Quaternion to, Quaternion from)
    {
        return to * Quaternion.Inverse(from);
    }
    public static Quaternion Add(this Quaternion start, Quaternion diff)
    {
        return diff * start;
    }
}
public class Perro
{
    private string name;
    public int startNumber;
   
    private Dictionary<string, string> rutas;
    private Dictionary<string, string> features;
    private float energia = 100.0F;
    public Perro(string name, Dictionary<string, string> rutas, Dictionary<string, string> features)
    {
        this.name = name;
        this.Rutas = rutas;
        this.Features = features;
    }
    public string Name { get => name; set => name = value; }
    public float Energia { get=>energia; set=>energia=value; }
    public Dictionary<string, string> Rutas { get => rutas; set => rutas = value; }
    public Dictionary<string, string> Features { get => features; set => features = value; }
}


