
//Interface for states
public interface IState
{
    void OnStateEnter();

    void OnStateExit();

    void OnStateTick();
}
