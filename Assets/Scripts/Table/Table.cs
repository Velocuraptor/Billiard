using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, ICheckerMove
{
    [SerializeField] private List<Hole> _holes;
    [SerializeField] private TrajectoryRenderer _trajectoryRenderer;
    [SerializeField] private StartPack _startPackPrefab;
    [SerializeField] private WhiteBall _whiteBall;
    private List<IBall> _balls;
    private StartPack _startPack;
    private Vector2 _packStartPosition = new Vector2(1.0f, 0.0f);
    private Vector2 _ballStartPosition;

    private void Start()
    {
        _ballStartPosition = _whiteBall.transform.position;

        foreach (var hole in _holes)
            hole.HoleEvent += BallKnocked;

        StartGame();
    }

    private void StartGame()
    {
        _whiteBall.transform.position = _ballStartPosition;
        InitializeBalls();
        _trajectoryRenderer.Initialize(_balls);
    }

    private void InitializeBalls()
    {
        _balls = new List<IBall>();
        _startPack = Instantiate(_startPackPrefab, _packStartPosition, Quaternion.identity);
        _balls.Add(_whiteBall);
        _balls.AddRange(_startPack.Balls);
    }

    private void BallKnocked(IBall ball)
    {
        if (PlayerActions.IsAction)
            return;

        if(ball.IsWhiteBall)
        {
            ResetAll();
            StartGame();
            return;
        }

        _trajectoryRenderer.DeleteSavedBody(ball);
        _balls.Remove(ball);
        ball.Destroy();
        if (_balls.Count == 1)
        {
            ResetAll();
            StartGame();
        }
    }

    private void ResetAll()
    {
        _trajectoryRenderer.DeleteTrajectory();
        _whiteBall.ResetData();

        _balls.Remove(_whiteBall);
        foreach (var ball in _balls)
            ball.Destroy();

        Destroy(_startPack.gameObject);
    }

    public bool IsMoving()
    {
        foreach (var ball in _balls)
        {
            if (!ball.IsStop)
                return true;
        }
        return false;
    }
}
