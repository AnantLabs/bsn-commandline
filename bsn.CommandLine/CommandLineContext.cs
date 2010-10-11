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
using System.IO;

using bsn.CommandLine.Context;

namespace bsn.CommandLine {
	public class CommandLineContext<TExecutionContext, T>: IExecutionContext<TExecutionContext> where T: RootContext<TExecutionContext>
	                                                                                            where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		private readonly TextReader input;
		private readonly TextWriter output;
		private readonly T rootContext;
		private ContextBase<TExecutionContext> context;

		public CommandLineContext(T rootContext, TextReader input, TextWriter output) {
			if (rootContext == null) {
				throw new ArgumentNullException("rootContext");
			}
			if (input == null) {
				throw new ArgumentNullException("input");
			}
			if (output == null) {
				throw new ArgumentNullException("output");
			}
			this.rootContext = rootContext;
			this.input = input;
			this.output = output;
		}

		public T RootContext {
			get {
				return rootContext;
			}
		}

		RootContext<TExecutionContext> IExecutionContext<TExecutionContext>.RootContext {
			get {
				return RootContext;
			}
		}

		public ContextBase<TExecutionContext> Context {
			get {
				return context;
			}
			set {
				Debug.Assert((value == null) || ReferenceEquals(value.FindRootContext(), rootContext));
				context = value;
			}
		}

		public TextWriter Output {
			get {
				return output;
			}
		}

		public TextReader Input {
			get {
				return input;
			}
		}
	                                                                                            }
}
