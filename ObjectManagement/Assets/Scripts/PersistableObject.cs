using UnityEngine;

[DisallowMultipleComponent]
public class PersistableObject : MonoBehaviour {
    public virtual void Load(GameDataReader reader) {
        transform.localPosition = reader.ReadVector3();
        transform.localRotation = reader.ReadQuaternion();
        transform.localScale = reader.ReadVector3();
    }

    public virtual void Save(GameDataWriter writer) {
        writer.Write(transform.localPosition);
        writer.Write(transform.localRotation);
        writer.Write(transform.localScale);
    }
}
