using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditor.Rendering;
using UnityEngine;
using Utility;

namespace Aspects {
    public readonly partial struct SpaceAspect : IAspect {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRW<SpaceProperties.SpawnValues> _spawnValues;
        
        private readonly RefRW<SpaceProperties.MoveSpeed> _moveSpeed;
        private readonly RefRO<SpaceProperties.Prefab> _prefab;

        public Entity GetPrefab => _prefab.ValueRO.Value;
        
        public float3 GetPosition => _transform.ValueRO.Position;

        public float SpawnRate => _spawnValues.ValueRO.Rate;
        public float SpawnTimer {
            get => _spawnValues.ValueRO.Timer;
            set => _spawnValues.ValueRW.Timer = value;
        }
        
        
      

        public void Move(float deltaTime) {
            _transform.ValueRW.Position += _transform.ValueRO.Up() * _moveSpeed.ValueRO.Value * deltaTime;
        }
        
    } 
}