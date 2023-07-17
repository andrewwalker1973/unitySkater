using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameCamera
    {
        Init = 0,
        Game = 1,
        Shop = 2,
        Respawn = 3
    }

    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;
    public PlayerMotor motor;
    public GameObject[] cameras;
    public WorldGeneration worldGeneration;
    public SceneChunkGeneration sceneChunkGeneration;



    private GameState state;

    private void Start()
    {
        instance = this;
        state = GetComponent<GameStateInit>();
        state.Construct();

    }

    private void Update()
    {
        state.UpdateState();
    }

    public void ChangeState(GameState s)
    {
        state.Destruct();
        state = s;
        state.Construct();
    }


    public void ChangeCamera(GameCamera c)
    {
        foreach (GameObject go in cameras)
            go.SetActive(false);

        cameras[(int)c].SetActive(true);
    }


}
