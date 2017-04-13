using Improbable.Player;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Player
{
    public class ProjectileShooter : MonoBehaviour {

        public Rigidbody projectilePrefab;

        [Require] private PlayerControls.Reader playerControlsReader;

        [SerializeField] private float projectileVelocity = 1.0f;
        // Angle above horizontal in degrees at which to fire the projectile.
        [SerializeField] private float fireAngle = 0.0f;

        private void OnEnable() {
            playerControlsReader.ShootTriggered.Add(OnShoot);
        }

        private void OnDisable() {
            playerControlsReader.ShootTriggered.Remove(OnShoot);
        }

        private void OnShoot(Shoot shootEvent) {
            ShootProjectile();
        }

        private void ShootProjectile() {
            Rigidbody projectile = (Rigidbody)Instantiate(
                projectilePrefab, transform.position + transform.forward, transform.rotation);
            Vector3 velocity = Quaternion.AngleAxis(fireAngle, -transform.right) *
                transform.forward * projectileVelocity;
            projectile.velocity = velocity;
        }
    }
}