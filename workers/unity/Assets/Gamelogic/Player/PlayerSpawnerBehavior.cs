using Assets.EntityTemplates;
using Improbable;
using Improbable.Entity.Component;
using Improbable.Math;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Gamelogic.Behaviours
{
	[WorkerType(WorkerPlatform.UnityWorker)]
	public class PlayerSpawnerBehavior : MonoBehaviour {

		[Require] private PlayerSpawner.Writer playerSpawnerWriter;

		private void OnEnable() {
			playerSpawnerWriter.CommandReceiver.OnSpawnPlayer.RegisterResponse(OnSpawnPlayer);
		}

		private void OnDisable() {
			playerSpawnerWriter.CommandReceiver.OnSpawnPlayer.DeregisterResponse();
		}

		private SpawnPlayerResponse OnSpawnPlayer(SpawnPlayerRequest request, ICommandCallerInfo callerinfo) {
			SpawnPlayer(callerinfo.CallerWorkerId);
			return new SpawnPlayerResponse();
		}

		private void SpawnPlayer(string clientWorkerId) {
			Debug.Log("Attempting to spawn player...");
			var playerEntityTemplate =
				PlayerTemplateFactory.GeneratePlayerTemplate(clientWorkerId);
			SpatialOS.Commands.CreateEntity(playerSpawnerWriter, "Player", playerEntityTemplate)
				.OnSuccess(entityId => Debug.Log(
					"Created player with ID: " + entityId + " for client: " + clientWorkerId))
				.OnFailure(failure => OnFailure(clientWorkerId, failure.ErrorMessage));
		}

		private void OnFailure(string clientWorkerId, string errorMessage) {
			Debug.LogError("Failed to Create Player Entity: " + errorMessage + ". Retrying...");
			SpawnPlayer(clientWorkerId);
		}
	}
}