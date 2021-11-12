using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEffect : MonoBehaviour
{
    [SerializeField] private int grappableLayerNumber = 6;
    [SerializeField] private ParticleSystem VisualEffect;
    [SerializeField] private PlayerVFX Effects;
    private Transform Player;
    private void Start()
    {
        Effects = GetComponent<PlayerVFX>();
        Player = GetComponent<Transform>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.layer == grappableLayerNumber)
        {
            ContactPoint2D[] contacts = new ContactPoint2D[1];
            collision.GetContacts(contacts);
            var contactPoint = contacts[0].point;

            if (Player.position.y > contactPoint.y)
            {
                SpriteRenderer sprite = collision.transform.GetComponent<SpriteRenderer>();

                print("On Ground! " + collision.transform.name);

                Color pixel_color = Effects.GetPixel(collision.transform.InverseTransformPoint(contactPoint), sprite);
                Color EffectColor = pixel_color - (Color.white - sprite.color);

                VisualEffect.gameObject.SetActive(true);
                ParticleSystem.MainModule settings = VisualEffect.GetComponent<ParticleSystem>().main;
                settings.startColor = EffectColor;
            }
        }
    }
}
