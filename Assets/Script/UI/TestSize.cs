using Script.Players;
using UnityEngine;

namespace Script.UI {
    public class TestSize : MonoBehaviour {
        [SerializeField]private Player playerSizeController;
        
        public void TestSizeUp() {
            playerSizeController.SizeUp();
        }

        public void TestSizeDown() {
            playerSizeController.SizeDown();
        }
    }
}