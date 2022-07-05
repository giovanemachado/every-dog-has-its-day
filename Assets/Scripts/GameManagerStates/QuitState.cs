using UnityEditor;
using UnityEngine;

namespace RouteTeamStudios.GameState
{
  public class QuitState : BaseGameState
  {
    public override void EnterState(GameManager gameManager)
    {
      QuitGame();
    }

    void QuitGame()
    {
#if UNITY_EDITOR
      EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }
  }
}