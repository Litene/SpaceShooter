using Unity.Entities;
using Unity.Mathematics;

namespace Components {
	public struct AsteroidProperties {
		public partial struct CircleData : IComponentData {
			public float MinAngleDistance;
			public float MaxAngleDistance;
			public float PreviousAngle;
			public float CircleRadius;
		}
		
		public partial struct RandomValue : IComponentData {
			public Random Value;
		}
	}
}