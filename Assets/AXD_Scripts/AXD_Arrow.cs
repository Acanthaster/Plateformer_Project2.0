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

<<<<<<< HEAD
    public void SetDirection(Directions pDir)
    {
        dir = pDir;
        if (dir == Directions.up)
        {
            sr.sprite = sprites[1];
            sr.flipX = true;
        }
        else if(dir == Directions.right)
        {
            sr.sprite = sprites[0];
            sr.flipY = true;
        }else if (dir == Directions.down)
        {
            sr.sprite = sprites[1];
            
        }else if(dir == Directions.left)
        {
            sr.sprite = sprites[0];
        }
=======
        transform.Translate(Vector3.left*Time.deltaTime*speed);
>>>>>>> parent of f324b5a... Fixing arrow launcher
    }
}
