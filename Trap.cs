using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    public enum TrapType
    {
        Contact,
        Triggered,
    }
    public TrapType typeOfTrap;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(typeOfTrap == TrapType.Contact)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Player p = collision.gameObject.GetComponent<Player>();
                p.TakeDamage(p.currentHealth);
            }
        }
    }
}
