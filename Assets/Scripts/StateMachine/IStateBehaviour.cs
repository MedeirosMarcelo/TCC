using UnityEngine;

public interface IStateBehaviour
{
    string DebugString { get; }
    string Name { get; }
    IState State { get; }

    void PreUpdate();
    void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args);
    void FixedUpdate();
    void Exit(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args);
    void OnCollisionEnter(Collision collision);
    void OnTriggerEnter(Collider colllider);
}