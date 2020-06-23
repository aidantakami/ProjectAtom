using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] public TextMeshProUGUI player1SignInText;
    [SerializeField] public TextMeshProUGUI player2SignInText;
    [SerializeField] public TextMeshProUGUI gameEndedText;
    [SerializeField] public GameObject PauseMenu;
    [SerializeField] public UnityEvent UnpauseGame;
    [SerializeField] public UnityEvent RestartGame;
    [SerializeField] public BoolVariable isPaused;

    //Cooldowns
    [SerializeField] public Image boomerangLifeCooldownImage;
    [SerializeField] public FloatVariable boomerangCooldown;

    //Abilities
    [SerializeField] public TextMeshProUGUI dogAbilityText;
    [SerializeField] public TextMeshProUGUI boomerangAbilityText;
    [SerializeField] public StringVariable dogSelectedAbility;
    [SerializeField] public StringVariable boomSelectedAbility;
    [SerializeField] public IntVariable dogAbilityTokens;
    [SerializeField] public IntVariable boomAbilityTokens;

    [SerializeField] List<Button> PauseMenuButtons = new List<Button> ();
    private bool joystick1ReturnToZero = true;
    private bool joystick2ReturnToZero = true;

    private bool boomerangThrown = false;
    private bool boomDead = false;

    private int selectedButton = 0;

    // Start is called before the first frame update
    void Start ()
    {
        //Set Text for player sign in
        player1SignInText.text = "Player 1 Press A";
        player2SignInText.text = "Player 2 Press B";
        gameEndedText.text = "Game Over";

        gameEndedText.gameObject.SetActive (false);

        player1SignInText.gameObject.SetActive (true);
        player2SignInText.gameObject.SetActive (true);

        PauseMenu.SetActive (false);

        dogAbilityText.gameObject.SetActive (false);
        boomerangAbilityText.gameObject.SetActive (false);
    }

    private void Update ()
    {
        //If paused
        if (isPaused.value)
        {
            //If player one pushed stick down
            if ((Input.GetAxis ("P1Left Stick Vertical") < -0.8f && joystick1ReturnToZero))
            {
                //Change selection
                if (selectedButton == 0)
                {
                    PauseMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton = PauseMenuButtons.Count - 1;
                }
                else
                {
                    PauseMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton--;
                }

                //Force player to reset joystick
                joystick1ReturnToZero = false;
            }
            else if (Input.GetAxis ("P2Left Stick Vertical") < -0.8f && joystick2ReturnToZero)
            {
                //Change selection
                if (selectedButton == 0)
                {
                    PauseMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton = PauseMenuButtons.Count - 1;
                }
                else
                {
                    PauseMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton--;
                }

                //Force player to reset joystick
                joystick2ReturnToZero = false;
            }

            //If player pushes stick up
            else if ((Input.GetAxis ("P1Left Stick Vertical") > 0.8f) && joystick1ReturnToZero)
            {
                if (selectedButton == PauseMenuButtons.Count - 1)
                {
                    PauseMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton = 0;
                }
                else
                {
                    PauseMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton++;
                }

                //bool to make player bring stick back to 0
                joystick1ReturnToZero = false;
            }
            else if (Input.GetAxis ("P2Left Stick Vertical") > 0.8f && joystick2ReturnToZero)
            {
                if (selectedButton == PauseMenuButtons.Count - 1)
                {
                    PauseMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton = 0;
                }
                else
                {
                    PauseMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton++;
                }

                //bool to make player bring stick back to 0
                joystick2ReturnToZero = false;
            }

            //Return Stick to 0
            if (!joystick1ReturnToZero)
            {
                if ((Input.GetAxis ("P1Left Stick Vertical") < 0.2f) && Input.GetAxis ("P1Left Stick Vertical") > -0.2f)
                {
                    joystick1ReturnToZero = true;
                }
            }

            if (!joystick2ReturnToZero)
            {
                if ((Input.GetAxis ("P2Left Stick Vertical") < 0.2f) && Input.GetAxis ("P2Left Stick Vertical") > -0.2f)
                {
                    joystick2ReturnToZero = true;
                }
            }

            PauseMenuButtons[selectedButton].image.color = Color.blue;

            //Player Pressed A
            if (Input.GetButtonDown ("P1A Button") || Input.GetButtonDown ("P2A Button"))
            {
                PauseMenuButtons[selectedButton].onClick.Invoke ();
            }

            //Keyboard
            if (Input.GetKeyDown (KeyCode.Q))
            {
                RestartFromUI ();
            }
        }

        if (boomerangThrown)
        {
            boomerangLifeCooldownImage.fillAmount = boomerangCooldown.value / 15f;
        }

        dogAbilityText.text = (dogAbilityTokens.value.ToString () + Environment.NewLine + dogSelectedAbility.value);
        boomerangAbilityText.text = (boomAbilityTokens.value.ToString () + Environment.NewLine + boomSelectedAbility.value);
    }

    public void PlayerSignedIn (int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1SignInText.text = "Dog, Signed In";
            player1SignInText.color = Color.green;
        }

        if (playerNumber == 2)
        {
            player2SignInText.text = "Boomerang, Signed In";
            player2SignInText.color = Color.green;
        }
    }

    public void UIManagerGameStart ()
    {
        gameEndedText.gameObject.SetActive (false);

        player1SignInText.color = Color.white;
        player2SignInText.color = Color.white;

        player1SignInText.gameObject.SetActive (false);
        player2SignInText.gameObject.SetActive (false);

        dogAbilityText.gameObject.SetActive (true);
        boomerangAbilityText.gameObject.SetActive (true);

        boomerangLifeCooldownImage.gameObject.SetActive (false);

        boomDead = false;
    }

    //Pause Menu
    #region
    public void UIPause ()
    {
        PauseMenu.SetActive (true);
    }

    //Listening to event
    public void UIUnpause ()
    {
        joystick1ReturnToZero = true;
        joystick2ReturnToZero = true;
        PauseMenu.SetActive (false);
    }

    //Used internally from buttons on pause menu
    public void UnpauseFromUI ()
    {
        UIUnpause ();
        UnpauseGame.Invoke ();
    }

    public void RestartFromUI ()
    {
        UIUnpause ();
        RestartGame.Invoke ();
        boomDead = false;
    }

    public void UIEndGame ()
    {
        gameEndedText.gameObject.SetActive (true);
        player1SignInText.gameObject.SetActive (true);
        player2SignInText.gameObject.SetActive (true);
    }

    public void QuitGame ()
    {
        SceneManager.LoadScene ("MainMenuScene", LoadSceneMode.Single);
    }
    #endregion

    //Cooldown Menu
    #region

    //Starts UI countdown for boomerang thrown
    public void BoomerangThrownUIStart ()
    {

        if (!boomDead)
        {
            boomerangLifeCooldownImage.gameObject.SetActive (true);
            boomerangLifeCooldownImage.fillAmount = 1;
            boomerangThrown = true;
        }
    }

    //Ends UI for boomerang countdown
    public void BoomerangThrownUIEnd ()
    {
        boomerangLifeCooldownImage.gameObject.SetActive (false);

        boomerangThrown = false;
    }

    //ends all Boomerang UI when dead
    public void BoomerangDeadUI ()
    {
        boomDead = true;
        BoomerangThrownUIEnd ();
    }

    public void BoomerangRevivedUI ()
    {
        boomDead = false;
    }

    #endregion
}