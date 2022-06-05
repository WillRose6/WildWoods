using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion : Wolf {

    [Header("Lion")]
    public EnvironmentManager environmentManager;
    private bool hasCollapsedBridges;
    public Waypoint[] secondSetOfWaypoints;
    public int HealAmount;
    public Animator animator;
    public GameObject[] emitters;

    public void CollapseBridge()
    {
        if(currentHealth  < (startHealth/2) && hasCollapsedBridges == false)
        {
            hasCollapsedBridges = true;
            environmentManager.CollapseBigBridge();
            SetUpNewWaypoints(secondSetOfWaypoints);
        }
    }

    public void HealMe()
    {
        int fifth = Mathf.CeilToInt(startHealth / 5);
        if (currentHealth <= fifth)
        {
            StartCoroutine(UseHeal());
        }
    }

    private IEnumerator UseHeal()
    {
        StopFiring();
        animator.Play("Lion heal");
        yield return new WaitForSeconds(1.10f);
        Heal(HealAmount);
        ResumeFiring();
    }

    public void StopFiring()
    {
        foreach(GameObject g in emitters)
        {
            g.SetActive(false);
        }
    }
    public void ResumeFiring()
    {
        foreach (GameObject g in emitters)
        {
            g.SetActive(true);
        }
    }

}
