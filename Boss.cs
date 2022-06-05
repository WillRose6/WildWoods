using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Boss : LivingBeing {

    [Header("References")]
    public Waypoint[] waypoints;
    public Waypoint nextWaypoint;
    public Waypoint currentWaypoint;
    public Transform waypointCheckPosition;
    public GameObject player;
    public float WaypointDetectionRange;
    protected Vector2 direction;
    private Vector2 placeholder;
    protected bool isDead;
    public Vector3 scale;

    public enum MovementStyle
    {
        Flying,
        Grounded,
    }

    public MovementStyle movementStyle;

    [Header("Grounded")]
    public float runSpeed;

    [Header("Flying")]
    public float flySpeed;
    public enum RotationStyle
    {
        NoRotation,
        FaceNextWaypoint,
        FacePlayer,
        TravelThenFacePlayer,
    }

    public enum FlyingStyle
    {
        Discrete,
        Continuous,
    }

    public FlyingStyle flyingStyle;
    public RotationStyle rotationStyle;
    public bool arrivedAtWaypoint;

    [Header("Health bar")]
    public Image healthBar;

    protected override void Start()
    {
        base.Start();
        SetUpMovementStyle();
        GetNextWaypoint();
        StartCoroutine(MoveToNextWaypoint());
    }

    private void SetUpMovementStyle()
    {
        if(movementStyle == MovementStyle.Grounded)
        {
            _rigidbody.gravityScale = 4;
            WaypointDetectionRange = 0.75f;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            rotationStyle = RotationStyle.NoRotation;

        }
        else
        {
            _rigidbody.gravityScale = 0;
            if (rotationStyle != RotationStyle.NoRotation)
            {
                _rigidbody.constraints = RigidbodyConstraints2D.None;
            }
            else
            {
                _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    protected void GetNextWaypoint()
    {
        int currentWaypointNum = Array.IndexOf(waypoints, nextWaypoint);
        if (currentWaypointNum == waypoints.Length-1)
        {
            nextWaypoint = waypoints[0].GetComponent<Waypoint>();
        }
        else
        {
            nextWaypoint = waypoints[Array.IndexOf(waypoints, nextWaypoint) + 1];
        }
        direction = new Vector2(nextWaypoint.transform.position.x - transform.position.x, nextWaypoint.transform.position.y - transform.position.y);
    }

    public void SetUpNewWaypoints(Waypoint[] newWaypoints)
    {
        waypoints = new Waypoint[newWaypoints.Length];
        
        for(int i = 0; i < newWaypoints.Length; i++)
        {
            waypoints[i] = newWaypoints[i];
        }

        nextWaypoint = waypoints[0];
    }

    protected virtual IEnumerator MoveToNextWaypoint()
    {
        if (isDead == false)
        {
            if (Vector2.Distance(waypointCheckPosition.position, nextWaypoint.transform.position) > WaypointDetectionRange)
            {
                if (movementStyle == MovementStyle.Grounded)
                {
                    float dir = 0;
                    if (direction.x > 0)
                    {
                        dir = 1;
                    }
                    else
                    {
                        dir = -1;
                    }
                    _rigidbody.velocity = new Vector2(dir * runSpeed, _rigidbody.velocity.y);
                    arrivedAtWaypoint = false;
                }
                else
                {
                    Vector2 dir = transform.position - nextWaypoint.transform.position;
                    if (dir.x > 0)
                    {
                        Flip(true);
                        if (rotationStyle == RotationStyle.FaceNextWaypoint || rotationStyle == RotationStyle.TravelThenFacePlayer)
                        {
                            transform.right = dir;
                        }
                    }
                    else
                    {
                        Flip(false);
                        if (rotationStyle == RotationStyle.FaceNextWaypoint || rotationStyle == RotationStyle.TravelThenFacePlayer)
                        {
                            transform.right = -dir;
                        }
                    }

                    if (flyingStyle == FlyingStyle.Discrete)
                    {
                        transform.position = Vector2.SmoothDamp(transform.position, nextWaypoint.transform.position, ref placeholder, flySpeed, Mathf.Infinity, Time.deltaTime);
                    }
                    else
                    {
                        transform.Translate(direction * flySpeed * Time.deltaTime);
                    }
                }
                yield return new WaitForFixedUpdate();
                StartCoroutine(MoveToNextWaypoint());
            }
            else
            {
                arrivedAtWaypoint = true;
                if (movementStyle == MovementStyle.Grounded)
                {
                    _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                }
                OnWaypointReached();

                yield return new WaitForSeconds(nextWaypoint.bossWaitTime);
                GetNextWaypoint();
                StartCoroutine(MoveToNextWaypoint());
                OnWaypointExit();

                if (direction.x > 0)
                {
                    Flip(false);
                }
                else
                {
                    Flip(true);
                }
            }
        }
    }

    public void Jump(float force)
    {
        _rigidbody.AddRelativeForce(Vector2.up * force);
    }

    protected virtual void Flip(bool Toggle)
    {
        if (Toggle == true)
        {
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        }
        else
        {
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
    }

    protected void Update()
    {
        if (rotationStyle == RotationStyle.FacePlayer)
        {
            FacePlayer();
        }

        if(rotationStyle == RotationStyle.TravelThenFacePlayer)
        {
            if (arrivedAtWaypoint)
            {
                FacePlayer();
            }
        }
    }

    private void FacePlayer()
    {
        Vector2 dir = transform.position - player.transform.position;
        if (dir.x > 0)
        {
            Flip(true);
            transform.right = dir;
        }
        else
        {
            Flip(false);
            transform.right = -dir;
        }
    }

    private void OnWaypointReached() {
        currentWaypoint = nextWaypoint;
        Waypoint w = nextWaypoint.GetComponent<Waypoint>();
        w.OnWaypointReachedEvent.Invoke();
    }

    protected virtual void OnWaypointExit()
    {
        Waypoint w = currentWaypoint.GetComponent<Waypoint>();
        w.OnWaypointExitEvent.Invoke();
    }

    protected override void Die()
    {
        isDead = true;
        base.Die();
    }

    protected override void UpdateHealthBar()
    {
        base.UpdateHealthBar();
        healthBar.fillAmount = (currentHealth/startHealth);
    }
}
