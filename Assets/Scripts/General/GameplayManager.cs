using System;
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
        [HideInInspector] public int DogPoints;
        [HideInInspector] public int HighScore = 0;

        [Header("Store")]
        public GameObject StoreManagerGO;
        StoreManager _storeManager;

        [Header("Current gameplay")]
        public int Score = 0;
        public TextMeshProUGUI ScoreText;
        public TextMeshProUGUI HighScoreText;

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

            if (state == GameManager.Instance.MainMenuState)
            {
                LoadData();
                UpdateScoreUI(Score, HighScore);
            }

            if (state == GameManager.Instance.GameOverState)
            {
                SaveData();
            }
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
            Score += amount;
            UpdateScoreUI(Score, HighScore);
        }

        public void ResetScore()
        {
            Score = 0;
            UpdateScoreUI(Score, HighScore);
        }

        void UpdateScoreUI(int score, int highScore)
        {
            ScoreText.text = "Score: " + score.ToString();
            HighScoreText.text = "High Score: " + highScore.ToString();
        }

        public void SaveData()
        {
            DogPoints += Score;

            if (Score > HighScore) HighScore = Score;

            SaveSystem.SaveData(new GameplayData(this));
        }

        public void LoadData()
        {
            GameplayData data = SaveSystem.LoadData(this);

            if (data == null)
            {
                DogPoints = 0;
                HighScore = 0;
                return;
            }

            DogPoints = data.DogPoints;
            HighScore = data.HighScore;

            _storeManager = StoreManagerGO.GetComponent<StoreManager>();
            _storeManager.DogPoints = DogPoints;
        }
    }
}
