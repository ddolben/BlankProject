using Assets.Gamelogic;
using Improbable.Entity.Component;
using Improbable.Player;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;

public class HandleClientHeartbeat : MonoBehaviour {

	[Require] private ClientConnection.Writer clientConnectionWriter;

	private void OnEnable() {
		clientConnectionWriter.CommandReceiver.OnHeartbeat.RegisterResponse(OnHeartbeat);
		InvokeRepeating("CheckHeartbeat", Settings.heartbeatInterval, Settings.heartbeatInterval);
	}

	private void OnDisable() {
		clientConnectionWriter.CommandReceiver.OnHeartbeat.DeregisterResponse();
		CancelInvoke("CheckHeartbeat");
	}

	private HeartbeatResponse OnHeartbeat(HeartbeatRequest request, ICommandCallerInfo callerinfo) {
		// Set the heartbeat counter back to maximum.
		SetHeartbeats(Settings.heartbeatTimeoutCount);

		return new HeartbeatResponse();
	}

	private void CheckHeartbeat() {
		var heartbeatsRemaining = clientConnectionWriter.Data.timeoutBeatsRemaining;
		if (heartbeatsRemaining <= 0) {
			CancelInvoke("CheckHeartbeat");
			SpatialOS.Commands.DeleteEntity(clientConnectionWriter, gameObject.EntityId(), _ => {});
			return;
		}
		SetHeartbeats(heartbeatsRemaining - 1);
	}

	private void SetHeartbeats(uint beats) {
		var update = new ClientConnection.Update();
		update.SetTimeoutBeatsRemaining(beats);
		clientConnectionWriter.Send(update);
	}
}
