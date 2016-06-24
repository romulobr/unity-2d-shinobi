using UnityEngine;
using System.Collections;

public class scrollingWall : MonoBehaviour
{
    public float speed;
    public GameObject secondaryWall;
    private GameObject primaryWall;
    private SpriteRenderer primaryRenderer;
    private SpriteRenderer secondaryRenderer;

    public void Start()
    {
        primaryWall = this.gameObject;
        primaryRenderer = primaryWall.GetComponent<SpriteRenderer>();
        secondaryRenderer = secondaryWall.GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        primaryWall.transform.Translate(Time.deltaTime*speed, 0, 0);
        if (speed < 0)
        {
            secondaryWall.transform.position = primaryWall.transform.position +
                                               new Vector3(primaryRenderer.bounds.size.x, 0);
        }
        else
        {
            secondaryWall.transform.position = primaryWall.transform.position -
                                               new Vector3(primaryRenderer.bounds.size.x, 0);
        }
        if (!primaryRenderer.isVisible && secondaryRenderer.isVisible)
        {
            SpriteRenderer previousPrimary = primaryRenderer;
            primaryRenderer = secondaryRenderer;
            secondaryRenderer = previousPrimary;

            GameObject previousPrimaryWall = primaryWall;
            primaryWall = secondaryWall;
            secondaryWall = previousPrimaryWall;
        }
    }
}