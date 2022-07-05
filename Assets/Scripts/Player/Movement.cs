using RouteTeamStudios.GameState;
using RouteTeamStudios.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RouteTeamStudios.Player
{
    public class Movement : MonoBehaviour
    {
        GameSettings gameSettings;
        bool shouldMove = false;
        Touch touch;
        float movementSpeedModifier;
        float xPos;
        float yPos;

        void Awake()
        {
            GameManager.OnGameStateChange += OnGameStateChange;
        }

        void OnDestroy()
        {
            GameManager.OnGameStateChange -= OnGameStateChange;
        }

        void OnGameStateChange(BaseGameState state)
        {
            shouldMove = state == GameManager.Instance.PlayingState;
        }

        void Start()
        {
            gameSettings = GameSettings.Instance;
            movementSpeedModifier = gameSettings.PlayerSpeedModifier;
        }

        void Update()
        {
            if (!shouldMove) return;

            if (Input.touchCount < 1) return;

            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                yPos = Mathf.Clamp(
                    transform.position.y + touch.deltaPosition.y * movementSpeedModifier,
                    gameSettings.Lanes[4].transform.position.y,
                    gameSettings.Lanes[0].transform.position.y
                );

                transform.position = new Vector2(
                    transform.position.x,
                    yPos
                 );
            }
        }
    }
}
