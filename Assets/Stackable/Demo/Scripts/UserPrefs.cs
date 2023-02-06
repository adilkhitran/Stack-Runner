using UnityEngine;

namespace KHiTrAN
{
    public static class UserPrefs
    {
        public static bool isGameEnd = false;
        public static bool isGameStarted = false;
        public static bool isAtFinalMomentum = false;


        public static void SetValue(string key, string _value)
        {
            PlayerPrefs.SetString(key, _value);
        }

        public static void SetValue(string key, float _value)
        {
            PlayerPrefs.SetFloat(key, _value);
        }
        public static void SetValue(string key, int _value)
        {
            PlayerPrefs.SetInt(key, _value);
        }
        public static void SetValue(string key, bool _value)
        {
            PlayerPrefs.SetInt(key, _value ? 1 : 0);
        }

        public static string GetValue(string key, string _value)
        {
            return PlayerPrefs.GetString(key, _value);
        }

        public static float GetValue(string key, float _value)
        {
            return PlayerPrefs.GetFloat(key, _value);
        }
        public static int GetValue(string key, int _value)
        {
            return PlayerPrefs.GetInt(key, _value);
        }
        public static bool GetValue(string key, bool _value)
        {
            return PlayerPrefs.GetInt(key, _value ? 1 : 0) == 1;
        }
    }
}