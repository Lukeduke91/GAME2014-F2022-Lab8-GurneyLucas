using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public PlatformDirection direction;

    [Header("Movement Properties")]
    [Range(1.0f, 20.0f)]
    public float horizontalDistance = 8.0f;
    [Range(1.0f, 20.0f)]
    public float horizontalSpeed = 1.0f;
    [Range(1.0f, 20.0f)]
    public float verticalDistance = 0.0f;
    [Range(1.0f, 20.0f)]
    public float verticalSpeed = 1.0f;
    [Range(0.01f, 1.0f)]
    public float customSpeedFactor = 0.02f;

    [Header("Platform Path Points")]
    public List<Vector2> points;

    private Vector2 startPoint;
    private Vector2 destinationPoint;

    private float timer;
    private int currentPoint;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        currentPoint = 0;
        startPoint = transform.position;
        destinationPoint = points[currentPoint];
        
        for(int i = 0; i < points.Count; i++)
        {
            points[i] += startPoint;
        }

        points.Add(startPoint);

        destinationPoint = points[currentPoint];
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        if (direction == PlatformDirection.CUSTOM)
        {
            if (timer <= 1.0f)
            {
                timer += customSpeedFactor;
            }

            if (timer >= 1.0f)
            {
                timer = 0.0f;


                currentPoint++;
                if (currentPoint >= points.Count)
                {
                    currentPoint = 0;
                }

                startPoint = transform.position;
                destinationPoint = points[currentPoint];

            }
        }
    }

    private void Move()
    {
        switch(direction)
        {
            case PlatformDirection.HORIZONTAL:
                transform.position = new Vector3(Mathf.PingPong(horizontalSpeed *Time.time, horizontalDistance) 
                    + startPoint.x, startPoint.y, 0.0f);
                break;
            case PlatformDirection.VERTICAL:
                transform.position = new Vector3(startPoint.x, Mathf.PingPong(verticalSpeed * Time.time, verticalDistance)
                    + startPoint.y, 0.0f);
                break;
            case PlatformDirection.DIAGONAL_UP:
                transform.position = new Vector3(Mathf.PingPong(horizontalSpeed * Time.time, horizontalDistance)
                    + startPoint.x, Mathf.PingPong(verticalSpeed * Time.time, verticalDistance)
                    + startPoint.y, 0.0f);
                break;
            case PlatformDirection.DIAGONAL_DOWN:
                transform.position = new Vector3(
                    Mathf.PingPong(horizontalSpeed * Time.time, horizontalDistance) + startPoint.x,
                    startPoint.y - Mathf.PingPong(verticalSpeed * Time.time, verticalDistance), 0.0f);
                break;
            case PlatformDirection.CUSTOM:
                transform.position = Vector2.Lerp(startPoint, startPoint + destinationPoint, timer); ;
                break;
        }
    }
}
