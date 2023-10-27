using Aspects;
using Components;
using Unity.Entities;
using UnityEngine;

namespace Systems {
	public partial struct DifficultySystem : ISystem {

		public void OnCreate(ref SystemState state) {
			state.RequireForUpdate<SpaceProperties.CircleData>();
		}

		public void OnUpdate(ref SystemState state) {
			var spaceEntity = SystemAPI.GetSingletonEntity<SpaceProperties.CircleData>();
			var spaceAspect = SystemAPI.GetAspect<SpaceAspect>(spaceEntity);
			spaceAspect.DifficultyTimer -= Time.deltaTime;
			if (spaceAspect.DifficultyTimer <= 0) {
				spaceAspect.DifficultyTimer = spaceAspect.DifficultyRate;
				spaceAspect.CurrentSpawnsPerSecond *= spaceAspect.DifficultyMultiplier;
				spaceAspect.CurrentDifficulty++;
			}
		}
	}
}