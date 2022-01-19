using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{

    [SerializeField] GameObject[] waypoints;

    public int currentWayPointIndex = 0;

    [SerializeField] float speed = 1f; // this will be adjusted in Unity editor
    void Update()
    {
        if (Vector3.Distance( transform.position, waypoints[currentWayPointIndex].transform.position) < .1f)
        {
            currentWayPointIndex++;
            if ( currentWayPointIndex >= waypoints.Length)
            {
                currentWayPointIndex = 0;
            }
        }
        // Vector3.MoveForwards : Calculate a position between the points specified by current and target, 
        // moving no farther than the distance specified by maxDistanceDelta
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWayPointIndex].transform.position, speed * Time.deltaTime);
    }
}
