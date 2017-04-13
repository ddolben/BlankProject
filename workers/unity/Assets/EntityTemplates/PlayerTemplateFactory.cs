using Assets.Gamelogic;
using Improbable.General;
using Improbable.Math;
using Improbable.Player;
using Improbable.Unity.Core.Acls;
using Improbable.Worker;

namespace Assets.EntityTemplates
{
	public class PlayerTemplateFactory {
		public static Entity GeneratePlayerTemplate(string clientWorkerId) {
			var playerTemplate = new Entity();

			playerTemplate.Add(new WorldTransform.Data(
				new Coordinates(0, 0.5f, 0),
				new Rotation(0, 0, 0, 1)));
			playerTemplate.Add(new ClientAuthorityCheck.Data());
			playerTemplate.Add(new ClientConnection.Data(Settings.heartbeatTimeoutCount));
            playerTemplate.Add(new PlayerControls.Data());

			var acl = Acl.Build()
				.SetReadAccess(CommonRequirementSets.PhysicsOrVisual)
				.SetWriteAccess<WorldTransform>(CommonRequirementSets.SpecificClientOnly(clientWorkerId))
				.SetWriteAccess<ClientAuthorityCheck>(CommonRequirementSets.SpecificClientOnly(clientWorkerId))
				.SetWriteAccess<ClientConnection>(CommonRequirementSets.PhysicsOnly)
                .SetWriteAccess<PlayerControls>(CommonRequirementSets.SpecificClientOnly(clientWorkerId));
			playerTemplate.SetAcl(acl);

			return playerTemplate;
		}
	}
}