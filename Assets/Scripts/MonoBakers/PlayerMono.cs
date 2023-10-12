using Components;
using Unity.Entities;
using UnityEngine;

namespace MonoBakers {
    public class PlayerMono : MonoBehaviour {
        public float MoveSpeed;
        public float RotationSpeed;
        public float ShootSpeed;
    }

    public class PlayerBaker : Baker<PlayerMono> {
        public override void Bake(PlayerMono authoring) {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlayerProperties.MoveSpeed {
                Value = authoring.MoveSpeed
            });
            AddComponent(entity, new PlayerProperties.RotationSpeed {
                Value = authoring.RotationSpeed
            });
            AddComponent(entity, new PlayerProperties.ShootSpeed {
                Value = authoring.ShootSpeed
            });
            AddComponent<PlayerProperties.Tag>(entity);
        }
    }
}