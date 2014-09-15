using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CrownAndAnchorGame;

namespace CrownAndAnchorGame.Tests
{
	[TestClass]
	public class UATBugTests
	{

		[TestMethod]
		[TestCategory("BUG001")]
		public void Test_Game_Does_Actually_Pay_Out_At_Correct_Level()
		{

			// Create the bet amount
			int bet = 5;
			int initialBalance = 10;

			// Create the player object
			Player player = new Player("Tester", initialBalance);

			// Take the bet
			player.takeBet(bet);

			// Assert that the bet has been deducted from the players balance
			Assert.IsTrue(player.Balance == (initialBalance - bet), "But has not been correctly deducted from the players' balance.");

			// Assume we have rolled 3 dice and from those 3, we have a single match
			int match = 1;

			// Generate the winnings
			int winnings = (match * bet);

			// Add the winnings to the players balance
			player.receiveWinnings(winnings);

			// Assert that the winnings have been applied and the players balance is now the initialBalance
			Assert.IsTrue((player.Balance == initialBalance), "The players balance does not match the initial balance.");

		}

		[TestMethod]
		[TestCategory("BUG002")]
		public void Bug_Test_Player_Cannot_Reach_Betting_Limit()
		{

			// Create the bet amount
			int amount = 5;
			int initialBalance = 5;

			// Create the Player object
			Player player = new Player("Tester", initialBalance);
			bool exceededLimit = player.balanceExceedsLimitBy(amount);

			Assert.IsFalse(exceededLimit, "The bug no longer exists as the balance can be the limit of the betting.");

		}

		[TestMethod]
		[TestCategory("BUG002")]
		public void Resolve_Test_Player_Can_Now_Reach_Betting_Limit()
		{

			// Create the bet amount
			int amount = 5;
			int initialBalance = 5;

			// Create the Player object
			Player player = new Player("Tester", initialBalance);
			bool exceededLimit = player.balanceExceedsLimitBy(amount);

			Assert.IsTrue(exceededLimit, "The bug still exists as the balance cannot reach the limit of the betting.");

		}
	}
}
