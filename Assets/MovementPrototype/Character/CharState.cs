using UnityEngine;

public abstract class CharState : BaseState
{
    public CharController Character { get; protected set; }
    public BaseInput Input { get; protected set; }
    public Rigidbody Rigidbody { get; protected set; }
    public Transform Transform { get; protected set; }

    public CharState(CharFsm fsm)
    {
        Fsm = fsm;
        Character = fsm.Character;
        Input = Character.input;
        Rigidbody = Character.rbody;
        Transform = Character.transform;
    }

    public override void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "Sword")
        {
            var otherCharacter = collider.transform.parent.GetComponent<CharController>();
            if (!ReferenceEquals(Character, otherCharacter))
            {
                var attackerState = otherCharacter.fsm.Current as AttackSwing;
                if (attackerState != null)
                {
                    Character.bloodAnimator.SetTrigger("Bleed");
                    Character.ReceiveDamage(attackerState.Damage);
                }
            }
        }
        // otherwise defer to base
        base.OnTriggerEnter(collider);
    }
}

