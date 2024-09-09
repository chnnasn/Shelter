using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
namespace Shelter
{
    public class JModel : AbstractModel
    {
        public struct Architectures
        {
            public string nameItem;
            public string name;
            public Sprite propArt;
            public bool hasAnim;
            public int animId;
        }
        public Dictionary<int, Architectures> archDic=new Dictionary<int, Architectures>();
        protected override void OnInit()
        {
            Architectures samArch = new Architectures();
            #region J2001
            samArch.nameItem = "防御建筑";
            samArch.name = "1";
            //samArch.propArt = Resources.Load("");
            archDic[2001]= samArch;
            #endregion
            #region J2002
            samArch.nameItem = "食物建筑";
            samArch.name = "1";
            //samArch.propArt = Resources.Load("");
            archDic[2002] = samArch;
            #endregion
        }
    }
}


