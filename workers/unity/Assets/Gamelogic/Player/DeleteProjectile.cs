using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Gamelogic.Player
{
    public class DeleteProjectile : MonoBehaviour {

        private bool didHit = false;

        private void FixedUpdate() {
            if (transform.position.y < 0 || didHit) {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other) {
            // Delete the object on the next FixedUpdate. We do this so _all_
            // OnTriggerEnter handlers get executed before deleting the object.
            didHit = true;
        }
    }
}