using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingBeing {

    public PlayerAnimator animator;
    public UI ui;
    public EnvironmentManager environmentManager;

    [Header("Camera shake")]
    public CameraShake shaker;
    public float shakeDuration;

    public override void TakeDamage(float amount)
    {
        animator.ChangeState(PlayerAnimator.CurrentState.Hurt);
        base.TakeDamage(amount);

        if (ui != null)
        {
            ui.UpdateHealthBar(startHealth, currentHealth);
        }
        StartCoroutine(ShakeCamera());
    }

    protected override void Die()
    {
        animator.ChangeState(PlayerAnimator.CurrentState.Die);
        if (ui != null)
        {
            ui.PlayerDied();
            environmentManager.PauseTime();
        }
    }

    public IEnumerator Dodge(float duration)
    {
        _renderer.color = new Color(1f,1f,1f,0.5f);
        yield return new WaitForSeconds(duration);
        _renderer.color = new Color(1f, 1f, 1f, 1f);
    }

    public IEnumerator ShakeCamera()
    {
        shaker.enabled = true;
        shaker.Initialise(shakeDuration);
        yield return new WaitForSeconds(shakeDuration);
        shaker.enabled = false;
    }
}
