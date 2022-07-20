using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Trajectory _trajectory;

    [SerializeField] private Player _playerPrefab;

    [SerializeField] private GameThing _gameSpikePrefab;
    [SerializeField] private GameThing _gameMoneyPrefab;

    [SerializeField] private Button _buttonRestart;

    [SerializeField] private TextMeshProUGUI _textWin;
    [SerializeField] private TextMeshProUGUI _textLose;

    [SerializeField] private List<Transform> _spawnPositions;

    [SerializeField] private Vector3 _startPosition;

    [SerializeField,Range(1f, 20f)] private float _speedPlayer;

    [SerializeField,Range(1,10)] private int _offsetSpawn;
    [SerializeField, Range(1, 5)] private int _scoreCoin;

    private List<GameThing> _countSpike = new List<GameThing> ();

    public List<GameThing> CountMoney = new List<GameThing>();

    public int ScoreCoin { get => _scoreCoin; private set => _scoreCoin = value; }

    public bool IsLose;

    public static Game _instance;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        CreatePlayer();
        CreateGameObject();
    }

    private void Update()
    {
        if (CountMoney.Count <= 0 && IsLose == false)
        {
            ActivateUi(_textWin);
        }
        if (IsLose)
        {
            ActivateUi(_textLose);
        }
    }

    private void EndGame()
    {
        foreach(var money in CountMoney)
        {
            Recycle(money.gameObject);
        }
        foreach(var obstacle in _countSpike)
        {
            Recycle (obstacle.gameObject);
        }
        foreach (var trajectory in _trajectory.Trajectorys)
        {
            Recycle(trajectory.gameObject);
        }
        _countSpike.Clear();
        CountMoney.Clear();       
        _trajectory.Trajectorys.Clear();        
    }

    public void RestartGame()
    {
        EndGame();
        CreatePlayer();
        CreateGameObject();
        DisableUi();
    }

    private void CreatePlayer()
    {        
        Player player  = Instantiate(_playerPrefab);
        player.transform.position = _startPosition;
        player.Initialize(_trajectory, _startPosition, _speedPlayer) ;
        IsLose = false;
    }

    private void CreateGameObject()
    {
        foreach (var position in _spawnPositions)
        {
            GameThing gameThing;
            int random = Random.Range(0, _offsetSpawn);
            if (random == 0)
            {
                gameThing = Instantiate(_gameSpikePrefab);
                _countSpike.Add(gameThing);
            }
            else
            {
                gameThing = Instantiate(_gameMoneyPrefab);
                CountMoney.Add(gameThing);
            }
            gameThing.transform.localPosition = position.transform.position +
                new Vector3(Random.Range(0, _offsetSpawn), Random.Range(0, _offsetSpawn),Random.Range(0, _offsetSpawn));
        }
    }

    public void DestroyMoney(Money money)
    {
        CountMoney.Remove(money.GetComponent<GameThing>());
        Destroy(money.gameObject);
    }

    public void DestroySpike(Obstacles obstacle)
    {
        _countSpike.Remove(obstacle.GetComponent<GameThing>());
    }

    private void ActivateUi(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);
        _buttonRestart.gameObject.SetActive(true);
    }

    private void Recycle(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    private void DisableUi()
    {
        _buttonRestart.gameObject.SetActive(false);
        _textLose.gameObject.SetActive(false);
        _textWin.gameObject.SetActive(false);
    }
}
