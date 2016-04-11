﻿public interface IState
{
    string Name { get; }
    string Debug { get; }
    BaseFsm Fsm { get; }

    // Evaluate Input and changes the FSM Current State
    // This base method should be used as a generic handler
    void PreUpdate();
    void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args);
    void FixedUpdate();
    void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args);
}