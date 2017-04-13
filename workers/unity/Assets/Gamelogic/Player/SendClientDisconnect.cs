using Improbable.Player;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;

public class SendClientDisconnect : MonoBehaviour {

	[Require] private ClientAuthorityCheck.Writer clientAuthorityCheckWriter;

	private void OnApplicationQuit() {
		if (SpatialOS.IsConnected) {
			SpatialOS.Commands.SendCommand(clientAuthorityCheckWriter,
				ClientConnection.Commands.DisconnectClient.Descriptor,
				new ClientDisconnectRequest(),
				gameObject.EntityId());
		}
	}
}
