using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
namespace Shelter
{
    public class AbstractController : IController
{
        public IArchitecture GetArchitecture()
        {
            return ShelterApp.Interface;
        }
    }
}


