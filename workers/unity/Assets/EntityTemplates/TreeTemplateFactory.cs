using Improbable.General;
using Improbable.Math;
using Improbable.Worker;
using Improbable.Unity.Core.Acls;

namespace Assets.EntityTemplates
{
    public class TreeTemplateFactory {
        public static SnapshotEntity GenerateTreeTemplate(Coordinates coordinates) {
            var treeTemplate = new SnapshotEntity { Prefab = "Tree" };

            treeTemplate.Add(new WorldTransform.Data(
                    coordinates,
                    new Rotation(0, 0, 0, 1)));

            var acl = Acl.Build()
                .SetReadAccess(CommonRequirementSets.PhysicsOrVisual)
                .SetWriteAccess<WorldTransform>(CommonRequirementSets.PhysicsOnly);
            treeTemplate.SetAcl(acl);

            return treeTemplate;
        }
    }
}