using Components;
using Unity.Entities;

namespace Aspects {
	public readonly partial struct StatsAspect : IAspect {

		public readonly Entity Entity;
		
		public readonly RefRW<StatsProperties.AsteroidCount> AsteroidCount;
		public readonly RefRW<StatsProperties.BulletCount> BulletCount;

		public int GetSetBulletCount {
			get => BulletCount.ValueRO.value;
			set => BulletCount.ValueRW.value = value;
		}

		public int GetSetAsteroidCount {
			get => AsteroidCount.ValueRO.value;
			set => AsteroidCount.ValueRW.value = value;
		}
	}
}