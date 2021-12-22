using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    public const int SimulatedSteps = 100;
    public const float LengthAfterCollided = 0.1f;

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private WhiteBallSimulated _whiteBallSimulated;

    public void ShowTrajectory(Transform origin, Vector2 impactForce)
    {
        var simulatedBall = Instantiate(_whiteBallSimulated, origin.position, origin.rotation);
        simulatedBall.GetComponent<Rigidbody2D>().AddRelativeForce(impactForce, ForceMode2D.Impulse);

        Physics2D.autoSimulation = false;

        var points = new Vector3[3];
        points[0] = origin.position;

        for (int i = 0; i < SimulatedSteps; i++)
        {
            Physics2D.Simulate(0.1f);
            if(i == SimulatedSteps - 1)
            {
                points[1] = simulatedBall.transform.position;
                break;
            }
            if (simulatedBall.Collision == null)
                continue;
        }

        if(simulatedBall.Collision == null)
        {
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPositions(points);
            Physics2D.autoSimulation = true;
            Destroy(simulatedBall.gameObject);
            return;
        }

        points[1] = simulatedBall.Collision.contacts[0].point;
        Physics2D.Simulate(0.1f);
        points[2] = simulatedBall.transform.position;
        _lineRenderer.positionCount = 3;

        _lineRenderer.SetPositions(points);
        Physics2D.autoSimulation = true;
        Destroy(simulatedBall.gameObject);
    }

    public void DeleteTrajectory()
    {
        _lineRenderer.positionCount = 0;
    }
}