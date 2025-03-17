using Photon.Pun;
using UnityEngine;

public class InputActions : MonoBehaviourPunCallbacks
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

   public override void OnEnable() { _inputSystem.Enable(); }
   public override void OnDisable() { _inputSystem.Disable(); }

   private void Update()
   {
      if (!photonView.IsMine) return;

      inputX = _inputSystem.Player.Move.ReadValue<Vector2>().x;
      inputY = _inputSystem.Player.Move.ReadValue<Vector2>().y;

      // Move player
      var movement = new Vector2(inputX, inputY) * (speed * Time.deltaTime);
      transform.Translate(movement, Space.World);

      // Update animations
      _animator.SetFloat("inputX", inputX);
      _animator.SetFloat("inputY", inputY);
   }

   // private void OnGUI()
   // {
   //    GUIStyle guiStyle = new()
   //    {
   //       fontSize = 50,
   //       fontStyle = FontStyle.Bold,
   //       padding = new RectOffset(5, 5, 5, 5),
   //       alignment = TextAnchor.LowerCenter,
   //       normal = { textColor = Color.white }
   //    };
   //
   //    GUI.Label(new Rect(20, 20,350, 300), $"X: {inputX}, Y: {inputY}", guiStyle);
   // }
}
