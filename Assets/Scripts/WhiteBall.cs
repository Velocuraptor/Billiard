using UnityEngine;

public class WhiteBall : MonoBehaviour
{
    [SerializeField] private TrajectoryRenderer _trajectoryRenderer;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _endPoint;

    [SerializeField] private float _forceFactor = 1.0f;


    private Vector2 _direction;

    private Vector2 _impactForce => _direction * _forceFactor;

    public void SetParameters(Vector2 tapPoint)
    {
        _endPoint.position = tapPoint;
        _direction = -_endPoint.localPosition;
        _trajectoryRenderer.ShowTrajectory(transform, _impactForce);
    }

    public void Hit()
    {
        _rigidbody2D.AddRelativeForce(_impactForce, ForceMode2D.Impulse);
        _direction = Vector2.zero;
        _trajectoryRenderer.DeleteTrajectory();
    }

    public void ResetData()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.angularVelocity = 0.0f;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}