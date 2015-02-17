// <copyright file="DaysOfWeek.cs" company="Mindfor">
//     Copyright (c) 2015, George Evstigneev All rights reserved.
//     
//     Redistribution and use in source and binary forms, with or without modification, are
//     permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright notice, this list of
//       conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice, this list of
//       conditions and the following disclaimer in the documentation and/or other materials
//       provided with the distribution.
//     * Neither the name of Sergey Mudrov nor the names of its contributors may be used to endorse
//       or promote products derived from this software without specific prior written permission.
//     
//     THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS
//     OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
//     MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL GEORGE
//     EVSTIGNEEV BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
//     CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
//     SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
//     THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR
//     OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
//                POSSIBILITY OF SUCH DAMAGE.
// </copyright>
// <author> George Evstigneev </author>

using System.Runtime.Serialization;

namespace DoubleGisApiWrapper.Types
{
	/// <summary>
	/// Monday working day class. working_hours1 available if there is a break in a working day. 
	/// </summary>
	[DataContract(Name = "Mon", Namespace = "")]
	public class Mon
	{
		/// <summary>
		/// Gets or sets the working hours. 
		/// </summary>
		/// <value> The working hours. </value>
		[DataMember(Name = "working_hours-0")]
		public WorkingHours WorkingHours0 { get; set; }

		/// <summary>
		/// Gets or sets the working hours. 
		/// </summary>
		/// <value> The working hours. </value>
		[DataMember(Name = "working_hours-1")]
		public WorkingHours WorkingHours1 { get; set; }
	}

	/// <summary>
	/// Tuesday working day class. working_hours1 available if there is a break in a working day. 
	/// </summary>
	[DataContract(Name = "Tue", Namespace = "")]
	public class Tue
	{
		/// <summary>
		/// Gets or sets the working hours. 
		/// </summary>
		/// <value> The working hours. </value>
		[DataMember(Name = "working_hours-0")]
		public WorkingHours WorkingHours0 { get; set; }

		/// <summary>
		/// Gets or sets the working hours. 
		/// </summary>
		/// <value> The working hours. </value>
		[DataMember(Name = "working_hours-1")]
		public WorkingHours WorkingHours1 { get; set; }
	}

	/// <summary>
	/// Wednesday working day class. working_hours1 available if there is a break in a working day. 
	/// </summary>
	[DataContract(Name = "Wed", Namespace = "")]
	public class Wed
	{
		/// <summary>
		/// Gets or sets the working hours. 
		/// </summary>
		/// <value> The working hours. </value>
		[DataMember(Name = "working_hours-0")]
		public WorkingHours WorkingHours0 { get; set; }

		/// <summary>
		/// Gets or sets the working hours. 
		/// </summary>
		/// <value> The working hours. </value>
		[DataMember(Name = "working_hours-1")]
		public WorkingHours WorkingHours1 { get; set; }
	}

	/// <summary>
	/// Thursday working day class. working_hours1 available if there is a break in a working day. 
	/// </summary>
	[DataContract(Name = "Thu", Namespace = "")]
	public class Thu
	{
		/// <summary>
		/// Gets or sets the working hours. 
		/// </summary>
		/// <value> The working hours. </value>
		[DataMember(Name = "working_hours-0")]
		public WorkingHours WorkingHours0 { get; set; }

		/// <summary>
		/// Gets or sets the working hours. 
		/// </summary>
		/// <value> The working hours. </value>
		[DataMember(Name = "working_hours-1")]
		public WorkingHours WorkingHours1 { get; set; }
	}

	/// <summary>
	/// Friday working day class. working_hours1 available if there is a break in a working day. 
	/// </summary>
	[DataContract(Name = "Fri", Namespace = "")]
	public class Fri
	{
		/// <summary>
		/// Gets or sets the working hours. 
		/// </summary>
		/// <value> The working hours. </value>
		[DataMember(Name = "working_hours-0")]
		public WorkingHours WorkingHours0 { get; set; }

		/// <summary>
		/// Gets or sets the working hours. 
		/// </summary>
		/// <value> The working hours. </value>
		[DataMember(Name = "working_hours-1")]
		public WorkingHours WorkingHours1 { get; set; }
	}

	/// <summary>
	/// Saturday working day class. working_hours1 available if there is a break in a working day. 
	/// </summary>
	[DataContract(Name = "Sat", Namespace = "")]
	public class Sat
	{
		/// <summary>
		/// Gets or sets the working hours. 
		/// </summary>
		/// <value> The working hours. </value>
		[DataMember(Name = "working_hours-0")]
		public WorkingHours WorkingHours0 { get; set; }

		/// <summary>
		/// Gets or sets the working hours. 
		/// </summary>
		/// <value> The working hours. </value>
		[DataMember(Name = "working_hours-1")]
		public WorkingHours WorkingHours1 { get; set; }
	}

	/// <summary>
	/// Sunday working day class. working_hours1 available if there is a break in a working day. 
	/// </summary>
	[DataContract(Name = "Sun", Namespace = "")]
	public class Sun
	{
		/// <summary>
		/// Gets or sets the working hours. 
		/// </summary>
		/// <value> The working hours. </value>
		[DataMember(Name = "working_hours-0")]
		public WorkingHours WorkingHours0 { get; set; }

		/// <summary>
		/// Gets or sets the working hours. 
		/// </summary>
		/// <value> The working hours. </value>
		[DataMember(Name = "working_hours-1")]
		public WorkingHours WorkingHours1 { get; set; }
	}
}