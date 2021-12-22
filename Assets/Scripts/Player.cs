using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerActions _playerActions;
    [SerializeField] private WhiteBall _whiteBall;

    private void Start()
    {
        _playerActions.TapEvent += CheckDirection;
        _playerActions.DropEvent += HitBall;
    }

    private void CheckDirection(Vector2 tapPoint)
    {
        _whiteBall.SetParameters(tapPoint);
    }

    private void HitBall()
    {
        _whiteBall.Hit();
    }
}