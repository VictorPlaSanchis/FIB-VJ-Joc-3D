using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelController : MonoBehaviour
{

    public Vector3 initialPositionPlayer;
    public GameObject playerPrefab;
    public camaraMovement levelCamera;

    private GameObject player;
    private bool gameEnded = false;
    private bool gameStarted = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(gameEnded) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restart the same scene
            if (!gameStarted) startGame();
        }
    }

    public void startGame()
    {
        gameStarted = true;
        createPlayer();
    }

    private void createPlayer()
    {
        player = Instantiate(playerPrefab, initialPositionPlayer, Quaternion.identity) as GameObject;
        this.levelCamera.setTarget(player.transform);
    }

    public void killPlayer()
    {
        gameEnded=true;
        Destroy(player);
    }

}
