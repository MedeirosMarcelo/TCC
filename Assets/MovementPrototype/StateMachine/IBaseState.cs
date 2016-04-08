public interface IBaseState
{
    BaseFsm Fsm { get; }
    string Name { get; }

    void Enter(StateTransitionArgs args);
    void Exit(StateTransitionArgs args);
    void PreUpdate();
    void Update();
}