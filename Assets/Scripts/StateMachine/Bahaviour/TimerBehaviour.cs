using UnityEngine;

public class TimerBehaviour : BaseBehaviour
{
    public delegate void Callback();
    public float Elapsed { get; protected set; }
    public float TotalTime { get; set; }
    public Callback OnFinish { get; set; }

    public TimerBehaviour(IState state) : base(state)
    {
    }
    public override void PreUpdate()
    {
        base.PreUpdate();
        if (Elapsed >= TotalTime && OnFinish != null)
        {
            OnFinish();
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Elapsed += Time.fixedDeltaTime;
    }
    public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0f, params object[] args)
    {
        base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        Elapsed = additionalDeltaTime;
    }
}