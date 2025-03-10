using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private float _speed;
    private Animator _animator;
    private Rigidbody2D _rb;
    private Vector2 _movementInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _animator.SetBool(IsWalking, false);
    }

    private void FixedUpdate()
    {
        var newVelocity = _rb.linearVelocity;
        newVelocity.x = _movementInput.x * _speed;
        newVelocity.y = _rb.linearVelocity.y * _speed;
        _rb.linearVelocity = newVelocity;
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
        _animator.SetBool(IsWalking, _movementInput != Vector2.zero);

        if (!(_movementInput.x > 0))
        {
            if (_movementInput.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
