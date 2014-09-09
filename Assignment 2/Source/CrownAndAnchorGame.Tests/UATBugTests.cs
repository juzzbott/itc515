using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CrownAndAnchorGame;

namespace CrownAndAnchorGame.Tests
{
	[TestClass]
	public class UATBugTests
	{
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
	}
}
