#if __ANDROID__

namespace Windows.UI.Notifications
{

	// small, dummy change to restart build

    public partial class TileUpdateManager
    {
        public static TileUpdater CreateTileUpdaterForApplication()
        {
            return new TileUpdater(null);
        }

        // for future implementation
        //public static TileUpdater CreateTileUpdaterForSecondaryTile(string tileId)
        //{
        //    return new TileUpdater(tileId);
        //}
    }
}

#endif
