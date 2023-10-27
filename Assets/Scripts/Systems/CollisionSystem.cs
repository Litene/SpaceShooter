using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Utility;

namespace Systems {
	[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
	[BurstCompile]public partial struct CollisionSystem : ISystem {

		[BurstCompile]public void OnUpdate(ref SystemState state) {
			
			// new CollisionEvent {
			//
			// }.Schedule();
		}

	}

	[BurstCompile]public struct CollisionEvent : ICollisionEventsJob {
		public EndFixedStepSimulationEntityCommandBufferSystem ecb;
		[BurstCompile]public void Execute(Unity.Physics.CollisionEvent collisionEvent) {
			DebugHelper.Log("yo");
			if (collisionEvent.EntityA != collisionEvent.EntityB) {
			}
		}

	}
}