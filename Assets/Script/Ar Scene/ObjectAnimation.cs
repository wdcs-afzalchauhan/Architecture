using UnityEngine;
using DG.Tweening;
public class ObjectAnimation : MonoBehaviour
{
    public Transform target;

    public Vector3 startPos;
    public Vector3 endPos;

    public float duration = 1;

    public ParticleSystem particle;

    private void OnEnable()
    {
        target.DOLocalMove(startPos, 0);
        particle.gameObject.SetActive(true);
        particle.Play();

        target.DOLocalMove(endPos, duration).OnComplete(() => { particle.Stop(); particle.gameObject.SetActive(false); });
    }
    private void OnDisable()
    {
        target.DOLocalMove(startPos, duration);
        particle.gameObject.SetActive(false);
    }
}
