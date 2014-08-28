﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Librarian.Daos;
using Librarian.Helpers;
using Librarian.Entities;
using Librarian.Interfaces.Daos;
using Librarian.Interfaces.Entities;
using Librarian.Interfaces.Helpers;

namespace Librarian.Tests
{
	[TestClass]
	public class ReturnBorrowedBook
	{

		#region Test Properties

		/// <summary>
		/// The mock IMember object.
		/// </summary>
		private IMember _mockMember;

		/// <summary>
		/// The collection of mock IBook objects.
		/// </summary>
		private IList<IBook> _mockBooks;

		/// <summary>
		/// The ILoanDAO object for storing loans into.
		/// </summary>
		private ILoanDAO _loanDao;

		/// <summary>
		/// The IBookDAO object for storing books into.
		/// </summary>
		private IBookDAO _bookDao;

		#endregion

		/// <summary>
		/// Test initialise method. Ensuring any required test data for the tests exists.
		/// </summary>
		[TestInitialize]
		public void Initialise()
		{

			// Create the mock member object
			IMemberHelper memberHelper = new MemberHelper();
			_mockMember = memberHelper.makeMember("Test", "Member", "03 9999 0000", "test@email.com", 1);

			// Instantiate the list of IBook objects
			_mockBooks = new List<IBook>();

			// Create the two mock book objects. One is to be returned undamaged, the other to be returned damaged.
			IBookHelper bookHelper = new BookHelper();
			_mockBooks.Add(bookHelper.makeBook("Test Author", "Leanrning Testing", "TAU-001", 1));
			_mockBooks.Add(bookHelper.makeBook("Another Author", "Writing Tests For Dummies", "AAU-001", 2));

			// Instantiate the ILoanDao object
			ILoanHelper loanHelper = new LoanHelper();
			this._loanDao = new LoanDAO(loanHelper);

			// Instantiate the IBookHelper and IBookDAO objects
			this._bookDao = new BookDAO(bookHelper);

		}

		/// <summary>
		/// Test cleanup method. Ensuring any cleanup required for test data is executed.
		/// </summary>
		[TestCleanup]
		public void Cleanup()
		{

			// Normally, for interation testing, you could remove any objects created in the database for testing here. 
			// To simulate the use the of the Cleanup method, we'll just clear the list of mock book objects
			if (_mockBooks != null)
			{
				_mockBooks.Clear();
			}
			
		}

		[TestMethod]
		[TestCategory("Use Case: Return Borrowed Book")]
		public void Test_Can_Create_Pending_Loan()
		{

			// To create the pending loan, we need to ensure that the member and books exist.
			Assert.IsNotNull(this._mockMember, "The mock IMember object is null.");
			Assert.IsNotNull(this._mockBooks, "The mock IBook collection is null.");
			Assert.IsTrue((this._mockBooks.Count >= 2), "The mock IBook collection does not contain 2 or more IBook objects.");

			// We also need to ensure the loan DAO object exists
			Assert.IsNotNull(this._loanDao, "The ILoanDAO object is null.");

			// Create the pending loan list
			_loanDao.createNewPendingList(_mockMember);

			// Create the pending loan object
			ILoan newLoan = _loanDao.createPendingLoan(_mockMember, _mockBooks[0], DateTime.Now, DateTime.Now.AddDays(LoanConstants.LOAN_PERIOD));

			// Validate the new ILoan object
			Assert.IsNotNull(newLoan, "The new loan object is null.");
			Assert.IsTrue(newLoan.getState() == LoanConstants.LoanState.PENDING, "The new ILoan is not in the pending state.");
			Assert.IsTrue((newLoan.getID() > 0), "The new ILoan has no valid Id.");
			Assert.IsNotNull(newLoan.getBook(), "The new ILoan object has no valid IBook object.");
			Assert.IsTrue((newLoan.getBook().getID() == _mockBooks[0].getID()), "The new ILoan objects book Id does not match the Id supplied to it.");

		}

		[TestMethod]
		[TestCategory("Use Case: Return Borrowed Book")]
		public void Test_Can_Get_Book_By_Id()
		{

			// To create the pending loan, we need to ensure that the member and books exist.
			Assert.IsNotNull(this._mockMember, "The mock IMember object is null.");
			Assert.IsNotNull(this._mockBooks, "The mock IBook collection is null.");
			Assert.IsTrue((this._mockBooks.Count >= 2), "The mock IBook collection does not contain 2 or more IBook objects.");

			// Add the two mock books to the _bookDAO object
			IBook newBook = _bookDao.addBook(_mockBooks[0].getAuthor(), _mockBooks[0].getTitle(), _mockBooks[0].getCallNumber());
			IBook additionalBook = _bookDao.addBook(_mockBooks[1].getAuthor(), _mockBooks[1].getTitle(), _mockBooks[1].getCallNumber());

			// Get the second book based on the id created
			IBook secondBookInDao = _bookDao.getBookByID(additionalBook.getID());

			// Ensure the book we retrieved was the correct book
			Assert.IsNotNull(secondBookInDao, "The IBookDAO getBookById returned a null book.");
			Assert.IsTrue((secondBookInDao.getID() == _mockBooks[1].getID()), "The book from the IBookDAO and mock book list do not have matching Ids.");
			Assert.IsTrue((secondBookInDao.getTitle() == _mockBooks[1].getTitle()), "The book from the IBookDAO and mock book list do not have matching titles.");
			Assert.IsTrue((secondBookInDao.getAuthor() == _mockBooks[1].getAuthor()), "The book from the IBookDAO and mock book list do not have matching authors.");
			Assert.IsTrue((secondBookInDao.getCallNumber() == _mockBooks[1].getCallNumber()), "The book from the IBookDAO and mock book list do not have matching call numbers.");

		}

		[TestMethod]
		[TestCategory("Use Case: Return Borrowed Book")]
		public void Test_Can_Return_Book()
		{
		}

		[TestMethod]
		[TestCategory("Use Case: Return Borrowed Book")]
		public void Test_Can_Get_Correct_Book_State()
		{
		}


	}
}
