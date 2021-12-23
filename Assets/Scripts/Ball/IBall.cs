using UnityEngine;

public interface IBall
{
    Rigidbody2D RigidBody2D { get; }
    bool IsStop { get; }
    bool IsWhiteBall { get; }

    void Destroy();
}