

using MegaChaseGame.Behaviours;
using UnityEngine;

namespace MegaChaseGame.Services
{
	public sealed class DistanceGameMessagePrinter
	{
	    private const string WinningMessage = "You Won!";
	    private const string LoseMessage = "You Lose!";

	    public void PrintWinningMessage() => Debug.Log(WinningMessage);
	    public void PrintLoseMessage() => Debug.Log(LoseMessage);
	}
}
