using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnvironmentManager : MonoBehaviour {

    public Animator bigBridgeAnimator;
    public Animator[] bridgeAnims;
    public SpriteChanger trainingDummySprites;
    public Text dummyFiringText;
    private bool trainingDummyFiring;
    public GameObject[] BabySquirrels;
    private int miniBossCounter = 0;
    public bool paused = false;

    public void PauseTime()
    {
        Time.timeScale = 0;
        paused = true;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1;
        paused = false;
    }

    public void CollapseBridges()
    {
        foreach (Animator a in bridgeAnims)
        {
            a.Play("Bridge collapse");
        }
    }

    public void ToggleTrainingDummyShoot()
    {
        if (trainingDummyFiring)
        {
            trainingDummyFiring = false;
            trainingDummySprites.ChangeSprite(0);
            dummyFiringText.text = "Make the scarecrow shoot!";
        }
        else
        {
            trainingDummyFiring = true;
            trainingDummySprites.ChangeSprite(1);
            dummyFiringText.text = "Stop the scarecrow shooting!";
        }
    }

    public void ReloadScene()
    {
        ResumeTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(int index)
    {
        ResumeTime();
        SceneManager.LoadScene(index);
    }

    public void LoadNextScene()
    {
        ResumeTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMainMenu()
    {
        ResumeTime();
        SceneManager.LoadScene(0);
    }

    public void LoadSpecificScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void EnableBabySquirrel()
    {
        if (miniBossCounter < BabySquirrels.Length)
        {
            BabySquirrels[miniBossCounter].SetActive(true);
            miniBossCounter++;
        }
    }

    public void CollapseBigBridge()
    {
        bigBridgeAnimator.Play("Big Bridge Collapse");
    }
}
