using RouteTeamStudios.GameState;
using RouteTeamStudios.General;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RouteTeamStudios.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static event Action OnGetFood;

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
                if (_gameSettings.IsGodModeOn())
                {
                    Destroy(collision.gameObject);
                    return;
                }

                GameManager.Instance.SwitchState(GameManager.Instance.GameOverState);
            }

            if (collision.gameObject.CompareTag("Food"))
            {
                Destroy(collision.gameObject);
                OnGetFood?.Invoke();
            }
        }

        void OnGameStateChange(BaseGameState state)
        {
            _animator.SetBool("isRunning", state == GameManager.Instance.PlayingState);
        }
    }
}
