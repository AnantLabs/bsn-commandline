using System;
using System.Diagnostics;
using System.Text;

using bsn.GoldParser.Semantic;

namespace bsn.CommandLine.Parser {
	[Terminal("QuotedLiteral")]
	public class QuotedLiteralToken: LiteralToken {
		internal static string UnquoteLiteral(string value) {
			StringBuilder result = new StringBuilder(value.Length-2);
			Debug.Assert(value[0] == '"');
			int i = 1;
			bool keepQuote = false;
			while (i < value.Length) {
				char c = value[i++];
				bool isNotQuote = c != '"';
				if (isNotQuote || keepQuote) {
					result.Append(c);
					keepQuote = false;
				} else {
					keepQuote = true;
				}
			}
			return result.ToString();
		}

		public QuotedLiteralToken(string value): base(UnquoteLiteral(value)) {}
	}
}
