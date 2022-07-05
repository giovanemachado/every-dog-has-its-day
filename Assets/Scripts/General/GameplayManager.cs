using System.Collections;
using System.Collections.Generic;
using RouteTeamStudios.GameState;
using UnityEngine;

namespace RouteTeamStudios.General
{
    public class GameplayManager : MonoBehaviour
    {
        GameSettings _gameSettings;
        bool _isPlaying;
        bool _gameSpeedIncreaseStarted;

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
            _gameSettings = GameSettings.Instance;
        }

        void Update()
        {
            if (!_isPlaying) return;

            if (!_gameSpeedIncreaseStarted) StartCoroutine(StartIncreaseGameSpeed());
        }

        void OnGameStateChange(BaseGameState state)
        {
            _isPlaying = state == GameManager.Instance.PlayingState;
        }

        IEnumerator StartIncreaseGameSpeed()
        {
            _gameSpeedIncreaseStarted = true;
            yield return new WaitForSeconds(_gameSettings.FirstGameSpeedIncreaseTiming);

            StartCoroutine(IncreaseGameSpeed());
        }

        IEnumerator IncreaseGameSpeed()
        {
            if (Time.timeScale >= _gameSettings.GameSpeedMax) yield break;

            Time.timeScale += _gameSettings.GameSpeedIncrease;
            yield return new WaitForSecondsRealtime(_gameSettings.IncreaseGameSpeedTiming);

            StartCoroutine(IncreaseGameSpeed());
        }
    }
}
