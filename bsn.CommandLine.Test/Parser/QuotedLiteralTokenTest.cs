using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace bsn.CommandLine.Parser {
	[TestFixture]
	public class QuotedLiteralTokenTest: AssertionHelper {
		[Test]
		public void UnquoteLiteralEmpty() {
			Expect(QuotedLiteralToken.UnquoteLiteral(@""""""), EqualTo(""));
		}

		[Test]
		public void UnquoteLiteralSimple() {
			Expect(QuotedLiteralToken.UnquoteLiteral(@"""Some text"""), EqualTo("Some text"));
		}

		[Test]
		public void UnquoteLiteralQuoteStart() {
			Expect(QuotedLiteralToken.UnquoteLiteral(@"""""""Start"""), EqualTo("\"Start"));
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
		public void UnquoteLiteralDoubleQuote() {
			Expect(QuotedLiteralToken.UnquoteLiteral(@"""Quoted""""""""Twice"""), EqualTo("Quoted\"\"Twice"));
		}
	}
}
