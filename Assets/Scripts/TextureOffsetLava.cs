using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureOffsetLava : MonoBehaviour {

    public float scrollSpeed = 0.01F;
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        //scroll the lava texture to simulate floating effect
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, offset));
    }
}