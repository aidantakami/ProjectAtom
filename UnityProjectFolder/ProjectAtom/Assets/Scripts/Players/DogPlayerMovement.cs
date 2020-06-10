using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DogPlayerMovement : MonoBehaviour
{

    //Serialized fields
    [SerializeField] public FloatVariable playerSpeed;
    [SerializeField] public Vector3Variable dogLocation;
    [SerializeField] public Vector3Variable boomLocation;
    [SerializeField] public BoolVariable playerCanMove;
    [SerializeField] public StringVariable selectedDogAbility;
    [SerializeField] public IntVariable dogAbilityTokens;
    [SerializeField] public BoomerangPlayerMovement boomPlayer;
    [SerializeField] public Transform leftLimit;
    [SerializeField] public Transform rightLimit;
    [SerializeField] public GameObject aimArrow;
    [SerializeField] public GameObject aimPoint;

    [SerializeField] public UnityEvent BoomerangMagnetEvent = new UnityEvent ();
    [SerializeField] public UnityEvent BoomerangReviveEvent = new UnityEvent ();
    [SerializeField] public UnityEvent DogSpinAttackEvent = new UnityEvent ();

    //private fields
    private Vector3 startingPosition;
    private bool dogIsRestarting;
    private bool boomIsDead;
    private int abilityIterator;

    //Referencing Components
    private Rigidbody rb;

    //State Machine & Awake
    #region
    //State machine and states
    private StateMachine _StateMachine;
    private ISDogRunning DogRunningState;
    private ISDogNoBoom DogNoBoomState;
    private ISStandby DogStandbyState;
    private ISDogBoomDead DogBoomDeadState;

    public void Awake ()
    {
        _StateMachine = new StateMachine ();
        StateConstructor ();

        //Used to restart player
        startingPosition = transform.position;
    }

    private void StateConstructor ()
    {
        DogRunningState = new ISDogRunning (dogLocation, playerSpeed, playerCanMove, this);
        DogNoBoomState = new ISDogNoBoom (dogLocation, boomLocation, playerCanMove, playerSpeed, this);
        DogStandbyState = new ISStandby (dogLocation, gameObject.transform);
        DogBoomDeadState = new ISDogBoomDead (dogLocation, playerSpeed, playerCanMove, this);
    }
    #endregion

    //Game Manager Responses
    #region
    //Called when game is paused
    public void DogGamePause ()
    {
        _StateMachine.EnterState (DogStandbyState);
    }

    //Called when game is unpaused
    public void DogGameUnpause ()
    {
        _StateMachine.ReturnToPreviousState ();
    }

    //Called when the dog begins to run, after both players signed in

    public void DogGameRestart ()
    {
        dogIsRestarting = true;
        transform.position = startingPosition;
        aimArrow.SetActive (true);
        aimPoint.SetActive (true);
        boomIsDead = false;
        _StateMachine.EnterState (DogRunningState);
        dogIsRestarting = false;
    }

    public void DogGameEnd ()
    {
        _StateMachine.EnterState (DogStandbyState);
    }
    #endregion

    // Update is called once per frame
    private void Update () => _StateMachine.Tick ();

    // Start is called before the first frame update
    void Start ()
    {
        _StateMachine.EnterState (DogStandbyState);
        rb = gameObject.GetComponent<Rigidbody> ();
        aimArrow.SetActive (true);
        aimPoint.SetActive (true);
        dogIsRestarting = false;
        boomIsDead = false;

        //Abilities
        abilityIterator = 0;
        selectedDogAbility.SetValue ("Boom Magnet");
    }

    //Functions
    #region

    //When boomerang is thrown
    public void BoomerangThrown (Vector3 aimLocation)
    {
        //Tell boomerang
        boomPlayer.BoomerangThrownAction (aimLocation);

        //Change states
        _StateMachine.EnterState (DogNoBoomState);
        aimArrow.SetActive (false);
        aimPoint.SetActive (false);

    }

    public void TryToCatchBoomerang ()
    {
        if (boomPlayer.TryToGetCaught ())
        {
            _StateMachine.EnterState (DogRunningState);
            aimArrow.SetActive (true);
            aimPoint.SetActive (true);

        }
    }

    //Used to access the limits of the aim set in editor
    public Vector3 GetLeftAimLimit ()
    {
        return leftLimit.position;
    }

    public Vector3 GetRightAimLimit ()
    {
        return rightLimit.position;
    }

    public GameObject GetAimArrow ()
    {
        return aimArrow;
    }

    public GameObject GetAimPoint ()
    {
        return aimPoint;
    }

    //Uses the currently selected ability for the dog
    public void UseSelectedDogAbility ()
    {
        if (abilityIterator == 0 && dogAbilityTokens.value >= 5 && _StateMachine.currentState == DogNoBoomState)
        {
            dogAbilityTokens.value -= 5;
            StartCoroutine (UseBoomMagnet ());

        }
        else if (abilityIterator == 1 && dogAbilityTokens.value >= 4)
        {
            dogAbilityTokens.value -= 4;
            UseSpinAttack ();
        }
        else if (abilityIterator == 2 && dogAbilityTokens.value >= 8 && boomIsDead)
        {
            dogAbilityTokens.value -= 8;
            ReviveBoom ();
        }
    }

    public void SwitchDogAbility ()
    {
        //If ability iterator is alreay at 2
        if (abilityIterator == 2)
        {
            //Set to 0
            abilityIterator = 0;
            selectedDogAbility.SetValue ("Boom Magnet");
        }

        //Iterate 
        else abilityIterator++;

        //If after iteration is 1
        if (abilityIterator == 1)
        {
            selectedDogAbility.SetValue ("Spin Attack");
        }
        //If after iteration is 2
        else if (abilityIterator == 2)
        {
            selectedDogAbility.SetValue ("Boom Revive");
        }
    }

    //Abilities
    #region

    private IEnumerator UseBoomMagnet ()
    {
        BoomerangMagnetEvent.Invoke ();
        yield return new WaitForSeconds (1);
        _StateMachine.EnterState (DogRunningState);
        aimArrow.SetActive (true);
        aimPoint.SetActive (true);

    }

    private void UseSpinAttack () => DogSpinAttackEvent.Invoke ();

    private void ReviveBoom ()
    {
        BoomerangReviveEvent.Invoke ();
        _StateMachine.EnterState (DogRunningState);
        aimArrow.SetActive (true);
        aimPoint.SetActive (true);
        boomIsDead = false;
    }

    #endregion

    //Boomerang Dies Response
    public void BoomerangDeadDogState ()
    {
        if (!dogIsRestarting && !boomIsDead && _StateMachine.currentState != DogStandbyState)
        {

            _StateMachine.EnterState (DogBoomDeadState);
            boomIsDead = true;

        }
    }

    #endregion

}