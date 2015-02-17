// <copyright file="Project.cs" company="Mindfor">
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

namespace DoubleGisApi.Types
{
	/// <summary>
	/// Class that represents Project. 
	/// </summary>
	[DataContract(Name = "project", Namespace = "")]
	public class Project
	{
		/// <summary>
		/// Gets or sets the projects ID. 
		/// </summary>
		/// <value> The projects ID. </value>
		[DataMember(Name = "id")]
		private string Id { get; set; }

		/// <summary>
		/// Gets or sets the projects name. 
		/// </summary>
		/// <value> The projects name. </value>
		[DataMember(Name = "name")]
		private string Name { get; set; }

		/// <summary>
		/// Gets or sets the language. 
		/// </summary>
		/// <value> The language. </value>
		[DataMember(Name = "language")]
		private string Language { get; set; }

		/// <summary>
		/// Gets or sets the country code. 
		/// </summary>
		/// <value> The country code. </value>
		[DataMember(Name = "country_code")]
		private string CountryCode { get; set; }

		/// <summary>
		/// Gets or sets the timezone. 
		/// </summary>
		/// <value> The timezone. </value>
		[DataMember(Name = "timezone")]
		private string Timezone { get; set; }

		/// <summary>
		/// Gets or sets the actual extent. Geometry of the project. 
		/// </summary>
		/// <value> The actual extent. </value>
		[DataMember(Name = "actual_extent")]
		private string ActualExtent { get; set; }

		/// <summary>
		/// Gets or sets the centroid. Historical center of the city. 
		/// </summary>
		/// <value> The centroid. </value>
		[DataMember(Name = "centroid")]
		private string Centroid { get; set; }

		/// <summary>
		/// Gets or sets the minimum zoomlevel. 
		/// </summary>
		/// <value> The minimum zoomlevel. </value>
		[DataMember(Name = "min_zoomlevel")]
		private string MinZoomLevel { get; set; }

		/// <summary>
		/// Gets or sets the maximum zoomlevel. 
		/// </summary>
		/// <value> The maximum zoomlevel. </value>
		[DataMember(Name = "max_zoomlevel")]
		private string MaxZoomLevel { get; set; }

		/// <summary>
		/// Gets or sets the zoomlevel. 
		/// </summary>
		/// <value> The zoomlevel. </value>
		[DataMember(Name = "zoomlevel")]
		private string ZoomLevel { get; set; }

		/// <summary>
		/// Gets or sets the transport. Is there any transport data. 
		/// </summary>
		/// <value> The transport. </value>
		[DataMember(Name = "transport")]
		private string Transport { get; set; }

		/// <summary>
		/// Gets or sets the traffic. Is there any traffic data. 
		/// </summary>
		/// <value> The traffic. </value>
		[DataMember(Name = "traffic")]
		private string Traffic { get; set; }

		/// <summary>
		/// Gets or sets the flamp. Is project in Flamp. 
		/// </summary>
		/// <value> The flamp. </value>
		[DataMember(Name = "flamp")]
		private string Flamp { get; set; }

		/// <summary>
		/// Gets or sets the firms count in the project. 
		/// </summary>
		/// <value> The firms count. </value>
		[DataMember(Name = "firmscount")]
		private string FirmsCount { get; set; }

		/// <summary>
		/// Gets or sets the filials count in the project. 
		/// </summary>
		/// <value> The filials count. </value>
		[DataMember(Name = "filialscount")]
		private string FilialsCount { get; set; }

		/// <summary>
		/// Gets or sets the rubrics count in the project. 
		/// </summary>
		/// <value> The rubrics count. </value>
		[DataMember(Name = "rubricscount")]
		private string RubricsCount { get; set; }

		/// <summary>
		/// Gets or sets the geos count in the project. 
		/// </summary>
		/// <value> The geos count. </value>
		[DataMember(Name = "geoscount")]
		private string GeosCount { get; set; }
	}
}