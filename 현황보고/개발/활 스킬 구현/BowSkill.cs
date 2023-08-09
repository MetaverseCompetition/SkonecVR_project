using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class BowSkill : MonoBehaviour
{
    public static float chargeTime;
    public Transform endPosition;
    public Transform startPosition;
    public XRController controller = null;

    private float maxDistance;
    private float currentDistance;

    public GameObject chargeEffect;
    public GameObject arrow;

    private AudioSource skillsound;
    private bool isPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        skillsound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButton))
        {
            maxDistance = Vector3.Distance(startPosition.position, endPosition.position);
            currentDistance = Vector3.Distance(startPosition.position, controller.transform.position);

            if (gripButton && currentDistance >= maxDistance && PullInteraction.isGripped)
            {
                if (!isPlayed)
                {
                    skillsound.Play();
                    isPlayed = true;
                }

                chargeTime += Time.deltaTime;
                chargeEffect.SetActive(true);
                

                if (chargeTime >= 2.0f)
                {
                    Debug.Log("Â÷Â¡ ¿Ï·á !");
                    chargeEffect.SetActive(false);

                }
            }

            else
            {
                chargeTime = 0f;
                chargeEffect.SetActive(false);
                skillsound.Stop();
                isPlayed=false;
                PullInteraction.isGripped = false;
            }
        }
    }
}
