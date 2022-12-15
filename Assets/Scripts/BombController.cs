using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;

    

    private float bombFusetime = 3f;
    public int bombAmount = 1;
    private int bombRemaining;

    private bool placeButton;

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explodeMask;
    public float explosionDur = 1f;
    public int explosionRadius = 1;

    [Header("Destructible")]
    public Tilemap destructibleTiles;
    public Destructible destructiblePrefab;

    private void Awake() {
        destructibleTiles = GameObject.Find("Destructible").GetComponent<Tilemap>();
    }
    public void OnButtonPlaceClick() {
        placeButton = true;
    }
    private void OnEnable() {
        bombRemaining = bombAmount;
    }

    private void Update()
    {
        if(bombRemaining > 0 && Input.GetKeyDown(inputKey) || 
        bombRemaining > 0 && placeButton) {
            AudioManager.instance.Play("placeBomb");
            StartCoroutine(PlaceBomb());
        }
        placeButton = false;
    }

    private IEnumerator PlaceBomb() 
    {
        Vector2 position = transform.position;    
        // position.x = Mathf.Round(position.x);
        // position.y = Mathf.Round(position.y);

        // clone a bomb at position
        // Quaternion.Identity return an object which mean no rotation
        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity );
        bombRemaining--;

        yield return new WaitForSeconds(bombFusetime);

        AudioManager.instance.Play("explode");
        
        position = bomb.transform.position;
        // position.x = Mathf.Round(position.x);
        // position.y = Mathf.Round(position.y);
        
        // explosion anim in the middle
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start); 
        explosion.DestroyAfter(explosionDur);

        // explosion anim in 4 direction
        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        Destroy(bomb.gameObject);

        bombRemaining++;
    }

    // recursion explosion animation
    private void Explode(Vector2 position, Vector2 direction, int Length)
    {
        if(Length <= 0) return;

        position += direction;

        // check if explosion collide with indestructible terrain
        if(Physics2D.OverlapBox(position, Vector2.one/2f, 0f, explodeMask))
        {   
            ClearDestructible(position);
            return;
        }
        

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity );
        explosion.SetActiveRenderer(Length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDur);


        Explode(position, direction, Length-1);

    }

    private void ClearDestructible(Vector2 position)
    {   
        // return position of individual tile
        // convert to cell position
        Vector3Int cell = destructibleTiles.WorldToCell(position);
        // get the position of tile at cell position
        TileBase tile = destructibleTiles.GetTile(cell);

        if (tile != null) {
            // clone an ofject brick destroy then set that tile to null
            Instantiate(destructiblePrefab, position, Quaternion.identity);
            destructibleTiles.SetTile(cell,null);
        }
    }

    public void AddBomb()
    {
        bombAmount++;
        bombRemaining++;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            other.isTrigger = false;
        }
    }


}
