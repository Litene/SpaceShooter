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
		private readonly RefRW<DifficultyComponent> _difficulty;
		
		public int CurrentDifficulty {
			get => _difficulty.ValueRO.CurrentDifficulty;
			set => _difficulty.ValueRW.CurrentDifficulty = value;
		}
		public float CurrentSpawnsPerSecond {
			get => _difficulty.ValueRO.SpawnsPerSecond;
			set => _difficulty.ValueRW.SpawnsPerSecond = value;
		}

		public float DifficultyTimer {
			get => _difficulty.ValueRO.DifficultyTimer;
			set => _difficulty.ValueRW.DifficultyTimer = value;
		}

		public float DifficultyRate {
			get => _difficulty.ValueRO.DifficultyRate;
			set => _difficulty.ValueRW.DifficultyRate = value;
		}

		public float DifficultyMultiplier {
			get => _difficulty.ValueRO.DifficultyMultiplier;
			set => _difficulty.ValueRW.DifficultyMultiplier = value;
		}

		public Entity GetPrefab => _prefab.ValueRO.Value;

		public float3 GetPosition => _transform.ValueRO.Position;

		public float SpawnTimer {
			get => _spawnValues.ValueRO.Timer;
			set => _spawnValues.ValueRW.Timer = value;
		}

		public LocalTransform GetRandomSpawnPos() {
			LocalTransform tempTransform;

			float minAngle = _circleData.ValueRO.PreviousAngle + _circleData.ValueRO.MinAngleDistance;
			float maxAngle = _circleData.ValueRO.PreviousAngle - _circleData.ValueRO.MinAngleDistance + 360;
			float angle = _randomValue.ValueRO.Value.NextFloat(minAngle, maxAngle);

			float angleRad = angle * MathHelper.Deg2Rad;
			float2 offset = new float2(math.sin(angleRad), math.cos(angleRad)) * _circleData.ValueRO.CircleRadius;
			_circleData.ValueRW.PreviousAngle = angle;

			tempTransform.Position = new float3(offset.x, offset.y, 0);
			tempTransform.Rotation = _transform.ValueRO.Rotation;
			tempTransform.Scale = 1.0f;

			return tempTransform;
		}


	}
}