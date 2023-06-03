using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    private int highCoins = 0;
    public int score = 0;
    public int coins = 0;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if(player)
            {
                Text godModeText = GameObject.FindGameObjectWithTag("godModeText").GetComponent<Text>();
                if (godModeText.text == "")
                {
                    godModeText.text = "god mode is enabled";
                    player.GetComponent<playerMovement>().enableGodMode();
                }
                else
                {
                    godModeText.text = "";
                    player.GetComponent<playerMovement>().disableGodMode();
                }
            }
        }

        GameObject objectToDestroy = GameObject.FindWithTag("GameMusic"); 
        if (objectToDestroy != null) Destroy(objectToDestroy); //destroy main menu music
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

    public void Start()
    {
        StreamReader inp_stm = new StreamReader("./score.txt");

        string inp_ln = inp_stm.ReadLine();
        string[] score = inp_ln.Split(';');
        this.highScore = int.Parse(score[0]);
        this.highCoins = int.Parse(score[1]);
        GameObject.FindGameObjectWithTag("highScoreText").GetComponent<Text>().text = "high score: " + this.highScore.ToString() + " (coins: " + this.highCoins.ToString() + ")";

        inp_stm.Close();
    }

    public void addCoin()
    {
        coins++;
        GameObject.FindGameObjectWithTag("scoreCoins").GetComponent<Text>().text = "coins: " + this.coins.ToString();
    }
    public void addScore(int add)
    {
        this.score += add;
        GameObject.FindGameObjectWithTag("scoreText").GetComponent<Text>().text = "score: " + this.score.ToString();
    }

    public void resetScore()
    {
        this.coins = 0;
        this.score = 0;
        GameObject.FindGameObjectWithTag("scoreText").GetComponent<Text>().text = "score: " + this.score.ToString();
        GameObject.FindGameObjectWithTag("scoreCoins").GetComponent<Text>().text = "coins: " + this.coins.ToString();
    }

    public void saveHighScore()
    {
        if(this.highScore < this.score)
        {
            this.highScore = this.score;
            this.highCoins = this.coins;
        } else if(this.highScore == this.score)
        {
            if(this.coins > this.highCoins)
            {
                this.highCoins = this.coins;
            }
        }
        GameObject.FindGameObjectWithTag("highScoreText").GetComponent<Text>().text = "high score: " + this.highScore.ToString() + " (coins: " + this.highCoins.ToString() + ")";

        System.IO.File.WriteAllText("./score.txt", this.highScore.ToString()+";"+this.highCoins.ToString());
    }

    public void startGame()
    {
        Debug.Log("GAME STARTED");
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
