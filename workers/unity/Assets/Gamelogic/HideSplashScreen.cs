using Improbable.Player;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    // Contains logic to be run when the player is connected.
    public class HideSplashScreen : MonoBehaviour {

        // Only enable this script on clients with write authority.
        [Require] private ClientAuthorityCheck.Writer clientAuthorityCheckWriter;

        private void OnEnable() {
            // Disable the splash screen since we've successfully spawned the player.
            SplashScreenController.HideSplashScreen();
        }
    }
}