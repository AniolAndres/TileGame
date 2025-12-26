using System;
using Assets.ScreenMachine;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Assets.Views
{
    public class TurnStartOverlayUiView : UiView
    {
        [SerializeField] 
        private TextMeshProUGUI roundDescription;

        [SerializeField] 
        private int positionDistance;
        
        private Sequence sequence;

        public event Action OnMovementCompleted;

        public void Setup(int roundNumber, string playerName)
        {
            var textTransform = roundDescription.transform;
            var originPosition = textTransform.localPosition;
            textTransform.localPosition = originPosition + Vector3.left * positionDistance;
            var endPosition = originPosition - Vector3.left * positionDistance;
            roundDescription.SetText($"{playerName} - Round {roundNumber}".ToUpperInvariant());
            sequence = DOTween.Sequence();
            sequence.Append(textTransform.DOLocalMove(originPosition, 0.5f).SetEase(Ease.OutBack));
            sequence.AppendInterval(2f);
            sequence.Append(textTransform.DOLocalMove(endPosition, 0.5f).SetEase(Ease.InBack));
            sequence.OnComplete(FireCompleteEvent);
            sequence.Play();
        }

        private void FireCompleteEvent()
        {
            OnMovementCompleted?.Invoke();
        }

        private void OnDestroy()
        {
            sequence?.Kill();
        }
    }
}