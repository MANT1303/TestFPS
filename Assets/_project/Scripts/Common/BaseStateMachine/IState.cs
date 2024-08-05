public interface IState
{
    protected StateMachine StateMachine { get; set; }
    public void Enter();
    public void Exit();
    public void Update();
}