using RouteTeamStudios.General;
using UnityEngine;

namespace RouteTeamStudios.GameState
{
    public class PausedState : BaseGameState
    {
        public override void EnterState(GameManager gameManager) {
            GameSettings.Instance.CurrentTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }
    } 
}
