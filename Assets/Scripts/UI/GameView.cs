using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
	public class GameView : MonoBehaviour
	{
		[SerializeField] private Button _exitButton = null;

		private void OnEnable()
		{
			_exitButton.onClick.AddListener(OnExitPressed);
		}

		private void OnDisable()
		{
			_exitButton.onClick.RemoveListener(OnExitPressed);
		}

		private void OnExitPressed()
		{
			Application.Quit();
		}
	}
}
