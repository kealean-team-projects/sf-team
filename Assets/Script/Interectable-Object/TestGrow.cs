using System;
using Script.Players;
using Script.Players.Components;
using UnityEngine;

namespace Script.Interectable_Object {
    public class TestGrow : MonoBehaviour, IInteractable {
        private Player _player;

        private void Awake() {
            _player = FindAnyObjectByType<Player>();
        }

        public void Interact(InteractManager owner) {
            owner.Player.ChangeSize();
        }
    }
}