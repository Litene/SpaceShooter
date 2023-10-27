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
       
        private readonly RefRO<SpaceProperties.Prefab> _prefab;

        private readonly RefRW<SpaceProperties.RandomValue> _randomValue;
        private readonly RefRW<SpaceProperties.CircleData> _circleData;
        
        public Entity GetPrefab => _prefab.ValueRO.Value;

        public float3 GetPosition => _transform.ValueRO.Position;

        public float SpawnRate => _spawnValues.ValueRO.Rate;
        public float SpawnTimer {
            get => _spawnValues.ValueRO.Timer;
            set => _spawnValues.ValueRW.Timer = value;
        }

        public LocalTransform GetRandomSpawnPos() {

            LocalTransform tempTransform;
            
            float minAngle = _circleData.ValueRO.PreviousAngle + _circleData.ValueRO.MinAngleDistance;
            float maxAngle = _circleData.ValueRO.PreviousAngle - _circleData.ValueRO.MinAngleDistance + 360; 
            float angle = _randomValue.ValueRO.Value.NextFloat(minAngle, maxAngle);
            
            float angleRad = angle  * MathHelper.Deg2Rad;
            float2 offset = new float2(math.sin(angleRad), math.cos(angleRad)) * _circleData.ValueRO.CircleRadius;
            _circleData.ValueRW.PreviousAngle = angle;

            tempTransform.Position = new float3(offset.x, offset.y, 0);
            tempTransform.Rotation = _transform.ValueRO.Rotation;
            tempTransform.Scale = 1.0f;
            
            return tempTransform;
        }
        
        
    } 
}