using UnityEngine;

namespace Project
{
	public class Cursor : MonoBehaviour
	{
		public static Cursor Instance { get; private set; }

		private IDrag _currentItem = null;

		public bool IsDragging => _currentItem != null;

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
		}

		private void OnDestroy()
		{
			if (Instance == this)
			{
				Instance = null;
			}
		}

		public void OnDrag(IDrag item)
		{
			_currentItem = item;
		}

		public void OnDrop()
		{
			_currentItem = null;
		}
	}

}
