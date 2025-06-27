using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController _cc;
    public PlayerStatHandler _playerStatHandler;
    public Animator _animator;
    public PlayerAttack playerAttack;

    public void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    public void Update()
    {
        MovePlayer();
    }

    public void MovePlayer()
    {
        
        Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveDirection.y = 0f;
        if (moveDirection != Vector3.zero)
        {
            _cc.Move(moveDirection * (_playerStatHandler.moveSpeed * Time.deltaTime));
            transform.LookAt(transform.position + moveDirection);
        }
        
    }
}
