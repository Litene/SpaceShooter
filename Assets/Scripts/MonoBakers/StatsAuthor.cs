using Components;
using Unity.Entities;
using UnityEngine;

namespace MonoBakers {
	public class StatsAuthor : MonoBehaviour {

		

	}
	
	public class StatsBaker : Baker<StatsAuthor>{
		public override void Bake(StatsAuthor authoring) {
			var entity = GetEntity(TransformUsageFlags.Dynamic);
			AddComponent<StatsProperties.BulletCount>(entity);
			AddComponent<StatsProperties.AsteroidCount>(entity);
			AddComponent<StatsProperties.Tag>(entity);
		}

	}
}