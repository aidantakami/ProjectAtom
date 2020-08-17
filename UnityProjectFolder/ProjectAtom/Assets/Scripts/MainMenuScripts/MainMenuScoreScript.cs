using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuScoreScript : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI highScoreMenu;

    public void Start ()
    {
        SetScoreMenuValues ();
    }

    private void SetScoreMenuValues ()
    {
        highScoreMenu.text = "HighScores!";
        highScoreMenu.text += ("\n" + (PlayerPrefs.GetInt ("HighScore0").ToString ()));
        highScoreMenu.text += ("\n" + (PlayerPrefs.GetInt ("HighScore1").ToString ()));
        highScoreMenu.text += ("\n" + (PlayerPrefs.GetInt ("HighScore2").ToString ()));
        highScoreMenu.text += ("\n" + (PlayerPrefs.GetInt ("HighScore3").ToString ()));
        highScoreMenu.text += ("\n" + (PlayerPrefs.GetInt ("HighScore4").ToString ()));
    }
}