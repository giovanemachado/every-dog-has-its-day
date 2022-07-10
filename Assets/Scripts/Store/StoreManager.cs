using System.Collections;
using System.Collections.Generic;
using RouteTeamStudios.GameState;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RouteTeamStudios.Store
{
    public class StoreManager : MonoBehaviour
    {
        [HideInInspector] public int DogPoints;

        public TextMeshProUGUI DogPointsText;
        public GameObject ConfirmBuyModal;

        GameObject _dogItemSelected;
        StoreItemController _storeItemController;
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
                ResetStore();
            }
        }

        public void ItemClicked(GameObject goClicked)
        {
            _dogItemSelected = goClicked;
            _storeItemController = _dogItemSelected.GetComponent<StoreItemController>();

            if (_storeItemController.storeItem.isPurchased)
            {
                Debug.Log("Already purchased");
                return;
            }

            if (_storeItemController.storeItem.price > DogPoints)
            {
                Debug.Log("No money");
                return;
            }

            ConfirmBuyModal.SetActive(true);
        }

        public void BuyPressed()
        {
            DogPoints -= _storeItemController.storeItem.price;
            _storeItemController.storeItem.isPurchased = true;
            ConfirmBuyModal.SetActive(false);

            Debug.Log("Buying: " + _storeItemController.storeItem.name);
            Debug.Log("Price: " + _storeItemController.storeItem.price);
        }

        public void GoBackButton()
        {
            ConfirmBuyModal.SetActive(false);
            ResetStore();
        }

        public void ResetStore()
        {
            _dogItemSelected = null;
        }
    }
}
