using Assets.Gamelogic.Extensions;
using Improbable.General;
using Improbable.Math;
using Improbable.Unity.Visualizer;
using UnityEngine;


namespace Assets.Gamelogic.Behaviours
{
    // This MonoBehaviour will be enabled on both client and server-side workers
    public class TransformReceiver : MonoBehaviour
    {
        // Inject access to the entity's WorldTransform component
        [Require]
        private WorldTransform.Reader worldTransformReader;

        void OnEnable() {
            // Initialize entity's gameobject transform from WorldTransform component values
            transform.position = worldTransformReader.Data.position.ToVector3();
			transform.rotation = worldTransformReader.Data.rotation.ToQuaternion();

            // Register callback for when component changes
            worldTransformReader.ComponentUpdated.Add(OnComponentUpdated);
        }

        void OnDisable() {
            // Deregister callback for when component changes
            worldTransformReader.ComponentUpdated.Remove(OnComponentUpdated);
        }

        // Callback for whenever one or more property of the WorldTransform component is updated
        void OnComponentUpdated(WorldTransform.Update update) {
            /* 
             * Only update the transform if this component is on a worker which isn't authorative over the
             * entity's WorldTransform component.
             * This synchronises the entity's local representation on the worker with that of the entity on
             * whichever worker is authoritative over its WorldTransform and is responsible for its movement.
             */
            if (!worldTransformReader.HasAuthority)
            {
				if (update.position.HasValue) {
					transform.position = update.position.Value.ToVector3();
				}
				if (update.rotation.HasValue) {
					transform.rotation = update.rotation.Value.ToQuaternion();
				}
            }
        }
    }
}