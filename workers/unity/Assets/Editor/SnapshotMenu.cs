using Assets.EntityTemplates;
using Improbable;
using Improbable.Math;
using Improbable.Worker;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SnapshotMenu : MonoBehaviour
{
    private static readonly string InitialWorldSnapshotPath = Application.dataPath +
                                                              "/../../../snapshots/initial_world.snapshot";

    [MenuItem("Improbable/Snapshots/Generate Snapshot Programmatically")]
    [UsedImplicitly]
    private static void GenerateSnapshotProgrammatically()
    {
        var snapshotEntities = new Dictionary<EntityId, SnapshotEntity>();
        var currentEntityId = 1;

		snapshotEntities.Add(new EntityId(currentEntityId++),
			PlayerSpawnerTemplateFactory.GeneratePlayerSpawnerTemplate());

        // Generate a square grid of trees centered at (0,0,0), along the x-z plane.
        const int treeRowCount = 10;
        const float maxDistanceFromOrigin = 50;
        const float distanceStep = (maxDistanceFromOrigin * 2.0f) / (float)treeRowCount;
        for (int y = 0; y < treeRowCount; y++) {
            for (int x = 0; x < treeRowCount; x++) {
                Coordinates location = new Coordinates(
                    (x * distanceStep) - maxDistanceFromOrigin,
                    0,
                    (y * distanceStep) - maxDistanceFromOrigin);
                snapshotEntities.Add(new EntityId(currentEntityId++),
                    TreeTemplateFactory.GenerateTreeTemplate(location));
            }
        }

        SaveSnapshot(snapshotEntities);
    }

    private static void SaveSnapshot(IDictionary<EntityId, SnapshotEntity> snapshotEntities)
    {
        File.Delete(InitialWorldSnapshotPath);
        var maybeError = Snapshot.Save(InitialWorldSnapshotPath, snapshotEntities);

        if (maybeError.HasValue)
        {
            Debug.LogErrorFormat("Failed to generate initial world snapshot: {0}", maybeError.Value);
        }
        else
        {
            Debug.LogFormat("Successfully generated initial world snapshot at {0}", InitialWorldSnapshotPath);
        }
    }
}
