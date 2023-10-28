using Aspects;
using Components;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Systems {
	[BurstCompile] public partial struct DifficultySystem : ISystem {

		[BurstCompile] public void OnCreate(ref SystemState state) {
			state.RequireForUpdate<SpaceProperties.CircleData>();
		}

		[BurstCompile] public void OnUpdate(ref SystemState state) {
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