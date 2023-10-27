using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    private Text text;


    public void Setup()
    {
        gameObject.SetActive(true);
        text.text = "Game Over!";
    }
}
