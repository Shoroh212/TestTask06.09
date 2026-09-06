using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PartsSystem : MonoBehaviour,
    IDragHandler,
    IBeginDragHandler,
    IEndDragHandler
{
    [Header("References")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _image;

    [Header("Item")]
    [SerializeField] private ItemDefinition _definition;

    [Header("Settings")]
    [SerializeField] private int _count;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _pickUpSound;
    [SerializeField] private AudioClip _dropSound;

    private RectTransform _rectTransform;

    private Vector2 _offset;
    private Vector3 _oldPosition;

    private bool _wasDropped;

    public ItemDefinition Definition => _definition;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

        if (_image == null)
            _image = GetComponent<Image>();

        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();
        _canvas = GetComponentInParent<Canvas>();


    }



    public void Initialize(ItemDefinition definition)
    {
        _definition = definition;

        if (_image != null && definition != null)
            _image.sprite = definition.Icon;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _wasDropped = false;
        _oldPosition = transform.position;

        RectTransform canvasRect =
            _canvas.transform as RectTransform;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );

        _offset = _rectTransform.anchoredPosition - localPoint;

        if (_pickUpSound != null && _audioSource != null)
            _audioSource.PlayOneShot(_pickUpSound);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform canvasRect =
            _canvas.transform as RectTransform;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );

        _image.raycastTarget = false;

        _rectTransform.anchoredPosition =
            localPoint + _offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_image != null)
            _image.raycastTarget = true;

        if (!_wasDropped)
            transform.position = _oldPosition;

        if (_dropSound != null && _audioSource != null)
            _audioSource.PlayOneShot(_dropSound);
    }

    public void SetDropped()
    {
        _wasDropped = true;
    }
}