using UnityEngine;
using TMPro;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GameStateInit : GameState
{

    public GameObject menuUI;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI fishcountText;
    [SerializeField] private AudioClip menuLoopMusic;

    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameManager.GameCamera.Init);

        highscoreText.text = "Highscore: " + SaveManager.Instance.save.Highscore.ToString();
        fishcountText.text = "Fish: " + SaveManager.Instance.save.Fish.ToString();


        menuUI.SetActive(true);

        AudioManager.Instance.PlayMusicWithCrossFade(menuLoopMusic, 0.5f);

    }


    public override void Destruct()
    {
        menuUI.SetActive(false);
    }

    public void OnPlayClick()
    {

        brain.ChangeState(GetComponent<GameStateGame>());
        GameStats.Instance.ResetSession();
    }


    public void OnShopClick()
    {
        
        brain.ChangeState(GetComponent<GameStateShop>());
    }


    public void OnAchievmentClick()
    {
        if (GPGS.Instance.isConnectedtoGooglePlay)
        {
            Social.ShowAchievementsUI();
        }
        else
        {
            GPGS.Instance.TriggerManualSignIn();
        }
    }

    public void OnLeaderboardClick()
    {
        if (GPGS.Instance.isConnectedtoGooglePlay)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            GPGS.Instance.TriggerManualSignIn();
        }
    }
  
}
