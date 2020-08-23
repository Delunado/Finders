using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float range = 2f;
    public float speed = 1f;

    private Vector3 originalPos;
    private Vector3 destinationPos;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        destinationPos = transform.position + (Vector3.down * range);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(originalPos, destinationPos, Mathf.PingPong(Time.time, 1));
    }
}
