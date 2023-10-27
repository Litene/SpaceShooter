using Components;
using Unity.Entities;
using UnityEngine;
namespace MonoBakers {
	public class WallAuthor : MonoBehaviour { }

	public class WallBaker : Baker<WallAuthor> {
		public override void Bake(WallAuthor authoring) {
			var entity = GetEntity(TransformUsageFlags.Dynamic);
			AddComponent<CollisionComponent>(entity);
		}
	}
}