using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Boss {

    [Header("Sprite changing")]
    public SpriteChanger spriteChanger;
    public GameObject howlSoundEffect;

    protected override void OnWaypointExit()
    {
        base.OnWaypointExit();
        if (!isDead)
        {
            spriteChanger.ChangeSprite(0);
        }
    }

    protected override void Die()
    {
        spriteChanger.ChangeSprite(3);
        base.Die();
    }

    public void Howl()
    {
        GameObject effect = Instantiate(howlSoundEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(effect, 1.65f);
    }
}
