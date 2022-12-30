using Opsive.UltimateCharacterController.Traits.Damage;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ZombieStats : MonoBehaviour, IDamageTarget
{
    [SerializeField] private float _reachDistance;
    [SerializeField] private float _damageAmount;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _timeToDestroy;

    private float _currentHealth;
    private bool _hitting;
    internal static int _fragsCount;

    private UnityEvent ZombieDied = new UnityEvent();

    public float ReachDistance => _reachDistance;
    public float DamageAmount => _damageAmount;
    public bool Hitting { get => _hitting; set => _hitting = value; }
    public float CurrentHealth => _currentHealth;
    public GameObject Owner => gameObject;
    public GameObject HitGameObject => gameObject;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _hitting = false;
        ZombieDied.AddListener(GetComponent<ZombieController>().DisableAI);
        ZombieDied.AddListener(RemoveBody);
        ZombieDied.AddListener(UpdateFragCounter);
    }

    private void OnDestroy()
    {
        ZombieDied.RemoveAllListeners();
    }

    private void RemoveBody()
    {
        StartCoroutine(DestroyZombie());
    }

    private IEnumerator DestroyZombie()
    {
        yield return new WaitForSeconds(_timeToDestroy);
        Destroy(gameObject);
    }

    private void UpdateFragCounter()
    {
        _fragsCount++;
        GameObject.Find("Frags Count").GetComponent<TMPro.TextMeshProUGUI>().text = _fragsCount.ToString();
    }

    public bool IsAlive()
    {
        if (_currentHealth > 0)
            return true;
        else
            return false;
    }

    public void Damage(DamageData damageData)
    {
        if (!IsAlive())
            return;
        _currentHealth -= damageData.Amount;
        if (!IsAlive())
            ZombieDied.Invoke();
    }
}
