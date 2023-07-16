using UnityEngine;
using TMPro;

public class GameStateInit : GameState
{

    public GameObject menuUI;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI fishcountText;

    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameManager.GameCamera.Init);

        highscoreText.text = "Highscore : " + "TBD";
        fishcountText.text = "Fish : " + "TBD";


        menuUI.SetActive(true);

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
       // brain.ChangeState(GetComponent<GameStateShop>());
    }
}
