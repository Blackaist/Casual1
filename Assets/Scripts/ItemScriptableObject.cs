using System;
using UnityEngine;

namespace Project
{
	[Serializable]
	[CreateAssetMenu(fileName = "Assets/ScriptableObjects/Item/NewItem", menuName = "Project/Add new item")]
	public class ItemScriptableObject : ScriptableObject
	{
		[SerializeField] private string _name = null;
		[SerializeField] private Sprite _sprite = null;
		[SerializeField] private Sprite _highlight = null;

		public string Name => _name;
		public Sprite Sprite => _sprite;
		public Sprite Highlight => _highlight;
	}
}
