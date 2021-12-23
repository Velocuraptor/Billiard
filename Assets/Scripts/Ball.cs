using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    public bool IsStop => _rigidbody2D.velocity.x < 1.0f || _rigidbody2D.velocity.y < 1.0f ||;
}