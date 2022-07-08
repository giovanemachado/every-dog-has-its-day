using System.Collections;
using System.Collections.Generic;
using RouteTeamStudios.GameState;
using UnityEngine;

namespace RouteTeamStudios.General
{
    public class FoodManager : MonoBehaviour
    {
        public GameObject Food;

        private GameSettings _gameSettings;
        private bool _isPlaying;
        private bool _foodStarted;
        private GameObject[] _foodLanes;

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
            if (!_gameSettings.IsSpawningFood()) return;

            if (!_isPlaying) return;

            if (!_foodStarted) StartCoroutine(StartFood());
        }

        void Start()
        {
            _gameSettings = GameSettings.Instance;
            _foodLanes = new GameObject[] { _gameSettings.Lanes[1], _gameSettings.Lanes[2], _gameSettings.Lanes[3] };
        }

        void OnGameStateChange(BaseGameState state)
        {
            _isPlaying = state == GameManager.Instance.PlayingState;
        }

        IEnumerator StartFood()
        {
            _foodStarted = true;
            yield return new WaitForSeconds(_gameSettings.FirstFoodTiming);

            StartCoroutine(SummonRandomFood(GetFoodTiming()));
        }

        IEnumerator SummonRandomFood(float timing)
        {
            Instantiate(Food, GetRandomLane().transform.position, Quaternion.identity);

            yield return new WaitForSeconds(timing);
            StartCoroutine(SummonRandomFood(GetFoodTiming()));
        }

        float GetFoodTiming()
        {
            return Random.Range(
                _gameSettings.FoodTimingMin,
                _gameSettings.FoodTimingMax
            );
        }

        GameObject GetRandomLane()
        {
            return _foodLanes[Random.Range(0, _foodLanes.Length)];
        }
    }
}
