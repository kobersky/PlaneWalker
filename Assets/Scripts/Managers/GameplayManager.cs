using System;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameConfiguration _gameConfiguration;
    [SerializeField] private Transform _levelWrapper;

    [SerializeField] InfoUIView _livesUI;
    [SerializeField] InfoUIView _coinsUI;
    [SerializeField] InfoUIView _keyUI;

    public static event Action OnGameWon;
    public static event Action OnGameLost;

    private int _coinsCollected;
    private bool _keyCollected;
    private int _livesLeft;

    private LevelManager _levelManager;

    private const string KEY_FOUND = "YES";
    private const string KEY_NOT_FOUND = "NO";
    private string GenerateKeyText => _keyCollected ? KEY_FOUND : KEY_NOT_FOUND;


    private void OnEnable()
    {
        PlayerController.OnResourceCollected += OnCoinCollected;
        PlayerController.OnVitalItemCollected += OnKeyCollected;
        PlayerController.OnHazardItemTriggered += OnPlayerHit;
        PlayerController.OnExitItemTriggered += OnExitLevelReached;
    }

    private void OnDisable()
    {
        PlayerController.OnResourceCollected -= OnCoinCollected;
        PlayerController.OnVitalItemCollected -= OnKeyCollected;
        PlayerController.OnHazardItemTriggered -= OnPlayerHit;
        PlayerController.OnExitItemTriggered -= OnExitLevelReached;
    }

    public void StartGame(LevelManager level)
    {
        ResetGameplayParameters();
        DeletePreviousLevelFromScene();
        LoadLevel(level);
    }

    private void ResetGameplayParameters()
    {
        _livesLeft = _gameConfiguration.MaxLives;
        _livesUI.SetContentText(_livesLeft.ToString());

        _coinsCollected = 0;
        _coinsUI.SetContentText(_coinsCollected.ToString());

        _keyCollected = false;
        _keyUI.SetContentText(GenerateKeyText);

        _player.gameObject.SetActive(false);
    }

    private void LoadLevel(LevelManager level)
    {
        GameObject levelGameObject = Instantiate(level.gameObject, _levelWrapper);
        _levelManager = levelGameObject.GetComponent<LevelManager>();

        _player.transform.position = _levelManager.PlayerStartingPoint.transform.position;
        _player.gameObject.SetActive(true);
    }

    private void DeletePreviousLevelFromScene()
    {
        for (int i = _levelWrapper.childCount - 1; i >= 0; i--)
        {
            Transform child = _levelWrapper.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    private void OnPlayerHit()
    {
        _livesLeft--;
        if (_livesLeft < 0)
        {
            GameLost();
        }
        else
        {
            _player.GetHit();
            _livesUI.SetContentText(_livesLeft.ToString());
        }
    }

    private void OnCoinCollected()
    {
        _coinsCollected++;
        _coinsUI.SetContentText(_coinsCollected.ToString());
    }

    private void OnKeyCollected()
    {
        _keyCollected = true;
        _levelManager.UnlockExit();
        _keyUI.SetContentText(KEY_FOUND);
    }

    private void OnExitLevelReached()
    {
        if (_keyCollected)
        {
            GameWon();
        }
    }

    private void GameWon()
    {
        ResetGameplayParameters();
        OnGameWon?.Invoke();
    }

    private void GameLost()
    {
        ResetGameplayParameters();
        OnGameLost?.Invoke();
    }
}
