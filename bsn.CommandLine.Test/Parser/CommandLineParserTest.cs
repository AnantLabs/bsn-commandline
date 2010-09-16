using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace bsn.CommandLine.Parser {
	[TestFixture]
	public class CommandLineParserTest: AssertionHelper {
		[Test]
		public void ConsistencyCheck() {
			CommandLineParser.GetSemanticActions();
		}

		[Test]
		[ExpectedException(typeof(FormatException))]
		public void ParseLexicalError() {
			CommandLineParser.Parse("error \"unclosed quoted value");
		}

		[Test]
		[ExpectedException(typeof(FormatException))]
		public void ParseSyntaxError() {
			CommandLineParser.Parse("error=value");
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
		public void ParseDotsCommand() {
			ParsedLine parsedLine = CommandLineParser.Parse("..");
			Expect(parsedLine.IsEmpty, EqualTo(false));
			Expect(parsedLine.Unnamed, Is.EquivalentTo(new[] {".."}));
		}

		[Test]
		public void ParseQuestionmarkCommand() {
			ParsedLine parsedLine = CommandLineParser.Parse("?");
			Expect(parsedLine.IsEmpty, EqualTo(false));
			Expect(parsedLine.Unnamed, Is.EquivalentTo(new[] { "?" }));
		}

		[Test]
		public void ParseSingleCommand() {
			ParsedLine parsedLine = CommandLineParser.Parse("help");
			Expect(parsedLine.IsEmpty, EqualTo(false));
			Expect(parsedLine.Unnamed, Is.EquivalentTo(new[] { "help" }));
		}

		[Test]
		public void ParseQuotedCommand() {
			ParsedLine parsedLine = CommandLineParser.Parse(@"""help""");
			Expect(parsedLine.IsEmpty, EqualTo(false));
			Expect(parsedLine.Unnamed, Is.EquivalentTo(new[] { "help" }));
		}
	}
}
