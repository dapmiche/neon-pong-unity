using UnityEngine;

public class PaddleAI : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 8f;
    [SerializeField] Transform _ball;

    [SerializeField] float _reactionTime = 0.2f;
    [SerializeField] float _aimError = 0.5f;
    float _targetY;
    float _reactionTimer;
    GameManager _gameManager;
    Rigidbody2D _ballRb;

    void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        if (_ball != null)
        {
            _ballRb = _ball.GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        if (_gameManager.CurrentState != GameManager.GameState.Playing) return;
        if (_ballRb == null) return;
        if (Mathf.Abs(_ballRb.linearVelocity.x) < 0.01f) return;

        _reactionTimer -= Time.deltaTime;

        if (_reactionTimer <= 0f)
        {
            ReactionTime();
        }

        Move();
        Boundaries();
    }

    private void Move()
    {
        float newY = Mathf.MoveTowards(
            transform.position.y,
            _targetY,
            _moveSpeed * Time.deltaTime
        );

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void Boundaries()
    {
        float clampedY = Mathf.Clamp(transform.position.y, -4f, 4f);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }
        private void ReactionTime()
    {
        float distanceX = transform.position.x - _ball.position.x;

        float timeToReach = distanceX / _ballRb.linearVelocity.x;

        float predictedY = _ball.position.y + _ballRb.linearVelocity.y * timeToReach;

        float dynamicError = _aimError * Mathf.Abs(distanceX) * 0.2f;

        _targetY = predictedY + Random.Range(-dynamicError, dynamicError);

        _targetY = Mathf.Clamp(_targetY, -4f, 4f);

        _reactionTimer = _reactionTime;
    }
}