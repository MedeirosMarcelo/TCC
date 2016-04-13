using Assets.MovementPrototype.Character.States;
using UnityEngine;

public abstract class CState : BaseState
{
    public CController Character { get; protected set; }
    public BaseInput Input { get; protected set; }
    public Rigidbody Rigidbody { get; protected set; }
    public Transform Transform { get; protected set; }

    public CState(BaseFsm fsm, CController character)
    {
        Fsm = fsm;
        Character = character;
        Input = Character.input;
        Rigidbody = Character.rbody;
        Transform = Character.transform;
    }

    public override void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "Sword")
        {
            var otherCharacter = collider.transform.parent.GetComponent<CController>();
            if (!ReferenceEquals(Character, otherCharacter))
            {
                var attackFsm = otherCharacter.fsm.Current as AttackFsm;
                if (attackFsm != null && attackFsm.Current.Name == "SWING")
                {
                    Character.bloodAnimator.SetTrigger("Bleed");
                    Character.ReceiveDamage(1);
                }
            }
        }
        // otherwise defer to base
        base.OnTriggerEnter(collider);
    }
}

