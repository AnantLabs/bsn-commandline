// bsn CommandLine Library
// -----------------------
// 
// Copyright 2010 by Arsène von Wyss - avw@gmx.ch
// 
// Development has been supported by Sirius Technologies AG, Basel
// 
// Source:
// 
// https://bsn-commandline.googlecode.com/hg/
// 
// License:
// 
// The library is distributed under the GNU Lesser General Public License:
// http://www.gnu.org/licenses/lgpl.html
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//  
using System;

using NUnit.Framework;

namespace bsn.CommandLine.Parser {
	[TestFixture]
	public class QuotedLiteralTokenTest: AssertionHelper {
		[Test]
		public void UnquoteLiteralDoubleQuote() {
			Expect(QuotedLiteralToken.UnquoteLiteral(@"""Quoted""""""""Twice"""), EqualTo("Quoted\"\"Twice"));
		}

		[Test]
		public void UnquoteLiteralEmpty() {
			Expect(QuotedLiteralToken.UnquoteLiteral(@""""""), EqualTo(""));
		}

		[Test]
		public void UnquoteLiteralQuoteEnd() {
			Expect(QuotedLiteralToken.UnquoteLiteral(@"""End"""""""), EqualTo("End\""));
		}

		[Test]
		public void UnquoteLiteralQuoteMiddle() {
			Expect(QuotedLiteralToken.UnquoteLiteral(@"""Quoted""""Once"""), EqualTo("Quoted\"Once"));
		}

		[Test]
		public void UnquoteLiteralQuoteStart() {
			Expect(QuotedLiteralToken.UnquoteLiteral(@"""""""Start"""), EqualTo("\"Start"));
		}

		[Test]
		public void UnquoteLiteralSimple() {
			Expect(QuotedLiteralToken.UnquoteLiteral(@"""Some text"""), EqualTo("Some text"));
		}
	}
}
