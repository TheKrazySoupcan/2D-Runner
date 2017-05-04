using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class AIControl : MonoBehaviour
{
    public Transform[] waypoints;
    public bool crouch = false;
    public bool jump = false;
    public float stoppingDistance = 5f;

    private Player character;
    private int currentPoint = 0;
    private float distance = 0;

    // Use this for initialization
    void Start()
    {
        character = GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        MoveToWaypoint();
    }

    void MoveToWaypoint ()
    {
        // Get our movement direction
        float move = Mathf.Clamp(GetWaypointPos().x, -1, 1);
        character.Move(move, crouch, jump);
        jump = false;

    }

    Vector3 GetWaypointPos()
    {
        // Get Distance from position to waypoint
        Vector3 waypointPos = waypoints[currentPoint].position;
        distance = (transform.position - waypointPos).x;
        // Check if I am close to stoppingDistance
        if(distance <= stoppingDistance)
        {
            // Go to next waypoint
            currentPoint++;
        }
        // check if currentPoint is outside the waypoints length
        if(currentPoint >= waypoints.Length)
        {
            currentPoint = 0;
        }
        return waypoints[currentPoint].position;
    }
}
