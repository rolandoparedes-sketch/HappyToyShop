using UnityEngine;

public class Toy : ItemBase, IInteractuable
{

    public ToyData toyData;


    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider col;

    private bool pickedUp;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (col == null)
            col = GetComponent<Collider>();
    }

    public void Interact()
    {
        if (pickedUp)
            return;

        PickUp();
    }
    private void PickUp()
    {
        Player3DInventory inventory = Player3DController.instance.inventory3D;

        if (inventory.itemInHandLeft != null)
        {
            Vector3 dropPosition = Player3DController.instance.transform.position + Player3DController.instance.transform.forward * 1.2f;

            ((Toy)inventory.itemInHandLeft).Drop(dropPosition);
        }

        pickedUp = true;

        inventory.itemInHandLeft = this;

        transform.SetParent(inventory.leftHandPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        col.enabled = false;
    }
    public void Drop(Vector3 position)
    {
        pickedUp = false;

        Player3DInventory inventory = Player3DController.instance.inventory3D;

        if (inventory.itemInHandLeft == this)
            inventory.itemInHandLeft = null;

        transform.SetParent(null);

        transform.position = position + Vector3.up * 0.2f;

        rb.isKinematic = false;
        rb.linearVelocity = Player3DController.instance.transform.forward * 2f;
        rb.angularVelocity = Vector3.zero;

        col.enabled = true;
    }
}