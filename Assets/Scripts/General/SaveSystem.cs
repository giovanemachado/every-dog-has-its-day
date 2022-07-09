using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using RouteTeamStudios.Player;
using UnityEngine;

namespace RouteTeamStudios.General
{
    public static class SaveSystem
    {
        static readonly string s_fullPath = Path.Combine(Application.persistentDataPath, "gameplayData");

        public static bool SaveData(GameplayData data)
        {
            try
            {
                if (!File.Exists(s_fullPath))
                {
                    File.Create(s_fullPath);
                }

                File.WriteAllText(s_fullPath, JsonUtility.ToJson(data));
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to write to {s_fullPath} with exception {e}");
                return false;
            }
        }

        public static GameplayData LoadData(GameplayManager gameplayManager)
        {
            GameplayData data = new GameplayData(gameplayManager);

            try
            {
                JsonUtility.FromJsonOverwrite(File.ReadAllText(s_fullPath), data);
                return data;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to read from {s_fullPath} with exception {e}");
                return null;
            }
        }
    }
}
