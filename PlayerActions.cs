using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : Actions {

    [Header("References")]
    public Player player;
    public Boss boss;

    public enum ShootingType
    {
        Charged,
        Instant,
    }

    [Header("Shooting")]
    public ShootingType shooterType;
    public GameObject bulletPrefab;
    public Transform shootingPosition;
    public bool ShootingFrozen;
    private bool charging;
    public AudioSource audioSource;
    public AudioClip arrowChargeClip;
    public AudioClip arrowReleaseClip;

    //Instant shooting
    public float firerate;
    public float fixedPower;
    private float fireCountdown;

    //Charged shooting
    public float maxCharge;
    private float currentCharge;

    [Header("Abilities")]
    public float currentAbilityCharge;
    public int abilitiesUnlocked;

    [Header("Dodging")]
    public float dodgeDuration;
    private bool invincible;

    public bool Invincible
    {
        get
        {
            return invincible;
        }

        set
        {
            invincible = value;
        }
    }

    private void Start()
    {
        if(ui == null)
        {
            GameObject temp = GameObject.Find("UI");
            if(temp != null)
            {
                ui = temp.GetComponent<UI>();
            }
        }
        if (player == null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
        if (ui != null)
        {
        }
    }

    public void Update()
    {
        if (player.environmentManager.paused == false)
        {
            if (shooterType == ShootingType.Instant)
            {
                if (fireCountdown <= 0)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        CreateArrow(25, 100);
                    }
                }

                fireCountdown -= Time.deltaTime;
            }

            if (shooterType == ShootingType.Charged)
            {
                if (Input.GetMouseButton(0))
                {
                    Charge();
                    charging = true;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    Release();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (!invincible)
                {
                    StartCoroutine(dodge());
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (abilitiesUnlocked >= 1)
                {
                    if (currentAbilityCharge >= 100)
                    {
                        currentAbilityCharge -= 100;
                        ui.UpdateAbilityBottles(currentAbilityCharge);
                        UseAbility(1);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (abilitiesUnlocked >= 2)
                {
                    if (currentAbilityCharge >= 200)
                    {
                        currentAbilityCharge -= 200;
                        ui.UpdateAbilityBottles(currentAbilityCharge);
                        UseAbility(2);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (abilitiesUnlocked >= 3)
                {
                    if (currentAbilityCharge >= 300)
                    {
                        currentAbilityCharge -= 300;
                        ui.UpdateAbilityBottles(currentAbilityCharge);
                        UseAbility(3);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (abilitiesUnlocked >= 4)
                {
                    if (currentAbilityCharge >= 400)
                    {
                        currentAbilityCharge -= 400;
                        ui.UpdateAbilityBottles(currentAbilityCharge);
                        UseAbility(4);
                    }
                }
            }
        }
    }

    public void CreateArrow(float power, float percentage)
    {
        if (ShootingFrozen == false)
        {
            GameObject arrow = Instantiate(bulletPrefab, shootingPosition.transform.position, Quaternion.identity);
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = new Vector2(point.x - arrow.transform.position.x, point.y - arrow.transform.position.y);
            arrow.transform.right = direction;
            fireCountdown = firerate;
            Arrow a = arrow.GetComponent<Arrow>();
            a.Initialize(this);
            a.MoveSpeed = power;
            a.damage = (percentage/100) * a.damage;
            a.abilityChargeGained = (percentage/100) * a.abilityChargeGained;
        }
    }

    public void Charge()
    {
        if (currentCharge < maxCharge)
        {
            currentCharge += Time.deltaTime;
        }
        else
        {
            currentCharge = maxCharge;
        }
        //Insert other charging mechanics here.

        if (charging == false)
        {
            player.animator.ChangeState(PlayerAnimator.CurrentState.Charging);
            audioSource.PlayOneShot(arrowChargeClip);
        }
    }

    public void Release()
    {
        charging = false;
        float percentage = currentCharge / maxCharge * 100;
        float power = percentage / fixedPower;
        CreateArrow(power, percentage);
        currentCharge = 0;
        //Insert other release mechanics here.

        player.animator.ChangeState(PlayerAnimator.CurrentState.Shooting);
        audioSource.PlayOneShot(arrowReleaseClip);
    }

    public void GainCharge(float amount)
    {
        currentAbilityCharge += amount;
        ui.UpdateAbilityBottles(Mathf.CeilToInt(currentAbilityCharge));
    }

    public void LoseCharge(float amount)
    {
        if (currentAbilityCharge - amount <= 0)
        {
            currentAbilityCharge = 0;
        }
        else
        {
            currentAbilityCharge -= amount;
        }

        if (ui != null)
        {
            ui.UpdateAbilityBottles(Mathf.CeilToInt(currentAbilityCharge));
        }
    }

    public IEnumerator dodge()
    {
        StartCoroutine(player.Dodge(dodgeDuration));
        invincible = true;
        yield return new WaitForSeconds(dodgeDuration);
        invincible = false;
    }

    public override void SplittingProjectile()
    {
        GameObject arrow = Instantiate(SplittingBullet, shootingPosition.transform.position, Quaternion.identity);
        Vector3 point = boss.transform.position;
        Vector2 direction = new Vector2(point.x - arrow.transform.position.x, point.y - arrow.transform.position.y);
        arrow.transform.right = direction;
        fireCountdown = firerate;
        SplittingArrow a = arrow.GetComponent<SplittingArrow>();
        a.Initialize(this);
        a.MoveSpeed = 15;
        a.target = boss.transform;
    }

    public override void ProjectileWhirlwind()
    {
        base.ProjectileWhirlwind();
        StartCoroutine(UseProjectileWhirlwind());
    }

    public IEnumerator UseProjectileWhirlwind()
    {
        ProjectileWhirlwindObject.SetActive(true);
        yield return new WaitForSeconds(projectileWhirlwindDuration);
        ProjectileWhirlwindObject.SetActive(false);
    }

    public override void Shield()
    {
        base.Shield();
        StartCoroutine(UseShield());
    }

    public IEnumerator UseShield()
    {
        GameObject obj = Instantiate(shieldObject, laserPosition.transform.position, Quaternion.identity);
        activate_shield ashield = obj.GetComponent<activate_shield>();
        ashield.player = this.gameObject;
        ashield.Sheild_active = true;
        StartCoroutine(player.Dodge(2));
        invincible = true;
        yield return new WaitForSeconds(2);
        invincible = false;
        ashield.Sheild_active = false;
        Destroy(obj);
    }

    public override void LaserBeam()
    {
        base.LaserBeam();
        StartCoroutine(UseLaserBeam());
    }

    public IEnumerator UseLaserBeam()
    {
        laserScript.enabled = true;
        yield return new WaitForSeconds(2);
        laserScript.enabled = false;
    }

}
