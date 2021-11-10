using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private int grappableLayerNumber = 6;
    [SerializeField] private ParticleSystem VisualEffect;
    [SerializeField] private PlayerVFX Effects;
    private void Awake()
    {
        Effects.GetComponent<PlayerVFX>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.layer == grappableLayerNumber)
        {
            print("On Ground!");
            //Vector2 Position = collision.contacts[0].point;
            SpriteRenderer sprite = collision.transform.GetComponent<SpriteRenderer>();
            Color color = Effects.GetAverageColorOfSprite(sprite);
            print(color + "jump");
            ParticleSystem.MainModule settings = VisualEffect.main;
            settings.startColor = color;
            VisualEffect.gameObject.SetActive(true);
        }
    }
}
