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

		[Rule("<LiteralList> ::= <LiteralList> <Literal> ~'=' <Literal>")]
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
