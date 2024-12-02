using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Platform_Sideways : MonoBehaviour
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
    // Start is called before the first frame update
    //private void Awake()
    //{
        //leftEdge = transform.position.x - movementDistance;
        //rightEdge = transform.position.x + movementDistance;
    //}
    void Start()
    {
        transform.position = waypoints[startingPoint].position;
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

        //if (movingLeft)
        //{
            //if(transform.position.x > leftEdge)
            //{
               // transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            //}
            //else
            //{
                //movingLeft = false;
            //}
        //}
        //else 
        //{
            //if(transform.position.x < rightEdge)
            //{
                //transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            //}
            //else
            //{
                //movingLeft = true;
            //}
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
