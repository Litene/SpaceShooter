using Aspects;
using Components;
using Unity.Burst;
using Unity.Entities;


namespace Systems {
	[BurstCompile] public partial struct BulletSpawnSystem : ISystem {

		[BurstCompile]
		public void OnCreate(ref SystemState state) {
			state.RequireForUpdate<StatsProperties.BulletCount>();
			state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
			foreach (var player in SystemAPI.Query<PlayerAspect>()) {
				player.ShootTimer = player.ShootCooldown + 0.1f;
			}
		}

		[BurstCompile]
		public void OnUpdate(ref SystemState state) {
			foreach (var playerAspect in SystemAPI.Query<PlayerAspect>()) {
				if (!playerAspect.AutoShoot) {
				}
				
				if (playerAspect.ShootPressed()) {
					var deltaTime = SystemAPI.Time.DeltaTime;

					playerAspect.ShootTimer += deltaTime; // must solve cooldown when not pressed for the not autoshot
					if (!playerAspect.HasShot &&
					    playerAspect.ShootTimer > playerAspect.ShootCooldown &&
					    !playerAspect.AutoShoot ||
					    playerAspect.ShootTimer > playerAspect.ShootCooldown && playerAspect.AutoShoot) {
						playerAspect.HasShot = true;
						playerAspect.ShootTimer = 0;
						var ecbParallel =
							SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
						
						var statsScreen = SystemAPI.GetSingletonEntity<StatsProperties.BulletCount>();
						var statsAspect = SystemAPI.GetAspect<StatsAspect>(statsScreen);
						statsAspect.GetSetBulletCount++;
						new ShootJob {
							DeltaTime = deltaTime,
							ECB = ecbParallel.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
						}.ScheduleParallel();
					}
				}
				else {
					if (playerAspect.HasShot) {
						playerAspect.HasShot = false;
					}

					// reset timer? 
				}
			}
		}

	}

	[BurstCompile] public partial struct ShootJob : IJobEntity {

		public float DeltaTime;
		public EntityCommandBuffer.ParallelWriter ECB;

		[BurstCompile] public void Execute(PlayerAspect aspect, [EntityIndexInQuery] int sortKey) {
			var bullet = ECB.Instantiate(sortKey, aspect.GetBulletEntity);
			ECB.SetComponent(sortKey, bullet, aspect.GetBulletSpawnPoint());
		}

	}
}