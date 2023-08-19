using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video ; 

public class MetaScena : MonoBehaviour

{
     public  int nextScene;

     public GameObject video;

     public VideoPlayer videoPlayer ; 

     public float time; 

     public float tiempoplay; 

    private void OnTriggerEnter2D(Collider2D other) { 
        if(other.CompareTag("Player")){

                video.SetActive (true) ;
                videoPlayer.Play();
                videoPlayer.time=tiempoplay;
                //videoPlayer.Pause=10f;
               
        
            
               // StartCoroutine(CambiarEscena());
               Invoke ("CambiarEscena", time);
               
                
               Debug.Log("hit Scene");
               }
             }
             private void CambiarEscena(){

             video.SetActive (false); 
           
       
        SceneManager.LoadScene(nextScene);
        
    }

}
