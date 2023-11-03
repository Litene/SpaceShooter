using Unity.Entities;
using Unity.Mathematics;

namespace Components {
	public struct AsteroidProperties {
		public partial struct Tag : IComponentData { }

		public partial struct MoveSpeed : IComponentData {
			public float Value;
		}
	}
}