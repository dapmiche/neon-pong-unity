using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 10f;

    Vector2 _rawInput;
    GameManager _gameManager;

    void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        if (_gameManager.CurrentState != GameManager.GameState.Playing) return;
        Move();
        Boundaries();
    }

    void Move()
    {
        float moveY = _rawInput.y * _moveSpeed * Time.deltaTime;
        transform.position += new Vector3(0, moveY, 0);
    }

    void Boundaries()
    {
        float clampedY = Mathf.Clamp(transform.position.y, -4f, 4f);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }

    void OnMove(InputValue value)
    {
        _rawInput = value.Get<Vector2>();
    }
}
