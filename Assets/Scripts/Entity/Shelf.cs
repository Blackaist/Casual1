using UnityEngine;

namespace Project
{
    public sealed class Shelf : BaseEntity
	{
		protected override void OnCollisionEnter2D(Collision2D collision)
		{
			if (!collision.collider.CompareTag("Item"))
			{
				base.OnCollisionEnter2D(collision);
			}
		}

		protected override void OnCollisionExit2D(Collision2D collision)
		{
			if (!collision.collider.CompareTag("Item"))
			{
				base.OnCollisionExit2D(collision);
			}
		}
	}
}