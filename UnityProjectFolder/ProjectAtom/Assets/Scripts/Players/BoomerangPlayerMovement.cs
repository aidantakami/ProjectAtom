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
    [SerializeField] public UnityEvent boomerangThrown = new UnityEvent();
    [SerializeField] public UnityEvent boomerangCaught = new UnityEvent();

    //Referencing Components
    private Rigidbody rb;
    private MeshRenderer boomMesh;

    //Auxilary Fields
    public float speedThrownForward = 1f;


    //State machine and states
    #region
    public StateMachine _StateMachine;
    protected ISBoomAway BoomAwayState;
    protected ISBoomThrown BoomThrownState;
    protected ISStandby BoomStandbyState;

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
    }

    //Functions
#region

       
    //When boomerang is thrown
    public void BoomerangThrownAction()
    {
        _StateMachine.EnterState(BoomThrownState);

        StartCoroutine(ThrownForwardAction());

        boomerangThrown.Invoke();
    }

    public IEnumerator ThrownForwardAction()
    {
        //Thrown Forward Process
        float elapsedTime = 0f;

        while (elapsedTime < speedThrownForward)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed * speedThrownForward);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return null;
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

#endregion
}
