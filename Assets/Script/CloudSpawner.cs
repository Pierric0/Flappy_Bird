using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject clouds;
    public float speed;
    public float heightAdjust;
    public float timer;
    public float maxTime = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > maxTime)
        {
            timer = 0;
            heightAdjust = Random.Range(-3, 3);
            Instantiate(clouds, new Vector3(this.transform.position.x, this.transform.position.y + heightAdjust,this.transform.position.z), this.transform.rotation);
        }
    }
}
