using System;
using Components;
using TMPro;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace MonoBehavior {
	public class ScreenSpaceUI : MonoBehaviour {

		[SerializeField] private TextMeshProUGUI _totalEntitiesText;
		[SerializeField] private TextMeshProUGUI _totalAsteroidText;
		[SerializeField] private TextMeshProUGUI _totalBulletsText;
		[SerializeField] private TextMeshProUGUI _fpsText;
		[SerializeField] private TextMeshProUGUI _currentDifficultyText;
		[SerializeField] private TextMeshProUGUI _TimeTillNextDifficultyText;
		[SerializeField] private TextMeshProUGUI _CurrentSpawnRateText;

		[SerializeField] private float _hudRefreshRate = 1f;

		private float _timer;

		private EntityManager _manager;

		private Entity _statsEntity;
		private Entity _spaceEntity;


		private void Start() {
			_manager = World.DefaultGameObjectInjectionWorld.EntityManager;
			_statsEntity = _manager.CreateEntityQuery(typeof(StatsProperties.Tag)).GetSingletonEntity();
			_spaceEntity = _manager.CreateEntityQuery(typeof(DifficultyComponent)).GetSingletonEntity();
			Application.targetFrameRate = 2000;
		}

		private void Update() {
			if (Time.unscaledTime > _timer) {
				int fps = (int)(1f / Time.unscaledDeltaTime);
				_fpsText.text = "FPS: " + fps;
				_timer = Time.unscaledTime + _hudRefreshRate;
			}

			int asteroidCount = _manager.GetComponentData<StatsProperties.AsteroidCount>(_statsEntity).value;
			int bulletCount = _manager.GetComponentData<StatsProperties.BulletCount>(_statsEntity).value;
			int totalCount = asteroidCount + bulletCount;

			int currentDifficulty = _manager.GetComponentData<DifficultyComponent>(_spaceEntity).CurrentDifficulty;
			float timeTillNextDifficulty = _manager.GetComponentData<DifficultyComponent>(_spaceEntity).DifficultyTimer;
			float currentSpawnsPerSecond = _manager.GetComponentData<DifficultyComponent>(_spaceEntity).SpawnsPerSecond;

			_currentDifficultyText.text = "Current difficulty: " + currentDifficulty.ToString();
			_TimeTillNextDifficultyText.text = "Difficulty increase in: " + $"{timeTillNextDifficulty:F1}s";
			_CurrentSpawnRateText.text = "Current spawn rate: " + $"{currentSpawnsPerSecond:F1}/s";
			_totalEntitiesText.text = "Total entity count: " + totalCount.ToString();
			_totalAsteroidText.text = "Total asteroid count: " + asteroidCount.ToString();
			_totalBulletsText.text = "Total bullets count: " + bulletCount.ToString();
		}

	}
}