using Unity.Entities;
using Unity.Mathematics;

namespace Components {
    public struct SpaceProperties {
        public partial struct SpawnValues : IComponentData {
            public float Rate;
            public float Timer;
        }
        

       

        public partial struct MoveSpeed : IComponentData {
            public float Value;
        }

        public partial struct Prefab : IComponentData {
            public Entity Value;
        }
    }
}