using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public Transform tip;

    private Rigidbody _rigidBody;
    private bool _inAir = false;
    private Vector3 _lastPosition = Vector3.zero;

    public GameObject HitEeffect;

    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _inAir = false;

        PullInteraction.PullActionReleased += Release;

        _trailRenderer = GetComponentInChildren<TrailRenderer>();
        Stop();
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= Release;
    }

    private void Release(float value)
    {
        PullInteraction.PullActionReleased -= Release;
        gameObject.transform.parent = null;
        _inAir = true;
        SetPhysics(true);
        _trailRenderer.emitting = true;

        Vector3 force = transform.forward * value * speed;
        _rigidBody.AddForce(force, ForceMode.Impulse);

        StartCoroutine(RotateWithVelocity());

        _lastPosition = tip.position;

        
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();

        while(_inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(_rigidBody.velocity, transform.forward);
            transform.rotation = newRotation;
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if(_inAir)
        {
            CheckCollision();
            _lastPosition = tip.position;
        }
    }

    private void CheckCollision()
    {
        if (Physics.Linecast(_lastPosition, tip.position, out RaycastHit hitInfo))
        {
            if(hitInfo.transform.gameObject.layer!=8)
            {
                if (hitInfo.collider.tag == "Monster")
                {
                    //몬스터랑만 충돌할때 몬스터오브젝트의 자식으로 arrow가 있도록 하고, 리스트에담아줌.
                    Debug.Log("Monster랑충돌!");
                    ++hitInfo.collider.GetComponent<Burrow>().HitCnt;
                    hitInfo.collider.GetComponent<Animator>().SetTrigger("Take Damage");

                    GameObject hitInstance = Instantiate(HitEeffect, hitInfo.transform.position, Quaternion.identity);
                    Destroy(hitInstance, 0.5f);
                    transform.parent = hitInfo.transform;
                }

                if (hitInfo.transform.TryGetComponent(out Rigidbody body))
                {

                    _rigidBody.interpolation = RigidbodyInterpolation.None;
                    transform.parent = hitInfo.transform;

                  
                    body.AddForce(_rigidBody.velocity, ForceMode.Impulse);
                }

                Stop();
            }
        }
      }

    private void Stop()
    {
        _inAir = false;
        SetPhysics(false);
        _trailRenderer.emitting = false;
        
    }

    private void SetPhysics(bool usePhysics)
    {
        _rigidBody.useGravity = usePhysics;
        _rigidBody.isKinematic = !usePhysics;
    }
}
