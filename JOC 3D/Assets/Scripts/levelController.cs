using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelController : MonoBehaviour
{

    public Vector3 initialPositionPlayer;
    public GameObject playerPrefab;
    public GameObject rockPrefab;
    public camaraMovement levelCamera;
    public Vector3 rockOffset;

    private GameObject player;
    private GameObject rock;
    private bool gameEnded = false;
    private bool gameStarted = false;
    private bool playerCreated = false;

    public int highScore = 0;
    public int score = 0;

    void Update()
    {
        if (playerCreated) return;
        if(gameStarted && !gameEnded)
        {
            if (levelCamera.isInitialPositionBeingMade) return;
            else
            {
                playerCreated = true;
                player.gameObject.SetActive(true);
                rock.gameObject.SetActive(true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (gameEnded) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restart the same scene
                if (!gameStarted) startGame();
            }
        }
    }

    public void addScore(int add)
    {
        this.score += add;
        GameObject.FindGameObjectWithTag("scoreText").GetComponent<Text>().text = "score: " + this.score.ToString();
    }

    public void resetScore()
    {
        this.score = 0;
        GameObject.FindGameObjectWithTag("scoreText").GetComponent<Text>().text = "score: " + this.score.ToString();
    }

    public void saveHighScore()
    {
        if(this.highScore < this.score)
        {
            this.highScore = this.score;
        }
        GameObject.FindGameObjectWithTag("highScoreText").GetComponent<Text>().text = "high score: " + this.highScore.ToString();
    }

    public void startGame()
    {
        Debug.Log("HOLA");
        createPlayer();
        levelCamera.doInitialAnimation();
        gameStarted = true;

    }

    private void createPlayer()
    {
        player = Instantiate(playerPrefab, initialPositionPlayer, Quaternion.identity) as GameObject;
        rock = Instantiate(rockPrefab, initialPositionPlayer + new Vector3(0,0,5.0f), Quaternion.identity) as GameObject;
        this.levelCamera.setTarget(player.transform);
        player.gameObject.SetActive(false);
        rock.gameObject.SetActive(false);
    }

    public void killPlayer()
    {
        saveHighScore();
        resetScore();
        gameEnded =true;
        playerCreated=false;
        Destroy(player);
    }

}
