using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(GenatorMap))]

public class GameManager : MonoBehaviour
{
    [Header("Adventure Mode")]
    [SerializeField] float smooth;
    [SerializeField] Animator finishAni;
    [SerializeField] TextMeshProUGUI currentLevel;
    [SerializeField] ParticleSystem finishEffect;
    public GameMode currentMode;
    private int currentLevelAdventure;
    [Space]
    [Header("Time Mode")]
    public Image leftTimer;
    public Image rightTimer;
    public float limitTime;
    public Animator endGameTimerPanel;
    public TextMeshProUGUI hightScoreTxt;
    public TextMeshProUGUI scoreTxt;


    private Camera mainCamera;
    private bool isEnd = false;
    private static GameManager instance;
    private float currentTimer;
    private int currentLevelTimer;
    private int repeatLevel = 0;
    private int row;
    private int col;
    private AdventureMode adventureData;
    private TiemrMode timerData;
    private CustomMode customData;

    public float Smooth { get => smooth; set => smooth = value; }
    public int Row { get => row; set => row = value; }
    public int Col { get => col; set => col = value; }
    public bool IsEnd { get => isEnd; set => isEnd = value; }
    public static GameManager Instance { get => instance; set => instance = value; }
    public Camera MainCamera { get => mainCamera; set => mainCamera = value; }
    public bool IsNext { get; set; }
    #region Singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
    #endregion
    void Start()
    {
        currentMode = ScenesManager.Instance.gameMode;
        mainCamera = Camera.main;
        isEnd = false;

        switch (currentMode)
        {
            case GameMode.ADVENTURE:
                adventureData = DataManager.Instance.GetDataGame().adventureData;
                repeatLevel = adventureData.repeatLevel;
                col = adventureData.currentCol;
                row = adventureData.currentRow;
                currentLevelAdventure = adventureData.currentLevel;
                SetLevel();
                GenatorMap.Instance.CreateMap(row, col);
                repeatLevel--;
                leftTimer.enabled = rightTimer.enabled = false;
                endGameTimerPanel.gameObject.SetActive(false);
                isEnd = IsNext = false;
                break;
            case GameMode.TIME:
                StartTimerMode();
                break;
            case GameMode.NEW_CUSTOM:
                StartNewCustomMode();
                break;
        }
    }
    private void Update()
    {
        switch (currentMode)
        {
            case GameMode.ADVENTURE:
                AdventureMode();
                break;
            case GameMode.TIME:
                TimeMode();
                break;
            case GameMode.NEW_CUSTOM:
            case GameMode.CONTINUE_CUSTOM:
                CustomMode();
                break;

        }
    }


    #region Adventure Mode
    private int RepeatLevel()
    {
        int insidePiece = (row - 2) * (col - 2);
        int outsidePiece = row * col - 4 - insidePiece;
        return 2 + outsidePiece * 2 + insidePiece * 3;
    }
    public void End(bool show)
    {
        isEnd = show;
        finishAni.SetBool("In-Out", show);
        if (show)
            finishEffect.Play();
    }
    void AdventureMode()
    {
        if (isEnd && IsNext)
        {
            if (repeatLevel == 0)
            {
                if (Mathf.Abs(row - col) > 3)
                {
                    if (row > col)
                        col++;
                    else
                        row++;
                }
                else
                {
                    int randomMode = Random.Range(0, 6);//ti le tang row hoac col
                    if (randomMode == 0)
                    {
                        int temp = row;
                        row = col;
                        col = temp;
                    }
                    else if (randomMode % 2 == 0)
                        col++;
                    else if (randomMode % 2 == 1)
                        row++;
                }
                repeatLevel = RepeatLevel();
            }
            GenatorMap.Instance.CreateMap(row, col);
            repeatLevel--;
            currentLevelAdventure++;
            SaveDataAdventureMode();
            SetLevel();
            IsNext = false;
        }
    }
    void SetLevel()
    {
        currentLevel.text = "#" + adventureData.currentLevel.ToString();
    }
    public void SaveDataAdventureMode()
    {
        adventureData.currentRow = row;
        adventureData.currentCol = col;
        adventureData.currentLevel = currentLevelAdventure;
        adventureData.repeatLevel = repeatLevel;
        DataManager.Instance.UpdateData(adventureData);
    }

    #endregion

    #region Timer Mode
    public void StartTimerMode()
    {
        timerData = DataManager.Instance.GetDataGame().timerData;
        row = col = 4;
        endGameTimerPanel.gameObject.SetActive(true);
        leftTimer.enabled = rightTimer.enabled = true;
        currentTimer = limitTime;
        currentLevelTimer = 1;
        SetLevelTimer();
        GenatorMap.Instance.CreateMap(row, col);
    }
    private void TimeMode()
    {
        if (GenatorMap.Instance.Complete())
        {
            GenatorMap.Instance.CreateMap(row, col);
            currentLevelTimer++;
            currentTimer = limitTime;
            SetLevelTimer();
        }
        Timer();
    }
    private void Timer()
    {
        if (!IsEnd && currentTimer <= 0)
        {
            EndGameTimerMode();
            return;
        }

        currentTimer -= Time.deltaTime;
        float ratioTimer = currentTimer / limitTime;
        leftTimer.fillAmount = ratioTimer;
        rightTimer.fillAmount = ratioTimer;

        if (ratioTimer < 0.2)
            SetTimerColor(Color.red);
        else if (ratioTimer < 0.5)
            SetTimerColor(Color.yellow);
        else
            SetTimerColor(Color.green);
    }
    private void SetTimerColor(Color color)
    {
        leftTimer.color = color;
        rightTimer.color = color;
    }
    private void SetLevelTimer()
    {
        currentLevel.text = "#" + currentLevelTimer.ToString();
    }

    private void EndGameTimerMode()
    {
        IsEnd = true;
        endGameTimerPanel.SetTrigger("Show");
        scoreTxt.text = currentLevelTimer.ToString();
        if (currentLevelTimer > timerData.maxLevel)
        {
            hightScoreTxt.text = "HIGHT SCORRE";
            timerData.maxLevel = currentLevelTimer;
            DataManager.Instance.UpdateData(timerData);
        }
        else
            hightScoreTxt.text = "SCORE";
    }
    #endregion

    #region Custom Mode
    private void CustomMode()
    {
        if (GenatorMap.Instance.Complete())
        {
            EndCustomMode();
        }

    }
    private void EndCustomMode()
    {

    }
    private void StartNewCustomMode()
    {
        customData = DataManager.Instance.GetDataGame().customData;
        leftTimer.enabled = rightTimer.enabled = false;
        endGameTimerPanel.gameObject.SetActive(false);
        row = PlayerPrefs.GetInt("Row");
        col = PlayerPrefs.GetInt("Col");
        GenatorMap.Instance.CreateMap(row, col);
    }
    #endregion
}
