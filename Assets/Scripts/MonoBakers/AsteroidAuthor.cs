using Components;
using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace MonoBakers {
	public class AsteroidAuthor : MonoBehaviour {
		public float CircleRadius;
		public float MinAngleDistance;
		public float MaxAngleDistance;
		public uint AsteroidSpawnSeed;
	}

	public class AsteroidBaker : Baker<AsteroidAuthor> {
		public override void Bake(AsteroidAuthor authoring) {
			var entity = GetEntity(TransformUsageFlags.Dynamic);
			AddComponent(entity, new AsteroidProperties.CircleData {
				CircleRadius = authoring.CircleRadius,
				MinAngleDistance = authoring.MinAngleDistance,
				MaxAngleDistance = authoring.MaxAngleDistance
			});
			AddComponent(entity, new AsteroidProperties.RandomValue {
				Value = Random.CreateFromIndex(authoring.AsteroidSpawnSeed)
			});
		}
	}
}