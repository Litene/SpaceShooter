using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Utility;

namespace Aspects {
	public readonly partial struct AsteroidAspect : IAspect {
		public readonly Entity Entity;

		private readonly RefRW<LocalTransform> _transform;
		private readonly RefRW<AsteroidProperties.CircleData> _circleData;
		private readonly RefRW<AsteroidProperties.RandomValue> _randomValue;
		
		public LocalTransform GetRandomSpawnPos() {

			LocalTransform tempTransform;
            
			float minAngle = _circleData.ValueRO.PreviousAngle + _circleData.ValueRO.MinAngleDistance;
			float maxAngle = _circleData.ValueRO.PreviousAngle - _circleData.ValueRO.MinAngleDistance + 360; 
			float angle = _randomValue.ValueRO.Value.NextFloat(minAngle, maxAngle);
            
			float angleRad = angle  * MathHelper.Deg2Rad;
			float2 offset = new float2(math.sin(angleRad), math.cos(angleRad)) * _circleData.ValueRO.CircleRadius;
			_circleData.ValueRW.PreviousAngle = angle;

			tempTransform.Position = new float3(offset.x, offset.y, 0);
			tempTransform.Rotation = GetRotation();
			tempTransform.Scale = 1.0f;
            
			return tempTransform;
		}
		public quaternion GetRotation() {
			var normalizedVector = new Vector2(_transform.ValueRO.Position.x, _transform.ValueRO.Position.y).normalized;
			var lookRotation = quaternion.LookRotationSafe(new float3(0, 0, 1), new float3(normalizedVector.x, normalizedVector.y, 0));
			return lookRotation;
		}

	}
}