using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(UIoutline))]
public class UIOutlineEditor : Editor
{
    private SerializedProperty drawTop;
    private SerializedProperty drawBottom;
    private SerializedProperty drawLeft;
    private SerializedProperty drawRight;
    private SerializedProperty topStartAlpha;
    private SerializedProperty topEndAlpha;
    private SerializedProperty bottomStartAlpha;
    private SerializedProperty bottomEndAlpha;
    private SerializedProperty leftStartAlpha;
    private SerializedProperty leftEndAlpha;
    private SerializedProperty rightStartAlpha;
    private SerializedProperty rightEndAlpha;

    private bool foldoutEdges = true;
    private bool foldoutAlphas = true;

    private void OnEnable()
    {
        // 绑定属性
        drawTop = serializedObject.FindProperty("drawTop");
        drawBottom = serializedObject.FindProperty("drawBottom");
        drawLeft = serializedObject.FindProperty("drawLeft");
        drawRight = serializedObject.FindProperty("drawRight");
        topStartAlpha = serializedObject.FindProperty("topStartAlpha");
        topEndAlpha = serializedObject.FindProperty("topEndAlpha");
        bottomStartAlpha = serializedObject.FindProperty("bottomStartAlpha");
        bottomEndAlpha = serializedObject.FindProperty("bottomEndAlpha");
        leftStartAlpha = serializedObject.FindProperty("leftStartAlpha");
        leftEndAlpha = serializedObject.FindProperty("leftEndAlpha");
        rightStartAlpha = serializedObject.FindProperty("rightStartAlpha");
        rightEndAlpha = serializedObject.FindProperty("rightEndAlpha");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // 显示颜色和宽度设置
        EditorGUILayout.PropertyField(serializedObject.FindProperty("lineColor"), new GUIContent("Line Color"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("lineWidth"), new GUIContent("Line Width"));

        // 边的生成控制折叠区域
        foldoutEdges = EditorGUILayout.Foldout(foldoutEdges, "边的生成控制");
        if (foldoutEdges)
        {
            EditorGUILayout.PropertyField(drawTop);
            EditorGUILayout.PropertyField(drawBottom);
            EditorGUILayout.PropertyField(drawLeft);
            EditorGUILayout.PropertyField(drawRight);
        }

        // 透明度渐变控制折叠区域
        foldoutAlphas = EditorGUILayout.Foldout(foldoutAlphas, "透明度渐变控制");
        if (foldoutAlphas)
        {
            EditorGUILayout.PropertyField(topStartAlpha);
            EditorGUILayout.PropertyField(topEndAlpha);
            EditorGUILayout.PropertyField(bottomStartAlpha);
            EditorGUILayout.PropertyField(bottomEndAlpha);
            EditorGUILayout.PropertyField(leftStartAlpha);
            EditorGUILayout.PropertyField(leftEndAlpha);
            EditorGUILayout.PropertyField(rightStartAlpha);
            EditorGUILayout.PropertyField(rightEndAlpha);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
public class UIoutline : MaskableGraphic
{

    protected override void Start()
    {
        base.Start();
        // 禁用 Raycast Target
        this.raycastTarget = false;
    }

    public Color lineColor = Color.black; // 默认描边颜色
    public float lineWidth = 1f; // 默认线宽，表示一个像素宽

    [Header("边的生成控制")]
    public bool drawTop = true;    // 是否绘制顶部边
    public bool drawBottom = true; // 是否绘制底部边
    public bool drawLeft = true;   // 是否绘制左侧边
    public bool drawRight = true;  // 是否绘制右侧边

    [Header("透明度渐变控制")]
    [Range(0, 1)] public float topStartAlpha = 1f;    // 顶部边开始的透明度
    [Range(0, 1)] public float topEndAlpha = 0.5f;    // 顶部边结束的透明度
    [Range(0, 1)] public float bottomStartAlpha = 1f; // 底部边开始的透明度
    [Range(0, 1)] public float bottomEndAlpha = 0.5f; // 底部边结束的透明度
    [Range(0, 1)] public float leftStartAlpha = 1f;   // 左侧边开始的透明度
    [Range(0, 1)] public float leftEndAlpha = 0.5f;   // 左侧边结束的透明度
    [Range(0, 1)] public float rightStartAlpha = 1f;  // 右侧边开始的透明度
    [Range(0, 1)] public float rightEndAlpha = 0.5f;  // 右侧边结束的透明度
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        base.OnPopulateMesh(vh);
        vh.Clear();

        // 绘制顶部边（渐变）
        if (drawTop)
        {
            UIVertex[] quadTop = new UIVertex[4];
            quadTop[0] = new UIVertex();
            quadTop[0].color = new Color(lineColor.r, lineColor.g, lineColor.b, topStartAlpha);
            quadTop[0].position = new Vector3(-rectTransform.rect.width * 0.5f, rectTransform.rect.height * 0.5f, 0);
            quadTop[0].uv0 = Vector2.zero;

            quadTop[1] = new UIVertex();
            quadTop[1].color = new Color(lineColor.r, lineColor.g, lineColor.b, topEndAlpha);
            quadTop[1].position = new Vector3(rectTransform.rect.width * 0.5f, rectTransform.rect.height * 0.5f, 0);
            quadTop[1].uv0 = Vector2.zero;

            quadTop[2] = new UIVertex();
            quadTop[2].color = new Color(lineColor.r, lineColor.g, lineColor.b, topEndAlpha);
            quadTop[2].position = new Vector3(rectTransform.rect.width * 0.5f, rectTransform.rect.height * 0.5f - lineWidth, 0);
            quadTop[2].uv0 = Vector2.zero;

            quadTop[3] = new UIVertex();
            quadTop[3].color = new Color(lineColor.r, lineColor.g, lineColor.b, topStartAlpha);
            quadTop[3].position = new Vector3(-rectTransform.rect.width * 0.5f, rectTransform.rect.height * 0.5f - lineWidth, 0);
            quadTop[3].uv0 = Vector2.zero;

            vh.AddUIVertexQuad(quadTop);
        }

        // 绘制底部边（渐变）
        if (drawBottom)
        {
            UIVertex[] quadBottom = new UIVertex[4];
            quadBottom[0] = new UIVertex();
            quadBottom[0].color = new Color(lineColor.r, lineColor.g, lineColor.b, bottomStartAlpha);
            quadBottom[0].position = new Vector3(-rectTransform.rect.width * 0.5f, -rectTransform.rect.height * 0.5f + lineWidth, 0);
            quadBottom[0].uv0 = Vector2.zero;

            quadBottom[1] = new UIVertex();
            quadBottom[1].color = new Color(lineColor.r, lineColor.g, lineColor.b, bottomEndAlpha);
            quadBottom[1].position = new Vector3(rectTransform.rect.width * 0.5f, -rectTransform.rect.height * 0.5f + lineWidth, 0);
            quadBottom[1].uv0 = Vector2.zero;

            quadBottom[2] = new UIVertex();
            quadBottom[2].color = new Color(lineColor.r, lineColor.g, lineColor.b, bottomEndAlpha);
            quadBottom[2].position = new Vector3(rectTransform.rect.width * 0.5f, -rectTransform.rect.height * 0.5f, 0);
            quadBottom[2].uv0 = Vector2.zero;

            quadBottom[3] = new UIVertex();
            quadBottom[3].color = new Color(lineColor.r, lineColor.g, lineColor.b, bottomStartAlpha);
            quadBottom[3].position = new Vector3(-rectTransform.rect.width * 0.5f, -rectTransform.rect.height * 0.5f, 0);
            quadBottom[3].uv0 = Vector2.zero;

            vh.AddUIVertexQuad(quadBottom);
        }

        // 绘制左侧边（渐变）
        if (drawLeft)
        {
            UIVertex[] quadLeft = new UIVertex[4];
            quadLeft[0] = new UIVertex();
            quadLeft[0].color = new Color(lineColor.r, lineColor.g, lineColor.b, leftStartAlpha);
            quadLeft[0].position = new Vector3(-rectTransform.rect.width * 0.5f, rectTransform.rect.height * 0.5f, 0);
            quadLeft[0].uv0 = Vector2.zero;

            quadLeft[1] = new UIVertex();
            quadLeft[1].color = new Color(lineColor.r, lineColor.g, lineColor.b, leftEndAlpha);
            quadLeft[1].position = new Vector3(-rectTransform.rect.width * 0.5f + lineWidth, rectTransform.rect.height * 0.5f, 0);
            quadLeft[1].uv0 = Vector2.zero;

            quadLeft[2] = new UIVertex();
            quadLeft[2].color = new Color(lineColor.r, lineColor.g, lineColor.b, leftEndAlpha);
            quadLeft[2].position = new Vector3(-rectTransform.rect.width * 0.5f + lineWidth, -rectTransform.rect.height * 0.5f, 0);
            quadLeft[2].uv0 = Vector2.zero;

            quadLeft[3] = new UIVertex();
            quadLeft[3].color = new Color(lineColor.r, lineColor.g, lineColor.b, leftStartAlpha);
            quadLeft[3].position = new Vector3(-rectTransform.rect.width * 0.5f, -rectTransform.rect.height * 0.5f, 0);
            quadLeft[3].uv0 = Vector2.zero;

            vh.AddUIVertexQuad(quadLeft);
        }

        // 绘制右侧边（渐变）
        if (drawRight)
        {
            UIVertex[] quadRight = new UIVertex[4];
            quadRight[0] = new UIVertex();
            quadRight[0].color = new Color(lineColor.r, lineColor.g, lineColor.b, rightStartAlpha);
            quadRight[0].position = new Vector3(rectTransform.rect.width * 0.5f - lineWidth, rectTransform.rect.height * 0.5f, 0);
            quadRight[0].uv0 = Vector2.zero;

            quadRight[1] = new UIVertex();
            quadRight[1].color = new Color(lineColor.r, lineColor.g, lineColor.b, rightEndAlpha);
            quadRight[1].position = new Vector3(rectTransform.rect.width * 0.5f, rectTransform.rect.height * 0.5f, 0);
            quadRight[1].uv0 = Vector2.zero;

            quadRight[2] = new UIVertex();
            quadRight[2].color = new Color(lineColor.r, lineColor.g, lineColor.b, rightEndAlpha);
            quadRight[2].position = new Vector3(rectTransform.rect.width * 0.5f, -rectTransform.rect.height * 0.5f, 0);
            quadRight[2].uv0 = Vector2.zero;

            quadRight[3] = new UIVertex();
            quadRight[3].color = new Color(lineColor.r, lineColor.g, lineColor.b, rightStartAlpha);
            quadRight[3].position = new Vector3(rectTransform.rect.width * 0.5f - lineWidth, -rectTransform.rect.height * 0.5f, 0);
            quadRight[3].uv0 = Vector2.zero;

            vh.AddUIVertexQuad(quadRight);
        }
    }
}
