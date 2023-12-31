using TMPro;
using UnityEngine;

public class GameStateGame : GameState
{
    public GameObject gameUI;
    [SerializeField] private TextMeshProUGUI fishcount;
    [SerializeField] private TextMeshProUGUI scorecount;
    [SerializeField] private AudioClip gameloopMusic;
    public override void Construct()
    {
        base.Construct();
        GameManager.Instance.motor.ResumePlayer();
        GameManager.Instance.ChangeCamera(GameManager.GameCamera.Game);

        GameStats.Instance.OnCollectFish += OnCollectFish;
        GameStats.Instance.OnScoreChange += OnScoreChange;


        gameUI.SetActive(true);
        AudioManager.Instance.PlayMusicWithCrossFade(gameloopMusic, 0.5f);
    }
    
    public void OnCollectFish(int amnCollected)
    {
        fishcount.text = GameStats.Instance.FishToText();
    }

    public void OnScoreChange(float amnScorechange)
    {
        scorecount.text = GameStats.Instance.ScoreToText();
    }
    public override void Destruct()
    {
        gameUI.SetActive(false);
        GameStats.Instance.OnCollectFish -= OnCollectFish;
        GameStats.Instance.OnScoreChange -= OnScoreChange;
    }

}
