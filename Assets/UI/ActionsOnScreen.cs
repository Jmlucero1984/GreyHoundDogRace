using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionsOnScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public TMPro.TextMeshProUGUI text;
    public UnityEngine.UI.Button boton;
    public AnimationCurve motionCurve;
    [Header("Elementos del Ranking")]
      public GameObject liebre;
   
    [SerializeField] float waitTime = 1f;
    [SerializeField] GameObject fixedCamera;
    private float timer = 0.0f;
    private int delayForFixedCamera=0;
    public static GameObject firstPosition;
    public Text primerPerro;
    public bool fadeInStart=false;
    private List<GameObject> Iconos;
    private List<GameObject> Participantes;
    public GameObject BarrasCanvas;
    public GameObject BarraPrefab;
    private List<GameObject> Barras;
    private List<Perro> dogList;
    public bool stillRacing=true;
    /// <summary>
    public List<Icono> listaIconos = new ();
    public List<Vector4> coordsPlaces = new();
    /// </summary>
 
 

    public class Icono
    {
        public GameObject icono;
        public GameObject dog;
        AnimationCurve animCurve;
        public float distancia = 0F;
        private bool isLocked = true;
        public Vector4 origininalCoords = new Vector4();
        private Vector4 incCoords = new Vector4();
       
        int frameInicial = 0;
        int frames;
        public Icono(GameObject icono, GameObject dog, AnimationCurve animCurve, Vector4 originalCoords)
        {
            this.icono = icono;
            this.animCurve = animCurve;
            this.dog = dog;
            this.origininalCoords = originalCoords;
            
           //Color color = icono.GetComponent<RawImage>().color;
           // color.a = 0;
           // icono.GetComponent<RawImage>().color = color;

        }
        public void setAnimation(Vector4 destino, int frames)
        {
            this.frames = frames;
            isLocked = false;
            frameInicial = 0;
            var rectTransform = icono.GetComponent<RectTransform>();
            origininalCoords[0] = rectTransform.anchoredPosition3D.x;
            origininalCoords[1] = rectTransform.anchoredPosition3D.y;
            origininalCoords[2] = rectTransform.anchoredPosition3D.z;
            origininalCoords[3] = rectTransform.sizeDelta.x;
            incCoords[0]  = (destino[0] - origininalCoords[0]);
            incCoords[1] = (destino[1] - origininalCoords[1]);
            incCoords[2] = (destino[2] - origininalCoords[2]);
            incCoords[3] = (destino[3] - origininalCoords[3]); 
        }

        public void animate()
        {

            if (frameInicial <= frames && isLocked==false)
            {
            
                icono.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(
                    origininalCoords[0] + animCurve.Evaluate(frameInicial * (1.0f / frames)) * incCoords[0],
                    origininalCoords[1] + animCurve.Evaluate(frameInicial * (1.0f / frames)) * incCoords[1],
                    origininalCoords[2] + animCurve.Evaluate(frameInicial * (1.0f / frames)) * incCoords[2]);
                icono.GetComponent<RectTransform>().sizeDelta = new Vector2(
                     origininalCoords[3] + animCurve.Evaluate(frameInicial * (1.0f / frames)) * incCoords[3],
                     origininalCoords[3] + animCurve.Evaluate(frameInicial * (1.0f / frames)) * incCoords[3]);
                frameInicial++;
            } 
            if(frameInicial>frames)
            {
                frameInicial = 0;
                isLocked = true;
            }

        }
    }

    void Start()
    {
        //boton.onClick.AddListener(clickedButton);
        Iconos = GetComponentInParent<GameManager>().GetIconos();
        Participantes = GetComponentInParent<GameManager>().GetParticipantes();
        for (int i = 0; i < Iconos.Count; i++)
        {
            var transf = Iconos[i].GetComponent<RectTransform>();
            var origininalCoords = new Vector4(transf.anchoredPosition3D.x, transf.anchoredPosition3D.y, transf.anchoredPosition3D.z, transf.sizeDelta.x);
            listaIconos.Add(new Icono(Iconos[i], Participantes[i], motionCurve, origininalCoords));
            coordsPlaces.Add(origininalCoords);
        }
        fixedCamera.GetComponent<Cinemachine.CinemachineClearShot>().LookAt=Participantes[0].transform;
        dogList = GetComponentInParent<GameManager>().GetDogList();
        Barras = CreateEnergyBars(Participantes);
    }
    private List<GameObject> CreateEnergyBars(List<GameObject> participantes)
    {
        List<GameObject> energyBars = new();
        int i = 0;
        const float incremento = -15;
        foreach (var item in dogList)
        {
            var barra = Instantiate(BarraPrefab);
            barra.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = item.Name;
            barra.transform.GetChild(1).gameObject.GetComponent<Image>().fillAmount =  item.Energia/100;
            barra.transform.parent = BarrasCanvas.transform;
            barra.transform.localPosition = new Vector3(13, (float)i * incremento, 0);
            energyBars.Add(barra);
            i++;
        }
     


        return energyBars;
    }
    // Update is called once per frame
    void Update()
    {
        if (fadeInStart)
        {
            foreach (var item in listaIconos)
            {
               // Color color = item.icono.GetComponent<RawImage>().color;
               // color.a++;
               // item.icono.GetComponent<RawImage>().color = color;
               // if (color.a > 255) fadeInStart = false;
            }
        }
        timer += Time.deltaTime;
            if (timer > waitTime && stillRacing==true)
            {
                cargaTexto("TIEMPO CUMPLIDO" + Environment.NewLine);
                delayForFixedCamera++;
                borraTexto();
                timer = 0;
                GetDistances(ref listaIconos, liebre);
                OrderByDistances(ref listaIconos);
                PerformIconMovements(ref listaIconos);
            ModifyEnergies();
            UpdateEnergyBars();

            }
        
    }
    private void UpdateEnergyBars()
    {
        //Debug.Log("ENERGIA DE UNO: " + dogList.ElementAt(0).Energia);
        for (int i=0; i < Barras.Count; i++)
        {
           
            Barras.ElementAt(i).transform.GetChild(1).gameObject.GetComponent<Image>().fillAmount = dogList.ElementAt(i).Energia / 100;
        }

    }

    private void ModifyEnergies()
    {
        foreach (var item in dogList)
        {
            item.Energia-=UnityEngine.Random.Range(0, 1.5f);


        }
    }


    private void PerformIconMovements(ref List<Icono> ranking)
    {
        if (ranking != null)
        {
            int j = 0;
            foreach (var item in ranking)
            {
                item.setAnimation(coordsPlaces[j], 30);
                j++;
            }
        }

 
    }
    private void FixedUpdate()
    {
        //numberFrame++;
        // cargaTexto(numberFrame.ToString());
        foreach (var item in listaIconos)
        {
            item.animate();
        }
    
    }

    public void GetDistances(ref List<Icono> participantes, GameObject objetivo)
    {
       
        foreach (var item in participantes)
        {
            var positionLiebre = objetivo.transform.position;
            var direction = positionLiebre - item.dog.transform.position;
            direction.y = 0;
            var distancia = (float)Math.Round(direction.magnitude, 4);
            item.distancia = distancia;     
        }
         
    }

    public void OrderByDistances(ref List<Icono> participantes)
    {
        List<Icono> nuevaLista = new();
        while (participantes.Count > 0)
        {
            var result = participantes.OrderBy(x => x.distancia).First();
            nuevaLista.Add(result);
            participantes.Remove(result);
        }
        participantes = nuevaLista;
        firstPosition = participantes[0].dog;
        primerPerro.text = "Primero: " + firstPosition.name;
        if(delayForFixedCamera>10){
           // Debug.Log("DELAY: "+delayForFixedCamera);
        fixedCamera.GetComponent<Cinemachine.CinemachineClearShot>().LookAt=firstPosition.transform;
        }
        


    }

    public void clickedButton()
    {
        text.text = "Boton Apretado";
    }
    public void cargaTexto(string mensaje)
    {
        text.text += mensaje;
    }
    public void borraTexto()
    {
        text.text ="";
    }
}
