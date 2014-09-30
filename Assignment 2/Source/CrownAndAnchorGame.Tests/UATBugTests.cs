using System;
using System.Collections.Generic;
using System.Linq;
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

		[TestMethod]
		[TestCategory("BUG003")]
		public void Dice_Roll_Will_Always_Return_A_High_Repetition_Rate()
		{

			// Create the variables for the number of iterations before change, and initial value
			List<int> iterationsBeforeChange = new List<int>();
			int iterationsIndex = 0;
			DiceValue initialValue = DiceValue.ANCHOR;

			// Add the initial iteration
			iterationsBeforeChange.Add(0);

			// Create the dice object
			Dice dice = new Dice();

			// Loop through 1000 rolls of the dice, and get the max iterations before value changes
			for (int i = 0; i < 1000; i++)
			{
				// Roll the dice
				DiceValue val = dice.roll();

				// If the iteration is 0, the set the initial value, otherwise check for changed value
				if (i == 0)
				{
					initialValue = val;
				}
				else
				{

					// if the initial value is the same, then continue to next roll
					if (initialValue == val)
					{
						iterationsBeforeChange[iterationsIndex] += 1;
					}
					else
					{
						// Value has changed, so if add new index and iteration value
						iterationsBeforeChange.Add(0);
						iterationsIndex++;
						initialValue = val;
					}
				}

			}

			bool excessiveRepeats = false;
			int numRepeats = 0;

			// Anything with more than 5 repeat iterations would be bad...
			for (int i = 0; i < iterationsBeforeChange.Count; i++)
			{
				if (iterationsBeforeChange[i] > 5)
				{
					excessiveRepeats = true;
					numRepeats = iterationsBeforeChange[i];
					break;
				}
			}

			// Assert the result
			Assert.IsFalse(excessiveRepeats, "Excessive repeats detected; Number of repeats detected: " + numRepeats.ToString());

		}

		[TestMethod]
		[TestCategory("BUG004")]
		public void Bug_Test_Output_Does_Not_Update_As_Die_Are_Rolled()
		{
			// Create the player object
			Player player = new Player("Test", 100);
			Dice die1 = new Dice();
			Dice die2 = new Dice();
			Dice die3 = new Dice();

			Game game = new Game(die1, die2, die3);

			DiceValue die1Val = DiceValue.ANCHOR;
			DiceValue die2Val = DiceValue.ANCHOR;
			DiceValue die3Val = DiceValue.ANCHOR;

			bool diceValuesChanged = false;

			// Iterate 20 times through a loop
			for (int i = 0; i < 20; i++)
			{

				game.playRound(player, DiceValue.CLUB, 1);

				// Only check after the first iteration so that we have something to check for.
				if (i > 0)
				{
					if (die1Val != game.CurrentDiceValues[0] || die2Val != game.CurrentDiceValues[1] ||
						die3Val != game.CurrentDiceValues[2])
					{
						diceValuesChanged = true;
						break;
					}
				}

				// Set the die values for next round.
				die1Val = game.CurrentDiceValues[0];
				die2Val = game.CurrentDiceValues[1];
				die3Val = game.CurrentDiceValues[2];
			}

			Assert.IsFalse(diceValuesChanged, "The dice values have been updated as part of the Game method 'playRound'");

		}

		[TestMethod]
		[TestCategory("BUG004")]
		public void Resolve_Output_Does_Not_Update_As_Die_Are_Rolled()
		{
			// Create the player object
			Player player = new Player("Test", 100);
			Dice die1 = new Dice();
			Dice die2 = new Dice();
			Dice die3 = new Dice();

			Game game = new Game(die1, die2, die3);

			DiceValue die1Val = DiceValue.ANCHOR;
			DiceValue die2Val = DiceValue.ANCHOR;
			DiceValue die3Val = DiceValue.ANCHOR;

			IList<bool> diceValuesChanged = new List<bool>();

			// Iterate 20 times through a loop
			for (int i = 0; i < 20; i++)
			{

				game.playRound(player, DiceValue.CLUB, 1);

				// Only check after the first iteration so that we have something to check for.
				if (i > 0)
				{

					// If we get the same three dice throws consecutively, the dice values are not updated.
					if (die1Val == game.CurrentDiceValues[0] && die2Val == game.CurrentDiceValues[1] &&
						die3Val == game.CurrentDiceValues[2])
					{
						diceValuesChanged.Add(false);
					}
					else
					{
						diceValuesChanged.Add(true);
					}
				}

				// Set the die values for next round.
				die1Val = game.CurrentDiceValues[0];
				die2Val = game.CurrentDiceValues[1];
				die3Val = game.CurrentDiceValues[2];
			}

			Assert.IsFalse(diceValuesChanged.Any(i => i == false), "The dice values have not been updated as part of the Game method 'playRound'");

		}

	}
}
