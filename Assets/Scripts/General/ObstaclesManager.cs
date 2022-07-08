using RouteTeamStudios.GameState;
using RouteTeamStudios.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RouteTeamStudios.General
{
    public class ObstaclesManager : MonoBehaviour
    {
        public GameObject Obstacle;

        GameSettings _gameSettings;
        bool _isPlaying;
        bool _obstaclesStarted = false;

        void Awake()
        {
            GameManager.OnGameStateChange += OnGameStateChange;
        }

        void Start()
        {
            _gameSettings = GameSettings.Instance;
        }

        void Update()
        {
            if (!_gameSettings.IsSpawningObstacles()) return;

            if (!_isPlaying) return;

            if (!_obstaclesStarted) StartCoroutine(StartObstacles());
        }

        void OnDestroy()
        {
            GameManager.OnGameStateChange -= OnGameStateChange;
        }


        void OnGameStateChange(BaseGameState state)
        {
            _isPlaying = state == GameManager.Instance.PlayingState;
        }

        IEnumerator StartObstacles()
        {
            _obstaclesStarted = true;
            yield return new WaitForSeconds(_gameSettings.FirstObstacleTiming);

            StartCoroutine(SummonRandomObstacles(GetObstacleTiming()));
        }

        IEnumerator SummonRandomObstacles(float timing)
        {
            Instantiate(Obstacle, GetRandomLane().transform.position, Quaternion.identity);

            yield return new WaitForSeconds(timing);
            StartCoroutine(SummonRandomObstacles(GetObstacleTiming()));
        }

        float GetObstacleTiming()
        {
            return Random.Range(
                _gameSettings.ObstaclesTimingMin,
                _gameSettings.ObstaclesTimingMax
            );
        }

        GameObject GetRandomLane()
        {
            return _gameSettings.Lanes[Random.Range(0, _gameSettings.Lanes.Length)];
        }
    }
}
