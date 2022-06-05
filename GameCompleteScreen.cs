using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCompleteScreen : MonoBehaviour {

    private void Start()
    {
        Invoke("SwitchToMainMenu", 17f);
    }

    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
