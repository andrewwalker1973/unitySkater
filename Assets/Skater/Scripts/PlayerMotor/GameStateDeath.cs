using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameStateDeath : GameState
{

    public GameObject deathUI;
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI fishTotal;
    [SerializeField] private TextMeshProUGUI currentFish;



    // completion circle field
    [SerializeField] private Image completionCircle;
    [SerializeField] private Image ReviveButton;
    public float timeToDecision = 3.5f;
    private float deathTime;


    public override void Construct()
    {
        base.Construct();
        deathUI.SetActive(true);
        GameManager.Instance.motor.PausePlayer();
        deathTime = Time.time;
        completionCircle.gameObject.SetActive(true);
        ReviveButton.gameObject.SetActive(true);


        highScore.text = "Highscore : TBD";
        currentScore.text = "12345";
        fishTotal.text = "Total : TBD";
        currentFish.text = "20"; 

    }

    public override void UpdateState()
    {
        float ratio = (Time.time - deathTime) / timeToDecision;
        completionCircle.color = Color.Lerp(Color.green, Color.red, ratio);
        completionCircle.fillAmount = 1 - ratio;

        if (ratio > 1)
        {
            completionCircle.gameObject.SetActive(false);
            ReviveButton.gameObject.SetActive(false);
        }

        
    }

    public override void Destruct()
    {
        deathUI.SetActive(false);
    }

    public void ResumeGame()
    {
        brain.ChangeState(GetComponent<GameStateGame>());
        GameManager.Instance.motor.RespawnPlayer();
        


    }

    public void ToMenu()
    {
        brain.ChangeState(GetComponent<GameStateInit>());
    }
}
