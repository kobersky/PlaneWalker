using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* A popup displaying a simple message.*/
public class MessagePopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMessage;
    [SerializeField] private Button _continueButton;

    public static event Action OnContinue;

    private void OnEnable()
    {
        _continueButton.onClick.AddListener(OnContinueClicked);
    }

    private void OnDisable()
    {
        _continueButton.onClick.RemoveAllListeners();
    }

    public void InitText(string text)
    {
        _textMessage.text = text;
    }

    private void OnContinueClicked()
    {
        OnContinue?.Invoke();
        Destroy(gameObject);
    }
}
