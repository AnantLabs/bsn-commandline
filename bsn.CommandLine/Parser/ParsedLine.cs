// bsn CommandLine Library
// -----------------------
// 
// Copyright 2010 by Arsène von Wyss - avw@gmx.ch
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
using System.Collections.Generic;

using bsn.GoldParser.Semantic;

namespace bsn.CommandLine.Parser {
	public class ParsedLine: CliToken {
		private readonly Dictionary<string, string> named = new Dictionary<string, string>();
		private readonly List<string> unnamed = new List<string>();

		[Rule("<CommandLine> ::=")]
		public ParsedLine() {}

		[Rule("<CommandLine> ::= <LiteralList>")]
		public ParsedLine(LiteralList list): this() {
			while (list != null) {
				if (list.Name != null) {
					named.Add(list.Name, list.Value);
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

		public IDictionary<string, string> Named {
			get {
				return named;
			}
		}

		public IList<string> Unnamed {
			get {
				return unnamed;
			}
		}
	}
}
