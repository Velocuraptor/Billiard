using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    public const int SimulationAccuracy = 100;

    [SerializeField] private GameObject _whiteBallSimulated;

    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private float _length = 2.0f;

    public void ShowTrajectory(Transform origin, Vector2 impactForce)
    {
        var newBall = Instantiate(_whiteBallSimulated, origin.position, origin.rotation);
        newBall.GetComponent<Rigidbody2D>().AddRelativeForce(impactForce, ForceMode2D.Impulse);

        Physics.autoSimulation = false;

        var points = new Vector3[100];

        for (int i = 0; i < 100; i++)
        {
            Physics.Simulate(0.1f);

            points[i] = newBall.transform.position;
        }

        _lineRenderer.SetPositions(points);

        Physics.autoSimulation = true;

        Destroy(newBall);
    }

    public void DeleteTrajectory()
    {
        //_lineRenderer.positionCount = 0;
    }
}