﻿// <copyright file="DaysOfWeek.cs" company="Mindfor">
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

using System;
using System.Runtime.Serialization;

namespace Midnfor.DoubleGisApi.Types
{
	public interface IDaySchedule
	{
		DayOfWeek Day { get; }

		/// <summary>
		/// Gets or sets the working hours. 
		/// </summary>
		/// <value> The working hours. </value>
		WorkingHours WorkingHours0 { get; set; }

		/// <summary>
		/// Gets or sets the working hours. Available if there is a break in a working day. 
		/// </summary>
		/// <value> The working hours. </value>
		WorkingHours WorkingHours1 { get; set; }
	}

	/// <summary>
	/// Monday working day class. working_hours1 available if there is a break in a working day. 
	/// </summary>
	[DataContract(Name = "mon", Namespace = "")]
	public class Mon : IDaySchedule
	{
		public DayOfWeek Day { get; } = DayOfWeek.Monday;

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
		[DataMember(Name = "working_hours-1", EmitDefaultValue = false)]
		public WorkingHours WorkingHours1 { get; set; }
	}

	/// <summary>
	/// Tuesday working day class. working_hours1 available if there is a break in a working day. 
	/// </summary>
	[DataContract(Name = "tue", Namespace = "")]
	public class Tue : IDaySchedule
	{
		public DayOfWeek Day { get; } = DayOfWeek.Tuesday;

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
		[DataMember(Name = "working_hours-1", EmitDefaultValue = false)]
		public WorkingHours WorkingHours1 { get; set; }
	}

	/// <summary>
	/// Wednesday working day class. working_hours1 available if there is a break in a working day. 
	/// </summary>
	[DataContract(Name = "wed", Namespace = "")]
	public class Wed : IDaySchedule
	{
		public DayOfWeek Day { get; } = DayOfWeek.Wednesday;

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
		[DataMember(Name = "working_hours-1", EmitDefaultValue = false)]
		public WorkingHours WorkingHours1 { get; set; }
	}

	/// <summary>
	/// Thursday working day class. working_hours1 available if there is a break in a working day. 
	/// </summary>
	[DataContract(Name = "thu", Namespace = "")]
	public class Thu : IDaySchedule
	{
		public DayOfWeek Day { get; } = DayOfWeek.Thursday;

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
		[DataMember(Name = "working_hours-1", EmitDefaultValue = false)]
		public WorkingHours WorkingHours1 { get; set; }
	}

	/// <summary>
	/// Friday working day class. working_hours1 available if there is a break in a working day. 
	/// </summary>
	[DataContract(Name = "fri", Namespace = "")]
	public class Fri : IDaySchedule
	{
		public DayOfWeek Day { get; } = DayOfWeek.Friday;

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
		[DataMember(Name = "working_hours-1", EmitDefaultValue = false)]
		public WorkingHours WorkingHours1 { get; set; }
	}

	/// <summary>
	/// Saturday working day class. working_hours1 available if there is a break in a working day. 
	/// </summary>
	[DataContract(Name = "sat", Namespace = "")]
	public class Sat : IDaySchedule
	{
		public DayOfWeek Day { get; } = DayOfWeek.Saturday;

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
		[DataMember(Name = "working_hours-1", EmitDefaultValue = false)]
		public WorkingHours WorkingHours1 { get; set; }
	}

	/// <summary>
	/// Sunday working day class. working_hours1 available if there is a break in a working day. 
	/// </summary>
	[DataContract(Name = "sun", Namespace = "")]
	public class Sun : IDaySchedule
	{
		public DayOfWeek Day { get; } = DayOfWeek.Sunday;

		/// <summary>
		/// Gets or sets the working hours. 
		/// </summary>
		/// <value> The working hours. </value>
		[DataMember(Name = "working_hours-0")]
		public WorkingHours WorkingHours0 { get; set; }

		[DataMember(Name = "working_hours-1", EmitDefaultValue = false)]
		public WorkingHours WorkingHours1 { get; set; }
	}
}