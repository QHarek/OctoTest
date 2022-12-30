using UnityEngine;
using UnityEngine.AI;
using Opsive.UltimateCharacterController.Traits;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieController : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private GameObject _player;
    private ZombieStats _stats;
    private Animator _animator;
    private float _hitTime;
    private float _damageDelay;
    private float _basicSpeed;
    private bool _hasTarget;
    private bool _hitOnce;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player");
        _stats = GetComponent<ZombieStats>();
        _animator = GetComponent<Animator>();
        _damageDelay = _animator.GetCurrentAnimatorStateInfo(0).length / 5;

        _basicSpeed = _navMeshAgent.speed;
        _hitOnce = false;
    }

    private void Update()
    {
        if (_stats.Hitting == true)
            StartHitting();

        if (_stats.IsAlive() && _player.GetComponent<CharacterHealth>().HealthValue > 0)
        {
            SetTarget();
            CheckReachDistance();
            UpdateAnimatorParameters();
        }
    }

    private void StartHitting()
    {
        _navMeshAgent.speed = 0;

        if (Time.time > _hitTime + _damageDelay && _hitOnce == false && _stats.IsAlive())
        {
            HitTarget();
            _hitOnce = true;
        }
        if (Time.time > _hitTime + _animator.GetCurrentAnimatorStateInfo(0).length)
        {
            _stats.Hitting = false;
            _hitOnce = false;
        }
    }

    private void SetTarget()
    {
        if (_stats.Hitting == false)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
            _navMeshAgent.speed = _basicSpeed;
            _hasTarget = true;
        }
    }

    private void CheckReachDistance()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) < _stats.ReachDistance && _stats.Hitting == false)
        {
            _hitTime = Time.time;
            _stats.Hitting = true;
        }
    }

    private void UpdateAnimatorParameters()
    {
        _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
        _animator.SetFloat("DistanceToPlayer", Vector3.Distance(_player.transform.position, transform.position));
        _animator.SetBool("HasTarget", _hasTarget);
    }

    private void HitTarget()
    {
        _player.GetComponent<CharacterHealth>().Damage(_stats.DamageAmount);
        if (_player.GetComponent<CharacterHealth>().HealthValue <= 0)
        {
            _hasTarget = false;
            _navMeshAgent.speed = 0;
        }
    
    }

    internal void DisableAI()
    {
        _navMeshAgent.enabled = false;
        _animator.enabled = false;
    }
}
