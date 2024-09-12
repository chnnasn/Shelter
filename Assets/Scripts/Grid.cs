using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid
{
    float size;
    GameObject cube;
    GameObject grid;
    Vector3 startPO;
    int currentObjects = 0; // 当前生成的对象数
    private Dictionary<Vector2Int, GameObject> occupiedPositions = new Dictionary<Vector2Int, GameObject>();
    int generationDepth;
    float scale;

    public Grid(float size, Vector3 startPO, GameObject cube, int generationDepth,float scale)
    {
        this.startPO = startPO;
        this.size = size;
        this.cube = cube;
        this.scale = scale;
        this.generationDepth = generationDepth;
        grid = new GameObject("Grid");

        // 从中心点开始生成
        Transform initialTransform = CreateObjectAtPosition(startPO, cube, 0);
        if (initialTransform != null)
        {
            Iteration(initialTransform);
        }
    }

    // 创建对象的方法

     Transform CreateObjectAtPosition(Vector3 gridPosition, GameObject cube, int index)
    {
        // 计算整数网格坐标
        int xIndex = Mathf.RoundToInt((gridPosition.x - startPO.x) / size);//算法计算x和y的值，使其为整数，并存入字典用来记录，防止重复
        int yIndex = Mathf.RoundToInt((gridPosition.y - startPO.y) / size);

        Vector2Int gridCoord = new Vector2Int(xIndex, yIndex);

        // 检查是否超出生成深度或位置已占用
        if (xIndex < -generationDepth || xIndex > generationDepth ||
            yIndex < -generationDepth || yIndex > generationDepth ||
            occupiedPositions.ContainsKey(gridCoord)) // 使用整数坐标
        {
            return null; // 如果坐标超出范围或已有cube存在，不生成新对象
        }

        // 将坐标转换回世界位置
        Vector3 correctedPosition = new Vector3(xIndex * size + startPO.x, yIndex * size + startPO.y, 0);

        // 添加到已占用位置集合中
        GameObject newCube = GameObject.Instantiate(cube, correctedPosition, Quaternion.identity);
        newCube.transform.parent = grid.transform;
        newCube.transform.localScale = new Vector3(scale, scale, scale);
        SpriteRenderer h = newCube.GetComponent<SpriteRenderer>();
        h.sortingOrder = 2;
        h.sprite = GridManager.instance.sprites[index];

        // 将位置和对象存储到字典中
        occupiedPositions[gridCoord] = newCube;

        currentObjects++; // 增加当前生成的对象数
        return newCube.transform;
    }

    void Iteration(Transform startTran)
    {
        // 使用一个队列来存储需要继续生成对象的位置
        Queue<Transform> queue = new Queue<Transform>();

        queue.Enqueue(startTran);

        int[,] directions = new int[,]
        {
            { 0, 1 },   // 上
            { 1, 0 },   // 右
            { 0, -1 },  // 下
            { -1, 0 },  // 左
            { -1, 1 },  // 左上
            { -1, -1 }, // 左下
            { 1, 1 },   // 右上
            { 1, -1 }   // 右下
        };

        while (queue.Count > 0 && currentObjects < Mathf.Pow(2 * generationDepth + 1, 2))
        {
            Transform currentTran = queue.Dequeue();

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                float dx = currentTran.position.x + directions[i, 0] * size;
                float dy = currentTran.position.y + directions[i, 1] * size;
                Vector3 position = new Vector3(dx, dy, 0);

                int index = GetIndexFromPosition(position);
                Transform newCubeTransform = CreateObjectAtPosition(position, this.cube, index);

                if (newCubeTransform != null)
                {
                    queue.Enqueue(newCubeTransform);//不为空就加入队列，接着去迭代，等于空就去下一次循环
                }

                // 如果已达到最大对象数，则停止
                if (currentObjects >= Mathf.Pow(2 * generationDepth + 1, 2))
                {
                    break;
                }
            }
        }
    }

    // 根据位置计算索引
    private int GetIndexFromPosition(Vector3 position)
    {
        int x = Mathf.FloorToInt((position.x - startPO.x) / size);
        int y = Mathf.FloorToInt((position.y - startPO.y) / size);

        // 计算距离中心的层数
        int centerX = 0; // 因为 startPO 已经在中心
        int centerY = 0; // 因为 startPO 已经在中心
        int distance = Mathf.Max(Mathf.Abs(x - centerX), Mathf.Abs(y - centerY));

        return distance;
    }

    public void setValue(Vector3 po)
    {
        foreach (var kvp in occupiedPositions)
        {
            Vector2Int position = kvp.Key;
            GameObject cube = kvp.Value;

            // 计算点击位置是否在该 cube 的范围内
            float halfSize = size / 2f;
            Vector3 cubePosition = new Vector3(position.x * size + startPO.x, position.y * size + startPO.y, 0);

            if (po.x >= cubePosition.x - halfSize && po.x <= cubePosition.x + halfSize &&
                po.y >= cubePosition.y - halfSize && po.y <= cubePosition.y + halfSize)
            {

                GameManager.instance.BuildUi.SetActive(true);

                RectTransform uiElement = GameManager.instance.BuildUi.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
;
                Transform h = uiElement.transform.GetChild(0);

                int x = GridManager.instance.sprites.IndexOf(cube.GetComponent<SpriteRenderer>().sprite);

                h.GetComponent<Image>().sprite = GridManager.instance.Buildsprites[x];

                h.GetChild(0).GetComponent<Text>().text = h.GetComponent<Image>().sprite.name;

                //h.GetChild(1).GetComponentInChildren<Text>() = GridManager.instance.Builds[x].GetComponent<Maker>();

                //h.GetChild(2).GetComponentInChildren<Text>();


                uiElement.localPosition = cubePosition * (float)((size * 10 + 4.5) / size);

                return;
            }
        }
    }

    public void makeBuild(Vector3 vector3) {

        Vector3 po = vector3 / (float)(20 / 1.5);

        foreach (var kvp in occupiedPositions)
        {
            Vector2Int position = kvp.Key;
            GameObject cube = kvp.Value;

            // 计算点击位置是否在该 cube 的范围内
            float halfSize = size / 2f;

            Vector3 cubePosition = new Vector3(position.x * size + startPO.x, position.y * size + startPO.y, 0);

            if (po.x >= cubePosition.x - halfSize && po.x <= cubePosition.x + halfSize &&
                po.y >= cubePosition.y - halfSize && po.y <= cubePosition.y + halfSize)
            {
                if (cube.transform.childCount == 0) {
                    GameObject h = GameObject.Instantiate(GridManager.instance.Builds[Mathf.Max((int)(Mathf.Abs(cubePosition.x / size)),
                        (int)(Mathf.Abs(cubePosition.y / size)))], cubePosition, Quaternion.identity);

                    h.transform.SetParent(cube.transform);
                    h.transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }


    }
}
