using UnityEngine;
using Es.WaveformProvider;
public class FloatingSphere : MonoBehaviour
{
    public Transform waterSurface; // PlaneのTransformを指定
    public float buoyancyForce = 10f; // 浮力の強さ
    public float dampingFactor = 0.5f; // 抵抗力
    private Rigidbody rb;
    private float sphereRadius;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        sphereRadius = sphereCollider.radius * transform.localScale.y; // スケールを考慮した半径
    }

    void FixedUpdate()
    {
        // 水面の高さを取得
        float waterHeight = waterSurface.position.y;

        // Sphereの底部の位置 (中心 - 半径)
        float sphereBottom = transform.position.y - sphereRadius;

        // Sphereが水面下にある場合に浮力を加える
        if (sphereBottom < waterHeight)
        {
            float depth = waterHeight - sphereBottom;

            // 浮力計算 (深さに比例)
            Vector3 buoyancy = Vector3.up * buoyancyForce * depth;

            // 抵抗力
            Vector3 drag = -rb.velocity * dampingFactor;

            // Rigidbodyに力を加える
            rb.AddForce(buoyancy + drag, ForceMode.Force);
        }
    }
}
