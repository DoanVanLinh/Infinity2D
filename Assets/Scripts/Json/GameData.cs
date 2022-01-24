using System.Collections.Generic;
using System;

[Serializable]
public class GameData
{

    public bool sound;
    public bool vibrate;
    public AdventureMode adventureData;
    public TiemrMode timerData;
    public CustomMode customData;

    public GameData()
    {
        this.sound = this.vibrate = true;
        this.adventureData = new AdventureMode();
        this.timerData = new TiemrMode();
        this.customData = new CustomMode();
    }

    public GameData(bool sound, bool vibrate, AdventureMode adventureData, TiemrMode timerData, CustomMode customData)
    {
        this.sound = sound;
        this.vibrate = vibrate;
        this.adventureData = adventureData;
        this.timerData = timerData;
        this.customData = customData;
    }
}
[Serializable]
public class AdventureMode
{
    public int currentLevel;
    public int currentRow;
    public int currentCol;
    public int repeatLevel;
    public AdventureMode()
    {
        this.currentLevel = repeatLevel = 1;
        this.currentRow = 2;
        this.currentCol = 3;
    }

}
[Serializable]
public class TiemrMode
{
    public int maxLevel;

    public TiemrMode()
    {
        this.maxLevel = 1;
    }
}
[Serializable]
public class CustomMode
{
    public List<Piece> listPiece;
    public int maxRow;
    public int maxCol;

    public CustomMode()
    {
        this.listPiece = new List<Piece>();
        this.maxRow = 1;
        this.maxCol = 1;
    }
}