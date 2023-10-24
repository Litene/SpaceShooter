using Unity.Entities;

namespace Components {
	public struct BulletProperties {

		public partial struct MoveSpeed : IComponentData {
			public float Value;
		}
	}
}