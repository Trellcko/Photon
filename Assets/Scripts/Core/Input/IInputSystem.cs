using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Input
{
    public interface IInputSystem
    {
        Vector2 Position { get; }

        float Scroll { get; }
    }
}
