using UnityEngine;

// Builds the small pickup-gated route used by FullUpgradeLevel.
// Keeping the layout here makes it easy to tune in one place while the prototype is evolving.
public class MicroMetroidvaniaLevel : MonoBehaviour
{
    Sprite square;

    void Awake()
    {
        ClearPreviousLayout();
        square = CreateSquareSprite();

        PlayerMovementUpgrades player = FindFirstObjectByType<PlayerMovementUpgrades>();
        if (player == null)
        {
            Debug.LogError("Micro level needs a PlayerMovementUpgrades component.");
            return;
        }

        player.transform.position = new Vector3(-11f, 0f, 0f);
        player.canWallJump = false;
        player.canSlash = false;
        player.canDoubleJump = false;

        // Start room: the first pickup is visible and reachable immediately.
        // The floor continues through the chimney: there is no pit before the first pickup.
        Ground("Start and Chimney Floor", -7.5f, -1.5f, 15f);
        Pickup("Wall Jump", new Vector2(-8f, -0.25f), AbilityPickup.AbilityType.WallJump, new Color(0.35f, 0.7f, 1f));

        // A narrow two-wall chimney turns the first pickup into the way forward.
        // The shorter left wall leaves a generous way into the chimney. The taller right wall
        // blocks the ground route, so the alternating wall jump is still the way forward.
        Wall("Chimney Left (Low Entry)", -5f, 2.75f, 1f, 3f);
        Wall("Chimney Right", -2f, 2.375f, 1f, 6.75f);
        Ground("Upper Ledge", 0.5f, 5.25f, 5f);
        Pickup("Slash", new Vector2(1f, 6.15f), AbilityPickup.AbilityType.Slash, new Color(1f, 0.72f, 0.2f));

        // Drop from the ledge into an enemy chokepoint. The enemy's collider fills the route,
        // so it has to be defeated with the newly acquired slash before the player can continue.
        Ground("Combat Floor", 6f, -1.5f, 12f);
        // A low canopy prevents a normal jump over the enemy, but the open left side makes
        // the fight immediately accessible after collecting Slash.
        Wall("Combat Canopy", 5.5f, 2.25f, 3.5f, 0.7f);
        EnemyHealth chokepointEnemy = Enemy("Chokepoint Enemy", new Vector2(5.5f, 0.15f));
        EnemyGate("Enemy Clear Gate", new Vector2(7f, 2f), chokepointEnemy);
        Pickup("Double Jump", new Vector2(8f, -0.2f), AbilityPickup.AbilityType.DoubleJump, new Color(0.35f, 1f, 0.48f));

        // A six-unit gap is beyond the normal jump but comfortable with the double jump.
        Ground("Exit Island", 21.5f, 0.5f, 7f);
        Goal("Exit", new Vector2(23.5f, 1.65f));
    }

    void ClearPreviousLayout()
    {
        foreach (AbilityPickup pickup in FindObjectsByType<AbilityPickup>(FindObjectsSortMode.None))
            Destroy(pickup.gameObject);
        foreach (EnemyHealth enemy in FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None))
            Destroy(enemy.gameObject);

        GameObject[] existing = GameObject.FindGameObjectsWithTag("Ground");
        foreach (GameObject item in existing) Destroy(item);
        existing = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject item in existing) Destroy(item);
    }

    void Ground(string label, float x, float y, float width)
    {
        Block(label, new Vector2(x, y), new Vector2(width, 1f), "Ground", new Color(0.18f, 0.24f, 0.32f));
    }

    void Wall(string label, float x, float y, float width, float height)
    {
        Block(label, new Vector2(x, y), new Vector2(width, height), "Wall", new Color(0.27f, 0.35f, 0.46f));
    }

    void Block(string label, Vector2 position, Vector2 size, string tagName, Color colour)
    {
        GameObject block = new GameObject(label);
        block.transform.position = position;
        block.tag = tagName;
        block.transform.localScale = size;
        SpriteRenderer renderer = block.AddComponent<SpriteRenderer>();
        renderer.sprite = square;
        renderer.color = colour;
        BoxCollider2D collider = block.AddComponent<BoxCollider2D>();
        collider.size = Vector2.one;
    }

    void Pickup(string label, Vector2 position, AbilityPickup.AbilityType ability, Color colour)
    {
        GameObject item = new GameObject(label + " Pickup");
        item.transform.position = position;
        SpriteRenderer renderer = item.AddComponent<SpriteRenderer>();
        renderer.sprite = square;
        renderer.color = colour;
        item.transform.localScale = Vector3.one * 0.65f;
        BoxCollider2D collider = item.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        item.AddComponent<AbilityPickup>().abilityType = ability;
        Label(label, position + Vector2.up * 0.72f, colour);
    }

    EnemyHealth Enemy(string label, Vector2 position)
    {
        GameObject enemy = new GameObject(label);
        enemy.transform.position = position;
        enemy.layer = LayerMask.NameToLayer("Enemies");
        // This reaches from the floor to the low ceiling, so jumping over it is not an option.
        enemy.transform.localScale = new Vector3(1.2f, 2.7f, 1f);
        SpriteRenderer renderer = enemy.AddComponent<SpriteRenderer>();
        renderer.sprite = square;
        renderer.color = new Color(0.92f, 0.25f, 0.32f);
        enemy.AddComponent<BoxCollider2D>();
        EnemyHealth health = enemy.AddComponent<EnemyHealth>();
        health.maxHealth = 1;
        Label("Defeat", position + Vector2.up * 1.5f, Color.white);
        return health;
    }

    void EnemyGate(string label, Vector2 position, EnemyHealth enemy)
    {
        GameObject gate = new GameObject(label);
        gate.transform.position = position;
        SpriteRenderer renderer = gate.AddComponent<SpriteRenderer>();
        renderer.sprite = square;
        renderer.color = new Color(0.92f, 0.25f, 0.32f, 0.35f);
        gate.transform.localScale = new Vector3(0.45f, 7f, 1f);
        BoxCollider2D collider = gate.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        gate.AddComponent<EnemyClearGate>().enemy = enemy;
    }

    void Goal(string label, Vector2 position)
    {
        GameObject goal = new GameObject(label);
        goal.transform.position = position;
        goal.transform.localScale = new Vector3(1.2f, 2.4f, 1f);
        SpriteRenderer renderer = goal.AddComponent<SpriteRenderer>();
        renderer.sprite = square;
        renderer.color = new Color(1f, 0.95f, 0.4f);
        BoxCollider2D collider = goal.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        goal.AddComponent<MicroLevelGoal>();
        Label("EXIT", position + Vector2.up * 1.7f, Color.white);
    }

    void Label(string text, Vector2 position, Color colour)
    {
        GameObject label = new GameObject(text + " Label");
        label.transform.position = position;
        TextMesh mesh = label.AddComponent<TextMesh>();
        mesh.text = text;
        mesh.anchor = TextAnchor.MiddleCenter;
        mesh.alignment = TextAlignment.Center;
        mesh.characterSize = 0.28f;
        mesh.fontSize = 42;
        mesh.color = colour;
    }

    Sprite CreateSquareSprite()
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();
        return Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1f);
    }
}

public class MicroLevelGoal : MonoBehaviour
{
    bool complete;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (complete || !other.CompareTag("Player")) return;

        PlayerMovementUpgrades player = other.GetComponent<PlayerMovementUpgrades>();
        if (player != null && player.canWallJump && player.canSlash && player.canDoubleJump)
        {
            complete = true;
            Debug.Log("Micro Metroidvania complete!");
        }
    }

    void OnGUI()
    {
        PlayerMovementUpgrades player = FindFirstObjectByType<PlayerMovementUpgrades>();
        if (player == null) return;
        string status = "WALL JUMP: " + (player.canWallJump ? "OK" : "--") +
                        "    SLASH: " + (player.canSlash ? "OK" : "--") +
                        "    DOUBLE JUMP: " + (player.canDoubleJump ? "OK" : "--");
        GUI.Label(new Rect(16, 16, 620, 28), status);
        GUI.Label(new Rect(16, 42, 760, 28), complete ? "LEVEL COMPLETE!" : "Find the upgrades: climb, defeat the enemy, then cross the gap.");
    }
}

// Stops a route skip without adding another combat obstacle. It automatically opens when the
// only enemy in this tiny arena has been defeated.
public class EnemyClearGate : MonoBehaviour
{
    public EnemyHealth enemy;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (enemy == null || !other.CompareTag("Player")) return;
        other.transform.position = new Vector3(3.25f, -0.2f, other.transform.position.z);
    }
}
