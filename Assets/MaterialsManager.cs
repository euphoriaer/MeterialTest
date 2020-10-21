using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialsManager : MonoBehaviour
{
    private Dictionary<string, Material> matDic = new Dictionary<string, Material>();

    [Header("材质库（管理所有材质：被替换的材质一定要在里面）")]
    public List<Material> materials;

    [Header("要替换的物体（自动检索所有物体是否带有对应材质）")]
    public List<GameObject> obj;

    [Header("启动按键")]
    public Button button, button1, button2;

    public void test1()
    {
        OnClickRpMat("Grass & dead leafs pattern 02", "Material.001");
    }

    public void test2()
    {
        OnClickRpMat("Material", "Grass pattern 04");
    }

    public void test3()
    {
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(test1);
            button1.onClick.AddListener(test2);
            button2.onClick.AddListener(test3);
        }
        else
        {
            Debug.LogError("启动按钮没有设置");
        }
        if (materials != null)
        {
            foreach (var item in materials)
            {
                // Debug.Log(item.name);
                matDic.Add(item.name + " (Instance)", item);
            }
        }
        else
        {
            Debug.LogError("材质列表是空的");
        }
    }

    /// <summary>
    /// 替换材质
    /// </summary>
    /// <param name="beMatName">被替换的材质名</param>
    /// <param name="matname">替换的材质名</param>
    private void OnClickRpMat(string beMatName, string matname)
    {
        string nameMat = "";
        string beReName = beMatName + " (Instance)";
        string ReName = matname + " (Instance)";
        foreach (var item in obj)
        {
            for (int i = 0; i < item.GetComponent<MeshRenderer>().materials.Length; i++)
            {
                Debug.Log(item.GetComponent<MeshRenderer>().materials[i].name);
                nameMat = item.GetComponent<MeshRenderer>().materials[i].name;

                if (beReName == nameMat)
                {
                    if (matDic.ContainsKey(ReName))
                    {
                        Debug.LogError("有相同材质：开始替换");
                        item.GetComponent<MeshRenderer>().materials[i].CopyPropertiesFromMaterial(matDic[ReName]);
                        item.GetComponent<MeshRenderer>().materials[i].shader = matDic[ReName].shader;
                        //item.GetComponent<Renderer>().materials[i] = testRed;
                    }
                    else
                    {
                        Debug.LogError("材质库里没有要替换的对应材质，请添加");
                    }
                }
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}