using RouteTeamStudios.GameState;
using RouteTeamStudios.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RouteTeamStudios.Player
{
    public class PlayerManager : MonoBehaviour
    {
        Animator _animator;
        GameSettings _gameSettings;

        void Awake()
        {
            GameManager.OnGameStateChange += OnGameStateChange;
            _animator = GetComponentInChildren<Animator>();
        }

        void OnDestroy()
        {
            GameManager.OnGameStateChange -= OnGameStateChange;
        }

        void Start()
        {
            _gameSettings = GameSettings.Instance;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                if (_gameSettings.GodMode)
                {
                    Destroy(collision.gameObject);
                    return;
                }

                GameManager.Instance.SwitchState(GameManager.Instance.GameOverState);
            }
        }

        void OnGameStateChange(BaseGameState state)
        {
            _animator.SetBool("isRunning", state == GameManager.Instance.PlayingState);
        }
    }
}
