using RouteTeamStudios.GameState;
using RouteTeamStudios.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RouteTeamStudios.Player
{
    public class PlayerMovement : MonoBehaviour
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

        void Start()
        {
            gameSettings = GameSettings.Instance;
            movementSpeedModifier = gameSettings.PlayerSpeedModifier;
        }

        void Update()
        {
            if (!shouldMove) return;

#if UNITY_EDITOR
            yPos = ClampMovement(Input.GetAxis("Mouse Y"));
            Move(yPos);
            return;
#endif

            if (Input.touchCount < 1) return;

            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                yPos = ClampMovement(touch.deltaPosition.y * movementSpeedModifier);
                Move(yPos);
            }
        }

        void OnDestroy()
        {
            GameManager.OnGameStateChange -= OnGameStateChange;
        }

        void OnGameStateChange(BaseGameState state)
        {
            shouldMove = state == GameManager.Instance.PlayingState;
        }


        float ClampMovement(float yMoveValue) {
            return Mathf.Clamp(
                transform.position.y + yMoveValue,
                gameSettings.Lanes[4].transform.position.y,
                gameSettings.Lanes[0].transform.position.y
            );
        }


        void Move(float yMoveValue)
        {
            transform.position = new Vector2(
                transform.position.x,
                yMoveValue
            );
        }
    }
}
