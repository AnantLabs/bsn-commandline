using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using bsn.GoldParser.Grammar;
using bsn.GoldParser.Parser;
using bsn.GoldParser.Semantic;

namespace bsn.CommandLine.Parser {
	public static class CommandLineParser {
		private static readonly object sync = new object();
		private static SemanticTypeActions<CliToken> semanticActions;

		internal static SemanticTypeActions<CliToken> GetSemanticActions() {
			lock (sync) {
				if (semanticActions == null) {
					semanticActions = new SemanticTypeActions<CliToken>(CompiledGrammar.Load(typeof(CliToken), "CommandLine.cgt"));
					semanticActions.Initialize();
				}
				return semanticActions;
			}
		}

		public static CommandLine Parse(string line) {
			if (line == null) {
				throw new ArgumentNullException("line");
			}
			using (StringReader reader = new StringReader(line)) {
				SemanticProcessor<CliToken> processor = new SemanticProcessor<CliToken>(reader, GetSemanticActions());
				ParseMessage result = processor.ParseAll();
				if (result != ParseMessage.Accept) {
					throw new FormatException(string.Format("The given string could not be parsed: {0} at position {1}", result, ((IToken)processor.CurrentToken).Position.Index+1));
				}
				return (CommandLine)processor.CurrentToken;
			}
		}
	}
}
