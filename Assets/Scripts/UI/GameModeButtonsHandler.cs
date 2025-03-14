using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameModeButtonsHandler : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _options;
    [SerializeField] private GameObject _background;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _lives;
    [SerializeField] private Player _player;

    private InputSystem_Actions _actions;
    private bool _playerDied = false;

    private void Awake()
    {
        _actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _actions.Enable();
        _actions.UI.Cancel.performed += OnCancelPressed;
        Enemy.Died += OnEnemyDied;
        _player.TakeDamaged += OnPlayerTakeDamaged;
    }


    private void OnDisable()
    {
        Enemy.Died -= OnEnemyDied;
        _player.TakeDamaged -= OnPlayerTakeDamaged;
        _actions.UI.Cancel.performed -= OnCancelPressed;
        _actions.Disable();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneLoader.Instance.LoadScene("Game");
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

        if (_menu.activeSelf && !_playerDied)
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

    private void OnEnemyDied(int points, Vector3 _)
    {
        _score.text = (int.Parse(_score.text) + points).ToString();
    }

    private void OnPlayerTakeDamaged(int currentHealth)
    {
        _lives.text = currentHealth.ToString();
        if (currentHealth == 0)
        {
            _playerDied = true;
            OnCancelPressed(new InputAction.CallbackContext());
        }
    }
}
