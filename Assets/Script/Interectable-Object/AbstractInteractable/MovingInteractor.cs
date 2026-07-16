using System;
using System.Collections.Generic;
using PrimeTween;
using Script.Players.Components;
using UnityEngine;

namespace Script.Interectable_Object.AbstractInteractable
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
        
        public void Interact(InteractManager owner)
        {
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
        }
    }

    [Serializable]
    public class MovePositionEntry
    {
        [field: SerializeField] public Vector3 Position { get; private set; }
        [field: SerializeField] public float Delay { get; private set; }
        [field:SerializeField] public Ease EasingType { get; private set; }
    }
}