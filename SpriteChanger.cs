using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour {

    public GameObject[] sprites;

    public void ChangeSprite(int newSprite)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].SetActive(false);
        }

        sprites[newSprite].SetActive(true);
    }
}
