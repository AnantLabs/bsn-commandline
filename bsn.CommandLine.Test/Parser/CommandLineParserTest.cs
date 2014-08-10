// bsn CommandLine Library
// -----------------------
// 
// Copyright 2014 by Arsène von Wyss - avw@gmx.ch
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

using Xunit;

namespace bsn.CommandLine.Parser {
	public class CommandLineParserTest {
		[Fact]
		public void ConsistencyCheck() {
			CommandLineParser.GetSemanticActions();
		}

		[Fact]
		public void ParseDotsCommand() {
			ParsedLine parsedLine = CommandLineParser.Parse("..");
			Assert.False(parsedLine.IsEmpty);
			Assert.Single(parsedLine.Unnamed, "..");
		}

		[Fact]
		public void ParseEmpty() {
			ParsedLine parsedLine = CommandLineParser.Parse("");
			Assert.True(parsedLine.IsEmpty);
		}

		[Fact]
		public void ParseEmptyWithComment() {
			ParsedLine parsedLine = CommandLineParser.Parse("# nothing to do here");
			Assert.True(parsedLine.IsEmpty);
		}

		[Fact]
		public void ParseLexicalError() {
			Assert.Throws<FormatException>(() => CommandLineParser.Parse("error \"unclosed quoted value"));
		}

		[Fact]
		public void ParseQuestionmarkCommand() {
			ParsedLine parsedLine = CommandLineParser.Parse("?");
			Assert.False(parsedLine.IsEmpty);
			Assert.Single(parsedLine.Unnamed, "?");
		}

		[Fact]
		public void ParseQuotedCommand() {
			ParsedLine parsedLine = CommandLineParser.Parse(@"""help""");
			Assert.False(parsedLine.IsEmpty);
			Assert.Single(parsedLine.Unnamed, "help");
		}

		[Fact]
		public void ParseSingleCommand() {
			ParsedLine parsedLine = CommandLineParser.Parse("help");
			Assert.False(parsedLine.IsEmpty);
			Assert.Single(parsedLine.Unnamed, "help");
		}

		[Fact]
		public void ParseSyntaxError() {
			Assert.Throws<FormatException>(() => CommandLineParser.Parse("error=value"));
		}
	}
}
