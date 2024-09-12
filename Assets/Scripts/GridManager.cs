using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    public Grid grid;


    public GameObject startpo;

    public int generationDepth;

    [HideInInspector]
    public GameObject cube;

    public List<Sprite> sprites = new List<Sprite>();
    public List<Sprite> Buildsprites = new List<Sprite>();
    public List<GameObject> Builds = new List<GameObject>();

    public float size;

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

        grid = new Grid(size, startpo.transform.position, cube, generationDepth, (float)(size *0.2));

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject()) {

                Vector3 mousePosition = Input.mousePosition;

                // 将屏幕坐标转换为世界坐标
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                worldPosition.z = 0;
                // 在网格中设置值
                grid.setValue(worldPosition);
            }

        }

    }
}
