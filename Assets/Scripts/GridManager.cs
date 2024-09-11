using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    public Grid grid;


    public GameObject startpo;

    public int generationDepth;


    public GameObject cube;

    public List<Sprite> sprites = new List<Sprite>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(1f, startpo.transform.position, cube, generationDepth);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Vector3 mousePosition = Input.mousePosition;

            // 将屏幕坐标转换为世界坐标
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0;
            // 在网格中设置值
            grid.setValue(worldPosition);
        }

    }
}
