using Components;
using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace MonoBakers {
	public class AsteroidAuthor : MonoBehaviour {
		public float MovementSpeed;

	}

	public class AsteroidBaker : Baker<AsteroidAuthor> {
		public override void Bake(AsteroidAuthor authoring) {
			var entity = GetEntity(TransformUsageFlags.Dynamic);
			AddComponent(entity, new AsteroidProperties.MoveSpeed {
				Value = authoring.MovementSpeed
			});
			AddComponent<AsteroidProperties.AsteroidTag>(entity);
		}
	}
}