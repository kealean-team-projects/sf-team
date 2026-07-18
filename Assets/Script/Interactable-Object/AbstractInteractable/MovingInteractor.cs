using System;
using System.Collections.Generic;
using PrimeTween;
using Script.Players.Components;
using UnityEngine;

namespace Script.Interactable_Object.AbstractInteractable
{
    public class MovingInteractor : MonoBehaviour, IInteractable
    {
        [SerializeField] private bool isLoop;
        [Header("True = Rewind, False = Reset")]
        [SerializeField] private bool loopType;
        [Header("Index 0 is First Position")]
        [SerializeField] private List<MovePositionEntry> movePositions;

        private bool _loopDirection; //false = straight, true = back
        private int _currentIndex = 1;
        private bool _stopMoving;
        private int _beforeLenght;
        protected InteractManager Owner;
        private bool _checkPos = true;

        protected virtual void Awake()
        {
            _checkPos = false;
        }

        public void Interact(InteractManager owner)
        {
            Owner = owner;
            if (_stopMoving) return;
            if (!isLoop)
            {
                Tween.Position(transform, movePositions[_currentIndex].Position, movePositions[_currentIndex].Delay,
                    movePositions[_currentIndex].EasingType);
                _currentIndex++;
                if (_currentIndex == movePositions.Count) _stopMoving = true;
            }
            else
            {
                if (loopType)
                {
                    if (!_loopDirection)
                    {
                        Tween.Position(transform, movePositions[_currentIndex].Position, movePositions[_currentIndex].Delay,
                            movePositions[_currentIndex].EasingType);
                        _currentIndex++;
                        if (_currentIndex == movePositions.Count)
                        {
                            _loopDirection = true;
                            _currentIndex--;
                        }
                    }
                    else
                    {
                        _currentIndex--;
                        Tween.Position(transform, movePositions[_currentIndex].Position, movePositions[_currentIndex].Delay,
                            movePositions[_currentIndex].EasingType);
                        if (_currentIndex == 0)
                        {
                            _loopDirection = false;
                            _currentIndex++;
                        }
                    }
                }
                else
                {
                    if (_currentIndex == movePositions.Count) _currentIndex = 0;
                    Tween.Position(transform, movePositions[_currentIndex].Position, movePositions[_currentIndex].Delay,
                        movePositions[_currentIndex].EasingType);
                    _currentIndex++;
                }
                    
            }

            InteractEffect();
        }

        public void SpecialInteract(InteractManager owner)
        {
            Owner = owner;
            SpecialInteractEffect();
        }

        protected virtual void SpecialInteractEffect()
        {
            
        }

        protected virtual void InteractEffect()
        {
        }

        [ContextMenu("ResetPositionToFirstPosition")]
        private void MoveFirstPos()
        {
            transform.position = movePositions[0].Position;
        }

        [ContextMenu("SetCurrentPos")]
        private void SetCurrentPos()
        {
            movePositions.Add(new MovePositionEntry());
            movePositions[^1].SetPos(transform.position);
        }
    }

    [Serializable]
    public class MovePositionEntry
    {
        [field: SerializeField] public Vector3 Position { get; private set; }
        [field: SerializeField] public float Delay { get; private set; }
        [field:SerializeField] public Ease EasingType { get; private set; }

        public void SetPos(Vector3 value)
        {
            Position = value;
        }
    }
}