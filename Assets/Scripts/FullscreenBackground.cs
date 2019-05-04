using System;
using UnityEngine;

public class FullscreenBackground: MonoBehaviour {
    SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale = new Vector2(1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector2(worldScreenWidth / width, worldScreenHeight / height);
    }
}
