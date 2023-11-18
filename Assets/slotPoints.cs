using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class slotPoints : MonoBehaviour
{

    [SerializeField] TextMeshPro numeralT;
    [SerializeField] TextMeshPro participantNameT;
    [SerializeField] TextMeshPro L1PointsT;
    [SerializeField] TextMeshPro L2PointsT;
    [SerializeField] TextMeshPro TotalPointsT;
    private int firstLapPoints=0;
    private int secondLapPoints=0;
    public int TotalPoints=0;
    public int numeral;
    public string participantName;

    // Start is called before the first frame update
    public void setNumeral(int numer){
        numeral=numer;

        numeralT.text=""+numer;
    }
    public void setParticipantName(string name){
        participantName=name;
        participantNameT.text=name;
    }
     public void setL1Points(int points){
        firstLapPoints=points;
        L1PointsT.text=""+firstLapPoints;
    }

     public void setL2Points(int points){
        secondLapPoints=points;
        L2PointsT.text=""+secondLapPoints;
        setTotalPoints();
    }

   
      void setTotalPoints(){
        TotalPoints=firstLapPoints+secondLapPoints;
 
        TotalPointsT.text=""+TotalPoints;
    }



    // Update is called once per frame
  
}
