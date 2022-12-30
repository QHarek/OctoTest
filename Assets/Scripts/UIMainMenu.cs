using Opsive.Shared.Input;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    private GameObject _canvasGUI;
    private GameObject _buttonRespawn;
    private GameObject _buttonQuit;
    private GameObject _buttonStartGame;
    private GameObject _labelFrags;

    private void Awake()
    {
        Time.timeScale = 0;
        FindObjectOfType<UnityInput>().DisableCursor = false;

        _canvasGUI = GameObject.Find("GUI Canvas");
        _buttonStartGame = GameObject.Find("Start Game Button");
        _buttonQuit = GameObject.Find("Quit Button");
        _buttonRespawn = GameObject.Find("Respawn Button");
        _labelFrags = GameObject.Find("Total Frags Label");

        _canvasGUI.SetActive(false);
        _buttonRespawn.SetActive(false);
        _labelFrags.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        FindObjectOfType<UnityInput>().DisableCursor = true;
        _buttonStartGame.SetActive(false);
        _buttonQuit.SetActive(true);
        _buttonRespawn.SetActive(true);
        _canvasGUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        Time.timeScale = 1;
        FindObjectOfType<UnityInput>().DisableCursor = true;
        _canvasGUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    internal void ShowStats()
    {
        _labelFrags.SetActive(true);
        GameObject.Find("Total Frags").GetComponent<TMPro.TextMeshProUGUI>().text = GameObject.Find("Frags Count").GetComponent<TMPro.TextMeshProUGUI>().text;
    }
}
