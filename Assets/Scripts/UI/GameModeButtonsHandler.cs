using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameModeButtonsHandler : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _options;
    [SerializeField] private GameObject _background;

    private InputSystem_Actions _actions;

    private void Awake()
    {
        _actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _actions.Enable();
        _actions.UI.Cancel.performed += OnCancelPressed;
    }


    private void OnDisable()
    {
        _actions.UI.Cancel.performed -= OnCancelPressed;
        _actions.Disable();
    }

    public void Exit()
    {
        SceneLoader.Instance.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }

    private void OnCancelPressed(InputAction.CallbackContext context)
    {
        if (_options.activeSelf)
        {
            _options.SetActive(false);
            _menu.SetActive(true);
            return;
        }

        if (_menu.activeSelf)
        {
            _menu.SetActive(false);
            _background.SetActive(false);
            Time.timeScale = 1.0f;
            return;
        }

        _background.SetActive(true);
        _menu.SetActive(true);
        Time.timeScale = 0f;
    }
}
