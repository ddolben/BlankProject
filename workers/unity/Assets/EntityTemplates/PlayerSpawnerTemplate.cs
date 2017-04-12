using Improbable.General;
using Improbable.Math;
using Improbable.Player;
using Improbable.Unity.Core.Acls;
using Improbable.Worker;
using UnityEngine;

namespace Assets.EntityTemplates
{
	public class PlayerSpawnerTemplate {
		public static SnapshotEntity GeneratePlayerSpawnerTemplate() {
			var playerSpawnerTemplate = new SnapshotEntity { Prefab = "PlayerSpawner" };

			playerSpawnerTemplate.Add(new WorldTransform.Data(
				Coordinates.ZERO, new Rotation(0.0f,0.0f,0.0f,1.0f)));
			playerSpawnerTemplate.Add(new PlayerSpawner.Data());

			var acl = Acl.GenerateServerAuthoritativeAcl(playerSpawnerTemplate);
			playerSpawnerTemplate.SetAcl(acl);

			return playerSpawnerTemplate;
		}
	}
}