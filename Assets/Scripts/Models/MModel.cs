using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
namespace Shelter
{
    public class MModel : AbstractModel
    {
        public struct Adventures
        {
            public string name;
            public string description;
            public int peopleNeed;
            public int right;
            public int number;
            public int totalOut;
            public bool isReduce;
            public int peopleReduced;
        }
        public Dictionary<int,Adventures> advDict=new Dictionary<int, Adventures>();
        protected override void OnInit()
        {
            Adventures samAdv= new Adventures();
            #region M3001
            samAdv.name = "食物";
            samAdv.description = "食物*10";
            samAdv.peopleNeed = 3;
            samAdv.right = 10000;
            samAdv.number = 10;
            samAdv.totalOut = 1;
            samAdv.isReduce = false;
            samAdv.peopleReduced = 0;
            advDict[3001] = samAdv;
            #endregion
            #region M3002
            samAdv.name = "石材";
            samAdv.description = "石材*5";
            samAdv.peopleNeed = 3;
            samAdv.right = 8000;
            samAdv.number = 10;
            samAdv.totalOut = 1;
            samAdv.isReduce = false;
            samAdv.peopleReduced = 0;
            advDict[3002] = samAdv;
            #endregion
            #region M3003
            samAdv.name = "食物";
            samAdv.description = "食物*10";
            samAdv.peopleNeed = 4;
            samAdv.right = 10000;
            samAdv.number = 10;
            samAdv.totalOut = 2;
            samAdv.isReduce = false;
            samAdv.peopleReduced = 0;
            advDict[3003] = samAdv;
            #endregion
            #region M3004
            samAdv.name = "石材";
            samAdv.description = "石材*5";
            samAdv.peopleNeed = 4;
            samAdv.right = 8000;
            samAdv.number = 10;
            samAdv.totalOut = 2;
            samAdv.isReduce = false;
            samAdv.peopleReduced = 0;
            advDict[3004] = samAdv;
            #endregion
        }

    }
}

