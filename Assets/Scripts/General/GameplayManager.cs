using System.Collections;
using System.Collections.Generic;
using RouteTeamStudios.GameState;
using RouteTeamStudios.Player;
using TMPro;
using UnityEngine;

namespace RouteTeamStudios.General
{
    public class GameplayManager : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        public int score = 0;

        GameSettings _gameSettings;
        bool _isPlaying;
        bool _gameSpeedIncreaseStarted;

        void Awake()
        {
            GameManager.OnGameStateChange += OnGameStateChange;
            PlayerManager.OnGetFood += OnGetFood;
        }

        void OnDestroy()
        {
            GameManager.OnGameStateChange -= OnGameStateChange;
            PlayerManager.OnGetFood -= OnGetFood;
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

        void OnGetFood()
        {
            IncreaseScore(1);
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

        public void IncreaseScore(int amount)
        {
            score += amount;
            UpdateScoreUI(score);
        }

        public void ResetScore()
        {
            score = 0;
            UpdateScoreUI(score);
        }

        void UpdateScoreUI(int score)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
