using UnityEngine;

public class Ball : MonoBehaviour, IBall
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    public Rigidbody2D RigidBody2D => _rigidbody2D;
    public bool IsStop => Mathf.Abs(_rigidbody2D.velocity.x) <= 0.2f && Mathf.Abs(_rigidbody2D.velocity.y) <= 0.2f;

    public bool IsWhiteBall => false;

    public void Destroy() =>
        Destroy(gameObject);

}