using UnityEngine;

public interface IHumanoidController
{
    Animator Animator { get; }
    Vector3 Destination { get; set; }
    MeshRenderer Mesh { get; }
    CapsuleCollider SwordCollider { get; }

    void Forward(Vector3 forward);
    void Move(Vector3 position);
    void ReceiveDamage(int damage);
}