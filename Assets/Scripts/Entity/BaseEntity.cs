using UnityEngine;

namespace Project
{
	public class BaseEntity : MonoBehaviour, IDragAndDropable
	{
		[SerializeField] protected ItemScriptableObject _item = null;
		[SerializeField] protected SpriteRenderer _renderer = null;

		protected Rigidbody2D _body = null;

		protected int _collideTouches = 0;

		protected bool _isBottomCollided = false;
		protected float _yOffset = 0.0f;

		public bool IsDragging { get; protected set; } = false;

		private void Awake()
		{
			_body = GetComponent<Rigidbody2D>();

			ChangeSprite(true);
		}

		public virtual void Drag()
		{
			if (!IsDragging)
			{
				IsDragging = true;
				ChangeSprite(false);

				_body.bodyType = RigidbodyType2D.Kinematic;
			}
		}

		public virtual void Drop()
		{
			if (IsDragging)
			{
				IsDragging = false;
				ChangeSprite(true);

				if (_isBottomCollided)
				{
					SetToPlaceablePosition();
				}

				if (_collideTouches == 0)
				{
					_body.bodyType = RigidbodyType2D.Dynamic;
				}
			}
		}

		private void ChangeSprite(bool isNormal)
		{
			if (_item != null)
			{
				if (isNormal)
				{
					if (_item.Sprite != null) 
					{ 
						_renderer.sprite = _item.Sprite;
					}
				}
                else
                {
					if (_item.Highlight != null)
					{
						_renderer.sprite = _item.Highlight;
					}
				}
            }
		}

		protected virtual void OnCollisionEnter2D(Collision2D collision)
		{
			_collideTouches++;

			_body.bodyType = RigidbodyType2D.Kinematic;

			if (collision.collider.CompareTag("Bottom"))
			{
				_isBottomCollided = true;
				UnplaceablePositionResolver(collision.collider);
			}
		}

		protected virtual void OnCollisionExit2D(Collision2D collision)
		{
			_collideTouches--;

			if (collision.collider.CompareTag("Bottom"))
			{
				_isBottomCollided = false;
			}

			if (_collideTouches == 0 && !IsDragging)
			{
				_body.bodyType = RigidbodyType2D.Dynamic;
			}
		}

		protected void UnplaceablePositionResolver(Collider2D collider)
		{
			_yOffset = collider.bounds.max.y + _renderer.sprite.bounds.extents.y * transform.localScale.y;
		}

		protected void SetToPlaceablePosition()
		{
			transform.position = new Vector3(transform.position.x, _yOffset, _yOffset);
			_isBottomCollided = false;
			_yOffset = 0.0f;
		}
	}

}
