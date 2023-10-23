using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Components {
    public static class PlayerProperties {
        public struct Tag : IComponentData {}

        public partial struct MousePosition : IComponentData {
            public float2 Value;
        }

        public partial struct MoveSpeed : IComponentData {
            public float Value;
        }

        public partial struct RotationSpeed : IComponentData {
            public float Value;
        }

        public partial struct ShootSpeed : IComponentData {
            public float Value;
        }

        public partial struct BulletEntity : IComponentData {
            public Entity Value;
        }

        public partial struct ShootInput : IComponentData {
            public bool Value;
        }

        public partial struct Shoot : IComponentData {
            public bool AutoShoot;
            public bool HasShot;
            public float ShootTimer;
            public float ShootCooldown;
        }

        public partial struct BulletCollection : IComponentData {
            public BlobArray<BulletEntity> Value;
        }
    }
}