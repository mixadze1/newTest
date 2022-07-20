using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Trajectory _trajectory;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private GameThing _gameSpikePrefab;
    [SerializeField] private GameThing _gameMoneyPrefab;
    [SerializeField] private GameObject _buttonRestart;
    [SerializeField] private GameObject _textWin;
    [SerializeField] private GameObject _textLose;
    [SerializeField] private List<Transform> _spawnPositions;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private float _speedPlayer;
   
    private List<GameThing> _countSpike = new List<GameThing> ();
    public List<GameThing> CountMoney = new List<GameThing>();

    private Player _player;

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
            Debug.Log(_player);
            Destroy(_player);
            EndGame();
            _textWin.SetActive(true);
            _buttonRestart.SetActive(true);
        }
        if (IsLose)
        {
            EndGame();
            _textLose.SetActive(true);
            _buttonRestart.SetActive(true);
        }
    }

    public void EndGame()
    {
        foreach(var money in CountMoney)
        {
            Destroy(money.gameObject);
        }
        foreach(var obstacle in _countSpike)
        {
            Destroy (obstacle.gameObject);
        }
        foreach (var trajectory in _trajectory.Trajectorys)
        {
            Destroy(trajectory.gameObject);
        }
        _countSpike.Clear();
        CountMoney.Clear();       
        _trajectory.Trajectorys.Clear();
        
    }

    public void RestartGame()
    {
        CreatePlayer();
        CreateGameObject();
        _buttonRestart.SetActive(false);
        _textLose.SetActive(false);
        _textWin.SetActive(false);
        
    }

    private void CreatePlayer()
    {
        
        Player player  = Instantiate(_playerPrefab);
        _player = player;
        player.transform.position = _startPosition;
        player.Initialize(_trajectory, _startPosition, _speedPlayer) ;
        IsLose = false;
    }


    private void CreateGameObject()
    {
        foreach(var position in _spawnPositions)
        {
                GameThing gameThing;
                int random = Random.Range(0, 2);
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
                gameThing.transform.localPosition = position.transform.position;
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
}
