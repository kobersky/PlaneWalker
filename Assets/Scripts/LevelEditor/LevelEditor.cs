using System;
using UnityEditor;
using UnityEngine;

/* Allows creating new levels */
[ExecuteInEditMode]
public class LevelEditor : MonoBehaviour
{
    [Header("Level Item Prefabs")]
    [SerializeField] GameObject _blockPrefab;
    [SerializeField] GameObject _trapPrefab;
    [SerializeField] GameObject _coinPrefab;
    [SerializeField] GameObject _keyPrefab;
    [SerializeField] GameObject _gatePrefab;
    [SerializeField] GameObject _playerPositionPrefab;

    [Header("Other")]
    [SerializeField] LevelManager _newLevel;

    [Header("Editor Actions")]
    [SerializeField] LevelEditorActionType _actionType;

    public string ActionMessage { get; set; }

    private const string INVALID_EXIT_MESSAGE = "Save failed! A level must include an exit (door-like)!";
    private const string INVALID_VITAL_MESSAGE = "Save failed! A level must include a vital item (key-like)!";
    private const string INVALID_PLAYER_POSITION_MESSAGE = "Save failed! A level must include a starting player position!";
    private const string UNKNOWN_ERROR_MESSAGE = "Save failed! unknown error";

    private const string SUCCESS_MESSAGE = "Save succeeded!";

    private void OnEnable()
    {
        ResetMessage();
    }

    public bool ExecuteEditorAction(GameObject clickedObject)
    {
        ResetMessage();

        bool successfulAction = false;

        var positionOfAction = new Vector3(clickedObject.transform.position.x,
            clickedObject.transform.position.y + 1,
            clickedObject.transform.position.z);

        switch (_actionType)
        {
            case LevelEditorActionType.Delete:
                if (clickedObject.transform.parent != _newLevel.BaseSurface) 
                { 
                    DestroyImmediate(clickedObject);
                    successfulAction = true;
                }
                break;

            case LevelEditorActionType.PlayerSpawnPosition:
                successfulAction = TryToCreatePlayerStartingPosition(_playerPositionPrefab, _newLevel.SpecialObjects, positionOfAction);
                break;

            case LevelEditorActionType.CreatePlatform:
                successfulAction = TryToCreateLevelObject(_blockPrefab, _newLevel.AdditionalPlatforms, positionOfAction);
                break;

            case LevelEditorActionType.CreateHazard:
                successfulAction = TryToCreateLevelObject(_trapPrefab, _newLevel.SpecialObjects, positionOfAction);
                break;

            case LevelEditorActionType.CreateResource:
                successfulAction = TryToCreateLevelObject(_coinPrefab, _newLevel.SpecialObjects, positionOfAction);
                break;

            case LevelEditorActionType.CreateVitalItem:
                successfulAction = TryToCreateVitalItem(_keyPrefab, _newLevel.SpecialObjects, positionOfAction);
                break;

            case LevelEditorActionType.CreateExit:
                successfulAction = TryToCreateExit(_gatePrefab, _newLevel.SpecialObjects, positionOfAction);
                break;
        }

        return successfulAction;

    }

    private bool TryToCreatePlayerStartingPosition(GameObject objectToPlace, Transform parent, Vector3 newPosition)
    {
        DestroyImmediate(_newLevel.PlayerStartingPoint);
        var levelObject = (GameObject)PrefabUtility.InstantiatePrefab(objectToPlace, parent);
        levelObject.transform.localPosition = newPosition;
        _newLevel.PlayerStartingPoint = levelObject;

        return true;
    }

    private bool TryToCreateVitalItem(GameObject objectToPlace, Transform parent, Vector3 newPosition)
    {
        DestroyImmediate(_newLevel.VitalItem);
        var levelObject = (GameObject)PrefabUtility.InstantiatePrefab(objectToPlace, parent);
        levelObject.transform.localPosition = newPosition;
        _newLevel.VitalItem = levelObject;
        return true;
    }

    private bool TryToCreateExit(GameObject objectToPlace, Transform parent, Vector3 newPosition)
    {
        DestroyImmediate(_newLevel.Exit);
        var levelObject = (GameObject)PrefabUtility.InstantiatePrefab(objectToPlace, parent);
        levelObject.transform.localPosition = newPosition;
        _newLevel.Exit = levelObject;
        return true;
    }

    private bool TryToCreateLevelObject(GameObject objectToPlace, Transform parent, Vector3 newPosition)
    {
        var levelObject = (GameObject)PrefabUtility.InstantiatePrefab(objectToPlace, parent);
        levelObject.transform.localPosition = newPosition;
        return true;
    }

    public void ResetLevel()
    {
        ResetMessage();
        _newLevel.ResetItems();
    }

    public void SaveNewLevel(string prefabPath)
    {
        var isValid = CheckIfLevelIsValid();
        if (!isValid.Item1)
        {
            ActionMessage = isValid.Item2;
        }
        else
        { 
            try
            { 
                PrefabUtility.SaveAsPrefabAsset(_newLevel.gameObject, prefabPath, out var success );
                ActionMessage = isValid.Item2;

            }
            catch(Exception e)
            {
                ActionMessage = UNKNOWN_ERROR_MESSAGE;
            }
        }
    }

    private (bool, string) CheckIfLevelIsValid()
    {
        if (_newLevel.Exit == null) return (false, INVALID_EXIT_MESSAGE);
        if (_newLevel.VitalItem == null) return (false, INVALID_VITAL_MESSAGE);
        if (_newLevel.PlayerStartingPoint == null) return (false, INVALID_PLAYER_POSITION_MESSAGE);

        return (true, SUCCESS_MESSAGE);
    }

    private void ResetMessage()
    {
        ActionMessage = string.Empty;
    }

    /* for initialization - no longer used */
    /*    public void CreateBaseSurface()
        {
         int floorRowAmount = 10;
         int floorColAmount = 10;

            for (int i = 0; i < floorRowAmount; i++)
            {
                for (int j = 0; j < floorColAmount; j++)
                {
                    var currentBlock = (GameObject)PrefabUtility.InstantiatePrefab(_blockPrefab, BaseSurface);
                    currentBlock.transform.localPosition = new Vector3(i, 0, j);
                }
            }
        }*/
}
