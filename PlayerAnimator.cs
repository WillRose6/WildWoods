using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator playerAnimator;

    public enum CurrentState
    {
        Idle,
        Walking,
        Charging,
        Shooting,
        Die,
        Hurt,
    }

    public void ChangeState(CurrentState newState)
    {
        switch (newState)
        {
            case CurrentState.Idle:
                playerAnimator.SetFloat("MoveSpeed", 0);
                break;

            case CurrentState.Walking:
                playerAnimator.SetFloat("MoveSpeed", 1);
                break;

            case CurrentState.Charging:
                playerAnimator.SetBool("Shooting", true);
                break;

            case CurrentState.Shooting:
                playerAnimator.SetBool("Shooting", false);
                break;

            case CurrentState.Die:
                playerAnimator.CrossFade("Die", 0.1f);
                break;

            case CurrentState.Hurt:
                playerAnimator.CrossFade("Hurt", 0.1f);
                break;
        }
    }
}
