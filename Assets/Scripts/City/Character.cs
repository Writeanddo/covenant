using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Camera _mainCamera;

    private Vector3 _mousePosition;
    private bool _move = false;
    private bool _isClickable = true;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mainCamera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isClickable)
        {
            Debug.Log("Upadta");
            _mousePosition = new Vector3(_mainCamera.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, transform.position.z);
            _move = true;
        }
    }

    private void FixedUpdate()
    {
        if (_move && transform.position.x != _mousePosition.x)
        {
            Debug.Log("FixedUpadta");

            transform.position = Vector2.MoveTowards(transform.position, _mousePosition, 4f * Time.deltaTime);
        }
        else if (_move)
            _move = false;
    }

    public void SetIsClickable() => _isClickable = true;
    public void SetIsNotClickable() => _isClickable = false;
    public void StopMove() => _move = false;

}
