using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private int _score;

    public static GUIManager _instance;

    void Awake()
    {
        _instance = GetComponent<GUIManager>();
        _scoreText.text = _score.ToString();
    }

    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
            _scoreText.text = _score.ToString();
        }
    }
}
