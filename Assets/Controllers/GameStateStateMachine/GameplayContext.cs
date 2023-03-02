using Assets.Catalogs;


namespace Assets.Controllers {
	public class GameplayContext {

		readonly private WarController warController;

		readonly private MapController mapController;

		readonly private UnitHandler unitHandler;

		readonly private BuildingHandler buildingHandler;

		readonly private CatalogsHolder catalogsHolder;

		public WarController WarController => warController;

		public MapController MapController => mapController;

		public UnitHandler UnitHandler => unitHandler;

		public BuildingHandler BuildingHandler => buildingHandler;
		public TilesCatalog TilesCatalog => catalogsHolder.TilesCatalog;

		public GameplayContext(WarController warController, MapController mapController, UnitHandler unitHandler, BuildingHandler buildingHandler,
			CatalogsHolder catalogsHolder) {
			this.warController = warController;
			this.mapController = mapController;
			this.unitHandler = unitHandler;
			this.buildingHandler = buildingHandler;
			this.catalogsHolder = catalogsHolder;
		}


	}
}