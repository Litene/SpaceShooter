using System;
using Components;
using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace MonoBakers {
	public class SpaceAuthor : MonoBehaviour {

		public float AsteroidSpawnsPerSecond;
		public uint AsteroidSpawnSeed;
		public float CircleRadius;
		public float MinAngleDistance;
		public float MaxAngleDistance;
		public float DifficultyMultiplier;
		public float TimePerDifficulty;
		public GameObject AsteroidPrefab;

	}

	public class SpaceBaker : Baker<SpaceAuthor> {

		public override void Bake(SpaceAuthor authoring) {
			var entity = GetEntity(TransformUsageFlags.Dynamic);

			AddComponent(entity,
				new SpaceProperties.SpawnValues {
					Rate = authoring.AsteroidSpawnsPerSecond > 0 ? 1 / authoring.AsteroidSpawnsPerSecond : 1
				});

			AddComponent(entity,
				new SpaceProperties.Prefab {
					Value = GetEntity(authoring.AsteroidPrefab, TransformUsageFlags.Dynamic)
				});
			AddComponent(entity,
				new SpaceProperties.CircleData {
					CircleRadius = authoring.CircleRadius,
					MinAngleDistance = authoring.MinAngleDistance,
					MaxAngleDistance = authoring.MaxAngleDistance
				});
			AddComponent(entity,
				new SpaceProperties.RandomValue {
					Value = Random.CreateFromIndex(authoring.AsteroidSpawnSeed)
				});
			AddComponent(entity,
				new DifficultyComponent {
					DifficultyMultiplier = authoring.DifficultyMultiplier,
					DifficultyTimer = authoring.TimePerDifficulty,
					DifficultyRate = authoring.TimePerDifficulty,
					SpawnsPerSecond = authoring.AsteroidSpawnsPerSecond
				});
		}

	}
}