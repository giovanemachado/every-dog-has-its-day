using System.Collections;
using System.Collections.Generic;
using RouteTeamStudios.GameState;
using TMPro;
using UnityEngine;

namespace RouteTeamStudios
{
    public class StoreManager : MonoBehaviour
    {
        public TextMeshProUGUI DogPointsText;
        [HideInInspector] public int DogPoints;
        bool _inStore;

        void Awake()
        {
            GameManager.OnGameStateChange += OnGameStateChange;
        }

        void Update()
        {
            if (!_inStore) return;
        }

        void OnDestroy()
        {
            GameManager.OnGameStateChange -= OnGameStateChange;
        }

        void OnGameStateChange(BaseGameState state)
        {
            _inStore = state == GameManager.Instance.StoreState;

            if (_inStore)
            {
                DogPointsText.text = "Dog Points: " + DogPoints;
            }
        }

        public void ItemClicked(GameObject goClicked)
        {
            Debug.Log(goClicked.name);
        }
    }
}
