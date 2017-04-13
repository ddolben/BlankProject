using Assets.Gamelogic.Extensions;
using Improbable.General;
using Improbable.Math;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[WorkerType(WorkerPlatform.UnityClient)]
public class PlayerMovement : MonoBehaviour {

	[SerializeField] public Vector3 cameraOffset;
	[SerializeField] public float speed = 1.0f;

	private float rotationSpeed = 1.0f;

	// Because of these Require statements, this component will only be enabled on players over
	// which a given client has authority.
	[Require] private ClientAuthorityCheck.Writer clientAuthorityCheckWriter;
	[Require] private WorldTransform.Writer worldTransformWriter;

	void OnEnable() {
		transform.position = worldTransformWriter.Data.position.ToVector3();
		transform.rotation = worldTransformWriter.Data.rotation.ToQuaternion();

		// TODO: move into separate script
		var camera = Camera.main;
		camera.transform.parent = transform;
		camera.transform.localPosition = cameraOffset;
		camera.transform.LookAt(transform.position + Vector3.up);
	}

	void Update () {
		var delta = Time.deltaTime;
		var inputSpeed = Input.GetAxis("Vertical");
		var inputRotation = Input.GetAxis("Horizontal");

		transform.position += transform.forward * speed * inputSpeed;
		transform.RotateAround(transform.position, Vector3.up, rotationSpeed * inputRotation);
	}

	void FixedUpdate() {
		worldTransformWriter.Send(new WorldTransform.Update()
			.SetPosition(transform.position.ToCoordinates())
			.SetRotation(transform.rotation.ToRotation()));
	}
}
