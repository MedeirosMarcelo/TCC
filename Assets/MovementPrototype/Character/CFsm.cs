using UnityEngine;

public class CFsm : BaseFsm {
    public CController Character { get; protected set; }
    public CFsm(CController character) : base("Assets.MovementPrototype.Character.States", typeof(CState)) {
        Character = character;
        current = dict["IDLE"];
    }

    public override void Update()
    {
        current.PreUpdate();
        current.Update();
    }
}