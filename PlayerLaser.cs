using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour {

    [SerializeField]
    private int Damage;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            Boss b = null;
            if (col.gameObject.GetComponent<ObjectReference>())
            {
                b = col.gameObject.GetComponent<ObjectReference>().References[0].GetComponent<Boss>();
            }
            else
            {
                b = col.gameObject.GetComponent<Boss>();
            }

            b.TakeDamage(Damage);
        }
    }
}
