using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBullet : EnemyBullet {

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            Boss b = null;
            if (col.gameObject.GetComponent<Boss>())
            {
                b = col.gameObject.GetComponent<Boss>();
            }
            else
            {
                b = col.gameObject.GetComponent<ObjectReference>().References[0].GetComponent<Boss>();
            }
            b.TakeDamage(Damage);

            if (destroyOnCollide)
            {
                if (col.gameObject.layer == LayerMask.NameToLayer("Boss"))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
