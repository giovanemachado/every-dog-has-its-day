using UnityEngine;

namespace RouteTeamStudios.GameState
{
    public class GameOverState : BaseGameState
    {
        public override void EnterState(GameManager gameManager)
        {
            Time.timeScale = 0;
        }
    }
}
