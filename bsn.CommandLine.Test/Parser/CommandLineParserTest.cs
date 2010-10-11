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
	public class CommandLineParserTest: AssertionHelper {
		[Test]
		public void ConsistencyCheck() {
			CommandLineParser.GetSemanticActions();
		}

		[Test]
		public void ParseDotsCommand() {
			ParsedLine parsedLine = CommandLineParser.Parse("..");
			Expect(parsedLine.IsEmpty, EqualTo(false));
			Expect(parsedLine.Unnamed, Is.EquivalentTo(new[] {".."}));
		}

		[Test]
		public void ParseEmpty() {
			ParsedLine parsedLine = CommandLineParser.Parse("");
			Expect(parsedLine.IsEmpty, EqualTo(true));
		}

		[Test]
		public void ParseEmptyWithComment() {
			ParsedLine parsedLine = CommandLineParser.Parse("# nothing to do here");
			Expect(parsedLine.IsEmpty, EqualTo(true));
		}

		[Test]
		[ExpectedException(typeof(FormatException))]
		public void ParseLexicalError() {
			CommandLineParser.Parse("error \"unclosed quoted value");
		}

		[Test]
		public void ParseQuestionmarkCommand() {
			ParsedLine parsedLine = CommandLineParser.Parse("?");
			Expect(parsedLine.IsEmpty, EqualTo(false));
			Expect(parsedLine.Unnamed, Is.EquivalentTo(new[] {"?"}));
		}

		[Test]
		public void ParseQuotedCommand() {
			ParsedLine parsedLine = CommandLineParser.Parse(@"""help""");
			Expect(parsedLine.IsEmpty, EqualTo(false));
			Expect(parsedLine.Unnamed, Is.EquivalentTo(new[] {"help"}));
		}

		[Test]
		public void ParseSingleCommand() {
			ParsedLine parsedLine = CommandLineParser.Parse("help");
			Expect(parsedLine.IsEmpty, EqualTo(false));
			Expect(parsedLine.Unnamed, Is.EquivalentTo(new[] {"help"}));
		}

		[Test]
		[ExpectedException(typeof(FormatException))]
		public void ParseSyntaxError() {
			CommandLineParser.Parse("error=value");
		}
	}
}
