using UnityEngine;

public class CFsm : BaseFsm {
    public CController Character { get; protected set; }
    public CFsm(CController character) : base("Assets.MovementPrototype.Character.States") {
        Character = character;
        StateLoader<CFsm, CState> loader = new StateLoader<CFsm, CState>();
        loader.LoadStates(this, @namespace);
        current = dict["IDLE"];
    }

    public override void Update()
    {
        current.PreUpdate();
        current.Update();
    }
}