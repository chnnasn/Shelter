using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    float size;
    GameObject cube;
    GameObject grid;
    Vector3 startPO;
    HashSet<Vector3> occupiedPositions = new HashSet<Vector3>();
    int currentObjects = 0; // 当前生成的对象数
    int generationDepth;


    public Grid(float size, Vector3 startPO, GameObject cube, int generationDepth)
    {
        this.startPO = startPO;
        this.size = size;
        this.cube = cube;
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
    private Transform CreateObjectAtPosition(Vector3 gridPosition, GameObject cube, int index)
    {
        if (occupiedPositions.Contains(gridPosition) || currentObjects >= Mathf.Pow(2 * generationDepth + 1, 2))
        {
            return null; // 位置已被占用或已经达到了对象数上限，不生成新的方块
        }

        // 添加到已占用位置集合中
        occupiedPositions.Add(gridPosition);

        GameObject newCube = GameObject.Instantiate(cube, gridPosition, Quaternion.identity);
        newCube.transform.parent = grid.transform;
        newCube.transform.localPosition = gridPosition;
        newCube.transform.localScale = new Vector3(0.18f, 0.18f, 0.18f);
        SpriteRenderer h = newCube.GetComponent<SpriteRenderer>();
        h.sortingOrder = 2;
        h.sprite = GridManager.instance.sprites[index];

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
                float dx = currentTran.localPosition.x + directions[i, 0] * size;
                float dy = currentTran.localPosition.y + directions[i, 1] * size;
                Vector3 position = new Vector3(dx, dy, 0);

                int index = GetIndexFromPosition(position);
                Transform newCubeTransform = CreateObjectAtPosition(position, this.cube, index);

                if (newCubeTransform != null)
                {
                    queue.Enqueue(newCubeTransform);
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
        int centerX = Mathf.FloorToInt((startPO.x - startPO.x) / size);
        int centerY = Mathf.FloorToInt((startPO.y - startPO.y) / size);
        int distance = Mathf.Max(Mathf.Abs(x - centerX), Mathf.Abs(y - centerY));

        return distance;
    }

    public void setValue(Vector3 po)
    {
        foreach (Vector3 position in occupiedPositions)
        {
            // 计算点击位置是否在该 cube 的范围内
            float halfSize = size / 2f;

            if (po.x >= position.x - halfSize && po.x <= position.x + halfSize &&
                po.y >= position.y - halfSize && po.y <= position.y + halfSize)
            {
                // 点击在 cube 上
                Debug.Log($"Clicked on Cube at {position}");
                return;
            }
        }
    }
}
