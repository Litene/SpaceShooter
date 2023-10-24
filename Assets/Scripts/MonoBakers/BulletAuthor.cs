using Components;
using Unity.Entities;
using UnityEngine;

namespace MonoBakers {
    public class BulletAuthor : MonoBehaviour {
        public float MoveSpeed;
    }
    
    public class BulletBaker : Baker<BulletAuthor>{
        public override void Bake(BulletAuthor authoring) {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new BulletProperties.MoveSpeed {
                Value = authoring.MoveSpeed
            });
        }
    }
}