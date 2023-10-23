using Aspects;
using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems {
    [BurstCompile] [UpdateBefore(typeof(TransformSystemGroup))] public partial struct PlayerMoveSystem : ISystem {
        
        [BurstCompile] public void OnUpdate(ref SystemState state) {
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            new MovePlayerJob {
                DeltaTime = deltaTime
            }.Schedule();
        }
    }

    [BurstCompile] public partial struct MovePlayerJob : IJobEntity {
        public float DeltaTime;
        [BurstCompile] public void Execute(PlayerAspect aspect, PlayerProperties.RotationSpeed rotationSpeed) {
            aspect.SetRotation(aspect.GetRotation(aspect.GetTransform));
        }
    }
}