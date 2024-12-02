using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;
    [SerializeField] private float minX, maxX;
    [SerializeField] private float minY;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!player)
        {
            return;
        }
        offset = transform.position;
        offset.x = player.position.x;
        if (offset.x < minX) {
            offset.x = minX;
        }
        else if (offset.x > maxX) {
            offset.x = maxX;
        }
        offset.y = player.position.y;
        if(offset.y < minY)
        {
            offset.y = minY;
        }
        transform.position = offset;
    }
}
