using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void PlayScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void InstructionsScene()
    {
        SceneManager.LoadScene("Instructions");
    }
    public void CreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }
    

}
