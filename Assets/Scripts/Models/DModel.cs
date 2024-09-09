using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using static Shelter.JModel;

namespace Shelter
{
    public class DModel : AbstractModel
    {
        public struct Objects
        {
            public string name;
            public Sprite propArt;
            public bool hasAnim;
            public int animId;
        }
        public Dictionary<int, Objects> objDic = new Dictionary<int, Objects>();
        protected override void OnInit()
        {
            Objects samObj = new Objects();
            #region D1001
            samObj.name = "石头";
            //samArch.propArt = Resources.Load("");
            objDic[1001] = samObj;
            #endregion
            #region D1002
            samObj.name = "木材";
            //samArch.propArt = Resources.Load("");
            objDic[1002] = samObj;
            #endregion
            #region D1003
            samObj.name = "食物";
            //samArch.propArt = Resources.Load("");
            objDic[1003] = samObj;
            #endregion
            #region D1004
            samObj.name = "居民";
            //samArch.propArt = Resources.Load("");
            objDic[1004] = samObj;
            #endregion
        }
    }
}

