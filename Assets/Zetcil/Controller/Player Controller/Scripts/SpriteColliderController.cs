using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class SpriteColliderController : MonoBehaviour
    {
        [Header("Collision Result")]
        public string CollisionTag;

        [Header("Trigger Result")]
        public string TriggerTag;

        void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionTag = collision.gameObject.tag;
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            CollisionTag = "";
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            TriggerTag = collider.gameObject.tag;
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            TriggerTag = "";
        }
    }
}
