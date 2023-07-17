using UnityEngine;
using System;


[System.Serializable]
public class SaveState
{

  [NonSerialized]  private const int HAT_COUNT = 16;  // Max number of hats
   public int Highscore { set; get; }
    public int Fish { set; get; }
    public DateTime LastSaveTime { set; get; }
    public int CurrentHatIndex { set; get; }
    public byte[] UnlockedHatFlag { set; get; }



    public SaveState()
    {
        Highscore = 0;
        Fish = 10;
        LastSaveTime = DateTime.Now;
        CurrentHatIndex = 0;
        UnlockedHatFlag = new byte[HAT_COUNT];
        UnlockedHatFlag[0] = 1;   // 1 = unlocked 0 = locked

        
    }
}
