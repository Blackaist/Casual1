using UnityEngine;

namespace Project
{
    public class Item : BaseEntity
	{
		protected override void OnCollisionEnter2D(Collision2D collision)
		{
			base.OnCollisionEnter2D(collision);

			if (collision.collider.CompareTag("Shelf"))
			{
				transform.parent = collision.collider.transform;
			}
		}

		protected override void OnCollisionExit2D(Collision2D collision)
		{
			base.OnCollisionExit2D(collision);

			if (collision.collider.CompareTag("Shelf"))
			{
				//for avoiding OnDestroy errors
				if (collision.gameObject.activeInHierarchy)
				{
					transform.parent = null;
				}
			}
		}
	}
}