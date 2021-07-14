using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private GameObject pauseMenu;

    private readonly ReactiveProperty<bool> _paused = new ReactiveProperty<bool>();
    public IReadOnlyReactiveProperty<bool> Paused => _paused;
    
    
    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        _paused.Value = !_paused.Value;
        pauseMenu.SetActive(_paused.Value);
    }
}
