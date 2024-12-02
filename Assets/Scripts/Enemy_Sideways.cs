using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    public int startingPoint;
    private int i;
    //[SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float checkDistance = 0.05f;
    //private bool movingLeft;
    //private float leftEdge;
    //private float rightEdge;

    private void Start()
    {
        transform.position = waypoints[startingPoint].position;
        //leftEdge = transform.position.x - movementDistance;
        //rightEdge = transform.position.x + movementDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, waypoints[i].position) < checkDistance)
        {
            i++;
            if (i == waypoints.Length)
            {
                i = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[i].position, speed * Time.deltaTime);
        // if (movingLeft)
        // {
        //     if(transform.position.x > leftEdge)
        //     {
        //         transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        //     }
        //     else
        //     {
        //         movingLeft = false;
        //     }
        // }
        // else 
        // {
        //     if(transform.position.x < rightEdge)
        //     {
        //         transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        //     }
        //     else
        //     {
        //         movingLeft = true;
        //     }
        // }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

