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
