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
using System.IO;

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
					semanticActions = new SemanticTypeActions<CliToken>(CompiledGrammar.Load(typeof(CliToken), "CommandLine.egt"));
					semanticActions.Initialize();
				}
				return semanticActions;
			}
		}

		public static ParsedLine Parse(string line) {
			if (line == null) {
				throw new ArgumentNullException("line");
			}
			using (StringReader reader = new StringReader(line)) {
				SemanticProcessor<CliToken> processor = new SemanticProcessor<CliToken>(reader, GetSemanticActions());
				ParseMessage result = processor.ParseAll();
				if (result != ParseMessage.Accept) {
					throw new FormatException(string.Format("The given string could not be parsed: {0} at position {1}", result, ((IToken)processor.CurrentToken).Position.Index+1));
				}
				return (ParsedLine)processor.CurrentToken;
			}
		}
	}
}
