using TMPro;
using UnityEngine;

namespace Core.Managers
{
    public class SurvivalTimer : MonoBehaviour
    {
        static SurvivalTimer _instance;

        public static SurvivalTimer Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = new GameObject().AddComponent<SurvivalTimer>();
                    // name it for easy recognition

                    _instance.name = _instance.GetType().ToString();
                    // mark root as DontDestroyOnLoad();

                    DontDestroyOnLoad(_instance.gameObject);
                }

                return _instance;
            }
        }

        [SerializeField] TextMeshProUGUI runTimerDisplay;

        void Update()
        {
            _currentRunTime += Time.deltaTime;

            UpdateDisplayedTime();
        }

        float _currentRunTime;

        public void ResetTimer() => _currentRunTime = 0f;

        void UpdateDisplayedTime() => runTimerDisplay.SetText(_currentRunTime.ToString("0.0"));
    }
}