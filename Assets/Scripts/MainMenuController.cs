using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public static event Action PlayPressed;
    public static event Action HostPressed;
    
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button HostButton;

    private void Start()
    {
        PlayButton.onClick.AddListener(() => PlayPressed?.Invoke());
        HostButton.onClick.AddListener(() => HostPressed?.Invoke());
    }
}
