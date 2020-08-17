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
    [SerializeField] public GameObject htpMenu;
    [SerializeField] public GameObject keyboardMenu;
    [SerializeField] public GameObject xboxMenu;
    [SerializeField] public GameObject basicsMenu;
    [SerializeField] public GameObject aboutMenu;

    [SerializeField] public AudioSource menuNextSelectionSound;
    private bool joystick1ReturnToZero = true;
    private bool joystick2ReturnToZero = true;

    private bool mainMenuActive = true;
    private bool optionsMenuActive = false;
    private bool scoresMenuActive = false;
    private bool htpMenuActive = false;
    private bool aboutMenuActive = false;

    private int selectedButton = 0;
    private Color clearColor = new Color (0, 0, 0, 0);
    [SerializeField] private List<Button> mainMenuButtons = new List<Button> ();
    [SerializeField] private List<Button> optionsMenuButtons = new List<Button> ();
    [SerializeField] private List<Slider> optionsMenuSliders = new List<Slider> ();
    [SerializeField] private List<Button> scoresMenuButtons = new List<Button> ();
    [SerializeField] private List<Button> htpMenuButtons = new List<Button> ();
    [SerializeField] private List<Button> aboutMenuButtons = new List<Button> ();

    // Start is called before the first frame update
    void Start ()
    {
        ScoresMenuGO.SetActive (false);
        OptionsMenuGO.SetActive (false);
        htpMenu.SetActive (false);
        aboutMenu.SetActive (false);
        SceneManager.LoadScene ("Audio", LoadSceneMode.Additive);
        SceneManager.LoadScene ("MenuLighting", LoadSceneMode.Additive);

    }

    // Update is called once per frame
    void Update ()
    {
        //Main Menu Navigation
        if (mainMenuActive)
        {
            if ((Input.GetAxis ("P1Left Stick Vertical") < -0.8f && joystick1ReturnToZero) || Input.GetKeyDown (KeyCode.UpArrow))
            {
                //Change Selection
                if (selectedButton == 0)
                {
                    mainMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton = mainMenuButtons.Count - 1;
                }
                else
                {
                    mainMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton--;
                }

                menuNextSelectionSound.Play ();
                joystick1ReturnToZero = false;

            }

            else if ((Input.GetAxis ("P2Left Stick Vertical") < -0.8f && joystick2ReturnToZero))
            {
                //Change Selection
                if (selectedButton == 0)
                {
                    mainMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton = mainMenuButtons.Count - 1;
                }
                else
                {
                    mainMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton--;
                }

                menuNextSelectionSound.Play ();
                joystick2ReturnToZero = false;

            }

            else if ((Input.GetAxis ("P1Left Stick Vertical") > 0.8f) && joystick1ReturnToZero || Input.GetKeyDown (KeyCode.DownArrow))
            {
                if (selectedButton == mainMenuButtons.Count - 1)
                {
                    mainMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton = 0;
                }
                else
                {
                    mainMenuButtons[selectedButton].image.color = clearColor;
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
                    mainMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton = 0;
                }
                else
                {
                    mainMenuButtons[selectedButton].image.color = clearColor;
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
            if (Input.GetButtonDown ("P1A Button") || Input.GetButtonDown ("P2A Button") || Input.GetKeyDown (KeyCode.Return))
            {
                mainMenuButtons[selectedButton].onClick.Invoke ();
                menuNextSelectionSound.Play ();
            }

            mainMenuButtons[selectedButton].image.color = Color.blue;
        }

        else if (optionsMenuActive)
        {
            if ((Input.GetAxis ("P1Left Stick Vertical") < -0.8f && joystick1ReturnToZero) || Input.GetKeyDown (KeyCode.UpArrow))
            {
                //Change Selection
                if (selectedButton == 0)
                {
                    optionsMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton = optionsMenuButtons.Count - 1;
                }
                else
                {
                    optionsMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton--;
                }

                joystick1ReturnToZero = false;

            }

            else if ((Input.GetAxis ("P2Left Stick Vertical") < -0.8f && joystick1ReturnToZero))
            {
                //Change Selection
                if (selectedButton == 0)
                {
                    optionsMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton = optionsMenuButtons.Count - 1;
                }
                else
                {
                    optionsMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton--;
                }

                joystick1ReturnToZero = false;

            }

            else if (((Input.GetAxis ("P1Left Stick Vertical") > 0.8f) && joystick1ReturnToZero) || Input.GetKeyDown (KeyCode.DownArrow))
            {
                if (selectedButton == optionsMenuButtons.Count - 1)
                {
                    optionsMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton = 0;
                }
                else
                {
                    optionsMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton++;
                }

                //bool to make player bring stick back to 0
                joystick1ReturnToZero = false;

            }
            else if (Input.GetAxis ("P2Left Stick Vertical") > 0.8f && joystick2ReturnToZero)
            {
                if (selectedButton == optionsMenuButtons.Count - 1)
                {
                    optionsMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton = 0;
                }
                else
                {
                    optionsMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton++;
                }

                //bool to make player bring stick back to 0
                joystick2ReturnToZero = false;

            }

            //Horizontal input
            if ((Input.GetAxis ("P1Left Stick Horizontal") > 0.8f && joystick1ReturnToZero) || Input.GetKeyDown (KeyCode.RightArrow))
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
            else if (Input.GetAxis ("P1Left Stick Horizontal") < -0.8f && joystick1ReturnToZero || Input.GetKeyDown (KeyCode.LeftArrow))
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
            if (Input.GetButtonDown ("P1A Button") || Input.GetButtonDown ("P2A Button") || Input.GetKeyDown (KeyCode.Return))
            {
                optionsMenuButtons[selectedButton].onClick.Invoke ();
            }

            optionsMenuButtons[selectedButton].image.color = Color.blue;
        }

        else if (scoresMenuActive)
        {

            //Player Pressed A
            if (Input.GetButtonDown ("P1A Button") || Input.GetButtonDown ("P2A Button") || Input.GetKeyDown (KeyCode.Return))
            {
                scoresMenuButtons[0].onClick.Invoke ();
            }

            scoresMenuButtons[0].image.color = Color.blue;
        }

        else if (htpMenuActive)
        {
            if ((Input.GetAxis ("P1Left Stick Vertical") < -0.8f && joystick1ReturnToZero) || Input.GetKeyDown (KeyCode.UpArrow))
            {
                //Change Selection
                if (selectedButton == 0)
                {
                    htpMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton = scoresMenuButtons.Count - 1;
                }
                else
                {
                    htpMenuButtons[selectedButton].image.color = clearColor;
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
                    htpMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton = scoresMenuButtons.Count - 1;
                }
                else
                {
                    htpMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton--;
                }

                menuNextSelectionSound.Play ();
                joystick1ReturnToZero = false;

            }

            else if (((Input.GetAxis ("P1Left Stick Vertical") > 0.8f) && joystick1ReturnToZero) || Input.GetKeyDown (KeyCode.DownArrow))
            {
                if (selectedButton == htpMenuButtons.Count - 1)
                {
                    htpMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton = 0;
                }
                else
                {
                    htpMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton++;
                }

                menuNextSelectionSound.Play ();

                //bool to make player bring stick back to 0
                joystick1ReturnToZero = false;

            }
            else if ((Input.GetAxis ("P2Left Stick Vertical") > 0.8f && joystick2ReturnToZero))
            {
                if (selectedButton == htpMenuButtons.Count - 1)
                {
                    htpMenuButtons[selectedButton].image.color = clearColor;
                    selectedButton = 0;
                }
                else
                {
                    htpMenuButtons[selectedButton].image.color = clearColor;
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
            if (Input.GetButtonDown ("P1A Button") || Input.GetButtonDown ("P2A Button") || Input.GetKeyDown (KeyCode.Return))
            {
                htpMenuButtons[selectedButton].onClick.Invoke ();
            }

            htpMenuButtons[selectedButton].image.color = Color.blue;
        }
        else if (aboutMenuActive)
        {

            //Player Pressed A
            if (Input.GetButtonDown ("P1A Button") || Input.GetButtonDown ("P2A Button") || Input.GetKeyDown (KeyCode.Return))
            {
                aboutMenuButtons[0].onClick.Invoke ();
            }

            aboutMenuButtons[0].image.color = Color.blue;
        }
    }

    public void MainMenuStartGame ()
    {

        SceneManager.LoadScene ("Main Game Scene", LoadSceneMode.Additive);
        SceneManager.LoadScene ("Main UI Scene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync ("MenuLighting");
        SceneManager.LoadScene ("Lighting", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync ("MainMenuScene");
        //SceneManager.LoadScene ("Audio", LoadSceneMode.Additive);
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

    public void HTPSelected ()
    {
        menuNextSelectionSound.Play ();
        MainMenuGO.SetActive (false);
        htpMenu.SetActive (true);
        selectedButton = 0;
        mainMenuActive = false;
        htpMenuActive = true;
        keyboardMenu.SetActive (false);
        basicsMenu.SetActive (false);

    }

    public void AboutSelected ()
    {
        menuNextSelectionSound.Play ();
        MainMenuGO.SetActive (false);
        aboutMenu.SetActive (true);
        selectedButton = 0;
        mainMenuActive = false;
        aboutMenuActive = true;
    }

    public void KeyboardControlsSelected ()
    {
        keyboardMenu.SetActive (true);
        xboxMenu.SetActive (false);
        basicsMenu.SetActive (false);

    }
    public void XboxControlsSelected ()
    {
        keyboardMenu.SetActive (false);
        xboxMenu.SetActive (true);
        basicsMenu.SetActive (false);

    }
    public void BasicsSelected ()
    {
        keyboardMenu.SetActive (false);
        xboxMenu.SetActive (false);
        basicsMenu.SetActive (true);
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
        htpMenu.SetActive (false);
        aboutMenu.SetActive (false);
        MainMenuGO.SetActive (true);

        selectedButton = 0;

        mainMenuActive = true;
        scoresMenuActive = false;
        optionsMenuActive = false;
        htpMenuActive = false;
        aboutMenuActive = false;
    }

    public void QuitGame ()
    {
        // Tell GM to Quit Game
        Application.Quit ();
        Debug.Log ("Game Quit");
    }

    public void ClearButtonColors ()
    {
        foreach (Button tempButton in mainMenuButtons)
        {
            tempButton.image.color = clearColor;
        }

        foreach (Button tempButton in optionsMenuButtons)
        {
            tempButton.image.color = clearColor;
        }

        foreach (Button tempButton in scoresMenuButtons)
        {
            tempButton.image.color = clearColor;
        }
    }

}