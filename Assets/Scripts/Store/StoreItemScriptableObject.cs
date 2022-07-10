using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RouteTeamStudios.Store
{
    [CreateAssetMenu(fileName = "StoreItem", menuName = "Store/StoreItem")]
    public class StoreItemScriptableObject : ScriptableObject
    {
        public string itemName;
        public int price;
        public bool isPurchased;
    }
}
