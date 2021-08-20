using UnityEngine;
using UnityEngine.SceneManagement;

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

    public bool IsPaused { get; private set; }
    UIManager _ui;

    void Awake()
    {
        _ui = GetComponent<UIManager>();
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;

        SetTimescaleForPaused(IsPaused);
        _ui.SetPauseMenuVisibility(IsPaused);
    }

    public void RestartGame()
    {
        SetTimescaleForPaused(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SetTimescaleForPaused(bool paused) => Time.timeScale = paused ? 0f : 1f;
}
}