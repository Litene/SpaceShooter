using Aspects;
using Unity.Entities;

namespace Systems {
    public partial struct AsteroidMoveSystem : ISystem {
        public void OnUpdate(ref SystemState state) {
            var deltaTime = SystemAPI.Time.DeltaTime;
            new AsteroidMoveJob {
                DeltaTime = deltaTime
            }.ScheduleParallel();
            
        }
        

    }

    public partial struct AsteroidMoveJob : IJobEntity {
        public float DeltaTime;
        public bool NotFirstIteration;
        private void Execute(AsteroidAspect aspect, [EntityIndexInQuery] int sortKey) {
            aspect.Move(DeltaTime, !NotFirstIteration);
            NotFirstIteration = true;
        }

    }
}