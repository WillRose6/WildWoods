using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrackingLaser : MonoBehaviour {

    private Boss target; //Attached to the object being targetted
    public float fireRate = 2f;
    private float ElapsedTimeAlive;
    public GameObject laserPrefab;
    [SerializeField]
    private int Damage;

    private void Start()
    {
        if(target == null)
        {
            target = GetComponent<PlayerActions>().boss;
        }
        ElapsedTimeAlive = fireRate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        ElapsedTimeAlive += dt;
        Quaternion angle = (Quaternion.Euler(0, 0, AngleInDeg(this.transform.position, target.transform.position)));
        if (ElapsedTimeAlive > fireRate)
        {
            Instantiate(laserPrefab, this.transform.position, angle);
            ElapsedTimeAlive = 0f;
        }
    }

    //This returns the angle in radians
    public static float AngleInRad(Vector3 vec1, Vector3 vec2)
    {
        return Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x);
    }

    //This returns the angle in degrees
    public static float AngleInDeg(Vector3 vec1, Vector3 vec2)
    {
        return AngleInRad(vec1, vec2) * 180 / Mathf.PI;
    }
}
