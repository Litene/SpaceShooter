using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Aspects {
    public readonly partial struct PlayerAspect : IAspect {
        public readonly Entity Entity;
        private readonly RefRO<PlayerProperties.MousePosition> _mousePosition;
        private readonly RefRO<PlayerProperties.ShootInput> _shootInput;
        private readonly RefRO<PlayerProperties.BulletEntity> _bulletEntity;
        private readonly RefRW<PlayerProperties.Shoot> _shoot;
        private readonly RefRW<LocalTransform> _transform;
        
        public Entity GetBulletEntity => _bulletEntity.ValueRO.Value;

        public bool HasShot {
            get => _shoot.ValueRO.HasShot;
            set => _shoot.ValueRW.HasShot = value;
        }
        public float ShootCooldown => _shoot.ValueRO.ShootCooldown;
        public float ShootTimer {
            get => _shoot.ValueRO.ShootTimer;
            set => _shoot.ValueRW.ShootTimer = value;
        }

        public bool AutoShoot {
            get => _shoot.ValueRO.AutoShoot;
            set => _shoot.ValueRW.AutoShoot = value;
        }

        public bool ShootPressed() {
            return _shootInput.ValueRO.Value;
        }

        public LocalTransform GetTransform {
            get => _transform.ValueRO;
            set => _transform.ValueRW = value;
        }

        public LocalTransform GetBulletSpawnPoint() {
            var trans = new LocalTransform();
            trans.Position = GetTransform.Up() * 1.4f;
            trans.Rotation = GetRotation(GetTransform);
            trans.Scale = 1f;
            return trans;
        }

        public void SetRotation(quaternion rotation) {
            _transform.ValueRW.Rotation = rotation;
        }

        public quaternion GetRotation(LocalTransform transform) {
            var directedMousePos = _mousePosition.ValueRO.Value - transform.Position.xy;
            var targetVector = new Vector2((directedMousePos - transform.Position.xy).x,
                (directedMousePos - transform.Position.xy).y);
            targetVector.Normalize();
            var lookRotation =
                quaternion.LookRotationSafe(
                    new float3(0, 0, 1), new float3(targetVector.x, targetVector.y, 0));

            return lookRotation;
        }
    }
}