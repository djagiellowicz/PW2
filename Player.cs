using UnityEngine;

public class Player : CombatBase
{
    // Start is called before the first frame update
    private Rigidbody _playerRigidbody;
    [SerializeField] private float _movementForce = 10.0f;
    [SerializeField] private float _maxSpeed = 5.0f;
    private float _maxSpeedSquared;
    private float _maxDirectionalSpeed;

    void Start()
    {
        if (Health <= 0) Health = 1000;
        if (Damage <= 0) Damage = 50;
        Team = "Player";
        if (AttackSpeed == 0) AttackSpeed = 1;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        ControlMovement();
    }

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnValidate()
    {
        _maxSpeedSquared = Mathf.Pow(_maxSpeed, 2);
        _maxDirectionalSpeed = _maxSpeed / Mathf.Sqrt(2);
    }

    private void ControlMovement()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        bool movingHorizontal = !Mathf.Approximately(horizontalAxis, 0);
        bool movingVertical = !Mathf.Approximately(verticalAxis, 0);

        NegateMovement(movingVertical, movingHorizontal);

        Vector3 forceVector = Vector3.zero;

        if (movingHorizontal ^ movingVertical)
        {
            if (_playerRigidbody.velocity.sqrMagnitude < _maxSpeedSquared)
            {
                forceVector = new Vector3(horizontalAxis, 0, verticalAxis).normalized;
            }
        }
        else if (movingHorizontal && movingVertical)
        {
            bool horizontalTooFast = Mathf.Abs(_playerRigidbody.velocity.x) > _maxDirectionalSpeed;
            bool verticalTooFast = Mathf.Abs(_playerRigidbody.velocity.z) > _maxDirectionalSpeed;

            float verticalSpeed = verticalTooFast ? -verticalAxis : verticalAxis;
            float horizontalSpeed = horizontalTooFast ? -horizontalAxis : horizontalAxis;

            forceVector = new Vector3(horizontalSpeed, 0, verticalSpeed).normalized;
        }
        _playerRigidbody.AddForce(forceVector * _movementForce, ForceMode.Impulse);
    }

    private void NegateMovement(bool movingVertical, bool movingHorizontal)
    {
        Vector3 currentVelocity = _playerRigidbody.velocity;
        if (!movingVertical)
        {
            currentVelocity.z = 0;
        }
        if (!movingHorizontal)
        {
            currentVelocity.x = 0;
        }
        _playerRigidbody.velocity = currentVelocity;
    }
}
