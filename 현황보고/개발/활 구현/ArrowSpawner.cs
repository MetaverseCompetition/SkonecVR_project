using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrow;
    public GameObject notch;
    public Transform InitCamera;

    private XRGrabInteractable _bow;
    private bool _arrowNotched = false;
    private GameObject _currentArrow = null;


    private void Start()
    {
        _bow = GetComponent<XRGrabInteractable>();
        PullInteraction.PullActionReleased += NotchEmpty;

    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= NotchEmpty;
    }

    private void Update()
    {
        if(_bow.isSelected&& _arrowNotched ==false)
        {
            _arrowNotched = true;
            StartCoroutine("DelayedSpawn");
        }

        if(!_bow.isSelected && _currentArrow!=null)
        {
            Destroy(_currentArrow);
            NotchEmpty(1f);
        }
    }

    private void NotchEmpty(float value)
    {
        _arrowNotched = false;
        _currentArrow = null;
    }

    IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(1f);
        _currentArrow = Instantiate(arrow, notch.transform);
    }
}
