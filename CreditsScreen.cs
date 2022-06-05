using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScreen : MonoBehaviour {

    public void StartCredits()
    {
        gameObject.SetActive(true);
    }

    public void StopCredits()
    {
        gameObject.SetActive(false);
    }
}
