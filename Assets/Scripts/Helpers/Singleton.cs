using UnityEngine;

namespace DroidDigital.PacMan.Helpers
{
    public class Singleton<T> : MonoBehaviour where T: Object, new()
    {
        public static T Instance
        {
            get { return _instance ?? (_instance = FindObjectOfType<T>()); }
        }

        private static T _instance;

        private void Awake()
        {
            if (Instance == null)
                _instance = new T();
        }
    }
}