using System;
using System.Collections;
using System.Collections.Generic;
using Aspects;
using Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace MonoBehavior {
	public class CollisionMonobehaviour : MonoBehaviour {
		// private Entity _asteroidCollisionEntity;
		// public List<Entity> Entities;
		// public List<AsteroidAspect> Aspects;
		//
		// private void Update() {
		// 	EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;
		// 	EntityQuery asteroidEntity = manager.CreateEntityQuery(typeof(AsteroidProperties.Tag));
		// 	Entities = new List<Entity>();
		// 	foreach (var entity in asteroidEntity.ToEntityArray(Allocator.Temp)) {
		// 		Entities.Add(entity);
		// 	}
		// 	
		// 	foreach (var entity in Entities) {
		// 		var asteroidAspect = manager.GetAspect<AsteroidAspect>(entity);
		// 		foreach (var entity2 in Entities) {
		// 			EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
		// 			var asteroidAspect2 = manager.GetAspect<AsteroidAspect>(entity2);
		// 			if (asteroidAspect.Entity != asteroidAspect2.Entity) {
		// 				if (Vector3.Distance(
		// 					    new Vector3(asteroidAspect.GetTransform.Position.x, asteroidAspect.GetTransform.Position.y, 0),
		// 					    new Vector3(asteroidAspect2.GetTransform.Position.x, asteroidAspect2.GetTransform.Position.y, 0)) < 1.0f) {
		// 					asteroidAspect.DestroyAsteroid(ecb, asteroidAspect.Entity);
		// 					asteroidAspect2.DestroyAsteroid(ecb, asteroidAspect2.Entity);
		// 				}
		// 			}
		// 		}
		// 	}
		// }

	}
}