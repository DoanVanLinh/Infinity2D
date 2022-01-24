using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;
    public GameMode gameMode;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    public void SwitchScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
    public void ChoseGameMode(int gameMode)
    {
        this.gameMode = (GameMode)gameMode;
    }
}
