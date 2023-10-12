using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems {
    [BurstCompile][UpdateBefore(typeof(TransformSystemGroup))] public partial struct PlayerMoveSystem : ISystem {
        [BurstCompile] public void OnUpdate(ref SystemState state) {
            var deltaTime = SystemAPI.Time.DeltaTime;
            new MovePlayerJob {
                DeltaTime = deltaTime
            }.Schedule();
        }
    }

    public partial struct MovePlayerJob : IJobEntity {
        public float DeltaTime;

        public void Execute(ref LocalTransform transform, in PlayerProperties.MoveInput moveInput,
            PlayerProperties.MoveSpeed moveSpeed, PlayerProperties.RotationSpeed rotationSpeed) {
            transform.Position.xy += moveInput.Value * moveSpeed.Value * DeltaTime;
            if (math.lengthsq(moveInput.Value) > float.Epsilon) {
                var forward = new float3(moveInput.Value.x, moveInput.Value.y, 0);
                var targetRot = forward * DeltaTime * rotationSpeed.Value;
                transform.Rotation = quaternion.LookRotation(targetRot, math.up());
            }
        }
    }
}