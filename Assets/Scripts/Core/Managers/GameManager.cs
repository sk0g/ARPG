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

    GameObject _player;

    public bool PlayerCanAct => !(_isDead || _isPaused);

    bool _isPaused;
    bool _isDead;

    void OnEnable()
    {
        _instance = this;
    }

    public void TogglePause()
    {
        if (_isDead) { return; } // can't pause if you're dead, surely

        _isPaused = !_isPaused;
        SetTimescale();

        UIManager.Instance.SetPauseMenuVisibility(_isPaused);
    }

    public void HandlePlayerDeath()
    {
        _isDead = true;
        SetTimescale();

        UIManager.Instance.SetDeathMenuVisibility(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        _isPaused = false;
        _isDead = false;

        SetTimescale();
    }

    void SetTimescale() => Time.timeScale = PlayerCanAct ? 1f : 0f;
}
}