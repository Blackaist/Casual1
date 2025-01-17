using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project
{
	[RequireComponent(typeof(IDragAndDropable))]
	public class MovableItemComponent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		private IDragAndDropable _item = null;

		private Coroutine _dragCoroutine = null;

		private void Awake()
		{
			_item = GetComponent<IDragAndDropable>();
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			_item.Drag();

			Cursor.Instance.OnDrag(_item);

			_dragCoroutine = StartCoroutine(UpdatePosition(eventData.position));
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			_item.Drop();

			Cursor.Instance.OnDrop();

			StopCoroutine(_dragCoroutine);
		}

		private IEnumerator UpdatePosition(Vector2 startPosition)
		{
			var _playerInputListener = new PlayerInputListener(startPosition);

			while (_item.IsDragging)
			{
				var newPosition = Camera.main.ScreenToWorldPoint(_playerInputListener.Position);
				newPosition.z = newPosition.y;
				transform.position = newPosition;

				yield return null;
			}
		}
	}

}
