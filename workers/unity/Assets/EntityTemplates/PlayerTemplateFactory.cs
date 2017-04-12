using Improbable.General;
using Improbable.Math;
using Improbable.Player;
using Improbable.Unity.Core.Acls;
using Improbable.Worker;
using UnityEngine;

namespace Assets.EntityTemplates
{
	public class PlayerTemplateFactory {
		public static Entity GeneratePlayerTemplate(string clientWorkerId) {
			var playerTemplate = new SnapshotEntity { Prefab = "Player" };

			playerTemplate.Add(new WorldTransform.Data(
				new Coordinates(0, 0.5f, 0),
				new Rotation(0, 0, 0, 1)));
			playerTemplate.Add(new ClientAuthorityCheck.Data());

			var acl = Acl.Build()
				.SetReadAccess(CommonRequirementSets.PhysicsOrVisual)
				.SetWriteAccess<WorldTransform>(CommonRequirementSets.SpecificClientOnly(clientWorkerId))
				.SetWriteAccess<ClientAuthorityCheck>(CommonRequirementSets.SpecificClientOnly(clientWorkerId));
			playerTemplate.SetAcl(acl);

			return playerTemplate;
		}
	}
}