using UnityEngine;

public class Hole : MonoBehaviour
{
    public delegate void OnHole(IBall gameObject);
    public event OnHole HoleEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var ball = collision.GetComponent<IBall>();
        if (ball == null)
            return;

        HoleEvent?.Invoke(ball);
    }
}
