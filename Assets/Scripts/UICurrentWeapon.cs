using Opsive.UltimateCharacterController.Inventory;
using Opsive.UltimateCharacterController.Items;
using Opsive.UltimateCharacterController.Items.Actions;
using UnityEngine;
using UnityEngine.UI;

public class UICurrentWeapon : MonoBehaviour
{
    private Sprite _weaponImage;
    private Inventory _inventory;
    private Item _activeItem;
    private Item _lastActiveItem;
    private int _currentClipAmmo;
    private int _currentTotalAmmo;
    
    private void Start()
    {
        GetActiveItem();
        if (_activeItem != null)
        {
            SetWeaponImage();
            _lastActiveItem = _activeItem;
        }
    }

    private void Update()
    {
        GetActiveItem();
        if (_activeItem != null)
        {
            if (_lastActiveItem != _activeItem)
            {
                SetWeaponImage();
                _lastActiveItem = _activeItem;
            }
            UpdateAmmoCounter();
        }
    }

    private void UpdateAmmoCounter()
    {
        GetCurrentAmmoAmount();
        var textTMPComponents = GetComponentsInChildren<TMPro.TextMeshProUGUI>();
        foreach (var textComponent in textTMPComponents)
        {
            if (textComponent.gameObject.name == "Clip Ammo")
                textComponent.text = _currentClipAmmo.ToString();
            if (textComponent.gameObject.name == "Total Ammo")
                textComponent.text = _currentTotalAmmo.ToString();
        }
    }

    private void GetCurrentAmmoAmount()
    {
        if(_activeItem != null)
        _currentClipAmmo = _activeItem.GetComponent<ShootableWeapon>().ClipRemaining;
        var ammoIdentifier = _activeItem.GetComponent<ShootableWeapon>().GetConsumableItemIdentifier();
        _currentTotalAmmo = _inventory.GetItemIdentifierAmount(ammoIdentifier);
    }

    private void GetActiveItem()
    {
        _inventory = FindObjectOfType<Inventory>();
        for (int i = 0; i < _inventory.SlotCount; i++)
        {
            _activeItem = FindObjectOfType<Inventory>().GetActiveItem(i);
            if(_activeItem != null)
                if (_activeItem.IsActive())
                    break;
        }
    }

    private void SetWeaponImage()
    {
        if(_activeItem != null)
        {
            _weaponImage = _activeItem.gameObject.GetComponent<ItemUIData>().weaponUIImage;
        }

        var imageComponents = GetComponentsInChildren<Image>();
        foreach (var imageComponent in imageComponents)
        {
            if (imageComponent.gameObject.name == "Weapon Image")
            {
                imageComponent.sprite = _weaponImage;
                imageComponent.color = Color.white;
                break;
            }
        }
    }
}
