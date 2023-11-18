using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoTableTitleScript : MonoBehaviour{

[Header("Main Title")]
[SerializeField] float waitTime = 0.1f;
 
[SerializeField] TextMeshPro titleText;
[SerializeField] int maxCharacter=30;
private float timer = 0.0f;
 
[SerializeField] string texto="\t15to Gran Premio de la Real Corona Británica\t-\t(2 LAPS x 550 mts)\t";
[Header("Date and Time")]
[SerializeField] TextMeshPro dateText;

[SerializeField] TextMeshPro timeText;

 
private Dictionary<int,string> months = new Dictionary<int, string>() {
    {1,"January"},{2,"February"},{3,"March"},{4,"April"},{5,"May"},{6,"June"},
    {7,"July"},{8,"August"},{9,"September"},{10,"October"},{11,"November"},{12,"December"}};
 

 
    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitTime){
             animateText();
            timer=0.0f;
        }
    }

     private void FixedUpdate()
    {
       var moment= System.DateTime.Now;
       timeText.text=""+moment.Hour+":"+moment.Minute+":"+moment.Second;
       dateText.text=""+moment.DayOfWeek+" "+moment.Day+", "+months[moment.Month]+" "+moment.Year;
           }

    private void animateText(){
        var line=texto.Substring(0,maxCharacter);
        titleText.text=texto;
        var letter=texto.Substring(0,1);
        texto=texto.Substring(1);
        texto+=letter;
    }

}
