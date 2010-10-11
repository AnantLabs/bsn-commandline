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

namespace bsn.CommandLine.Context {
	[NamedItem("show", "Displays configuration information.")]
	internal class ConfigurationShowCommand<TExecutionContext>: ConfigurationCommandBase<TExecutionContext, IConfigurationRead<TExecutionContext>> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		public ConfigurationShowCommand(ContextBase<TExecutionContext> owner): base(owner) {}

		protected override CommandActionCommandBase<TExecutionContext, IConfigurationRead<TExecutionContext>> CreateActionCommand(IConfigurationRead<TExecutionContext> item) {
			return new ConfigurationShowActionCommand<TExecutionContext, IConfigurationRead<TExecutionContext>>(this, item);
		}
	}
}
