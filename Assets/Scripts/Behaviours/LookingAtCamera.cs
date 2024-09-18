using UnityEngine;

namespace MegaChaseGame.Behaviours
{
	public sealed class LookingAtCamera : MonoBehaviour
	{
		private void LateUpdate()
		{
			transform.rotation = Camera.main.transform.rotation;
		}
	}
}
