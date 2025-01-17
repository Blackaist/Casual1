using UnityEngine;

namespace Project
{
	public class CameraBounds : MonoBehaviour
	{
		[SerializeField] private BoxCollider2D _cameraBound = null;

		private Bounds _bounds;
		

		private void Awake()
		{
			var camera = Camera.main;

			var height = camera.orthographicSize;
			var width = height * camera.aspect;

			var minX = _cameraBound.bounds.min.x + width;
			var maxX = _cameraBound.bounds.extents.x - width;

			var minY = _cameraBound.bounds.min.y - height;
			var maxY = _cameraBound.bounds.extents.y - height;

			_bounds = new Bounds();
			_bounds.SetMinMax(
				new Vector3(minX, minY, 0.0f),
				new Vector3(maxX, maxY, 0.0f)
				);
		}

		private void LateUpdate()
		{
			transform.position = GetCameraBounds();
		}

		private Vector3 GetCameraBounds()
		{
			var currentPosition = transform.position;
			return new Vector3(
				Mathf.Clamp(currentPosition.x, _bounds.min.x, _bounds.max.x),
				Mathf.Clamp(currentPosition.y, _bounds.min.y, _bounds.max.y),
				transform.position.z
				);
		}
	}

}
