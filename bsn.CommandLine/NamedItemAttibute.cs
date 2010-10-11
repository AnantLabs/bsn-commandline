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

namespace bsn.CommandLine {
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public sealed class NamedItemAttribute: Attribute {
		private readonly string description;
		private readonly string name;
		private string remarks;

		public NamedItemAttribute(string name, string description) {
			if (string.IsNullOrEmpty(name)) {
				throw new ArgumentNullException("name");
			}
			if (description == null) {
				throw new ArgumentNullException("description");
			}
			this.name = name;
			this.description = description;
		}

		public string Description {
			get {
				return description;
			}
		}

		public string Name {
			get {
				return name;
			}
		}

		public string Remarks {
			get {
				return remarks;
			}
			set {
				remarks = value;
			}
		}
	}
}
