using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Waypoint : MonoBehaviour {
    public UnityEvent OnWaypointReachedEvent;
    public UnityEvent OnWaypointExitEvent;
    public float bossWaitTime;
}
