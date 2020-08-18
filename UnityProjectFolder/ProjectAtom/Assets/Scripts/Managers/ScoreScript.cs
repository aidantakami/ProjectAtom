using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{

    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI incrementerText;
    [SerializeField] public TextMeshProUGUI warningText;
    [SerializeField] public TextMeshProUGUI highScoreMenu;
    [SerializeField] public IntVariable playerCurrentScore;
    [SerializeField] public IntVariable boomRangeWarning;

    private bool gameIsPlaying = false;
    private int incrementOverTime = 10;
    private float boomAwayTimer = 0;

    private int incrementPauseTemp;

    // Start is called before the first frame update
    void Start ()
    {
        playerCurrentScore.value = 0;
        warningText.color = Color.white;
        warningText.gameObject.SetActive (false);
    }

    // Update is called once per frame
    void Update ()
    {
        scoreText.text = playerCurrentScore.value.ToString ();
        incrementerText.text = ("Bonus: +" + incrementOverTime.ToString ());

        if (boomRangeWarning == 0)
        {
            warningText.gameObject.SetActive (false);
            if (boomAwayTimer != 0) boomAwayTimer = 0;
        }
        else if (boomRangeWarning == 1)
        {
            warningText.gameObject.SetActive (true);
            warningText.color = Color.black;
            warningText.text = "Range warning!";

            if (boomAwayTimer != 0) boomAwayTimer = 0;

        }
        else if (boomRangeWarning == 2)
        {
            warningText.color = Color.red;
            warningText.text = "Too Far From Zoomer!";

            if (boomAwayTimer != 0) boomAwayTimer = 0;
        }
        else if (boomRangeWarning == 3)
        {
            boomAwayTimer += Time.deltaTime;

            if (boomAwayTimer > 6)
            {
                warningText.color = Color.black;
                warningText.gameObject.SetActive (true);
                warningText.text = GetBoomThrowText ();
                boomAwayTimer = 0;
            }
        }
    }

    public string GetBoomThrowText ()
    {
        int randomSeed = (int) Random.Range (0f, 10f);

        if (randomSeed == 0)
        {
            return "Throw Me!";
        }
        else if (randomSeed == 1)
        {
            return "Let Me At Em!";
        }
        else if (randomSeed == 2)
        {
            return "Need Help?";
        }
        else if (randomSeed == 3)
        {
            return "I Want To Fly!";
        }
        else if (randomSeed == 4)
        {
            return "I'm Ready!";
        }
        else if (randomSeed == 5)
        {
            return "Throw Me For Help!";
        }
        else if (randomSeed == 6)
        {
            return "I'd Be Faster!";
        }
        else if (randomSeed == 7)
        {
            return "Time to Toss!";
        }
        else if (randomSeed == 8)
        {
            return "Tag Me In!";
        }
        else if (randomSeed == 9)
        {
            return "My Turn!";
        }
        else return "Boomer Time!";

    }

    public void TokenScoreIncrement ()
    {
        playerCurrentScore.value += 100;
    }

    public void RestartScoreText ()
    {
        playerCurrentScore.value = 0;
        incrementOverTime = 10;
        highScoreMenu.gameObject.SetActive (false);
    }

    public void ScoreTextGameStart ()
    {
        gameIsPlaying = true;
        StartCoroutine (GameScoreIncrementer ());
    }

    public void ScoreTextGameEnd ()
    {
        gameIsPlaying = false;
        StopCoroutine (GameScoreIncrementer ());
        HighScoreCalculator ();
        DisplayScoreMenu ();
    }

    public void ChunkCompletedIncrementChange ()
    {
        incrementOverTime += 1;
    }

    public void BoomThrownScore ()
    {
        incrementOverTime += 10;
    }

    public void BoomCaughtScore ()
    {
        incrementOverTime -= 10;
        playerCurrentScore.value += 100;
    }

    private IEnumerator GameScoreIncrementer ()
    {
        while (gameIsPlaying)
        {
            playerCurrentScore.value += incrementOverTime;
            yield return new WaitForSeconds (1f);
        }
    }

    public void ScoreScriptPause ()
    {
        incrementPauseTemp = incrementOverTime;
        incrementOverTime = 0;
    }

    public void ScoreScriptUnpause ()
    {
        incrementOverTime = incrementPauseTemp;
    }

    public void HighScoreCalculator ()
    {
        if (playerCurrentScore.value > PlayerPrefs.GetInt ("HighScore0"))
        {
            int tempScoreStorage1 = PlayerPrefs.GetInt ("HighScore0");
            int tempScoreStorage2 = PlayerPrefs.GetInt ("HighScore1");
            int tempScoreStorage3 = PlayerPrefs.GetInt ("HighScore2");
            int tempScoreStorage4 = PlayerPrefs.GetInt ("HighScore3");

            PlayerPrefs.SetInt ("HighScore0", playerCurrentScore.value);
            PlayerPrefs.SetInt ("HighScore1", tempScoreStorage1);
            PlayerPrefs.SetInt ("HighScore2", tempScoreStorage2);
            PlayerPrefs.SetInt ("HighScore3", tempScoreStorage3);
            PlayerPrefs.SetInt ("HighScore4", tempScoreStorage4);

        }
        else if (playerCurrentScore.value > PlayerPrefs.GetInt ("HighScore1"))
        {
            int tempScoreStorage2 = PlayerPrefs.GetInt ("HighScore1");
            int tempScoreStorage3 = PlayerPrefs.GetInt ("HighScore2");
            int tempScoreStorage4 = PlayerPrefs.GetInt ("HighScore3");
            PlayerPrefs.SetInt ("HighScore1", playerCurrentScore.value);

            PlayerPrefs.SetInt ("HighScore2", tempScoreStorage2);
            PlayerPrefs.SetInt ("HighScore3", tempScoreStorage3);
            PlayerPrefs.SetInt ("HighScore4", tempScoreStorage4);
        }
        else if (playerCurrentScore.value > PlayerPrefs.GetInt ("HighScore2"))
        {
            int tempScoreStorage3 = PlayerPrefs.GetInt ("HighScore2");
            int tempScoreStorage4 = PlayerPrefs.GetInt ("HighScore3");
            PlayerPrefs.SetInt ("HighScore2", playerCurrentScore.value);

            PlayerPrefs.SetInt ("HighScore3", tempScoreStorage3);
            PlayerPrefs.SetInt ("HighScore4", tempScoreStorage4);
        }
        else if (playerCurrentScore.value > PlayerPrefs.GetInt ("HighScore3"))
        {
            int tempScoreStorage4 = PlayerPrefs.GetInt ("HighScore3");
            PlayerPrefs.SetInt ("HighScore3", playerCurrentScore.value);

            PlayerPrefs.SetInt ("HighScore4", tempScoreStorage4);
        }
        else if (playerCurrentScore.value > PlayerPrefs.GetInt ("HighScore4"))
        {
            PlayerPrefs.SetInt ("HighScore4", playerCurrentScore.value);
        }

    }

    public void DisplayScoreMenu ()
    {
        highScoreMenu.gameObject.SetActive (true);
        highScoreMenu.text = "";

        highScoreMenu.text = "HighScores!";
        highScoreMenu.text += ("\n" + (PlayerPrefs.GetInt ("HighScore0").ToString ()));
        highScoreMenu.text += ("\n" + (PlayerPrefs.GetInt ("HighScore1").ToString ()));
        highScoreMenu.text += ("\n" + (PlayerPrefs.GetInt ("HighScore2").ToString ()));
        highScoreMenu.text += ("\n" + (PlayerPrefs.GetInt ("HighScore3").ToString ()));
        highScoreMenu.text += ("\n" + (PlayerPrefs.GetInt ("HighScore4").ToString ()));

    }
}