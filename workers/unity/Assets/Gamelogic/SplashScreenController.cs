using Improbable.Unity.Core;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Gamelogic
{
    public class SplashScreenController : MonoBehaviour {

        // An object that will be disabled when the splash screen gets turned off. Use for
        // displaying a scene behind the splash screen.
        [SerializeField] private GameObject splashScene;

        [SerializeField] private GameObject failureWarning;
        [SerializeField] private Button connectButton;

        private static SplashScreenController instance;
        private const string gameEntryGameObjectName = "GameEntry";

        private void Awake() {
            instance = this;
        }

        public void AttemptToConnect() {
            DisableConnectButton();
            instance.failureWarning.SetActive(false);
            instance.AttemptConnection();
        }

        private void DisableConnectButton() {
            connectButton.interactable = false;
        }

        private void AttemptConnection() {
            if (!GameObject.Find(gameEntryGameObjectName).GetComponent<Bootstrap>()) {
                throw new Exception("Couldn't find Bootstrap script on GameEntry in ClientScene");
            }
            Bootstrap bootstrap = GameObject.Find(gameEntryGameObjectName).GetComponent<Bootstrap>();
            bootstrap.AttemptToConnectClient();
			StartCoroutine(StartConnectionTimeout(Settings.clientConnectionTimeout));
        }

		private IEnumerator StartConnectionTimeout(int timeout) {
			yield return new WaitForSeconds(timeout);
			ConnectionTimeout();
		}

        private void ConnectionTimeout() {
            if (SpatialOS.IsConnected) {
                SpatialOS.Disconnect();
            }
            instance.failureWarning.SetActive(true);
            connectButton.interactable = true;
        }

        public static void HideSplashScreen() {
            instance.failureWarning.SetActive(false);
            instance.splashScene.SetActive(false);

            // This will prevent ConnectionTimeout from being called by the coroutine from
            // AttemptConnection().
            instance.gameObject.SetActive(false);
        }
    }
}
