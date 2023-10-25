using System;
using Components;
using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace MonoBakers {
    public class SpaceAuthor : MonoBehaviour {
        public float AsteroidSpawnRate;
        public uint AsteroidSpawnSeed;
        public float CircleRadius;
        public float MinAngleDistance;
        public float MaxAngleDistance;
        public GameObject AsteroidPrefab;
        
    }

    public class SpaceBaker : Baker<SpaceAuthor> {
        public override void Bake(SpaceAuthor authoring) {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<SpaceProperties.MoveSpeed>(entity);
            AddComponent(entity, new SpaceProperties.SpawnValues {
                Rate = authoring.AsteroidSpawnRate
            });
            AddComponent(entity, new SpaceProperties.RandomValue {
                Value = Random.CreateFromIndex(authoring.AsteroidSpawnSeed)
            });
            AddComponent(entity, new SpaceProperties.CircleData {
                CircleRadius = authoring.CircleRadius,
                MinAngleDistance = authoring.MinAngleDistance,
                MaxAngleDistance = authoring.MaxAngleDistance
            });
            AddComponent(entity, new SpaceProperties.Prefab {
                Value = GetEntity(authoring.AsteroidPrefab, TransformUsageFlags.Dynamic)
            });
        }
    }
}