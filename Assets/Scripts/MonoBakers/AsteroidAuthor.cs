using Components;
using Unity.Entities;
using UnityEngine;

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
			AddComponent<AsteroidProperties.Tag>(entity);
			AddComponent<CollisionComponent>(entity);
			AddComponent<DestroyComponent>(entity);
		}
	}
}