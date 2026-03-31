using System;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float _initialBallSpeed = 8f;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _bounceSound;
    float _currentBallSpeed = 8f;
    Rigidbody2D _rb;
    Vector2 _moveDirection;
    GameManager _gameManager;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gameManager = FindAnyObjectByType<GameManager>();

        StartCoroutine(MoveBall());
    }

    void Update()
    {
        if (_gameManager.CurrentState != GameManager.GameState.Playing) return;
    }

    IEnumerator MoveBall()
    {
        yield return new WaitForSeconds(1f);
        //randomize initial direction
        float randomY = UnityEngine.Random.Range(-0.5f, 0.5f);
        _moveDirection = new Vector2(UnityEngine.Random.value < 0.5f ? -1 : 1, randomY).normalized;
        _rb.linearVelocity = _moveDirection * _currentBallSpeed;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(_gameManager.CurrentState != GameManager.GameState.Playing) return;
        if (collision.gameObject.CompareTag("Paddle"))
        {
            Bounce(collision);
        }
    }

    void Bounce(Collision2D collision)
    {
        _audioSource.PlayOneShot(_bounceSound);
        _currentBallSpeed *= 1.5f; // Increase speed with each hit
        _currentBallSpeed = Mathf.Min(_currentBallSpeed, 15f); // Increase speed with each hit, up to a max
        float paddleY = collision.transform.position.y;
        float ballY = transform.position.y;
        float difference = ballY - paddleY;
        difference = Mathf.Clamp(difference, -1f, 1f);
        float directionX = transform.position.x > collision.transform.position.x ? 1 : -1;
        Vector2 newDirection = new Vector2(directionX, difference).normalized;
        _rb.linearVelocity = newDirection * _currentBallSpeed;
        _rb.position += newDirection * 0.1f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(_gameManager.CurrentState != GameManager.GameState.Playing) return;
        if (collision.gameObject.CompareTag("PointPlayer"))
        {
            _gameManager.AddPlayerScore();
        }
        else if (collision.gameObject.CompareTag("PointAI"))
        {
            _gameManager.AddAIScore();
        }
    }

    public void ResetBall(int direction)
    {
        _rb.linearVelocity = Vector2.zero;
        transform.position = Vector3.zero;
        StartCoroutine(ResetBallAfterDelay(direction));
    }

    private System.Collections.IEnumerator ResetBallAfterDelay(int direction)
    {
        yield return new UnityEngine.WaitForSeconds(1f);
        Vector2 newDirection = new Vector2(direction > 0 ? 1 : -1, UnityEngine.Random.Range(-0.5f, 0.5f)).normalized;
        _rb.linearVelocity = newDirection * _initialBallSpeed;
        _currentBallSpeed = _initialBallSpeed;
    } 

    public void StopBall()
    {
        _rb.linearVelocity = Vector2.zero;
    }          
    
}
