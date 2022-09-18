using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

namespace Assets.ScreenMachine {
    public interface IPreloadable{

        Task Preload();
    }
}