using Assets.Scripts.Common;
using UnityEngine;
using UnityEngine.Assertions;
using CharacterController = Assets.Scripts.Character.CharacterController;
using MinionController = Assets.Scripts.Minion.MinionController;

public static class Extensions
{
    public static Vector3 xy(this Vector3 v)
    {
        v.z = 0;
        return v;
    }
    public static Vector3 xz(this Vector3 v)
    {
        v.y = 0;
        return v;
    }
    public static Vector3 yz(this Vector3 v)
    {
        v.x = 0;
        return v;
    }
    public static bool IsAttack(this Collider collider, out IAttack attack)
    {
        if (collider.gameObject.tag == "CharacterAttackCollider")
        {
            var character = collider.transform.root.GetComponent<CharacterController>();
            Assert.IsFalse(character == null);
            attack = character.Fsm.Current as IAttack;
        }
        else if (collider.gameObject.tag == "MinionAttackCollider")
        {
            var minion = collider.transform.root.GetComponent<MinionController>();
            Assert.IsFalse(minion == null);
            attack = minion.Fsm.Current as IAttack;
        }
        else
        {
            attack = null;
        }
        return (attack != null);
    }

}
