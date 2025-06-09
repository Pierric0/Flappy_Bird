using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPipe : MonoBehaviour
{

    public GameObject pipe;
    public float startSpawnrate = 4f;
    private float timer = 100;
    private float heightOffset;
    public float heightOffsetMax = 30;
    public GameObject player;
    public List<GameObject> pipes = new List<GameObject>();
    public float deadzone = -24f;
    private GameObject lastPipe;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if((lastPipe == null || lastPipe.transform.position.x<deadzone)&& player.GetComponent<PlayerScript>().alive)
        {
            heightOffset = Random.Range(0, heightOffsetMax);
            timer = 0;
            
            GameObject newPipe = Instantiate(pipe, new Vector3(this.transform.position.x, this.transform.position.y + heightOffset), transform.rotation);
            pipes.Add(newPipe);
            lastPipe = newPipe;
            
            for (int i = pipes.Count - 1; i >= 0; i--)
            {
                if (pipes[i] == null)
                {
                    pipes.RemoveAt(i);
                }
            }
        }


        //timer += Time.deltaTime;
        //if (timer > spawnrate && player.GetComponent<PlayerScript>().alive)
        //{
            //heightOffset = Random.Range(0, heightOffsetMax);
            //timer = 0;
            //pipes.Add(Instantiate(pipe, new Vector3(this.transform.position.x, this.transform.position.y + heightOffset), transform.rotation));

            //for (int i = pipes.Count - 1; i >= 0; i--)
            //{
                //if (pipes[i] == null)
                //{
                   // pipes.RemoveAt(i);
                //}
            //}
        //}
    }

    public void stop()
    {
        foreach (GameObject i in pipes)
        {
            if (i != null)
            {
                i.GetComponent<MoveBehaviour>().stopMoving();
            }
        }
    }

    public void setSpeed(float x)
    {
        foreach (GameObject i in pipes)
        {
            if (i != null)
            {
                i.GetComponent<MoveBehaviour>().moveSpeed += x;
            }
        }
    }

    public void deleteAllPipes()
    {
        foreach (GameObject i in pipes)
        {
            Destroy(i);
        }
        pipes.Clear();
    }
}
