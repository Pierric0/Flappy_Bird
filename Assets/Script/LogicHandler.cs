using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicHandler : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public int highscoreInt = 0;
    public Text highscoreText;
    public Text newHighscore;
    public GameObject gameOverScreen;
    public GameObject player;
    public GameObject pointSound;
    public Image unmutedPic;
    public Image mutedPic;
    public GameObject startGameToggle;
    public GameObject pipeSpawner;
    public GameObject moveSpeed;
    public SpriteRenderer faceObject;
    public Sprite face;
    public CameraShake camera;
    public float difficultyPipeSpawnAdjustMult = 0.25f; // hier
    public float difficultySpeedAdjustAdd = 0.5f;
    public Vector3 playerStartPosition;

    [ContextMenu("Diff")]
    private void difficultyAdd()
    {
        //pipeSpawner.GetComponent<SpawnPipe>().spawnrate *= difficultyPipeSpawnAdjustMult;  
        moveSpeed.GetComponent<MoveBehaviour>().moveSpeed += difficultySpeedAdjustAdd;
        pipeSpawner.GetComponent<SpawnPipe>().setSpeed(difficultySpeedAdjustAdd);
        //Debug.Log("Spawnrate: " + pipeSpawner.GetComponent<SpawnPipe>().spawnrate);
        //Debug.Log("Movespeed: " + moveSpeed.GetComponent<MoveBehaviour>().moveSpeed);
    }


    private IEnumerator Start()
    {



        if (PlayerPrefs.HasKey("Highscore"))
        {
            highscoreInt = PlayerPrefs.GetInt("Highscore");

            if (highscoreText == null)              //no fucking idea why I have to do this like this. highscoreText should technically exist by now
            {
                //Debug.LogError("highscoreText is null");
                yield return null;
            }
            else
            {
                highscoreText.text = highscoreInt.ToString();
            }

        }
        else
        {
            PlayerPrefs.SetInt("Highscore", 0);
        }
        camera = FindObjectOfType(typeof(CameraShake)) as CameraShake;
    }
    public void muteUnmute(bool mute)
    {
        if (mute)
        {
            AudioListener.volume = 0;
            unmutedPic.enabled = false;
            mutedPic.enabled = true;
            //print(unmutedPic.enabled+ " and "+ mutedPic.enabled);
        }
        else
        {
            AudioListener.volume = 1;
            unmutedPic.enabled = true;
            mutedPic.enabled = false;
        }
    }



    [ContextMenu("play sound")]
    private void playSound()
    {

        pointSound.GetComponent<AudioSource>().Play();
        if (highscoreText == null)
        {
            Debug.LogError("HighscoreText still null");
        }

    }

    [ContextMenu("Score erhöhen")]
    public void addScore()
    {
        difficultyAdd();
        if (player.GetComponent<PlayerScript>().alive)
        {
            playerScore++;
            scoreText.text = playerScore.ToString();
            playSound();
        }

    }

    public void startGame()
    {
        moveSpeed.GetComponent<MoveBehaviour>().resetSpeed();
        player.GetComponent<Rigidbody2D>().simulated = true;
        player.GetComponent<PlayerScript>().alive = true;
        startGameToggle.SetActive(false);
    }

    public void restartGame()
    {
        moveSpeed.GetComponent<MoveBehaviour>().resetSpeed();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pipeSpawner.GetComponent<SpawnPipe>().deleteAllPipes();
        gameOverScreen.SetActive(false);
        player.GetComponent<PlayerScript>().alive = true;
        player.GetComponent<Transform>().position = playerStartPosition;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        player.GetComponent<Rigidbody2D>().rotation = 0f;
        faceObject.sprite = face;
        camera.enabled = false;

        setHighScore();
        newHighscore.enabled = false;
        scoreText.text = "0";
        playerScore = 0;


    }
    [ContextMenu("show GameOver")]
    public void showGameOver()
    {

        if (playerScore > highscoreInt)
        {
            newHighscore.enabled = true;
        }

        gameOverScreen.SetActive(true);
    }

    private void setHighScore()
    {
        if (playerScore > highscoreInt)
        {
            highscoreInt = playerScore;
            highscoreText.text = highscoreInt.ToString();
            PlayerPrefs.SetInt("Highscore", highscoreInt);
            PlayerPrefs.Save();
        }
    }
    public void closeApp()
    {
        setHighScore();
        Application.Quit();
        Debug.Log("Beende Game");
    }

}
