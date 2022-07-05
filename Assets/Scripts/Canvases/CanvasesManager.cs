using RouteTeamStudios.GameState;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RouteTeamStudios.Canvases
{
    public class CanvasesManager : MonoBehaviour
    {
        public GameObject MainMenuCanvas;
        public GameObject PausedMenuCanvas;
        public GameObject HUDCanvas;
        public GameObject GameOverCanvas;
        public GameObject QuitCanvas;

        void Awake()
        {
            GameManager.OnGameStateChange += OnGameStateChange;
        }

        void OnDestroy()
        {
            GameManager.OnGameStateChange -= OnGameStateChange;
        }

        void OnGameStateChange(BaseGameState state)
        {
            MainMenuCanvas.SetActive(state == GameManager.Instance.MainMenuState);
            HUDCanvas.SetActive(state == GameManager.Instance.PlayingState);
            PausedMenuCanvas.SetActive(state == GameManager.Instance.PausedState);
            GameOverCanvas.SetActive(state == GameManager.Instance.GameOverState);
            QuitCanvas.SetActive(state == GameManager.Instance.QuitState);
        }

        // Main Menu
        public void PlayButtonPressed()
        {
            GameManager.Instance.SwitchState(GameManager.Instance.PlayingState);
        }

        public void QuitButtonPressed()
        {
            GameManager.Instance.SwitchState(GameManager.Instance.QuitState);
        }

        // In game HUD
        public void PauseButtonPressed()
        {
            GameManager.Instance.SwitchState(GameManager.Instance.PausedState);
        }

        // Paused Menu
        public void ContinueButtonPressed()
        {
            GameManager.Instance.SwitchState(GameManager.Instance.PlayingState);
        }

        public void MenuButtonPressed()
        {
            SceneManager.LoadScene("GameScene"); // TODO is this the best way?
        }

        // Game over menu
        public void PlayAgainButtonPressed()
        {
            SceneManager.LoadScene("GameScene"); // TODO is this the best way?
        }
    }
}
