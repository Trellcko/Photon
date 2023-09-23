using Fusion;
using System.Collections.Generic;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core
{
	public class ObjectRegister : NetworkBehaviour
	{
		[SerializeField] private List<NetworkObject> _networkObjects;

        public override void Spawned()
        {
            base.Spawned();
            Runner.RegisterSceneObjects(_networkObjects);

        }

    }
}
