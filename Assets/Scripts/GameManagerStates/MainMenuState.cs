using UnityEngine;

namespace RouteTeamStudios.GameState
{
  public class MainMenuState : BaseGameState
  {
    public override void EnterState(GameManager gameManager)
    {
      Time.timeScale = 0;
    }
  }
}
