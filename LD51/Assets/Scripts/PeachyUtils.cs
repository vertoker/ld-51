using System.Collections;
using UnityEngine;

public static class PeachyUtils
{
    public static IEnumerator FadeIn(float alpha)
    {
        while (alpha > 0)
        {
            alpha = Mathf.MoveTowards(alpha, 0, 2 * Time.deltaTime);
            yield return null;
        }
    }
}
