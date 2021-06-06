using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamageable
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    private CharacterController _enemyCC;
    private Player _player;
    [SerializeField] private float _enemySpeed = 3f;
    [SerializeField] private float _gravity = 1f;

    [SerializeField] private int _enemyHealth = 100;
    private bool _isAttacking = false;

    private Vector3 _velocity;
    [SerializeField] private EnemyState _currentState = EnemyState.Chase;
    private float _coolDownTime = 1.5f;
    private float _canFire = 0;

    public int Health
    {
        get
        {
            return _enemyHealth;
        }
        set
        {
            _enemyHealth = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        _enemyCC = GetComponent<CharacterController>();
        if(_enemyCC == null)
        {
            Debug.LogError("Enemy Character Controller is null");
        }
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Enemy could not get the player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(_currentState)
        {
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Chase:
                Move();
                break;
        }
        //if(!_isAttacking)

    }

    private void Move()
    {
        if (_enemyCC.isGrounded)
        {
            Vector3 direction = _player.transform.position - transform.position;
            direction.Normalize(); //this is for setting the Vector length to 1.
            direction.y = 0; //to make sure that enemy will not lean towards us.
            Quaternion fastlook = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, fastlook, Time.deltaTime*3); //works
            _velocity = direction * _enemySpeed;
        }
        //transform.localRotation = Quaternion.LookRotation(direction);
        _velocity.y -= _gravity;
        _enemyCC.Move(_velocity * Time.deltaTime);
    }

    public void Damage(int damageAmount)
    {
        _enemyHealth-= damageAmount;
        if(_enemyHealth<1)
        {
            Destroy(this.gameObject, 0.5f);
        }
    }

    private void Attack()
    {
        if (Time.time > _coolDownTime + _canFire)
        {
            if(_player != null)
            {
                _player.Damage(10); //I already have referenced the player here - no need for the interface
            }
            _canFire = Time.time + _coolDownTime;
        }
    }

    public void StartAttack()
    {
        _currentState = EnemyState.Attack;
    }

    public void StopAttack()
    {
        _currentState = EnemyState.Chase;
    }

}
