using Aspects;
using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Systems {
	[BurstCompile] public partial struct BulletMoveSystem : ISystem {

		[BurstCompile]
		public void OnUpdate(ref SystemState state) {
			var deltaTime = SystemAPI.Time.DeltaTime;
			new MoveBulletJob {
				DeltaTime = deltaTime
			}.ScheduleParallel();
		}

	}

	[BurstCompile] public partial struct MoveBulletJob : IJobEntity {

		public float DeltaTime;

		 private void Execute(BulletAspect bulletAspect) {
			bulletAspect.MoveBullet(DeltaTime);
		}

	}
}