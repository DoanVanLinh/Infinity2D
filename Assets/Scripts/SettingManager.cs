using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public void Home()
    {
        
        if (GameManager.Instance.currentMode == GameMode.NEW_CUSTOM || GameManager.Instance.currentMode == GameMode.CONTINUE_CUSTOM)
        {
            GameManager.Instance.SaveCustomMode();
        }
        ScenesManager.Instance.SwitchScene("HomeScene");
        
    }
    public void Sound()
    {
        DataManager.Instance.UpdateData(true, false);
    }
    public void Vibrate()
    {
        DataManager.Instance.UpdateData(false, true);
    }
    public void SwitchScene(string scenesName)
    {
        ScenesManager.Instance.SwitchScene(scenesName);
    }
    public void ChoseMode(int gameMode)
    {
        ScenesManager.Instance.ChoseGameMode(gameMode);
    }
}
