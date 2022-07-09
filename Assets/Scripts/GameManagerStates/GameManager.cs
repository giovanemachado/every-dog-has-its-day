using RouteTeamStudios.General;
using RouteTeamStudios.Utilities;
using System;
using UnityEngine;

namespace RouteTeamStudios.GameState
{
    public class GameManager : Singleton<GameManager>
    {
        [HideInInspector] public BaseGameState currentState;
        [HideInInspector] public MainMenuState MainMenuState = new MainMenuState();
        [HideInInspector] public PlayingState PlayingState = new PlayingState();
        [HideInInspector] public PausedState PausedState = new PausedState();
        [HideInInspector] public QuitState QuitState = new QuitState();
        [HideInInspector] public GameOverState GameOverState = new GameOverState(); 
        [HideInInspector] public StoreState StoreState = new StoreState();

        public static event Action<BaseGameState> OnGameStateChange;

        void Start()
        {
            InitStateAndInvokeEvent(MainMenuState);
        }

        void Update()
        {
            currentState.UpdateState(this);
        }

        public void SwitchState(BaseGameState newState)
        {
            currentState.ExitState(this);
            InitStateAndInvokeEvent(newState);
        }

        void InitStateAndInvokeEvent(BaseGameState initState)
        {
            currentState = initState;
            OnGameStateChange?.Invoke(currentState);
            currentState.EnterState(this);
        }
    }
}
