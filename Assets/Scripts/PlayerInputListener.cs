using UnityEngine;
using UnityEngine.InputSystem;

namespace Project
{
	public class PlayerInputListener
	{
		private const string _pointKey = "Point";

		private InputAction _pointAction = null;

		Vector2 _position = Vector2.zero;

		public Vector2 Position => _position;

		public PlayerInputListener(Vector2 startPosition)
		{
			_position = startPosition;

			_pointAction = InputSystem.actions.FindAction(_pointKey);

			_pointAction.performed += OnPointPerformed;
		}

		~PlayerInputListener()
		{
			_pointAction.performed -= OnPointPerformed;
		}

		private void OnPointPerformed(InputAction.CallbackContext context)
		{
			_position = context.ReadValue<Vector2>();
		}
	}

}
