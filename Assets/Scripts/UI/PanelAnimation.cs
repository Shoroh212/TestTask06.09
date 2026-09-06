using DG.Tweening;
using UnityEngine;

public class PanelAnimation : MonoBehaviour
{
    [SerializeField] private float targetY = 0f;
    [SerializeField] private float fallDuration = 0.6f;
    [SerializeField] private float bounceHeight = 20f;
    [SerializeField] private float bounceDuration = 0.12f;

    [SerializeField] private GameObject _hand;

    private void Start()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(
            transform.DOMoveY(targetY, fallDuration)
                .SetEase(Ease.InQuad)
        );

        sequence.Append(
            transform.DOMoveY(targetY + bounceHeight, bounceDuration)
                .SetEase(Ease.OutQuad)
        );

        sequence.Append(
            transform.DOMoveY(targetY, bounceDuration)
                .SetEase(Ease.InQuad)
        );


        // Движение руки по локальной диагонали
        Vector3 handStart = _hand.transform.localPosition;
        Vector3 handEnd = handStart + new Vector3(6f, -6f, 0f);

        Sequence handSequence = DOTween.Sequence();

        handSequence.Append(
            _hand.transform.DOLocalMove(handEnd, 0.3f)
                .SetEase(Ease.InOutQuad)
        );

        handSequence.Append(
            _hand.transform.DOLocalMove(handStart, 0.3f)
                .SetEase(Ease.InOutQuad)
        );

        handSequence.SetLoops(-1);
    }


}