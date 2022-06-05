using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour {

    protected UI ui;

    [Header("Splitting bullet")]
    public GameObject SplittingBullet;

    [Header("Laser")]
    public PlayerTrackingLaser laserScript;
    public Transform laserPosition;

    [Header("Shield")]
    public GameObject shieldObject;
    public float shieldDuration;

    [Header("Projectile whirlwind")]
    public GameObject ProjectileWhirlwindObject;
    public float projectileWhirlwindDuration;

    private void Start()
    {
        ui = GameObject.Find("UI").GetComponent<UI>();
    }

    public void UseAbility(int num)
    {
        switch (num)
        {
            case 1:
                SplittingProjectile();
                break;

            case 2:
                LaserBeam();
                break;

            case 3:
                Shield();
                break;

            case 4:
                ProjectileWhirlwind();
                break;

            case 5:
                ClearScreen();
                break;
        }
    }

    public virtual void SplittingProjectile()
    {

    }

    public virtual void LaserBeam()
    {

    }

    public virtual void Shield()
    {

    }

    public virtual void ProjectileWhirlwind()
    {
    }

    public virtual void ClearScreen()
    {

    }
}
