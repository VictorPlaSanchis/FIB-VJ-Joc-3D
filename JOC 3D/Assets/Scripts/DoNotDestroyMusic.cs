using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroyMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
        if (musicObj.Length > 1){
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "sampleScene")
        {
            Debug.Log("Estás en una escena 'sampleScene'.");
            GameObject[] musicObjToDestroy = GameObject.FindGameObjectsWithTag("GameMusic");
            foreach (var i in musicObjToDestroy)
            {
                Destroy(i);
            }
            
        }
           
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
