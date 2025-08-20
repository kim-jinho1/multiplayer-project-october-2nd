using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Global
{
    [CreateAssetMenu(fileName = "PlayerInput", menuName = "SO/PlayerInput", order = 0)]
    public class PlayerInputSO : ScriptableObject,InputSystem.IPlayerActions
    {
        public Vector2 MousePosition { get; private set; }
        public Action OnMouseLeftClick;
        
        private InputSystem _inputSystem;

        private void OnEnable()
        {
            if (_inputSystem == null)
            {
                _inputSystem = new InputSystem();
                _inputSystem.Player.SetCallbacks(this);
            }
            _inputSystem.Player.Enable();
        }

        private void OnDisable()
        {
            _inputSystem.Player.Disable();
        }

        public void OnMouseClick(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnMouseLeftClick?.Invoke();
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            MousePosition  = context.ReadValue<Vector2>();
        }
    }
}