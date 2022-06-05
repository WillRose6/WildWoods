using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEmitter : MonoBehaviour
{

    public GameObject ProjectilePrefab;

    public Transform SpawnLocation;

    public float AngularVelocity; //degrees per second

    public float EmitterRate; //# of emits per second

    public float ProjectileInitialSpeed;

    private Vector3 EmitterDirection;

    private float ElapsedTime;

    private float EmitInterval;

    // Use this for initialization
    void Start()
    {
        EmitterDirection = gameObject.transform.up; //use the up axis in 2D as the forward direction
        ElapsedTime = 0.0f;
        EmitInterval = 1.0f / EmitterRate;
    }

    // Use fixed update to generate projectile at a fixed delta time
    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;

        if (ElapsedTime > EmitInterval)
        {
            //Instantiate a projectile from a prefab
            //Initial position is inherited from the parent, i.e. the emitter
            //Rotation is Identity 
            GameObject ProjectileSpawn = (GameObject)Instantiate(ProjectilePrefab, SpawnLocation.transform.position, Quaternion.identity);

            Quaternion rotation = Quaternion.AngleAxis(AngularVelocity * dt, new Vector3(0.0f, 0.0f, 1.0f));

            if (ProjectileSpawn.GetComponent<Projectile>())
            {
                EmitterDirection = rotation * EmitterDirection;

                Projectile p = ProjectileSpawn.GetComponent<Projectile>();

                p.Velocity = EmitterDirection * ProjectileInitialSpeed;
            }

            ElapsedTime = 0.0f;
        }

        ElapsedTime += dt;
    }
}