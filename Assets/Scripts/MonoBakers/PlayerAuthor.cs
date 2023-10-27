using Components;
using Unity.Entities;
using UnityEngine;

namespace MonoBakers {
    public class PlayerAuthor : MonoBehaviour {
        public float MoveSpeed;
        public float RotationSpeed;
        public float ShootSpeed;
        public bool AutoShoot;
        public float ShootCooldown;
        public int TotalLives;
        public GameObject BulletPrefab;
    }

    public class PlayerBaker : Baker<PlayerAuthor> {
        public override void Bake(PlayerAuthor authoring) {
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
            AddComponent(entity, new PlayerProperties.BulletEntity {
                Value = GetEntity(authoring.BulletPrefab, TransformUsageFlags.Dynamic)
            });
            AddComponent(entity, new PlayerProperties.Shoot {
                AutoShoot = authoring.AutoShoot,
                ShootCooldown = authoring.ShootCooldown
            });
            AddComponent(entity, new PlayerProperties.Lives {
                Value = authoring.TotalLives
            });
            AddComponent<PlayerProperties.MousePosition>(entity);
            AddComponent<PlayerProperties.Tag>(entity);
            AddComponent<PlayerProperties.ShootInput>(entity);
            AddComponent<CollisionComponent>(entity);
        }
    }
}