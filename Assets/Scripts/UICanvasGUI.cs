using Opsive.Shared.Input;
using UnityEngine;

public class UICanvasGUI : MonoBehaviour
{
    private GameObject _canvasMenu;

    private void Awake()
    {
        _canvasMenu = GameObject.Find("Menu Canvas");
    }

    public void DeathPause()
    {
        Time.timeScale = 0;
        FindObjectOfType<UnityInput>().DisableCursor = false;

        _canvasMenu.SetActive(true);
        _canvasMenu.GetComponent<UIMainMenu>().ShowStats();
        ZombieStats._fragsCount = 0;
        GameObject.Find("Frags Count").GetComponent<TMPro.TextMeshProUGUI>().text = ZombieStats._fragsCount.ToString();
        gameObject.SetActive(false);
    }
}
