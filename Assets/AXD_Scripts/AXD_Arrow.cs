using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXD_Arrow : MonoBehaviour
{
    public float speed;
    private SpriteRenderer sr;
    public float detectionDistance;
    public List<Sprite> sprites;
    private Directions dir;
    public RaycastHit2D hit;
    private Vector2 vDir;
    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(dir == Directions.up)
        {
            hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + (sr.sprite.rect.height / 2 / sr.sprite.pixelsPerUnit) - detectionDistance / 2 / sr.sprite.pixelsPerUnit),
            Vector2.up * detectionDistance / sr.sprite.pixelsPerUnit, detectionDistance / sr.sprite.pixelsPerUnit);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + (sr.sprite.rect.height / 2 / sr.sprite.pixelsPerUnit) - detectionDistance / 2 / sr.sprite.pixelsPerUnit),
                Vector2.up * detectionDistance / sr.sprite.pixelsPerUnit, Color.blue);
            
        }
        else if (dir == Directions.right)
        {
            hit = Physics2D.Raycast(new Vector2(transform.position.x + (sr.sprite.rect.width / 2 / sr.sprite.pixelsPerUnit) - detectionDistance / 2 / sr.sprite.pixelsPerUnit, transform.position.y),
            Vector2.right * detectionDistance / sr.sprite.pixelsPerUnit, detectionDistance / sr.sprite.pixelsPerUnit);
            Debug.DrawRay(new Vector2(transform.position.x + (sr.sprite.rect.width / 2 / sr.sprite.pixelsPerUnit) - detectionDistance / 2 / sr.sprite.pixelsPerUnit, transform.position.y),
                Vector2.right * detectionDistance / sr.sprite.pixelsPerUnit, Color.blue);
        }
        else if (dir == Directions.down)
        {
            hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - (sr.sprite.rect.height / 2 / sr.sprite.pixelsPerUnit) + detectionDistance / 2 / sr.sprite.pixelsPerUnit),
            Vector2.down * detectionDistance / sr.sprite.pixelsPerUnit, detectionDistance / sr.sprite.pixelsPerUnit);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - (sr.sprite.rect.height / 2 / sr.sprite.pixelsPerUnit) + detectionDistance / 2 / sr.sprite.pixelsPerUnit),
                Vector2.down * detectionDistance / sr.sprite.pixelsPerUnit, Color.blue);
            
        }
        else if (dir == Directions.left)
        {
            hit = Physics2D.Raycast(new Vector2(transform.position.x - (sr.sprite.rect.width / 2 / sr.sprite.pixelsPerUnit) + detectionDistance / 2 / sr.sprite.pixelsPerUnit, transform.position.y),
            Vector2.left * detectionDistance / sr.sprite.pixelsPerUnit, detectionDistance / sr.sprite.pixelsPerUnit);
            Debug.DrawRay(new Vector2(transform.position.x - (sr.sprite.rect.width / 2 / sr.sprite.pixelsPerUnit) + detectionDistance / 2 / sr.sprite.pixelsPerUnit, transform.position.y),
                Vector2.left * detectionDistance / sr.sprite.pixelsPerUnit, Color.blue);
        }
        else
        {
            Debug.Log("What the fuck man ?");

        }
        
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
        if(dir == Directions.up)
        {
            vDir = Vector2.up;
        }else if(dir == Directions.right)
        {
            vDir = Vector2.right;
        }
        else if(dir == Directions.down)
        {
            vDir = Vector2.down;
        }
        else if(dir == Directions.left)
        {
            vDir = Vector2.left;
        }
        transform.Translate(vDir*Time.deltaTime*speed);
    }

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
    }
}
