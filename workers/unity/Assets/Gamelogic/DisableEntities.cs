using Improbable.Player;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    // Disables all specified entities when the client connects to SpatialOS.
    public class DisablesEntities : MonoBehaviour {

        // Only enable this script on the player.
        [Require]
        private ClientAuthorityCheck.Writer clientAuthorityCheckWriter;

        public GameObject[] entities;

        private void OnEnable() {
            foreach (GameObject entity in entities) {
                entity.SetActive(false);
            }
        }
    }
}