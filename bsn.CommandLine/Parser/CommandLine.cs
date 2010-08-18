using System;
using System.Collections.Generic;
using System.Text;

using bsn.GoldParser.Semantic;

namespace bsn.CommandLine.Parser {
	public class CommandLine: CliToken {
		private List<KeyValuePair<string, string>> named = new List<KeyValuePair<string, string>>();
		private List<string> unnamed = new List<string>();

		[Rule("<CommandLine> ::=")]
		public CommandLine() {
		}

		[Rule("<CommandLine> ::= <LiteralList>")]
		public CommandLine(LiteralList list) : this() {
			while (list != null) {
				if (list.Name != null) {
					named.Insert(0, new KeyValuePair<string, string>(list.Name, list.Value));
				} else {
					unnamed.Insert(0, list.Value);
				}
				list = list.Previous;
			}
		}

		public bool IsEmpty {
			get {
				return unnamed.Count == 0;
			}
		}

		public List<KeyValuePair<string, string>> Named {
			get {
				return named;
			}
		}

		public List<string> Unnamed {
			get {
				return unnamed;
			}
		}
	}
}
