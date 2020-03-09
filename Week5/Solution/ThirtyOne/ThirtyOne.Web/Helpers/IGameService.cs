using ThirtyOne.Shared.Models;

namespace ThirtyOne.Web.Helpers
{
    public interface IGameService
    {
        void DeleteGame(int id);
        bool GameExist(int id);
        Game LoadGame(int id);
        void SaveGame(Game g);
    }
}