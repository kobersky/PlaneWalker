using System.Collections.Generic;
using UnityEngine;

/* Manages lifecycle of everything outside of the main gameplay loop */
public class AppManager : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] private GameplayManager _gameplayManager;
    [SerializeField] private List<LevelManager> _levelsList;

    [Header("Popups")]
    [SerializeField] private ChooseLevelPopup _chooseLevelPopupPrefab;
    [SerializeField] private MessagePopup _messagePopupPrefab;

    private const string WIN_TEXT = "You Won!!";
    private const string LOSE_TEXT = "You Lost!!";

    private void Start()
    {        
        LaunchLevelChoicePopup();
    }

    private void OnEnable()
    {
        GameplayManager.OnGameWon += LaunchWinPopup;
        GameplayManager.OnGameLost += LaunchLossPopup;

        MessagePopup.OnContinue += LaunchLevelChoicePopup;
        ChooseLevelPopup.OnLevelChosen += StartGame;
    }

    private void OnDisable()
    {
        GameplayManager.OnGameWon -= LaunchWinPopup;
        GameplayManager.OnGameLost -= LaunchLossPopup;

        MessagePopup.OnContinue += LaunchLevelChoicePopup;
        ChooseLevelPopup.OnLevelChosen -= StartGame;
    }

    private void StartGame(LevelManager level)
    {
        _gameplayManager.StartGame(level);
    }

    private void LaunchLevelChoicePopup()
    {
        var chooseLevelPopup = Instantiate(_chooseLevelPopupPrefab);
        chooseLevelPopup.Initialize(_levelsList);
    }

    private void LaunchWinPopup()
    {
        var winPopup = Instantiate(_messagePopupPrefab);
        winPopup.InitText(WIN_TEXT);
    }

    private void LaunchLossPopup()
    {
        var lossPopup = Instantiate(_messagePopupPrefab);
        lossPopup.InitText(LOSE_TEXT);
    }
}
