using System.Collections;
using RouteTeamStudios.General;
using UnityEngine;

namespace RouteTeamStudios.Player
{
    public class GameplayData
    {
        public int DogPoints;
        public int HighScore;

        public GameplayData(GameplayManager gameplayManager)
        {
            DogPoints = gameplayManager.DogPoints;
            HighScore = gameplayManager.HighScore;
        }
    }
}
