using UnityEngine;

public class Shape : PersistableObject {
    int shapeId = int.MinValue;

    public int ShapeId {
        get {
            return shapeId;
        }
        set {
            if (shapeId == int.MinValue && value != int.MinValue) {
                shapeId = value;
            } else {
                Debug.LogError("Not allowed to change shapeId");
            }
        }
    }

    public int MeterialId {
        get; private set;
    }

    public void SetMaterial(Material material, int materialId) {
        GetComponent<MeshRenderer>.material = material;
        materialId = materialId;
    }
}