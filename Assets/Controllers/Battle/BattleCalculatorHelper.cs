using System.Collections;
using UnityEngine;

namespace Assets.Controllers {
	public class BattleCalculatorHelper {

		public BattleConfiguration SimulateBattle(BattleConfiguration config) {

			return new BattleConfiguration {
				AttackerHp = config.AttackerHp,
				DefenderHp = 0,
				AttackerPosition = config.AttackerPosition,
				DefenderPosition = config.DefenderPosition,
				AttackerTile = config.AttackerTile,
				DefenderTile = config.DefenderTile,
				AttackerUnit = config.AttackerUnit,
				DefenderUnit = config.DefenderUnit
			};
		}


	}
}