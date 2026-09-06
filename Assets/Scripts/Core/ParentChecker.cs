using UnityEngine;

public class ParentChecker : MonoBehaviour
{
    [SerializeField] private GameObject _objectToEnable;

    private void OnTransformParentChanged()
    {
        if (transform.parent != null)
        {
            _objectToEnable.SetActive(true);
        }
    }
}