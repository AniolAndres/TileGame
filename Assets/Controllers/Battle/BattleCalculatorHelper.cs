using System.Collections;
using UnityEngine;

namespace Assets.Controllers {
	public class BattleCalculatorHelper {

		private const int maxHp = 1000;

		public BattleConfiguration SimulateBattle(BattleConfiguration config) {

			var attackerHpModifier = RemapFloat(0f, 1f, 0.4f, 1f, config.AttackerHp / maxHp);
			var attackSpecs = config.AttackerUnit.UnitSpecificationConfig;
			var totalAttackerPower = attackSpecs.Attack * attackerHpModifier;

			var defenderHp = Mathf.Max(Mathf.FloorToInt(config.DefenderHp - totalAttackerPower), 0);

			var isDefenderAlive = defenderHp > 0;
			var defenderHpModifier = RemapFloat(0f, 1f, 0.4f, 1f, defenderHp / maxHp);
			var defenderSpecs = config.DefenderUnit.UnitSpecificationConfig;
			var totalCounterPower = defenderSpecs.Attack * defenderHpModifier;
			var potentialAttackerHp = Mathf.Max(Mathf.FloorToInt(config.AttackerHp - totalCounterPower), 0);

			var attackerHp = isDefenderAlive ? potentialAttackerHp : config.AttackerHp;

			return new BattleConfiguration {
				AttackerHp = attackerHp,
				DefenderHp = defenderHp,
				AttackerPosition = config.AttackerPosition,
				DefenderPosition = config.DefenderPosition,
				AttackerTile = config.AttackerTile,
				DefenderTile = config.DefenderTile,
				AttackerUnit = config.AttackerUnit,
				DefenderUnit = config.DefenderUnit
			};
		}

		private float RemapFloat(float prevFrom, float prevTo, float from, float to, float value) {
			var prevRange = prevTo - prevFrom;
			var prevDiff = value - prevFrom;
			var ratio = prevDiff / prevRange;

			var newRange = to - from;
			var newDiff = ratio * newRange;

			return newDiff + from;
		}


	}
}