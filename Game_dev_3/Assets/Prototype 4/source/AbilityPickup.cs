using UnityEngine;


public class AbilityPickup : MonoBehaviour
{
    public enum AbilityType { WallJump, Slash }
    public AbilityType abilityType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerMovementUpgrades player = other.GetComponent<PlayerMovementUpgrades>();
        if (player == null) return;

        switch (abilityType)
        {
            case AbilityType.WallJump:
                player.UnlockWallJump();
                break;
            case AbilityType.Slash:
                player.UnlockSlash();
                break;
        }

        // TODO: play pickup VFX/SFX, show an "ability acquired" popup, etc.
        Destroy(gameObject);
    }
}
