using UnityEngine;

namespace MegaChaseGame.Configs
{
	[CreateAssetMenu(fileName = "Distance Game Config", menuName = "Configs/Distance Game")]
	public class DistanceGameConfig : ScriptableObject
	{
		[SerializeField] private float _maxAllowedDistance;
		[SerializeField] private float _timeToSurviveSeconds;

		public float MaxAllowedDistance => _maxAllowedDistance;
		public float TimeToSurviveSeconds => _timeToSurviveSeconds;
	}
}
