using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private List<Hole> _holes;
    [SerializeField] private TrajectoryRenderer _trajectoryRenderer;
    [SerializeField] private List<GameObject> _balls;
    [SerializeField] private GameObject _startPack;

    private Vector2 _packStartPosition = new Vector2(1.0f, 0.0f);

    private void Start()
    {
        foreach (var hole in _holes)
            hole.HoleEvent += BallKnocked;


        StartGame();
    }

    private void StartGame()
    {
        _trajectoryRenderer.Initialize(_balls);
    }

    private void BallKnocked(GameObject ball)
    {
        if (PlayerActions.IsAction)
            return;

        _trajectoryRenderer.DeleteSavedBody(ball);
        _balls.Remove(ball);
        Destroy(ball);
    }

}
