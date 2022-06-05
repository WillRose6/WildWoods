using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    [Header("In game UI")]
    public Animator anim;
    public Image HealthBar;


    [Header("Ability charging")]
    public Image[] Bottles;
    private Image currentBottle;

    private void Start()
    {
        UpdateAbilityBottles(0);
    }

    public void UpdateHealthBar(float current, float start)
    {
        float percentage = start / current;
        HealthBar.fillAmount = percentage;
    }

    public void UpdateAbilityBottles(float currentCharge)
    {

        float remainder = 0;
        if (currentCharge > 0 && currentCharge <= 100)
        {
            currentBottle = Bottles[0];
            remainder = currentCharge;

            for(int i = 1; i < Bottles.Length; i++)
            {
                Bottles[i].fillAmount = 0;
            }
        }
        else if (currentCharge > 100 && currentCharge <= 200)
        {
            currentBottle = Bottles[1];
            remainder = currentCharge - 100;

            for (int i = 2; i < Bottles.Length; i++)
            {
                Bottles[i].fillAmount = 0;
            }
        }
        else if (currentCharge > 200 && currentCharge <= 300)
        {
            currentBottle = Bottles[2];
            remainder = currentCharge - 200;

            for (int i = 3; i < Bottles.Length; i++)
            {
                Bottles[i].fillAmount = 0;
            }
        }
        else if (currentCharge > 300 && currentCharge <= 400)
        {
            currentBottle = Bottles[3];
            remainder = currentCharge - 300;

            for (int i = 4; i < Bottles.Length; i++)
            {
                Bottles[i].fillAmount = 0;
            }
        }
        else if (currentCharge > 400 && currentCharge <= 500)
        {
            currentBottle = Bottles[4];
            remainder = currentCharge - 400;
        }
        else
        {
            currentBottle = Bottles[0];
            currentCharge = 500;
        }

        currentBottle.fillAmount = (remainder / 100);


    }

    public void SlowCreature()
    {
        anim.Play("Slow creature");
    }

    public void ResumeCreature()
    {
        anim.Play("Resume creature");
    }

    public void PlayerDied()
    {
        anim.Play("Death");
    }

    public void PlayerWon()
    {
        anim.Play("Win");
    }
}
