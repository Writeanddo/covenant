using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    private Camera _mainCamera;
    private Animator _animator;
    private SpriteRenderer _sprite;

    private Vector3 _mousePosition;
    private bool _move = false;
    public bool _isClickable = true;
    private void Awake()
    {
        _mainCamera = FindObjectOfType<Camera>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isClickable)
        {
            _mousePosition = new Vector3(_mainCamera.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, transform.position.z);
            _move = true;
            _animator.SetBool("walk", true);
            if (_mousePosition.x > transform.position.x)
                _sprite.flipX = true;
            else
                _sprite.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        if (_move && transform.position.x != _mousePosition.x)
        {

            transform.position = Vector2.MoveTowards(transform.position, _mousePosition, _moveSpeed * Time.deltaTime);
        }
        else if (_move)
        {
            _move = false;
            _animator.SetBool("walk", false);
        }
    }

    public void SetIsClickable() => _isClickable = true;
    public void SetIsNotClickable() => _isClickable = false;
    public void StopMove()
    {
        _move = false;
        _animator.SetBool("walk", false);
    }

}
