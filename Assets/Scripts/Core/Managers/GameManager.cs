using UnityEngine;

namespace Core.Managers
{
    public class GameManager : MonoBehaviour
    {
        static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = new GameObject().AddComponent<GameManager>();
                    // name it for easy recognition

                    _instance.name = _instance.GetType().ToString();
                    // mark root as DontDestroyOnLoad();

                    DontDestroyOnLoad(_instance.gameObject);
                }

                return _instance;
            }
        }

        // implement your Awake, Start, Update, or other methods here...
    }
}