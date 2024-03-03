using UnityEngine;

/* represents an item that has to exist in a level*/
public class VitalItem : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _particleEffectPrefab;
    private ItemType _itemType;
    private float MAX_PARTICLE_LIFETIME = 2f;

    private void Awake()
    {
        _itemType = ItemType.VitalItem;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagNames.PLAYER))
        {
            OnInteract();
        }
    }

    public void OnInteract()
    {
        GameObject particleEffect = Instantiate(_particleEffectPrefab, transform.position, Quaternion.identity);
        Destroy(particleEffect, MAX_PARTICLE_LIFETIME);
        Destroy(gameObject);
    }

    public ItemType GetItemType()
    {
        return _itemType;
    }
}
