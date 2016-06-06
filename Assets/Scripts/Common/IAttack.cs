using UnityEngine;

namespace Assets.Scripts.Common
{
    public enum AttackDirection
    {
        Horizontal,
        Vertical
    }
    public interface IAttack
    {
        bool IsHeavy { get; }
        AttackDirection Direction { get; }
        int Damage { get; }
        bool GetCollisionPoint(out RaycastHit hitInfo);
        void Blocked();
    }
}
