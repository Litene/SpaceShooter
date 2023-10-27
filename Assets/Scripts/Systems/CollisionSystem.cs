using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;
using Utility;

namespace Systems {
	[BurstCompile] public partial struct CollisionSystem : ISystem {

		public void OnCreate(ref SystemState state) {
			state.RequireForUpdate<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>();
			state.RequireForUpdate<SimulationSingleton>();
		}

		[BurstCompile]
		public void OnUpdate(ref SystemState state) {
			var ecb = SystemAPI.GetSingleton<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>();
			state.Dependency = new CollisionEvent {
				CollisionLookup = SystemAPI.GetComponentLookup<CollisionComponent>(),
				DestroyLookup = SystemAPI.GetComponentLookup<DestroyComponent>(),
				ECB = ecb.CreateCommandBuffer(state.WorldUnmanaged)
			}.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
		}

	}

	[BurstCompile] public struct CollisionEvent : ICollisionEventsJob {

		[ReadOnly] public ComponentLookup<CollisionComponent> CollisionLookup;
		[ReadOnly] public ComponentLookup<DestroyComponent> DestroyLookup;
		public EntityCommandBuffer ECB;

		public void Execute(Unity.Physics.CollisionEvent collisionEvent) {
			// revert methods inside
			if (!CollisionLookup.HasComponent(collisionEvent.EntityA) || !CollisionLookup.HasComponent(collisionEvent.EntityB)) return;
		
			if (DestroyLookup.HasComponent(collisionEvent.EntityA)) {
				ECB.DestroyEntity(collisionEvent.EntityA);
			}

			if (DestroyLookup.HasComponent(collisionEvent.EntityB)) {
				ECB.DestroyEntity(collisionEvent.EntityB);
			}
		}
	}
}