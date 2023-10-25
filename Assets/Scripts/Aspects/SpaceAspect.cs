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
        private readonly RefRW<SpaceProperties.RandomValue> _randomValue;
        private readonly RefRW<SpaceProperties.SpawnValues> _spawnValues;
        private readonly RefRW<SpaceProperties.CircleData> _circleData;
        private readonly RefRW<SpaceProperties.MoveSpeed> _moveSpeed;
        private readonly RefRO<SpaceProperties.Prefab> _prefab;

        public Entity GetPrefab => _prefab.ValueRO.Value;
        
        public float3 GetPosition => _transform.ValueRO.Position;

        public float SpawnRate => _spawnValues.ValueRO.Rate;
        public float SpawnTimer {
            get => _spawnValues.ValueRO.Timer;
            set => _spawnValues.ValueRW.Timer = value;
        }
        
        
        public LocalTransform GetRandomSpawnPos(LocalTransform transform) {
            LocalTransform tempTransform;
            
            float minAngle = _circleData.ValueRO.PreviousAngle + _circleData.ValueRO.MinAngleDistance;
            float maxAngle = _circleData.ValueRO.PreviousAngle - _circleData.ValueRO.MinAngleDistance + 360; 
            float angle = _randomValue.ValueRO.Value.NextFloat(minAngle, maxAngle);
            
            float angleRad = angle  * MathHelper.Deg2Rad;
            float2 offset = new float2(math.sin(angleRad), math.cos(angleRad)) * _circleData.ValueRO.CircleRadius;
            _circleData.ValueRW.PreviousAngle = angle;

            tempTransform.Position = new float3(offset.x, offset.y, 0);
            tempTransform.Rotation = GetRotation(transform);
            tempTransform.Scale = 1.0f;
            
            return tempTransform;
        }

        public void Move(float deltaTime) {
            _transform.ValueRW.Position += _transform.ValueRO.Up() * _moveSpeed.ValueRO.Value * deltaTime;
        }
        
        public quaternion GetRotation(LocalTransform transform) {
            var origos = _transform.ValueRO.Position.xy - transform.Position.xy;
            var targetVector = new Vector2((origos - transform.Position.xy).x,
                (origos - transform.Position.xy).y);
            targetVector.Normalize();
            var lookRotation =
                quaternion.LookRotationSafe(
                    new float3(0, 0, 1), new float3(targetVector.x, targetVector.y, 0));

            return lookRotation;
        }
        
    } 
}