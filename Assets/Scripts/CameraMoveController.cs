using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Project
{
	public class CameraMoveController : MonoBehaviour
	{
		private const string _pointKey = "Point";
		private const string _clickKey = "Click";

		[SerializeField] private float _maxSpeed = 0.5f;
		[SerializeField] private float _speedMultiplier = 3f;
		[SerializeField] private Transform _leftBorder = null;
		[SerializeField] private Transform _rightBorder = null;

		private int _isClickedCount = 0;

		private InputAction _pointAction = null;
		private InputAction _clickAction = null;

		private Vector2 _prevPosition = Vector2.zero;
		private Vector2 _currentPosition = Vector2.zero;

		private void Awake()
		{
			_pointAction = InputSystem.actions.FindAction(_pointKey);
			_clickAction = InputSystem.actions.FindAction(_clickKey);
		}

		private void OnEnable()
		{
			_pointAction.performed += OnPointPerformed;
			_clickAction.performed += OnClickPerformed;
		}

		private void OnDisable()
		{
			_pointAction.performed -= OnPointPerformed;
			_clickAction.performed -= OnClickPerformed;
		}

		private void Update()
		{
			if (_isClickedCount == 1) 
			{
				if (!Cursor.Instance.IsDragging) 
				{
					if (_prevPosition != Vector2.zero)
					{
						Vector3 delta = Camera.main.ScreenToWorldPoint(_prevPosition) - Camera.main.ScreenToWorldPoint(_currentPosition);

						delta.z = delta.y = 0;

						transform.position += delta;
					}
				}
				else if(_prevPosition != Vector2.zero)
				{
					var position = Camera.main.ScreenToWorldPoint(_currentPosition).x;

					if (position < _leftBorder.position.x)
					{
						MoveToPosition(position, _leftBorder.position.x, Time.deltaTime);
					}
					else if (position > _rightBorder.position.x)
					{
						MoveToPosition(position, _rightBorder.position.x, Time.deltaTime);
					}
				}

				_prevPosition = _currentPosition;
			}
		}


		private void OnPointPerformed(InputAction.CallbackContext context)
		{
			if (_isClickedCount > 0)
			{
				_currentPosition = context.ReadValue<Vector2>();
			}
		}

		private void OnClickPerformed(InputAction.CallbackContext context)
		{
			_isClickedCount += context.ReadValue<float>() > float.Epsilon ? 1 : -1;

			if (_isClickedCount == 0)
			{
				_prevPosition = Vector2.zero;
				_currentPosition = Vector2.zero;
			}
		}

		private void MoveToPosition(float fromX, float toX, float deltaTime)
		{
			var lerpValue = Mathf.Pow(fromX - toX, 3);
			var multiplier = Mathf.Min(Mathf.Abs(lerpValue) * _speedMultiplier, _maxSpeed) * Mathf.Sign(lerpValue);

			Vector3 delta = Vector3.one * multiplier * deltaTime;

			delta.z = delta.y = 0;

			transform.position += delta;
		}
	}
}
