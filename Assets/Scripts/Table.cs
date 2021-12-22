using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private List<Hole> _holes;

    [SerializeField] private List<GameObject> _balls;
    [SerializeField] private GameObject _startPack;

    private Vector2 _packStartPosition = new Vector2(1.0f, 0.0f);

    private void Start()
    {
        foreach (var hole in _holes)
            hole.HoleEvent += BallKnocked;
    }

    private void StartGame()
    {

    }

    private void BallKnocked(GameObject ball)
    {
        _balls.Remove(ball);
        Destroy(ball);
    }

}
