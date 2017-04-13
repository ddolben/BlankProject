using Improbable;
using Improbable.Collections;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Configuration;
using Improbable.Unity.Core;
using Improbable.Unity.Core.EntityQueries;
using Improbable.Worker;
using Improbable.Worker.Query;
using System;
using UnityEngine;


// Placed on a gameobject in client scene to execute connection logic on client startup
public class Bootstrap : MonoBehaviour
{
    public WorkerConfigurationData Configuration = new WorkerConfigurationData();

    public void Start()
    {
        SpatialOS.ApplyConfiguration(Configuration);

        switch (SpatialOS.Configuration.WorkerPlatform)
        {
            case WorkerPlatform.UnityWorker:
                SpatialOS.OnDisconnected += reason => Application.Quit();

                var targetFramerate = 120;
                var fixedFramerate = 20;

                Application.targetFrameRate = targetFramerate;
                Time.fixedDeltaTime = 1.0f / fixedFramerate;

				SpatialOS.Connect(gameObject);
                break;
            case WorkerPlatform.UnityClient:
                SpatialOS.OnConnected += OnConnected;
                break;
        }
    }

	public void AttemptToConnectClient() {
		SpatialOS.Connect(gameObject);
	}

    public void OnConnected() {
		FindPlayerSpawnerEntity(RequestPlayerSpawn);
    }

	private static void FindPlayerSpawnerEntity(Action<EntityId> spawnRequestCallback) {
		Debug.Log("Finding player spawner...");
		var playerSpawnerQuery = Query.HasComponent<PlayerSpawner>().ReturnOnlyEntityIds();
		SpatialOS.WorkerCommands.SendQuery(playerSpawnerQuery)
			.OnSuccess(OnSuccessfulQuery)
			.OnFailure(failure => Debug.Log("Failed to find PlayerSpawner."));
	}

	private static void OnSuccessfulQuery(EntityQueryResult queryResult) {
		if (queryResult.EntityCount < 1) {
			Debug.LogError("Failed to find PlayerSpawner.");
			return;
		}

		Map<EntityId, Entity> resultMap = queryResult.Entities;
		var playerSpawnerEntityId = resultMap.First.Value.Key;
		RequestPlayerSpawn(playerSpawnerEntityId);
	}

	private static void RequestPlayerSpawn(EntityId playerSpawnerEntityId) {
		Debug.Log("Requesting player spawn...");
		SpatialOS.WorkerCommands.SendCommand(PlayerSpawner.Commands.SpawnPlayer.Descriptor,
			new SpawnPlayerRequest(),
			playerSpawnerEntityId);
	}
}