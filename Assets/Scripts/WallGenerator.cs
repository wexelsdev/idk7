using UnityEngine;

public class WallGenerator : MonoBehaviour {
    public int width = 5;
    public int height = 3;
    public float blockSize = 1f;
    public GameObject blockPrefab;

    private void Awake() {
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                Vector3 localOffset = new Vector3(x * blockSize, y * blockSize, 0);
                Vector3 worldOffset = transform.TransformDirection(localOffset);
                Vector3 blockPosition = transform.position + worldOffset;

                GameObject block;
                if (blockPrefab != null)
                    block = Instantiate(blockPrefab, blockPosition, transform.rotation, transform);
                else
                    block = GameObject.CreatePrimitive(PrimitiveType.Cube);

                block.transform.localScale = Vector3.one * blockSize;
                block.transform.SetParent(transform);
            }
        }
    }
}