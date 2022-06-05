using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Projectile {

    [SerializeField]
    protected int Damage;
    [SerializeField]
    private int lostCharge;
    [SerializeField]
    protected bool destroyOnCollide;

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Player")){
            Player p = col.gameObject.GetComponent<Player>();
            PlayerActions pa = col.gameObject.GetComponent<PlayerActions>();
            pa.LoseCharge(lostCharge);

            if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (!pa.Invincible)
                {
                    p.TakeDamage(Damage);

                }
                else
                {
                    Debug.Log("No damage");
                }
            }
        }

        if (destroyOnCollide)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Destroy(gameObject);
            }
        }
    }

}
