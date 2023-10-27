using Unity.Entities;

namespace Components {
	public partial struct DifficultyComponent : IComponentData {
		public float SpawnsPerSecond;
		public float DifficultyTimer;
		public float DifficultyRate;
		public float DifficultyMultiplier;
		public int CurrentDifficulty;

	}
}