using Aspects;
using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Utility;

namespace Systems {
	[BurstCompile] public partial struct AsteroidSpawnSystem : ISystem {

		public void OnCreate(ref SystemState state) {
			state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
			state.RequireForUpdate<SpaceProperties.SpawnValues>();
		}

		[BurstCompile]
		public void OnUpdate(ref SystemState state) {
			var spaceEntity = SystemAPI.GetSingletonEntity<SpaceProperties.SpawnValues>();
			var spaceAspect = SystemAPI.GetAspect<SpaceAspect>(spaceEntity);
			var deltaTime = SystemAPI.Time.DeltaTime;
			var ecbParallel = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
			spaceAspect.SpawnTimer += deltaTime;
			
		
			if (spaceAspect.SpawnTimer > spaceAspect.SpawnRate) {
				DebugHelper.Log("We do be in here");
				spaceAspect.SpawnTimer = 0;
				new SpawnJob {
					ECB = ecbParallel.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
					DeltaTime = deltaTime
				}.ScheduleParallel();
			}
		}

	}

	[BurstCompile] public partial struct SpawnJob : IJobEntity {

		public float DeltaTime;
		public EntityCommandBuffer.ParallelWriter ECB;
		[BurstCompile] private void Execute(SpaceAspect spaceAspect, AsteroidAspect asteroidAspect, [EntityIndexInQuery] int sortKey) {
			var bullet = ECB.Instantiate(sortKey, spaceAspect.GetPrefab);
			ECB.SetComponent(sortKey, bullet, asteroidAspect.GetRandomSpawnPos());
		}

	}
}