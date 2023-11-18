using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    // Start is called before the first frame update
    public float slowMotionTimeScale;
    private float startTimeScale;
    private float startFixedDeltaTime;
    void Start()
    {
        startTimeScale=Time.timeScale;
        startFixedDeltaTime=Time.fixedDeltaTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartSlowMotion();
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            StopSlowMotion();
        }
    }
    public void StartSlowMotion() {
        Time.timeScale=slowMotionTimeScale;
        Time.fixedDeltaTime=startFixedDeltaTime*slowMotionTimeScale;
    }
    public void StopSlowMotion(){
        Time.timeScale=startTimeScale;
        Time.fixedDeltaTime=startFixedDeltaTime;
        playFinalShout();
    }
    
    public GameManager.callBack playFinalShout;
    public void StartSlowMotion2(GameManager.callBack method){
        playFinalShout=method;
        Time.timeScale=slowMotionTimeScale;
        Time.fixedDeltaTime=startFixedDeltaTime*slowMotionTimeScale;
    }


}
