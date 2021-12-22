using UnityEngine;

public class Hole : MonoBehaviour
{
    public delegate void OnHole(GameObject gameObject);
    public event OnHole HoleEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Ball")
            return;

        HoleEvent?.Invoke(collision.gameObject);
    }
}
