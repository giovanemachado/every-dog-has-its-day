using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using RouteTeamStudios.GameState;
using RouteTeamStudios.Utilities;

namespace RouteTeamStudios.General
{
    public class BackgroundManager : MonoBehaviour
    {
        public GameObject TilemapBG1;
        public GameObject TilemapBG2;

        bool _shouldUpdateBackground = false;
        Vector2 resetBackgroundPosition;
        float backgroundSpeed;

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
            _shouldUpdateBackground = state == GameManager.Instance.PlayingState;
        }

        void Start()
        {
            backgroundSpeed = GameSettings.Instance.BackgroundMovementSpeed;
            resetBackgroundPosition = TilemapBG2.transform.position;
        }

        void FixedUpdate()
        {
            if (!_shouldUpdateBackground) return;

            TilemapBG1.transform.position = new Vector2(TilemapBG1.transform.position.x, TilemapBG1.transform.position.y) + (Vector2.left * backgroundSpeed * Time.deltaTime);

            if (TilemapBG1.transform.position.x < -58.58f) // TODO  remove this literal
            {
                TilemapBG1.transform.position = resetBackgroundPosition;
            }

            TilemapBG2.transform.position = new Vector2(TilemapBG2.transform.position.x, TilemapBG2.transform.position.y) + (Vector2.left * backgroundSpeed * Time.deltaTime);

            if (TilemapBG2.transform.position.x < -58.58f) // TODO remove this literal
            {
                TilemapBG2.transform.position = resetBackgroundPosition;
            }
        }
    }
}
