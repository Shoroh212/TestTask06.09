using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float _distance = 9f;
    [SerializeField] private float _duration = 2f;
    [SerializeField] private float _delay = 3f;
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _firstSlots;
    [SerializeField] private GameObject _secondsSlots;







    private void Start()
    {
        _background = GameObject.Find("Background");
        _firstSlots = GameObject.Find("FirstSlots");
        _secondsSlots = GameObject.Find("SecondSlots");



        StartCoroutine(MoveWithDelay());

    }
    private IEnumerator MoveWithDelay()
    {
        yield return new WaitForSeconds(_delay);
    
        MoveRight();
        MoveLeftSlots();
        MoveRightSecondsSlots();
        Moveobject();
    }

    private void MoveRight()
    {
        _background.transform.DOMoveX(transform.position.x - _distance, _duration)
            .SetEase(Ease.OutCubic);

    }
    private void MoveLeftSlots()
    {
        _firstSlots.transform.DOMoveX(transform.position.x - _distance, _duration)
            .SetEase(Ease.OutCubic);
    }
    private void MoveRightSecondsSlots()
    {
        _secondsSlots.transform.DOMoveX(transform.position.x - 14, _duration)
            .SetEase(Ease.OutCubic);
    }


    private void Moveobject()
    {
        _firstSlots.gameObject.SetActive(false);


    }
}