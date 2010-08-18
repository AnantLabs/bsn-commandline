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
			CommandLine commandLine = CommandLineParser.Parse("");
			Expect(commandLine.IsEmpty, EqualTo(true));
		}

		[Test]
		public void ParseEmptyWithComment() {
			CommandLine commandLine = CommandLineParser.Parse("# nothing to do here");
			Expect(commandLine.IsEmpty, EqualTo(true));
		}

		[Test]
		public void ParseDotsCommand() {
			CommandLine commandLine = CommandLineParser.Parse("..");
			Expect(commandLine.IsEmpty, EqualTo(false));
			Expect(commandLine.Unnamed, Is.EquivalentTo(new[] {".."}));
		}

		[Test]
		public void ParseQuestionmarkCommand() {
			CommandLine commandLine = CommandLineParser.Parse("?");
			Expect(commandLine.IsEmpty, EqualTo(false));
			Expect(commandLine.Unnamed, Is.EquivalentTo(new[] { "?" }));
		}

		[Test]
		public void ParseSingleCommand() {
			CommandLine commandLine = CommandLineParser.Parse("help");
			Expect(commandLine.IsEmpty, EqualTo(false));
			Expect(commandLine.Unnamed, Is.EquivalentTo(new[] { "help" }));
		}

		[Test]
		public void ParseQuotedCommand() {
			CommandLine commandLine = CommandLineParser.Parse(@"""help""");
			Expect(commandLine.IsEmpty, EqualTo(false));
			Expect(commandLine.Unnamed, Is.EquivalentTo(new[] { "help" }));
		}
	}
}
