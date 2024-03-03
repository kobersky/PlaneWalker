using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* A popup Allowing chosing levels. 
 * Is populated by entries that represent levels. */
public class ChooseLevelPopup : MonoBehaviour
{
    [SerializeField] private RectTransform _contentTransform;
    [SerializeField] private Button _levelButtonPrefab;

    public static event Action<LevelManager> OnLevelChosen;


    public void Initialize(List<LevelManager> levelList)
    {
        foreach (LevelManager level in levelList)
        {
            var button = Instantiate(_levelButtonPrefab, _contentTransform.transform);
            var buttonTextComponent = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonTextComponent != null)
            {
                buttonTextComponent.text = level.name;
            }

            button.onClick.AddListener(() => OnLevelClicked(level));
        }
    }

    public void OnLevelClicked(LevelManager level)
    {
        OnLevelChosen?.Invoke(level);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        for (int i = _contentTransform.childCount - 1; i >= 0; i--)
        {
            var button = _contentTransform.GetChild(i).gameObject.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
        }
    }
}
