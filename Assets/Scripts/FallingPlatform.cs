using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private float fallDelay = 1f;
    private float timeToReturn = 2.5f;
    private Rigidbody2D rb;
    private Vector3 platfromInitialPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        platfromInitialPos = transform.position;
    }

    void Update()
    {
        if (transform.position.y < -4)
        {
            StopCoroutine(Fall());
            StartCoroutine(ResetPlatform());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }

    }
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private IEnumerator ResetPlatform()
    {   
        transform.position = transform.position;
        rb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(timeToReturn);
        transform.position = platfromInitialPos;
    }
}
