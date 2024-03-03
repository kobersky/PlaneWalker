using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/* A general UI view for displaying info during gameplay */
public class InfoUIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _content;

    public void SetContentText(string text)
    { 
        _content.text = text;
    }
}
