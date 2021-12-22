using UnityEngine;

public class WhiteBallSimulated : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;

    public Collision2D Collision { get; private set; }

    private void OnCollisionEnter2D(Collision2D collision) =>
        Collision = collision;
}
