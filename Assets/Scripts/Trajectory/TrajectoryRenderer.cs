using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    public const int SimulatedSteps = 50;
    public const float LengthAfterCollided = 0.1f;

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LineRenderer _lineRendererPlus;
    [SerializeField] private WhiteBallSimulated _whiteBallSimulated;

    private Dictionary<Rigidbody2D, BodyData> _savedBodies;

    public void Initialize(List<IBall> balls)
    {
        _savedBodies = new Dictionary<Rigidbody2D, BodyData>();
        foreach (var ball in balls)
            _savedBodies.Add(ball.RigidBody2D, new BodyData());
    }

    public void DeleteSavedBody(IBall ball) =>
        _savedBodies.Remove(ball.RigidBody2D);

    public void ShowTrajectory(Transform origin, Vector2 impactForce)//TODO::Refactor
    {
        DeleteTrajectory();
        SaveBodiesData();
        Physics2D.autoSimulation = false;
        FindPoints(origin, impactForce);
        Physics2D.autoSimulation = true;
        LoadBodiesData();
    }

    private void FindPoints(Transform origin, Vector2 impactForce)
    {
        var simulatedBall = Instantiate(_whiteBallSimulated, origin.position, origin.rotation);
        simulatedBall.GetComponent<Rigidbody2D>().AddRelativeForce(impactForce, ForceMode2D.Impulse);

        var points = new Vector3[3];
        points[0] = origin.position;

        for (int i = 0; i < SimulatedSteps; i++)
        {
            Physics2D.Simulate(0.0045f);
            points[1] = simulatedBall.transform.position;
            if (simulatedBall.Collision == null)
                continue;
        }

        if (simulatedBall.Collision == null)
        {
            _lineRenderer.positionCount = 2;
        }
        else
        {
            points[1] = simulatedBall.Collision.contacts[0].point;
            Physics2D.Simulate(0.00001f);
            points[2] = simulatedBall.transform.position;
            _lineRenderer.positionCount = 3;

            if (simulatedBall.Collision.transform.tag == "Ball")
            {
                _lineRendererPlus.positionCount = 2;
                _lineRendererPlus.SetPosition(0, points[1]);
                _lineRendererPlus.SetPosition(1, simulatedBall.Collision.transform.position);
            }
        }

        _lineRenderer.SetPositions(points);
        Destroy(simulatedBall.gameObject);
    }

    public void DeleteTrajectory()
    {
        _lineRenderer.positionCount = 0;
        _lineRendererPlus.positionCount = 0;
    }

    private void SaveBodiesData()
    {
        foreach (var body in _savedBodies)
        {
            body.Value.position = body.Key.transform.position;
            body.Value.rotation = body.Key.transform.rotation;
            body.Value.velocity = body.Key.velocity;
            body.Value.angularVelocity = body.Key.angularVelocity;
        }
    }

    private void LoadBodiesData()
    {
        foreach (var body in _savedBodies)
        {
            body.Key.transform.position = body.Value.position;
            body.Key.transform.rotation = body.Value.rotation;
            body.Key.velocity = body.Value.velocity;
            body.Key.angularVelocity = body.Value.angularVelocity;
        }
    }
}

public class BodyData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
    public float angularVelocity;
}