using Aspects;
using Components;
using Unity.Burst;
using Unity.Entities;

namespace Systems {
	[BurstCompile] public partial struct StatsSystem : ISystem {

		private EntityQuery _asteroidQuery;
		private EntityQuery _bulletQuery;

		[BurstCompile] public void OnCreate(ref SystemState state) {
			state.RequireForUpdate<StatsProperties.AsteroidCount>();
			_asteroidQuery = state.GetEntityQuery(ComponentType.ReadOnly<AsteroidProperties.Tag>());
			_bulletQuery = state.GetEntityQuery(ComponentType.ReadOnly<BulletProperties.Tag>());
		}
		
		[BurstCompile] public void OnUpdate(ref SystemState state) {
			var statsScreen = SystemAPI.GetSingletonEntity<StatsProperties.AsteroidCount>();
			var statsAspect = SystemAPI.GetAspect<StatsAspect>(statsScreen);
			var asteroidCount = _asteroidQuery.CalculateEntityCount();
			var bulletCount = _bulletQuery.CalculateEntityCount();

			statsAspect.GetSetBulletCount = bulletCount;	
			statsAspect.GetSetAsteroidCount = asteroidCount;
		}

	}
}