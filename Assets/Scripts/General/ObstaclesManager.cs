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

        GameSettings gameSettings;

        bool isPlaying;
        bool obstaclesStarted = false;

        void Awake()
        {
            GameManager.OnGameStateChange += OnGameStateChange;
        }

        void OnDestroy()
        {
            GameManager.OnGameStateChange -= OnGameStateChange;
        }

        void Update()
        {
            if (!isPlaying) return;

            if (!obstaclesStarted) StartCoroutine(StartObstacles());
        }

        void Start()
        {
            gameSettings = GameSettings.Instance;
        }

        void OnGameStateChange(BaseGameState state)
        {
            isPlaying = state == GameManager.Instance.PlayingState;
        }

        IEnumerator StartObstacles()
        {
            obstaclesStarted = true;
            yield return new WaitForSeconds(gameSettings.FirstObstacleTiming);

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
                gameSettings.ObstaclesTimingMin,
                gameSettings.ObstaclesTimingMax
            );
        }

        GameObject GetRandomLane()
        {
            return gameSettings.Lanes[Random.Range(0, gameSettings.Lanes.Length)];
        }
    }
}
