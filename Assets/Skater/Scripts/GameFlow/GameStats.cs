using UnityEngine;
using System;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance { get { return instance; } }
    private static GameStats instance;


    //Score
    public float score;
    public float highScore;
    public float distanceModifier = 1.5f; // fish * this for points


    // Fish
    public int totalFish;
    public int fishCollectedThisSession;
    public float pointsPerFish = 10.0f;
    public AudioClip fishCollectSFX;

    // Internal Cooldown   this stops us updating score every frame now every 0.2f
    private float lastScoreUpdate;
    private float scoreUpdateDelta = 0.2f;

    // Action
    public Action<int> OnCollectFish;
    public Action<float> OnScoreChange;

    private void Awake()
    {
        instance = this;
        OnCollectFish += SendAchievmentProgress;
    }
    public void Update()
    {
        float s = GameManager.Instance.motor.transform.position.z * distanceModifier;  // each fish total * distancemod to increase score
        s += fishCollectedThisSession * pointsPerFish;

        if (s > score)
        {
            score = s;          // keep cuurent score after reset
            if (Time.time - lastScoreUpdate > scoreUpdateDelta)
            {
                lastScoreUpdate = Time.time; // reset time to allow score update
                OnScoreChange?.Invoke(score);
            }
        }
    }

    public void CollectFish()
    {
        fishCollectedThisSession++;
        OnCollectFish?.Invoke(fishCollectedThisSession);
        AudioManager.Instance.PlaySFX(fishCollectSFX, 0.7f);

    }


    public void ResetSession()  // reset all counts at start
    {
        score = 0;
        fishCollectedThisSession = 0;
        OnScoreChange?.Invoke(score);
        OnCollectFish?.Invoke(fishCollectedThisSession);
    }

    public string ScoreToText()
    {
        return score.ToString("000000");

    }

    public string FishToText()
    {
        return fishCollectedThisSession.ToString("000");
    }

    private void SendAchievmentProgress(int fishCount)
    {
        switch (fishCount)
        {
            case 10:
                Social.ReportProgress(GPGSIds.achievement_collect_10_fish, 100.0f, null);
                break;
            case 25:
                Social.ReportProgress(GPGSIds.achievement_collect_25_fish, 100.0f, null);
                break;
            default:
                break;
        }
    }
}
