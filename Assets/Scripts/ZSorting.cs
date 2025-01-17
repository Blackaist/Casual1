using UnityEngine;

namespace Project
{
	public class ZSorting : MonoBehaviour
	{
		[SerializeField] private float _zOffset = 0f;
		void LateUpdate()
		{
			if (transform.position.y != transform.position.z + _zOffset)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y + _zOffset);
			}
		}
	}

}
