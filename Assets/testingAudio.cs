using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingAudio : MonoBehaviour
{
    public AudioClip clipNums;
    private AudioSource audioSourceNums;
    // Start is called before the first frame update
    void Start()
    {
        audioSourceNums = CreateAudioSource(clipNums, false);
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

    // Update is called once per frame
    void Update()
    { 
        if(Input.GetKeyDown(KeyCode.RightShift))
        {
            StartSound();
        }
        if(Input.GetKeyUp(KeyCode.RightShift)){
            StopSound();
        }
        
    }

     public void StartSound() {
        audioSourceNums.time=3.145f;
       audioSourceNums.Play();
    }
    public void StopSound(){
        audioSourceNums.Stop();
     
    }
}
