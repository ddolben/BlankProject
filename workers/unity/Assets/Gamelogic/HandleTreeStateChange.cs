using Improbable.Tree;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityClient)]
public class HandleTreeStateChange : MonoBehaviour {

    [SerializeField] private GameObject baseTree;
    [SerializeField] private GameObject hitTree;

    [Require] private TreeState.Reader treeStateReader;
	
    private void OnEnable() {
        if (treeStateReader.Data.status == TreeStatus.HIT) {
            SetTreeHit(true);
        }
        treeStateReader.StatusUpdated.Add(OnStatusUpdated);
    }

    private void OnDisable() {
        treeStateReader.StatusUpdated.Remove(OnStatusUpdated);
    }

    private void OnStatusUpdated(TreeStatus status) {
        SetTreeHit(status == TreeStatus.HIT);
    }

    private void SetTreeHit(bool isHit) {
        baseTree.SetActive(!isHit);
        hitTree.SetActive(isHit);
    }
}
