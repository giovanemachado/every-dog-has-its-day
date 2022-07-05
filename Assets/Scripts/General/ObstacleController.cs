using RouteTeamStudios.GameState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RouteTeamStudios.General

{
    public class ObstacleController : MonoBehaviour
    {
        GameSettings gameSettings;

        bool isPlaying;

        void Awake()
        {
            GameManager.OnGameStateChange += OnGameStateChange;
        }

        void OnDestroy()
        {
            GameManager.OnGameStateChange -= OnGameStateChange;
        }

        void Start()
        {
            gameSettings = GameSettings.Instance;
        }

        void FixedUpdate()
        {
            transform.position = transform.position + (Vector3.left * (gameSettings.ObstacleMovementSpeed * Time.deltaTime));

            if (transform.position.x < -50)
            {
                Destroy(gameObject);
            }
        }

        void OnGameStateChange(BaseGameState state)
        {
            isPlaying = state == GameManager.Instance.PlayingState;
        }
    }
}
