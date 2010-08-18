using System;
using System.Collections.Generic;
using System.Text;

using bsn.GoldParser.Semantic;

namespace bsn.CommandLine.Parser {
	[Terminal("(EOF)")]
	[Terminal("(Error)")]
	[Terminal("(Whitespace)")]
	[Terminal("(Comment Line)")]
	[Terminal("=")]
	public class CliToken: SemanticToken {
	}
}
