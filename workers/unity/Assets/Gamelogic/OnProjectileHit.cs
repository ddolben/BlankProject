using Improbable.Tree;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Player
{
    [WorkerType(WorkerPlatform.UnityWorker)]
    public class OnProjectileHit : MonoBehaviour {

        [Require] private TreeState.Writer treeStateWriter;

        private void OnTriggerEnter(Collider other) {
            if (treeStateWriter == null) return;
            if (treeStateWriter.Data.status == TreeStatus.HIT) return;
            if (other.tag == "Projectile") {
                treeStateWriter.Send(new TreeState.Update().SetStatus(TreeStatus.HIT));
            }
        }
    }
}