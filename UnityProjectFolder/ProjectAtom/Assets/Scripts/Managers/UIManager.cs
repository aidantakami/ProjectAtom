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
    [SerializeField] public TextMeshProUGUI boomerangDeadText;
    [SerializeField] public BoolVariable boomCanBeCaught;
    [SerializeField] public GameObject boomerangCatchPackage;
    [SerializeField] public GameObject PauseMenu;
    [SerializeField] public UnityEvent UnpauseGame;
    [SerializeField] public UnityEvent RestartGame;
    [SerializeField] public UnityEvent QuitGameEvent;
    [SerializeField] public BoolVariable isPaused;

    //Cooldowns
    [SerializeField] public Image boomerangLifeCooldownImage;
    [SerializeField] public GameObject buttonInstructions;
    [SerializeField] public FloatVariable boomerangCooldown;

    //Abilities
    [SerializeField] public TextMeshProUGUI dogAbilityText;
    [SerializeField] public TextMeshProUGUI dogAbilityTokenText;
    [SerializeField] public TextMeshProUGUI boomerangAbilityText;
    [SerializeField] public TextMeshProUGUI boomerangAbilityTokenText;
    [SerializeField] public StringVariable dogSelectedAbility;
    [SerializeField] public StringVariable boomSelectedAbility;
    [SerializeField] public IntVariable dogAbilityTokens;
    [SerializeField] public IntVariable boomAbilityTokens;

    [SerializeField] List<Button> PauseMenuButtons = new List<Button> ();
    private bool joystick1ReturnToZero = true;
    private bool joystick2ReturnToZero = true;

    private bool boomerangThrown = false;
    private bool boomDead = false;

    private Color clearColor = new Color (0f, 0f, 0f, 0f);

    private int selectedButton = 0;

    // Start is called before the first frame update
    void Start ()
    {
        //Set Text for player sign in
        player1SignInText.text = "Player 1 Press A\n Keyboard: E";
        player2SignInText.text = "Player 2 Press B\n Keyboard: RShift";
        gameEndedText.text = "Zoomer Down \n \nGame Over!";

        gameEndedText.gameObject.SetActive (false);

        player1SignInText.gameObject.SetActive (true);
        player2SignInText.gameObject.SetActive (true);
        boomerangCatchPackage.SetActive (false);

        PauseMenu.SetActive (false);

        dogAbilityText.gameObject.SetActive (false);
        dogAbilityTokenText.gameObject.SetActive (false);
        boomerangAbilityText.gameObject.SetActive (false);
        boomerangAbilityTokenText.gameObject.SetActive (false);
        boomerangDeadText.gameObject.SetActive (false);
        buttonInstructions.SetActive (false);

    }

    private void Update ()
    {
        //If paused
        if (isPaused.value)
        {
            //If player one pushed stick down
            if ((Input.GetAxis ("P1Left Stick Vertical") < -0.8f && joystick1ReturnToZero) || Input.GetKeyDown (KeyCode.UpArrow))
            {
                //Change selection
                if (selectedButton == 0)
                {
                    PauseMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton = PauseMenuButtons.Count - 1;
                }
                else
                {
                    PauseMenuButtons[selectedButton].image.color = clearColor;
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
                    PauseMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton = PauseMenuButtons.Count - 1;
                }
                else
                {
                    PauseMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton--;
                }

                //Force player to reset joystick
                joystick2ReturnToZero = false;
            }

            //If player pushes stick up
            else if ((Input.GetAxis ("P1Left Stick Vertical") > 0.8f) && joystick1ReturnToZero || Input.GetKeyDown (KeyCode.DownArrow))
            {
                if (selectedButton == PauseMenuButtons.Count - 1)
                {
                    PauseMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton = 0;
                }
                else
                {
                    PauseMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton++;
                }

                //bool to make player bring stick back to 0
                joystick1ReturnToZero = false;
            }
            else if (Input.GetAxis ("P2Left Stick Vertical") > 0.8f && joystick2ReturnToZero)
            {
                if (selectedButton == PauseMenuButtons.Count - 1)
                {
                    PauseMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton = 0;
                }
                else
                {
                    PauseMenuButtons[selectedButton].image.color = clearColor;
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
            if (Input.GetButtonDown ("P1A Button") || Input.GetButtonDown ("P2A Button") || Input.GetKeyDown (KeyCode.Return))
            {
                PauseMenuButtons[selectedButton].onClick.Invoke ();
            }

        }

        if (boomerangThrown)
        {
            boomerangLifeCooldownImage.fillAmount = boomerangCooldown.value / 15f;

            if (boomCanBeCaught.value)
            {
                boomerangCatchPackage.SetActive (true);
            }
            else boomerangCatchPackage.SetActive (false);
        }

        dogAbilityText.text = (dogSelectedAbility.value);
        dogAbilityTokenText.text = dogAbilityTokens.value.ToString ();
        boomerangAbilityText.text = (boomSelectedAbility.value);
        boomerangAbilityTokenText.text = boomAbilityTokens.value.ToString ();

    }

    public void PlayerSignedIn (int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1SignInText.text = PlayerSignInText (true);
            player1SignInText.color = Color.green;
        }

        if (playerNumber == 2)
        {
            player2SignInText.text = PlayerSignInText (false);
            player2SignInText.color = Color.green;
        }
    }

    private string PlayerSignInText (bool isZoomer)
    {
        int randomNumber = UnityEngine.Random.Range (0, 5);

        if (isZoomer)
        {
            if (randomNumber == 0)
            {
                return "Zoomer locked and loaded.";
            }
            else if (randomNumber == 1)
            {
                return "Zoomer wants to run";
            }
            else if (randomNumber == 2)
            {
                return "Zoomer ready to zoom";
            }
            else if (randomNumber == 3)
            {
                return "Zoomer wants to go!";
            }
            else if (randomNumber == 4)
            {
                return "Zoomer ready to throw Boomer!";
            }
            else return "Zoomer Ready!";
        }
        else
        {
            if (randomNumber == 0)
            {
                return "Boomer ready to fly";
            }
            else if (randomNumber == 1)
            {
                return "Boomer cleared for liftoff";
            }
            else if (randomNumber == 2)
            {
                return "Boomer ready to be thrown";
            }
            else if (randomNumber == 3)
            {
                return "Boomer WANTS TO FLY";
            }
            else if (randomNumber == 4)
            {
                return "Boomer waiting on Zoomer";
            }
            else return "Boomer Ready!";
        }
    }

    public void UIManagerGameStart ()
    {
        gameEndedText.gameObject.SetActive (false);

        player1SignInText.color = Color.white;
        player2SignInText.color = Color.white;

        player1SignInText.gameObject.SetActive (false);
        player2SignInText.gameObject.SetActive (false);
        boomerangDeadText.gameObject.SetActive (false);

        dogAbilityText.gameObject.SetActive (true);
        boomerangAbilityText.gameObject.SetActive (true);

        dogAbilityTokenText.gameObject.SetActive (true);
        boomerangAbilityTokenText.gameObject.SetActive (true);

        buttonInstructions.SetActive (true);

        boomerangLifeCooldownImage.gameObject.SetActive (false);

        boomDead = false;
    }

    //Pause Menu
    #region
    public void UIPause ()
    {
        PauseMenu.SetActive (true);
        buttonInstructions.SetActive (false);

    }

    //Listening to event
    public void UIUnpause ()
    {
        joystick1ReturnToZero = true;
        joystick2ReturnToZero = true;
        buttonInstructions.SetActive (true);

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

        player1SignInText.text = "Player 1 Press A";
        player2SignInText.text = "Player 2 Press B";

    }

    public void QuitGame ()
    {
        QuitGameEvent.Invoke ();
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
        boomerangCatchPackage.SetActive (false);

        boomerangThrown = false;
    }

    //ends all Boomerang UI when dead
    public void BoomerangDeadUI ()
    {
        boomDead = true;
        boomerangDeadText.gameObject.SetActive (true);
        boomerangCatchPackage.SetActive (false);
        BoomerangThrownUIEnd ();
    }

    public void BoomerangRevivedUI ()
    {
        boomerangDeadText.gameObject.SetActive (false);
        boomDead = false;
    }

    #endregion
}