using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXD_Arrow : MonoBehaviour
{
    public float speed;
    private SpriteRenderer sr;
    public float detectionDistance;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x - (sr.sprite.rect.width / 2 / sr.sprite.pixelsPerUnit) + detectionDistance / 2 / sr.sprite.pixelsPerUnit, transform.position.y),
            Vector2.left * detectionDistance / sr.sprite.pixelsPerUnit, detectionDistance / sr.sprite.pixelsPerUnit);
        Debug.DrawRay(new Vector2(transform.position.x - (sr.sprite.rect.width / 2 / sr.sprite.pixelsPerUnit) + detectionDistance / 2 / sr.sprite.pixelsPerUnit, transform.position.y),
            Vector2.left*detectionDistance/sr.sprite.pixelsPerUnit, Color.blue);
        if(hit)
        {
            Debug.Log("Arrow Layer : "+LayerMask.LayerToName(hit.collider.gameObject.layer));
            if (LayerMask.LayerToName(hit.collider.gameObject.layer).Equals("TempleGround"))
            {
                Debug.Log("Mur");
                Destroy(this.gameObject);
            }
            else if(LayerMask.LayerToName(hit.collider.gameObject.layer).Equals("Player"))
            {
                Debug.Log("Joueur");
                hit.collider.gameObject.GetComponent<AXD_PlayerStatus>().TakeDamage();
                Destroy(this.gameObject);
            }
        }

        transform.Translate(Vector3.left*Time.deltaTime*speed);
    }
}
