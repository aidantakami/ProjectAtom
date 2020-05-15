using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class BoomerangPlayerMovement : MonoBehaviour
{
    //Serialized fields
    [SerializeField] public FloatReference playerSpeed;
    [SerializeField] public Vector3Variable boomLocation;
    [SerializeField] public Vector3Variable dogLocation;
    [SerializeField] public float maximumDistanceFromDog;
    [SerializeField] public UnityEvent boomerangThrown = new UnityEvent();
    [SerializeField] public UnityEvent boomerangCaught = new UnityEvent();


    //Events
    [SerializeField] public UnityEvent springboardPlacedEvent = new UnityEvent();

    //Prefabs
    [SerializeField] public GameObject springboardPrefab;

    //Referencing Components
    private Rigidbody rb;
    private MeshRenderer boomMesh;

    //Auxilary Fields
    //Will determine how quickly boomerang will finish lerp forwards
    //Please be careful tweaking
    public float speedThrownForward = 2f;
    private bool springboardIsOut = false;


    //State machine and states
    #region
    public StateMachine _StateMachine;
    protected ISBoomAway BoomAwayState;
    protected ISBoomThrown BoomThrownState;
    protected ISStandby BoomStandbyState;
    protected ISBoomDead BoomDeadState;

    public void Awake()
    {
        _StateMachine = new StateMachine();
        StateConstructor();
    }

    private void StateConstructor()
    {
        BoomAwayState = new ISBoomAway(boomLocation, dogLocation, this);
        BoomThrownState = new ISBoomThrown(boomLocation, dogLocation, playerSpeed, this);
        BoomStandbyState = new ISStandby(boomLocation, gameObject.transform);
        BoomDeadState = new ISBoomDead(boomLocation, dogLocation, this);

    }
#endregion


    //Game Manager Responses
#region
    //Called when game is paused
    public void BoomGamePause()
    {
        _StateMachine.EnterState(BoomStandbyState);
    }

    //Called when game is unpaused
    public void BoomGameUnpause()
    {
        _StateMachine.ReturnToPreviousState();
    }

    public void BoomGameStart()
    {
        _StateMachine.EnterState(BoomAwayState);
        springboardPrefab.transform.position = new Vector3(-10, -10, -10);

    }

    public void BoomGameRestart()
    {
        _StateMachine.EnterState(BoomAwayState);
    }

    public void BoomGameEnd()
    {
        _StateMachine.EnterState(BoomStandbyState);
    }

#endregion

    // Update is called once per frame
    private void Update() => _StateMachine.Tick();
    
    
    // Start is called before the first frame update
    void Start()
    {
        //Auto enters standby
        _StateMachine.EnterState(BoomStandbyState);
        rb = gameObject.GetComponent<Rigidbody>();
        boomMesh = gameObject.GetComponent<MeshRenderer>();

        springboardPrefab = Instantiate(springboardPrefab);
        springboardPrefab.transform.position = new Vector3(-10, -10, -10);
    }

    //Functions
#region

       
    //When boomerang is thrown
    public void BoomerangThrownAction(Vector3 aimLocation)
    {
        _StateMachine.EnterState(BoomThrownState);

        StartCoroutine(ThrownForwardAction(aimLocation));

        boomerangThrown.Invoke();
    }

    public IEnumerator ThrownForwardAction(Vector3 aimLocation)
    {

        Vector3 tempOriginalPos = transform.position;

        //Thrown Forward Process
        float elapsedTime = 0f;

        //Will move player while in range towards aim location
        while (elapsedTime < speedThrownForward && BoomIsInRange(-2))
        { 
            transform.position = Vector3.Lerp(transform.position, new Vector3(aimLocation.x, transform.position.y, transform.position.z + 1), elapsedTime / speedThrownForward);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //Set box collider to enabled
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public bool TryToGetCaught()
    {
        if (Input.GetButton("P2A Button") || Input.GetKey(KeyCode.RightShift))
        {
            //Boomerang was caught
            boomerangCaught.Invoke();
            _StateMachine.EnterState(BoomAwayState);
            return true;

        }
        else return false;
    }

    public bool BoomIsInRange()
    {
        if (Vector3.Distance(dogLocation.value, boomLocation.value) < maximumDistanceFromDog) return true;
        else return false;
    }

    //Will allow modifier to allowed distance before returns false
    //Good for warning before actually out of range
    public bool BoomIsInRange(float modifier)
    {
        if (Vector3.Distance(dogLocation.value, boomLocation.value) < maximumDistanceFromDog + modifier) return true;
        else return false;
    }

    //Called when boomerang timer runs out
    public void BoomerangThrowTimeOut()
    {
        _StateMachine.EnterState(BoomDeadState);
    }

    public void PlaceSpringboard()
    {

        if (!springboardIsOut)
        {
            springboardPlacedEvent.Invoke();
            springboardPrefab.gameObject.SetActive(true);
            springboardPrefab.transform.position = transform.position -= new Vector3(0, 0.5f, 1);
            springboardIsOut = true;
        }
    }

    public void SpringboardExpired()
    {
        springboardIsOut = false;
    }

    #endregion
}
