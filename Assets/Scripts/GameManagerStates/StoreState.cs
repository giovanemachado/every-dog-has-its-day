using RouteTeamStudios.General;
using UnityEngine;

namespace RouteTeamStudios.GameState
{
    public class StoreState : BaseGameState
    {
        public override void EnterState(GameManager gameManager)
        {
            Time.timeScale = 0;
        }
    }
}
