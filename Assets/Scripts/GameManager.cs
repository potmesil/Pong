using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Shared;

    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private GUISkin _guiSkin;

    private readonly Stopwatch _stopwatch = new Stopwatch();
    private readonly List<GameObject> _balls = new List<GameObject>();

    private int _playerScore1;
    private int _playerScore2;
    private float _ballIdleTime;

    private void Awake()
    {
        if (Shared == null)
        {
            Shared = this;
        }
    }

    private void Start()
    {
        RestartGame();
    }

    private void Update()
    {
        if (_stopwatch.ElapsedMilliseconds >= 10000 + (_ballIdleTime * 1000))
        {
            AddBall(0);
        }
    }

    private void OnGUI()
    {
        GUI.skin = _guiSkin;
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + _playerScore1);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + _playerScore2);

        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            RestartGame();
        }
    }

    public void AddScore(string wallName)
    {
        if (wallName == "RightWall")
        {
            _playerScore1++;
        }
        else
        {
            _playerScore2++;
        }

        _balls.ForEach(Destroy);
        _balls.Clear();
        AddBall(1);
    }

    private void RestartGame()
    {
        _playerScore1 = 0;
        _playerScore2 = 0;

        _balls.ForEach(Destroy);
        _balls.Clear();
        AddBall(2);
    }

    private void AddBall(float idleTime)
    {
        _ballIdleTime = idleTime;

        // Bez kolizi micu
        var ball = Instantiate(_ballPrefab);
        ball.GetComponent<BallControl>().Init(_ballIdleTime);
        _balls.ForEach(x => Physics2D.IgnoreCollision(ball.GetComponent<Collider2D>(), x.GetComponent<Collider2D>()));
        _balls.Add(ball);
        
        // Povolene kolize micu
        //_balls.Add(Instantiate(_ballPrefab));
        //_balls.Last().GetComponent<BallControl>().Init(_ballIdleTime);

        _stopwatch.Restart();
    }

    private void OnEscapePress()
    {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
    }
}