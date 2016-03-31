using UnityEngine;

public class PlayerFsm : CFsm {
    public PlayerFsm(CController character) : base(character, "Assets.MovementPrototype.Character.States") {
        Name = "FSM";
        current = dict["IDLE"];
    }

    protected override void StateTransitionRequestedListener(StateTransitionEventArgs obj)
    {
        Debug.Log("ChangeState: from " + current + " to " + obj.RequestedStateName);

        // Notice there is no check if is a different state, this is by design
        next = dict[obj.RequestedStateName];
        last = current;
        last.Exit();

        current = next;
        next = null;
        current.Enter(obj);

        if (obj.RunNextState)
        {
            current.Update();
        }
    }

    public override void Update()
    {
        current.PreUpdate();
        current.Update();
    }
}