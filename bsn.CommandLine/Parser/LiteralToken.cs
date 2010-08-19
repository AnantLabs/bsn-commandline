using System;

using bsn.GoldParser.Semantic;

namespace bsn.CommandLine.Parser {
	[Terminal("UnquotedLiteral")]
	public class LiteralToken: CliToken {
		private readonly string value;

		public LiteralToken(string value) {
			this.value = value;
		}

		public string Value {
			get {
				return value;
			}
		}
	}
}
