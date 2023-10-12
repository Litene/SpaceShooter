using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Components {
    public static class PlayerProperties {
        public struct Tag : IComponentData {}
        public partial struct MoveInput : IComponentData { public float2 Value; }
        public partial struct MoveSpeed : IComponentData { public float Value; }
        public partial struct RotationSpeed : IComponentData { public float Value; }
        public partial struct ShootSpeed : IComponentData { public float Value; }
        
    }
}