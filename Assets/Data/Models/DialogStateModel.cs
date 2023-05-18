

using Assets.Catalogs;

namespace Assets.Data.Models {
	public class DialogStateModel {

		private readonly DialogsCatalog dialogsCatalog;

		private int dialogIndex = 0;

		public DialogStateModel(DialogsCatalog dialogsCatalog) {
			this.dialogsCatalog = dialogsCatalog;
		}

	}
}