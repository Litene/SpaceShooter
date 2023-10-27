using Unity.Entities;

namespace Components {
	public struct StatsProperties {

		public partial struct AsteroidCount : IComponentData {
			public int value;
		}

		public partial struct BulletCount : IComponentData {
			public int value;
		}
		
		public partial struct Tag : IComponentData { }

	}
}