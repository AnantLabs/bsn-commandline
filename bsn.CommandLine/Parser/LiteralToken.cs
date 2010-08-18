using System;
using System.Collections.Generic;
using System.Text;

using bsn.GoldParser.Semantic;

namespace bsn.CommandLine.Parser {
	[Terminal("UnquotedLiteral")]
	public class LiteralToken: CliToken {
		private readonly string value;

		public string Value {
			get {
				return value;
			}
		}

		public LiteralToken(string value) {
			this.value = value;
		}
	}
}
