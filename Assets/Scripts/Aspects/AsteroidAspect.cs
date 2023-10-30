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

		private readonly RefRO<AsteroidProperties.Tag> _tag;

		private readonly RefRO<AsteroidProperties.MoveSpeed> _moveSpeed;

		public void Move(float deltaTime) {
			_transform.ValueRW.Rotation = GetRotation();
			_transform.ValueRW.Position -= _transform.ValueRO.Up() * _moveSpeed.ValueRO.Value * deltaTime;
		}

		public void DestroyAsteroid(EntityCommandBuffer ecb, Entity entity) {
			ecb.DestroyEntity(entity);
		}

		public LocalTransform GetTransform {
			get => _transform.ValueRO;
			set => _transform.ValueRW = value;
		}

		public quaternion GetRotation() {
			var normalizedVector = new Vector2(_transform.ValueRW.Position.x, _transform.ValueRW.Position.y).normalized;
			var lookRotation =
				quaternion.LookRotationSafe(new float3(0, 0, 1), new float3(normalizedVector.x, normalizedVector.y, 0));

			return lookRotation;
		}

	}
}