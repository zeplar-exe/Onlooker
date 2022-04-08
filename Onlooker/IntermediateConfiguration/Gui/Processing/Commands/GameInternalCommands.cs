using Onlooker.Monogame;
using Onlooker.Monogame.Controllers;

namespace Onlooker.IntermediateConfiguration.GUI.Processing.Commands;

public static class GameInternalCommands
{
    public static void Play()
    {
        GameManager.Current.MainController.State.MoveTo(GameState.WorldStarted);
    }
}