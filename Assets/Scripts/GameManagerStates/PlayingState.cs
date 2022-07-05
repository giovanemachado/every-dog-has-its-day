using RouteTeamStudios.General;
using UnityEngine;

namespace RouteTeamStudios.GameState
{
    public class PlayingState : BaseGameState
    {
        public override void EnterState(GameManager gameManager)
        {
            Time.timeScale = GameSettings.Instance.CurrentTimeScale;
        }
    }
}
