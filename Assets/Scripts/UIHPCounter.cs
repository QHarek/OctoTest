using Opsive.UltimateCharacterController.Traits;
using UnityEngine;

public class UIHPCounter : MonoBehaviour
{
    private GameObject _player;
    private GameObject _currentHPCounter;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _currentHPCounter = GameObject.Find("Current HP Counter");
    }

    private void Update()
    {
        _currentHPCounter.GetComponent<TMPro.TextMeshProUGUI>().text = _player.GetComponent<CharacterHealth>().HealthValue.ToString();
    }
}
