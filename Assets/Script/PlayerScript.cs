using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public LogicHandler logic;
    public float flapStrenght;
    public bool alive = true;
    public SpriteRenderer faceObject;
    public Sprite face;
    public GameObject spawnPipe;
    public CameraShake kamera;
    public float jumpRotation = 10f;
    public float fallRotation = -0.1f;
    public float rotationSpeed = 50f;
    public bool over = false;
    public AudioSource jumpSound;
    public AudioSource crashSound;


    void Start()
    {
        kamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        spawnPipe = GameObject.FindGameObjectWithTag("PipeSpawner");
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<LogicHandler>();
        if (face == null)
        {
            Debug.LogError("Face ist nicht gesetzt");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && alive)
        {
            myRigidbody.velocity = Vector2.up * flapStrenght;
            jumpSound.Play();
        }




        if (Input.GetKeyDown(KeyCode.R))
        {
            logic.restartGame();
        }

        if ((this.transform.position.y < -20 || this.transform.position.y > 20) && alive)
        {
            OnCollisionEnter2D(null);
            this.transform.position = new Vector2(-50, -50);
            kamera.enabled = true;
        }


    }

    [ContextMenu("show gameover")]
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (alive)
        {
            crashSound.Play();
        }
        alive = false;
        logic.showGameOver();
        kamera.enabled = true;
        spawnPipe.GetComponent<SpawnPipe>().stop();
        faceObject.sprite = face;

    }

    

}
