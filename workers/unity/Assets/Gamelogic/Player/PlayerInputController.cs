using Improbable.Player;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Player
{
    public class PlayerInputController : MonoBehaviour {

        [Require] PlayerControls.Writer playerControlsWriter;
        
        void Update() {
            if (Input.GetButtonUp("Fire1")) {
                playerControlsWriter.Send(new PlayerControls.Update().AddShoot(new Shoot()));
            }
        }
    }
}