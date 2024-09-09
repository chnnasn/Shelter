using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
namespace Shelter
{
    public class ShelterApp : Architecture<ShelterApp>
    {
        protected override void Init()
        {
            RegisterModel(new GModel());
            RegisterModel(new JModel());
            RegisterModel(new DModel());
            RegisterModel(new MModel());
        }
    }
}

