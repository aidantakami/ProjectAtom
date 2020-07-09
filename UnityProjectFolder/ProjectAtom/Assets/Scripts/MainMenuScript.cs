using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    [SerializeField] public GameObject MainMenuGO;
    [SerializeField] public GameObject OptionsMenuGO;
    [SerializeField] public GameObject ScoresMenuGO;

    [SerializeField] public AudioSource menuNextSelectionSound;
    private bool joystick1ReturnToZero = true;
    private bool joystick2ReturnToZero = true;

    private bool mainMenuActive = true;
    private bool optionsMenuActive = false;
    private bool scoresMenuActive = false;

    private int selectedButton = 0;
    [SerializeField] private List<Button> mainMenuButtons = new List<Button> ();
    [SerializeField] private List<Button> optionsMenuButtons = new List<Button> ();
    [SerializeField] private List<Slider> optionsMenuSliders = new List<Slider> ();
    [SerializeField] private List<Button> scoresMenuButtons = new List<Button> ();

    // Start is called before the first frame update
    void Start ()
    {
        ScoresMenuGO.SetActive (false);
        OptionsMenuGO.SetActive (false);

    }

    // Update is called once per frame
    void Update ()
    {
        //Main Menu Navigation
        if (mainMenuActive)
        {
            if ((Input.GetAxis ("P1Left Stick Vertical") < -0.8f && joystick1ReturnToZero))
            {
                //Change Selection
                if (selectedButton == 0)
                {
                    mainMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton = mainMenuButtons.Count - 1;
                }
                else
                {
                    mainMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton--;
                }

                menuNextSelectionSound.Play ();
                joystick1ReturnToZero = false;

            }

            else if ((Input.GetAxis ("P2Left Stick Vertical") < -0.8f && joystick1ReturnToZero))
            {
                //Change Selection
                if (selectedButton == 0)
                {
                    mainMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton = mainMenuButtons.Count - 1;
                }
                else
                {
                    mainMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton--;
                }

                menuNextSelectionSound.Play ();
                joystick2ReturnToZero = false;

            }

            else if ((Input.GetAxis ("P1Left Stick Vertical") > 0.8f) && joystick1ReturnToZero)
            {
                if (selectedButton == mainMenuButtons.Count - 1)
                {
                    mainMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton = 0;
                }
                else
                {
                    mainMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton++;
                }

                menuNextSelectionSound.Play ();

                //bool to make player bring stick back to 0
                joystick1ReturnToZero = false;

            }
            else if (Input.GetAxis ("P2Left Stick Vertical") > 0.8f && joystick2ReturnToZero)
            {
                if (selectedButton == mainMenuButtons.Count - 1)
                {
                    mainMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton = 0;
                }
                else
                {
                    mainMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton++;
                }

                menuNextSelectionSound.Play ();

                //bool to make player bring stick back to 0
                joystick2ReturnToZero = false;

            }

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

            //Player Pressed A
            if (Input.GetButtonDown ("P1A Button") || Input.GetButtonDown ("P2A Button"))
            {
                mainMenuButtons[selectedButton].onClick.Invoke ();
                menuNextSelectionSound.Play ();
            }

            mainMenuButtons[selectedButton].image.color = Color.blue;
        }

        else if (optionsMenuActive)
        {
            if ((Input.GetAxis ("P1Left Stick Vertical") < -0.8f && joystick1ReturnToZero))
            {
                //Change Selection
                if (selectedButton == 0)
                {
                    optionsMenuButtons[selectedButton].image.color = Color.black;
                    selectedButton = optionsMenuButtons.Count - 1;
                }
                else
                {
                    optionsMenuButtons[selectedButton].image.color = Color.black;
                    selectedButton--;
                }

                joystick1ReturnToZero = false;

            }

            else if ((Input.GetAxis ("P2Left Stick Vertical") < -0.8f && joystick1ReturnToZero))
            {
                //Change Selection
                if (selectedButton == 0)
                {
                    optionsMenuButtons[selectedButton].image.color = Color.black;
                    selectedButton = optionsMenuButtons.Count - 1;
                }
                else
                {
                    optionsMenuButtons[selectedButton].image.color = Color.black;
                    selectedButton--;
                }

                joystick1ReturnToZero = false;

            }

            else if ((Input.GetAxis ("P1Left Stick Vertical") > 0.8f) && joystick1ReturnToZero)
            {
                if (selectedButton == optionsMenuButtons.Count - 1)
                {
                    optionsMenuButtons[selectedButton].image.color = Color.black;
                    selectedButton = 0;
                }
                else
                {
                    optionsMenuButtons[selectedButton].image.color = Color.black;
                    selectedButton++;
                }

                //bool to make player bring stick back to 0
                joystick1ReturnToZero = false;

            }
            else if (Input.GetAxis ("P2Left Stick Vertical") > 0.8f && joystick2ReturnToZero)
            {
                if (selectedButton == optionsMenuButtons.Count - 1)
                {
                    optionsMenuButtons[selectedButton].image.color = Color.black;
                    selectedButton = 0;
                }
                else
                {
                    optionsMenuButtons[selectedButton].image.color = Color.black;
                    selectedButton++;
                }

                //bool to make player bring stick back to 0
                joystick2ReturnToZero = false;

            }

            //Horizontal input
            if (Input.GetAxis ("P1Left Stick Horizontal") > 0.8f && joystick1ReturnToZero)
            {
                if (selectedButton <= 2)
                {
                    optionsMenuSliders[selectedButton].value += 1;
                }
            }
            else if (Input.GetAxis ("P2Left Stick Horizontal") > 0.8f && joystick2ReturnToZero)
            {
                if (selectedButton <= 2)
                {
                    optionsMenuSliders[selectedButton].value += 1;
                }
            }
            else if (Input.GetAxis ("P1Left Stick Horizontal") < -0.8f && joystick1ReturnToZero)
            {
                if (selectedButton <= 2)
                {
                    optionsMenuSliders[selectedButton].value -= 1;
                }
            }
            else if (Input.GetAxis ("P2Left Stick Horizontal") < -0.8f && joystick2ReturnToZero)
            {
                if (selectedButton <= 2)
                {
                    optionsMenuSliders[selectedButton].value -= 1;
                }
            }

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

            //Player Pressed A
            if (Input.GetButtonDown ("P1A Button") || Input.GetButtonDown ("P2A Button"))
            {
                optionsMenuButtons[selectedButton].onClick.Invoke ();
            }

            optionsMenuButtons[selectedButton].image.color = Color.blue;
        }

        else if (scoresMenuActive)
        {
            if ((Input.GetAxis ("P1Left Stick Vertical") < -0.8f && joystick1ReturnToZero))
            {
                //Change Selection
                if (selectedButton == 0)
                {
                    scoresMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton = scoresMenuButtons.Count - 1;
                }
                else
                {
                    scoresMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton--;
                }

                menuNextSelectionSound.Play ();
                joystick1ReturnToZero = false;

            }

            else if ((Input.GetAxis ("P2Left Stick Vertical") < -0.8f && joystick1ReturnToZero))
            {
                //Change Selection
                if (selectedButton == 0)
                {
                    scoresMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton = scoresMenuButtons.Count - 1;
                }
                else
                {
                    scoresMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton--;
                }

                menuNextSelectionSound.Play ();
                joystick1ReturnToZero = false;

            }

            else if ((Input.GetAxis ("P1Left Stick Vertical") > 0.8f) && joystick1ReturnToZero)
            {
                if (selectedButton == scoresMenuButtons.Count - 1)
                {
                    scoresMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton = 0;
                }
                else
                {
                    scoresMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton++;
                }

                menuNextSelectionSound.Play ();

                //bool to make player bring stick back to 0
                joystick1ReturnToZero = false;

            }
            else if (Input.GetAxis ("P2Left Stick Vertical") > 0.8f && joystick2ReturnToZero)
            {
                if (selectedButton == scoresMenuButtons.Count - 1)
                {
                    scoresMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton = 0;
                }
                else
                {
                    scoresMenuButtons[selectedButton].image.color = Color.white;
                    selectedButton++;
                }

                menuNextSelectionSound.Play ();

                //bool to make player bring stick back to 0
                joystick2ReturnToZero = false;

            }

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

            //Player Pressed A
            if (Input.GetButtonDown ("P1A Button") || Input.GetButtonDown ("P2A Button"))
            {
                scoresMenuButtons[selectedButton].onClick.Invoke ();
            }

            scoresMenuButtons[selectedButton].image.color = Color.blue;
        }
    }

    public void MainMenuStartGame ()
    {
        SceneManager.LoadScene ("Main Game Scene", LoadSceneMode.Single);
        SceneManager.LoadScene ("Main UI Scene", LoadSceneMode.Additive);
        SceneManager.LoadScene ("Lighting", LoadSceneMode.Additive);
        SceneManager.LoadScene ("Audio", LoadSceneMode.Additive);
    }

    public void OptionsSelected ()
    {
        menuNextSelectionSound.Play ();

        MainMenuGO.SetActive (false);
        OptionsMenuGO.SetActive (true);
        selectedButton = 0;
        mainMenuActive = false;
        optionsMenuActive = true;
    }

    public void ScoresSelected ()
    {
        menuNextSelectionSound.Play ();

        MainMenuGO.SetActive (false);
        ScoresMenuGO.SetActive (true);
        selectedButton = 0;
        mainMenuActive = false;
        scoresMenuActive = true;

    }

    public void BackButton ()
    {
        menuNextSelectionSound.Play ();

        ClearButtonColors ();

        OptionsMenuGO.SetActive (false);
        ScoresMenuGO.SetActive (false);
        MainMenuGO.SetActive (true);

        selectedButton = 0;

        mainMenuActive = true;
        scoresMenuActive = false;
        optionsMenuActive = false;
    }

    public void QuitGame ()
    {
        // Tell GM to Quit Game
        Application.Quit ();
    }

    public void ClearButtonColors ()
    {
        foreach (Button tempButton in mainMenuButtons)
        {
            tempButton.image.color = Color.white;
        }

        foreach (Button tempButton in optionsMenuButtons)
        {
            tempButton.image.color = Color.black;
        }

        foreach (Button tempButton in scoresMenuButtons)
        {
            tempButton.image.color = Color.white;
        }
    }

}