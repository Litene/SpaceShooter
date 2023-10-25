using System;
using Components;
using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace MonoBakers {
    public class SpaceAuthor : MonoBehaviour {
        public float AsteroidSpawnRate;
        public GameObject AsteroidPrefab;
        
    }

    public class SpaceBaker : Baker<SpaceAuthor> {
        public override void Bake(SpaceAuthor authoring) {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<SpaceProperties.MoveSpeed>(entity);
            AddComponent(entity, new SpaceProperties.SpawnValues {
                Rate = authoring.AsteroidSpawnRate
            });
            AddComponent(entity, new SpaceProperties.Prefab {
                Value = GetEntity(authoring.AsteroidPrefab, TransformUsageFlags.Dynamic)
            });
        }
    }
}