using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float MaxLifeTime;

    public Vector3 Velocity { get; set; }

    public Vector3 MaxSize;

    private Vector3 InitialSize;

    private float ElapsedTimeAlive;

    // Use this for initialization
    void Start()
    {
        //cache the initial size of a project as it is spawned
        InitialSize = gameObject.transform.localScale;
        ElapsedTimeAlive = 0.0f;
    }

    // Use fixed update to compute the position of a projectile at a fixed interval
    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;

        transform.position = transform.position + Velocity * dt;

        Vector3 Scale = InitialSize + MaxSize * Mathf.Sin(ElapsedTimeAlive / MaxLifeTime * Mathf.PI);

        transform.localScale = Scale;
        transform.position = transform.position + this.Velocity * dt;
        ElapsedTimeAlive += dt;

        if (ElapsedTimeAlive > MaxLifeTime)
        {
            Destroy(gameObject);
        }
    }
}
