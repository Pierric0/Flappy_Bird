using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{

    public float moveSpeed = 5;
    public float defaultMoveSpeed=10f;
    public float deadZone = -35;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }

    public void stopMoving()
    {
        moveSpeed = 0;
    }

    public void resetSpeed()
    {
        moveSpeed = defaultMoveSpeed;
    }
}
