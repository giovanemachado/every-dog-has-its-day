using RouteTeamStudios.GameState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RouteTeamStudios.General

{
    public class ObstacleController : MonoBehaviour
    {
        GameSettings _gameSettings;
        bool _isPlaying;

        void Awake()
        {
            GameManager.OnGameStateChange += OnGameStateChange;
        }

        void Start()
        {
            _gameSettings = GameSettings.Instance;
            _isPlaying = GameManager.Instance.currentState == GameManager.Instance.PlayingState;
        }

        void FixedUpdate()
        {
            if (!_isPlaying) return;

            transform.position = transform.position + (Vector3.left * (_gameSettings.ObstacleMovementSpeed * Time.deltaTime));

            if (transform.position.x < -50)
            {
                Destroy(gameObject);
            }
        }

        void OnDestroy()
        {
            GameManager.OnGameStateChange -= OnGameStateChange;
        }

        void OnGameStateChange(BaseGameState state)
        {
            _isPlaying = state == GameManager.Instance.PlayingState;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Food"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
