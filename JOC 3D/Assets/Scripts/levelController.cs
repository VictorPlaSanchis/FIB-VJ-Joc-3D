using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelController : MonoBehaviour
{

    private bool gameStarted = false;

    public Vector3 initialPositionPlayer;
    public GameObject playerPrefab;
    public camaraMovement levelCamera;

    private GameObject player;

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!gameStarted) startGame();
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
        gameStarted=false;
        Destroy(player);
    }

}
