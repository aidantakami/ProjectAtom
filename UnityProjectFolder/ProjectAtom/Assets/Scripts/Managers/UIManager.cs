﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] public TextMeshProUGUI boomerangLifeCooldownText;
    [SerializeField] public FloatVariable boomerangCooldown;
    [SerializeField] public TextMeshProUGUI springboardCooldownText;
    [SerializeField] public FloatVariable springboardCooldown;


    [SerializeField] List<Button> PauseMenuButtons = new List<Button>();
    private bool joystick1ReturnToZero = true;
    private bool joystick2ReturnToZero = true;

    private bool boomerangThrown = false;
    private bool springboardDown = false;


    private int selectedButton = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Set Text for player sign in
        player1SignInText.text = "Player 1 Press A";
        player2SignInText.text = "Player 2 Press B";
        gameEndedText.text = "Game Over";

        gameEndedText.gameObject.SetActive(false);

        player1SignInText.gameObject.SetActive(true);
        player2SignInText.gameObject.SetActive(true);

        PauseMenu.SetActive(false);
    }

    private void Update()
    {
        //If paused
        if (isPaused.value)
        {
            //If player one pushed stick down
            if((Input.GetAxis("P1Left Stick Vertical") < -0.8f && joystick1ReturnToZero))
            {
                //Change selection
                if(selectedButton == 0)
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
            else if(Input.GetAxis("P2Left Stick Vertical") < -0.8f && joystick2ReturnToZero)
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
            else if((Input.GetAxis("P1Left Stick Vertical") > 0.8f) && joystick1ReturnToZero)
            {
                if (selectedButton == PauseMenuButtons.Count-1)
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
            else if(Input.GetAxis("P2Left Stick Vertical") > 0.8f && joystick2ReturnToZero)
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
                if((Input.GetAxis("P1Left Stick Vertical") < 0.2f) && Input.GetAxis("P1Left Stick Vertical") > -0.2f)
                {
                    joystick1ReturnToZero = true;
                }
            }

            if (!joystick2ReturnToZero)
            {
                if ((Input.GetAxis("P2Left Stick Vertical") < 0.2f) && Input.GetAxis("P2Left Stick Vertical") > -0.2f)
                {
                    joystick2ReturnToZero = true;
                }
            }

            PauseMenuButtons[selectedButton].image.color = Color.blue;

            //Player Pressed A
            if(Input.GetButtonDown("P1A Button") || Input.GetButtonDown("P2A Button"))
            {
                PauseMenuButtons[selectedButton].onClick.Invoke();
            }

            //Keyboard
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RestartFromUI();
            }
        }


        if (boomerangThrown)
        {
            boomerangLifeCooldownText.text = boomerangCooldown.value.ToString();
        }

        if (springboardDown)
        {
            springboardCooldownText.text = springboardCooldown.value.ToString();
        }
    }

    public void PlayerSignedIn(int playerNumber)
    {
        if(playerNumber == 1)
        {
            player1SignInText.text = "Dog, Signed In";
            player1SignInText.color = Color.green;
        }
        
        if(playerNumber == 2)
        {
            player2SignInText.text = "Boomerang, Signed In";
            player2SignInText.color = Color.green;
        }
    }

    public void UIManagerGameStart()
    {
        gameEndedText.gameObject.SetActive(false);

        player1SignInText.color = Color.white;
        player2SignInText.color = Color.white;

        player1SignInText.gameObject.SetActive(false);
        player2SignInText.gameObject.SetActive(false);

        boomerangLifeCooldownText.gameObject.SetActive(false);
        springboardCooldownText.gameObject.SetActive(false);
    }

    //Pause Menu
    #region
    public void UIPause()
    {
        PauseMenu.SetActive(true);
    }

    //Listening to event
    public void UIUnpause()
    {
        joystick1ReturnToZero = true;
        joystick2ReturnToZero = true;
        PauseMenu.SetActive(false);
    }

    //Used internally from buttons on pause menu
    public void UnpauseFromUI()
    {
        UIUnpause();
        UnpauseGame.Invoke();
    }

    public void RestartFromUI()
    {
        UnpauseFromUI();
        RestartGame.Invoke();    
    }

    public void UIEndGame()
    {
        gameEndedText.gameObject.SetActive(true);
        player1SignInText.gameObject.SetActive(true);
        player2SignInText.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        // Tell GM to Quit Game
    }
    #endregion


    //Cooldown Menu
    #region

    public void BoomerangThrownUIStart()
    {
        boomerangLifeCooldownText.gameObject.SetActive(true);
        boomerangThrown = true;
    }

    public void BoomerangThrownUIEnd()
    {
        boomerangLifeCooldownText.gameObject.SetActive(false);
        boomerangThrown = false;
    }

    public void SpringboardUIStart()
    {
        springboardCooldownText.gameObject.SetActive(true);
        springboardDown = true;
    }

    public void SpringboardUIEnd()
    {
        springboardCooldownText.gameObject.SetActive(false);
        springboardDown = false;
    }


    #endregion
}
