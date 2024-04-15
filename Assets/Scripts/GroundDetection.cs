using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public Vector3 groundCheckPosition { get => transform.position + offset; }
    public float groundCheckRadius = 0.5f; // 검출할 반지름
    public Vector3 offset = Vector3.zero;
    public LayerMask groundLayerMask; // 땅으로 간주할 레이어 마스크

    [HideInInspector] public bool isGrounded; // 땅에 붙어 있는지 여부

    private void Update()
    {
        // 플레이어 주변의 땅 검출
        Collider[] colliders = Physics.OverlapSphere(groundCheckPosition, groundCheckRadius, groundLayerMask);
        isGrounded = colliders.Length > 0;
    }

    private void OnDrawGizmos()
    {
        // Scene 뷰에서 검출 범위를 시각적으로 표시하기 위해 DrawWireSphere 사용
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPosition, groundCheckRadius);
    }
}
