using System.Linq;
using Aspects;
using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Entities.Internal;
using Unity.Jobs;
using Utility;

namespace Systems {
	[BurstCompile] public partial struct AsteroidSpawnSystem : ISystem {
		
		[BurstCompile] public void OnCreate(ref SystemState state) {
			state.RequireForUpdate<StatsProperties.AsteroidCount>();
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
			var spawnRate = spaceAspect.CurrentSpawnsPerSecond > 0 ? 1 / spaceAspect.CurrentSpawnsPerSecond : 1;
			if (spaceAspect.SpawnTimer > spawnRate) {
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

		[BurstCompile] public void Execute(SpaceAspect spaceAspect, [EntityIndexInQuery] int sortKey) { 
			var asteroid = ECB.Instantiate(sortKey, spaceAspect.GetPrefab);
			ECB.SetComponent(sortKey, asteroid, spaceAspect.GetRandomSpawnPos()); 
		}

	}
}