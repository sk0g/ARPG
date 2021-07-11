using UnityEngine;

namespace Core.Managers
{
    public class SingletonSimple : MonoBehaviour
    {
        static SingletonSimple _instance;

        public static SingletonSimple Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = new GameObject().AddComponent<SingletonSimple>();
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