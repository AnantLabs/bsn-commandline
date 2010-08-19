using System;
using System.Diagnostics;

using bsn.GoldParser.Semantic;

namespace bsn.CommandLine.Parser {
	public class LiteralList: CliToken {
		private readonly string name;
		private readonly LiteralList previous;
		private readonly string value;

		[Rule("<LiteralList> ::= <Literal>")]
		public LiteralList(LiteralToken value): this(null, null, value) {}

		[Rule("<LiteralList> ::= <LiteralList> <Literal>")]
		public LiteralList(LiteralList previous, LiteralToken value): this(previous, null, value) {}

		[Rule("<LiteralList> ::= <LiteralList> <Literal> '=' <Literal>", ConstructorParameterMapping = new[] {0, 1, 3})]
		public LiteralList(LiteralList previous, LiteralToken name, LiteralToken value) {
			Debug.Assert(value != null);
			this.previous = previous;
			this.name = (name != null) ? name.Value : null;
			this.value = value.Value;
		}

		public string Name {
			get {
				return name;
			}
		}

		public LiteralList Previous {
			get {
				return previous;
			}
		}

		public string Value {
			get {
				return value;
			}
		}
	}
}
