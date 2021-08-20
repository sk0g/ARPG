using UnityEngine;

namespace Core.Managers
{
public class UIManager : MonoBehaviour
{
    static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = new GameObject().AddComponent<UIManager>();
                // name it for easy recognition

                _instance.name = _instance.GetType().ToString();
                // mark root as DontDestroyOnLoad();

                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject deathUI;

    public void SetPauseMenuVisibility(bool visible) => pauseUI.SetActive(visible);

    public void SetDeathMenuVisibility(bool visibility) => deathUI.SetActive(visibility);
}
}