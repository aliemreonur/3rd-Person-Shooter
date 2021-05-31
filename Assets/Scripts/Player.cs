using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _cc;
    [SerializeField] private float _speed = 3f;
    private float _jumpHeight = 8f;
     private float _gravity = 20f;

    private Vector3 _direction, _velocity;

    // Start is called before the first frame update
    void Start()
    {
        _cc = GetComponent<CharacterController>();
        if(_cc == null)
        {
            Debug.LogError("Character Controller is null bro");
        }

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_cc.isGrounded)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            _direction = new Vector3(horizontal, 0, vertical);
            _velocity = _direction * _speed;
            //_direction *= _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _velocity.y = _jumpHeight;
            }

        }
        _velocity.y -= _gravity * Time.deltaTime;

        _velocity = transform.TransformDirection(_velocity);
        //for transforming from local space to world space.

        _cc.Move(_velocity * Time.deltaTime);
    }
}
