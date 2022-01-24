using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    [SerializeField]const string fileName = "Data.txt";
    [SerializeField] private GameData gameData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (File.Exists(Application.dataPath + "/" + fileName))
                gameData = SaveLoadJson.Load<GameData>(fileName);
            else
            { 
                gameData = new GameData();
                SaveLoadJson.Create(fileName, gameData);
            }
        }
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            UpdateData(true, true);
    }
    public GameData GetDataGame()
    {
        return gameData;
    }
    public void UpdateData(AdventureMode adventure)
    {
        this.gameData.adventureData = adventure;
        SaveLoadJson.Create<GameData>(fileName, gameData);
    }
    public void UpdateData(TiemrMode timer)
    {
        this.gameData.timerData = timer;
        SaveLoadJson.Create<GameData>(fileName, gameData);
    }
    public void UpdateData(CustomMode custom)
    {
        this.gameData.customData = custom;
        SaveLoadJson.Create<GameData>(fileName, gameData);
    }
    public void UpdateData(bool changeSound = false, bool changeVibrate = false)
    {
        gameData.sound = changeSound ? !gameData.sound : gameData.sound;
        gameData.vibrate = changeVibrate ? !gameData.vibrate : gameData.vibrate;
        SaveLoadJson.Create<GameData>(fileName, gameData);
    }
}