using UnityEngine;
using DG.Tweening;

using System.Collections;

public class WindowSwitch : MonoBehaviour
{
    private Transform[] windowPopups;
    private Vector3[] popupScales;

    private void Awake()
    {
        windowPopups = new Transform[transform.childCount];
        popupScales = new Vector3[windowPopups.Length];
    }

    private void OnEnable()
    {
        for (int i = 0; i < windowPopups.Length; i++)
        {
            windowPopups[i] = transform.GetChild(i);
            popupScales[i] = windowPopups[i].localScale;
        }

        Switch();
    }

    public void Switch()
    {
        foreach(Transform popup in windowPopups)
            popup.localScale = Vector3.zero;

        StartCoroutine("SwitchIEnum");
    }

    private IEnumerator SwitchIEnum()
    {
        int i = 0;
        while (i < windowPopups.Length)
        {
            windowPopups[i].localScale = Vector3.zero;
            windowPopups[i].DOScale(popupScales[i], .2f);

            yield return new WaitForSeconds(.05f);

            i++;
        }
    }
}
