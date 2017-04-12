using Improbable.Entity.Component;
using Improbable.Player;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;

public class HandleClientDisconnect : MonoBehaviour {

	[Require] private ClientConnection.Writer clientConnectionWriter;

	private void OnEnable() {
		clientConnectionWriter.CommandReceiver.OnDisconnectClient.RegisterAsyncResponse(
			DeletePlayerEntity);
	}

	private void DeletePlayerEntity(
			ResponseHandle<ClientConnection.Commands.DisconnectClient,
			ClientDisconnectRequest, ClientDisconnectResponse> handle) {
		SpatialOS.Commands.DeleteEntity(clientConnectionWriter, gameObject.EntityId(), _ => {});
	}
}
