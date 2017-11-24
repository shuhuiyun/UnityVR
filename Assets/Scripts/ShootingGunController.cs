using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.VR;
public class ShootingGunController : MonoBehaviour
{
    public VRInput vrInput;
    public AudioSource audioSource;
    public ParticleSystem flareParticle;
    public float defaultLineLenght = 70f;
    public LineRenderer gunFlare;
    public float gunFareVisibleSeconds = 0.07f;
    public Transform gunEnd;
    private void OnEnable()
    {
        vrInput.OnDown += HandleDown; /*事件的用法是+=*/

    }
    private void OnDisable()
    {
        vrInput.OnDown -= HandleDown;
    }
    private void HandleDown()
    {
       
        StartCoroutine (Fire());
       
    }
    private IEnumerator Fire()
    {
        audioSource.Play();
        float lineLength = defaultLineLenght;

        flareParticle.Play();
        gunFlare.enabled = true;

        yield return StartCoroutine(MoveLineRenderer(lineLength));
        gunFlare.enabled = false;
    }
    private IEnumerator MoveLineRenderer(float lineLenght)
    {
        float timer = 0f;
        while(timer < gunFareVisibleSeconds)
        {
            gunFlare.SetPosition(0, gunEnd.position);
            gunFlare.SetPosition(1, gunEnd.position + gunEnd.forward * lineLenght);
            yield return null;
            timer += Time.deltaTime;

        }
    }

}
