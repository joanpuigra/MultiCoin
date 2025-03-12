using UnityEngine;

public class InputActions : MonoBehaviour
{
   private InputSystem _inputSystem;
   private Animator _animator;

   [Header("Player Movement")]
   [SerializeField]
   private float inputX;
   [SerializeField]
   private float inputY;
   [SerializeField]
   private float speed = 5f;

   private void Awake()
   {
      _inputSystem = new InputSystem();
      _animator = GetComponent<Animator>();
   }

   private void OnEnable() { _inputSystem.Enable(); }
   private void OnDisable() { _inputSystem.Disable(); }

   private void Update()
   {
      inputX = _inputSystem.PlayerGirl.Move.ReadValue<Vector2>().x;
      inputY = _inputSystem.PlayerGirl.Move.ReadValue<Vector2>().y;

      Debug.Log($"X: {inputX}, Y: {inputY}");

      // Move player
      var movement = new Vector2(inputX, inputY) * (speed * Time.deltaTime);
      transform.Translate(movement, Space.World);

      // Update animations
      // _animator.SetFloat("Speed", movement.magnitude);
      _animator.SetFloat("InputX", inputX);
      _animator.SetFloat("InputY", inputY);
   }
}
