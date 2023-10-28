using Aspects;
using Unity.Burst;
using Unity.Entities;

namespace Systems {
    [BurstCompile] public partial struct AsteroidMoveSystem : ISystem {
        [BurstCompile] public void OnUpdate(ref SystemState state) {
            var deltaTime = SystemAPI.Time.DeltaTime;
            new AsteroidMoveJob {
                DeltaTime = deltaTime
            }.ScheduleParallel();
            
        }
        

    }

    [BurstCompile] public partial struct AsteroidMoveJob : IJobEntity {
        public float DeltaTime;
        [BurstCompile] private void Execute(AsteroidAspect aspect, [EntityIndexInQuery] int sortKey) {
            aspect.Move(DeltaTime);
        }

    }
}