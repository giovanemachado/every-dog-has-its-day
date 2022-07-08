using RouteTeamStudios.Utilities;
using System.Collections;
using UnityEngine;

namespace RouteTeamStudios.General
{
    public class GameSettings : Singleton<GameSettings>
    {
        [Header("Debug Mode")]
        public bool GodMode;
        public bool ShouldSpawnObstacles;
        public bool ShouldSpawnFood;

        [Header("General")]
        public float BackgroundMovementSpeed;
        public float GameSpeedIncrease;
        public float GameSpeedMax;
        public float FirstGameSpeedIncreaseTiming;
        public float IncreaseGameSpeedTiming;
        public float CurrentTimeScale;

        [Header("Player")]
        public float PlayerSpeedModifier;

        [Header("Lanes")]
        public GameObject[] Lanes;

        [Header("Obstacles")]
        public float FirstObstacleTiming;
        public float ObstaclesTimingMin;
        public float ObstaclesTimingMax;
        public float ObstacleMovementSpeed;

        [Header("Food")]
        public float FirstFoodTiming;
        public float FoodTimingMin;
        public float FoodTimingMax;
        public float FoodMovementSpeed;

        public bool IsGodModeOn()
        {
            #if UNITY_EDITOR
                return GodMode;
            #else
                return false;
            #endif
        }
        public bool IsSpawningObstacles()
        {
            #if UNITY_EDITOR
                return ShouldSpawnObstacles;
            #else
                return true;
            #endif
        }
        public bool IsSpawningFood()
        {
            #if UNITY_EDITOR
                return ShouldSpawnFood;
            #else
                return true;
            #endif
        }
    }
}
