using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveAbility : MonoBehaviour
{
    CharacterController _characterController;
    Animator _animator;

    public float MoveSpeed = 2f;
    public float RunSpeed = 5f;
    public float SlowSpeed = 1f;
    public float JumpPower = 5f;
    private float _yVelocity = 0f;
    private float _gravity = -9.8f;

    public float enhancedJumpPower = 15f; 
    private float _lastGroundedHeight; 

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _lastGroundedHeight = transform.position.y;
    }

    private void Update()
    {
        bool isGrounded = _characterController.isGrounded;
        if (isGrounded)
        {
            if (_yVelocity < 0)
            {
                _yVelocity = -0.5f; 
            }

           

            _lastGroundedHeight = transform.position.y; 
        }

     

        HandleMovement(isGrounded);

        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            StartJump(JumpPower);
        }

        ApplyGravity();
    }

    void HandleMovement(bool isGrounded)
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDirection = Camera.main.transform.TransformDirection(h, 0, v).normalized;

        float speed = CalculateSpeed(h, v);
        Vector3 movement = moveDirection * speed * Time.deltaTime;
        _characterController.Move(movement);

        UpdateAnimator(moveDirection.magnitude, speed);
    }

    float CalculateSpeed(float h, float v)
    {
        if (Input.GetKey(KeyCode.LeftShift) && (h != 0 || v != 0))
        {
            return RunSpeed;
        }
        if (Input.GetMouseButton(0) && (h != 0 || v != 0))
        {
            return SlowSpeed;
        }
        return MoveSpeed;
    }

    void UpdateAnimator(float magnitude, float speed)
    {

        float currentBlend = _animator.GetFloat("Move");
        float targetBlend;

        if (magnitude > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                targetBlend = 1.0f; 
            }
            else if (Input.GetMouseButton(0))
            {
                targetBlend = 0.33f; 
            }
            else
            {
                targetBlend = 0.66f; 
            }
        }
        else
        {
            targetBlend = 0f; 
        }

       
        _animator.SetFloat("Move", Mathf.Lerp(currentBlend, targetBlend, Time.deltaTime * 8));
    }

    void ApplyGravity()
    {
        _yVelocity += _gravity * Time.deltaTime;
        _characterController.Move(new Vector3(0, _yVelocity, 0) * Time.deltaTime);
    }

    void StartJump(float power)
    {
        _yVelocity = power;
        _animator.SetTrigger("Jump");
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Bed") && _characterController.isGrounded)
        {
            StartJump(enhancedJumpPower);
        }
    }
}