using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoomerangPlayerMovement : MonoBehaviour
{
    //Serialized fields
    [SerializeField] public FloatVariable playerSpeed;
    [SerializeField] public Vector3Variable boomLocation;
    [SerializeField] public Vector3Variable dogLocation;
    [SerializeField] public BoolVariable playerCanMove;
    [SerializeField] public IntVariable boomerangRangeWarning;
    [SerializeField] public IntVariable boomAbilityTokens;
    [SerializeField] public StringVariable selectedBoomAbility;
    [SerializeField] public float maximumDistanceFromDog;
    [SerializeField] public UnityEvent boomerangThrown = new UnityEvent ();
    [SerializeField] public UnityEvent boomerangCaught = new UnityEvent ();
    [SerializeField] public UnityEvent gustOfWind = new UnityEvent ();
    [SerializeField] public ParticleSystem gustOfWindPS;

    //Prefabs
    [SerializeField] public GameObject springboardPrefab;

    [SerializeField] public GameObject abilityTokenPrefab;
    [SerializeField] private int tokensConverted;

    [SerializeField] public GameObject boomerangIcon;
    private Vector3 iconPositionStorage;

    //Referencing Components
    private Rigidbody rb;
    private MeshRenderer boomMesh;

    private List<GameObject> leftBehindTokens = new List<GameObject> ();

    //Auxilary Fields
    //Will determine how quickly boomerang will finish lerp forwards
    //Please be careful tweaking
    public float speedThrownForward = 2f;
    private bool isBeingThrown = false;
    private int abilityIterator;
    private int boomerangRangeTemp = 0;

    //State machine and states
    #region 
    public StateMachine _StateMachine;
    protected ISBoomAway BoomAwayState;
    protected ISBoomThrown BoomThrownState;
    protected ISStandby BoomStandbyState;
    protected ISBoomDead BoomDeadState;

    public void Awake ()
    {
        _StateMachine = new StateMachine ();
        StateConstructor ();
    }

    private void StateConstructor ()
    {
        BoomAwayState = new ISBoomAway (boomLocation, dogLocation, this);
        BoomThrownState = new ISBoomThrown (boomLocation, dogLocation, playerCanMove, playerSpeed, this);
        BoomStandbyState = new ISStandby (boomLocation, gameObject.transform);
        BoomDeadState = new ISBoomDead (boomLocation, dogLocation, this);

    }
    #endregion

    //Game Manager Responses
    #region 
    //Called when game is paused
    public void BoomGamePause ()
    {
        _StateMachine.EnterState (BoomStandbyState);
    }

    //Called when game is unpaused
    public void BoomGameUnpause ()
    {
        _StateMachine.ReturnToPreviousState ();
    }

    public void BoomGameRestart ()
    {
        _StateMachine.EnterState (BoomAwayState);
        boomerangIcon.gameObject.SetActive (false);
        boomerangRangeTemp = 0;
        boomerangRangeWarning.value = 3;
        springboardPrefab.transform.position = new Vector3 (-10, -10, -10);
        isBeingThrown = false;

        foreach (GameObject item in leftBehindTokens)
        {
            item.gameObject.SetActive (false);
        }
    }

    public void BoomGameEnd ()
    {
        boomerangIcon.gameObject.SetActive (false);
        boomerangRangeWarning.value = 0;
        _StateMachine.EnterState (BoomStandbyState);
    }

    public void BoomerangMagnetResponse ()
    {
        StartCoroutine (BoomerangMagnetCoroutine ());
        boomerangRangeWarning.value = 3;
    }

    private IEnumerator BoomerangMagnetCoroutine ()
    {
        if (_StateMachine.currentState == BoomThrownState)
        {
            _StateMachine.EnterState (BoomAwayState);
            boomerangIcon.gameObject.SetActive (false);
            yield return new WaitForSeconds (1);
            boomerangCaught.Invoke ();
        }
    }

    public void BoomerangReviveResponse ()
    {
        _StateMachine.EnterState (BoomAwayState);
        boomerangIcon.gameObject.SetActive (false);
        boomerangRangeWarning.value = 3;
    }

    #endregion

    // Update is called once per frame
    private void Update ()
    {
        //Ticks state machine
        _StateMachine.Tick ();

        //Moves the Icon to follow Boomer
        iconPositionStorage.Set (transform.position.x, transform.position.y + 1, transform.position.z);
        boomerangIcon.transform.position = iconPositionStorage;

        //Rotates icon over boomer
        boomerangIcon.transform.Rotate (1f, 0.0f, 0.0f, Space.Self);
    }

    // Start is called before the first frame update
    void Start ()
    {

        //Auto enters standby
        _StateMachine.EnterState (BoomStandbyState);
        rb = gameObject.GetComponent<Rigidbody> ();
        boomMesh = gameObject.GetComponent<MeshRenderer> ();

        springboardPrefab = Instantiate (springboardPrefab);
        springboardPrefab.transform.position = new Vector3 (-10, -10, -10);

        boomerangIcon.gameObject.SetActive (false);
        iconPositionStorage = new Vector3 (0f, 0f, 0f);

        //Initial set of the selected ability
        abilityIterator = 0;
        selectedBoomAbility.SetValue ("Springboard, \nCost 2");

        for (int rep = 0; rep < tokensConverted; rep++)
        {
            var tempGOVar = Instantiate (abilityTokenPrefab);
            leftBehindTokens.Add (tempGOVar);
        }
    }

    //Functions
    #region

    //When boomerang is thrown
    public void BoomerangThrownAction (Vector3 aimLocation)
    {
        _StateMachine.EnterState (BoomThrownState);

        StartCoroutine (ThrownForwardAction (aimLocation));

        boomerangThrown.Invoke ();
    }

    public IEnumerator ThrownForwardAction (Vector3 aimLocation)
    {

        isBeingThrown = true;

        //Set box collider to enabled
        gameObject.GetComponent<BoxCollider> ().enabled = false;

        //Thrown Forward Process
        float elapsedTime = 0f;

        //Will move player while in range towards aim location
        while (elapsedTime < speedThrownForward && BoomIsInRange (-2))
        {
            transform.position = Vector3.Lerp (transform.position, new Vector3 (aimLocation.x, transform.position.y, transform.position.z + 1), elapsedTime / speedThrownForward);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame ();

            if ((Vector3.Distance (transform.position, dogLocation.value) > 1.5) && gameObject.GetComponent<BoxCollider> ().enabled == false)
            {
                gameObject.GetComponent<BoxCollider> ().enabled = true;
            }
        }

        isBeingThrown = false;

    }

    public bool TryToGetCaught ()
    {
        if (Input.GetButton ("P2A Button") || Input.GetKey (KeyCode.RightShift) && _StateMachine.currentState != BoomDeadState)
        {
            //Boomerang was caught
            boomerangCaught.Invoke ();
            _StateMachine.EnterState (BoomAwayState);
            boomerangRangeWarning.value = 3;
            return true;

        }
        else return false;
    }

    public bool BoomIsInRange ()
    {
        if (Vector3.Distance (dogLocation.value, boomLocation.value) < maximumDistanceFromDog)
        {

            if (Vector3.Distance (dogLocation.value, boomLocation.value) > maximumDistanceFromDog - 8f)
            {
                if (boomerangRangeTemp == 1)
                {
                    boomerangIcon.gameObject.SetActive (true);
                    boomerangRangeWarning.value = 1;
                    boomerangRangeTemp = 0;
                }
            }
            else if (boomerangRangeTemp == 0)
            {
                boomerangIcon.gameObject.SetActive (false);
                boomerangRangeWarning.value = 0;
                boomerangRangeTemp = 1;
            }
            return true;
        }
        else
        {
            if (boomerangRangeTemp == 1)
            {
                boomerangIcon.gameObject.SetActive (true);
                boomerangRangeTemp = 0;
            }

            boomerangRangeWarning.value = 2;
            return false;
        }
    }

    //Will allow modifier to allowed distance before returns false
    //Good for warning before actually out of range
    public bool BoomIsInRange (float modifier)
    {
        if (Vector3.Distance (dogLocation.value, boomLocation.value) < maximumDistanceFromDog + modifier)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Called when boomerang timer runs out
    public void BoomerangThrowTimeOut ()
    {
        boomerangIcon.gameObject.SetActive (false);
        boomerangRangeWarning.value = 0;
        _StateMachine.EnterState (BoomDeadState);
    }

    //TAYGH
    //This is where you will put in the calls to your abilites.
    public void UseSelectedBoomAbility ()
    {
        if (abilityIterator == 0 && boomAbilityTokens.value >= 2 && _StateMachine.currentState == BoomThrownState)
        {
            UseSpringboard ();
            boomAbilityTokens.value -= 2;
        }
        else if (abilityIterator == 1 && boomAbilityTokens.value >= 4 && _StateMachine.currentState == BoomThrownState)
        {
            UseGust ();
            boomAbilityTokens.value -= 5;
        }
        else if (abilityIterator == 2 && boomAbilityTokens.value >= 2)
        {

            StartCoroutine (TokenCarePackage ());
            boomAbilityTokens.value -= 3;
        }

    }

    public void SwitchBoomAbility ()
    {
        //If ability iterator is alreay at 2
        if (abilityIterator == 2)
        {
            //Set to 0
            abilityIterator = 0;
            selectedBoomAbility.SetValue ("Springboard, \nCost 2");
        }

        //Iterate 
        else abilityIterator++;

        //If after iteration is 1
        if (abilityIterator == 1)
        {
            selectedBoomAbility.SetValue ("Gust of Wind, \nCost 4");
        }
        //If after iteration is 2
        else if (abilityIterator == 2)
        {
            selectedBoomAbility.SetValue ("Extra Tokens, \nCost 2");
        }
    }

    //Plaes the springboard behind the player
    public void UseSpringboard ()
    {
        springboardPrefab.gameObject.SetActive (true);
        Vector3 temp = transform.position;
        springboardPrefab.transform.position = temp -= new Vector3 (0, 0.8f, 1f);
    }

    public void UseGust ()
    {
        gustOfWindPS.transform.position = transform.position;
        gustOfWindPS.transform.position += new Vector3 (0, 0, 2);
        gustOfWindPS.Play ();
        gustOfWind.Invoke ();
    }

    public IEnumerator TokenCarePackage ()
    {

        for (int rep = 0; rep < tokensConverted; rep++)
        {
            if (_StateMachine.currentState == BoomThrownState)
            {
                Vector3 tempPos = new Vector3 (transform.position.x, transform.position.y + 1f, transform.position.z - 4f);
                leftBehindTokens[rep].gameObject.SetActive (true);
                leftBehindTokens[rep].transform.position = tempPos;
                yield return new WaitForSeconds (0.1f);
            }

        }
    }

    #endregion
}