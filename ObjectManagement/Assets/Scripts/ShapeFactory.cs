using UnityEngine;

[CreateAssetMenu]
public class ShapeFactory : ScriptableObject {
    [SerializeField]
    Material[] materials;
    [SerializeField]
    Shape[] prefabs;

    public Shape Create(int shapeId) {
        Shape instnace = Instantiate(prefabs[shapeId]);
        instnace.ShapeId = shapeId;
        return instnace;
    }

    public Shape CreateRandom() {
        return Create(Random.Range(0, prefabs.Length));
    }
}
