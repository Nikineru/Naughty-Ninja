using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public string ID { get => _id; }
    [SerializeField] private string _id;

    private void OnValidate()
    {
        CalculateID();
    }
    protected void CalculateID() 
    {
        Hash128 hash = new Hash128();

        hash.Append(GetInstanceID());
        hash.Append(name);

        _id = hash.ToString();
    }
}