using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects {
	public readonly partial struct BulletAspect : IAspect {
		public readonly Entity Entity;
		
		private readonly RefRO<BulletProperties.MoveSpeed> _moveSpeed;
		private readonly RefRW<LocalTransform> _transform;
	
		
		public float3 Position {
			get => _transform.ValueRO.Position;
			set => _transform.ValueRW.Position = value;
		}

		public quaternion Rotation {
			get => _transform.ValueRO.Rotation;
			set => _transform.ValueRW.Rotation = value;
		}

		public float MoveSpeed => _moveSpeed.ValueRO.Value;

		public void MoveBullet(float deltaTime) {
			_transform.ValueRW.Position += _transform.ValueRO.Up() * _moveSpeed.ValueRO.Value * deltaTime;
		}
		
		

	}
}