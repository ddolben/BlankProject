using Improbable.Player;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;

public class SendClientHeartbeat : MonoBehaviour {

	[Require] private ClientAuthorityCheck.Writer clientAuthorityCheckWriter;

	[SerializeField] private float heartbeatInterval;

	private void OnEnable() {
		InvokeRepeating("SendHeartbeat", 0, heartbeatInterval);
	}

	private void OnDisable() {
		CancelInvoke("SendHeartbeat");
	}

	private void SendHeartbeat() {
		SpatialOS.Commands.SendCommand(clientAuthorityCheckWriter,
			ClientConnection.Commands.Heartbeat.Descriptor,
			new HeartbeatRequest(),
			gameObject.EntityId());
	}
}
