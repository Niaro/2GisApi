﻿// <copyright file="Project.cs" company="Company">
//
//    Copyright (c) 2011, Anton Gubarenko
//    All rights reserved.
//
//    Redistribution and use in source and binary forms, with or without
//    modification, are permitted provided that the following conditions are met:
//        * Redistributions of source code must retain the above copyright
//          notice, this list of conditions and the following disclaimer.
//        * Redistributions in binary form must reproduce the above copyright
//          notice, this list of conditions and the following disclaimer in the
//          documentation and/or other materials provided with the distribution.
//        * Neither the name of Sergey Mudrov nor the
//          names of its contributors may be used to endorse or promote products
//          derived from this software without specific prior written permission.
//
//    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
//    ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//    WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//    DISCLAIMED. IN NO EVENT SHALL ANTON GUBARENKO BE LIABLE FOR ANY
//    DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
//    (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
//    LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
//    ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
//    (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//    SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
// </copyright>
// <author>Anton Gubarenko</author>
namespace GisApiWrapper.Types
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Class that represents Project.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "JSON deserialization properties must be identical to API names")]
    [CLSCompliant(true)]
    public class Project
    {
        /// <summary>
        /// Gets or sets the projects ID.
        /// </summary>
        /// <value>
        /// The projects ID.
        /// </value>
        public string id {get; set; }

        /// <summary>
        /// Gets or sets the projects name.
        /// </summary>
        /// <value>
        /// The projects name.
        /// </value>
        public string name {get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string language { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>
        public string country_code { get; set; }

        /// <summary>
        /// Gets or sets the timezone.
        /// </summary>
        /// <value>
        /// The timezone.
        /// </value>
        public string timezone {get; set; }

        /// <summary>
        /// Gets or sets the actual extent. Geometry of the project.
        /// </summary>
        /// <value>
        /// The actual extent.
        /// </value>
        public string actual_extent { get; set; }

        /// <summary>
        /// Gets or sets the centroid. Historical center of the city.
        /// </summary>
        /// <value>
        /// The centroid.
        /// </value>
        public string centroid { get; set; }

        /// <summary>
        /// Gets or sets the minimum zoomlevel.
        /// </summary>
        /// <value>
        /// The minimum zoomlevel.
        /// </value>
        public string min_zoomlevel { get; set; }

        /// <summary>
        /// Gets or sets the maximum zoomlevel.
        /// </summary>
        /// <value>
        /// The  maximum zoomlevel.
        /// </value>
        public string max_zoomlevel { get; set; }

        /// <summary>
        /// Gets or sets the zoomlevel.
        /// </summary>
        /// <value>
        /// The zoomlevel.
        /// </value>
        public string zoomlevel { get; set; }

        /// <summary>
        /// Gets or sets the transport. Is there any transport data.
        /// </summary>
        /// <value>
        /// The transport.
        /// </value>
        public string transport { get; set; }

        /// <summary>
        /// Gets or sets the traffic. Is there any traffic data.
        /// </summary>
        /// <value>
        /// The traffic.
        /// </value>
        public string traffic { get; set; }

        /// <summary>
        /// Gets or sets the flamp. Is project in Flamp.
        /// </summary>
        /// <value>
        /// The flamp.
        /// </value>
        public string flamp { get; set; }

        /// <summary>
        /// Gets or sets the firms count in the project.
        /// </summary>
        /// <value>
        /// The firms count.
        /// </value>
        public string firmscount { get; set; }

        /// <summary>
        /// Gets or sets the filials count in the project.
        /// </summary>
        /// <value>
        /// The filials count.
        /// </value>
        public string filialscount { get; set; }

        /// <summary>
        /// Gets or sets the rubrics count in the project.
        /// </summary>
        /// <value>
        /// The rubrics count.
        /// </value>
        public string rubricscount { get; set; }

        /// <summary>
        /// Gets or sets the geos count in the project.
        /// </summary>
        /// <value>
        /// The geos count.
        /// </value>
        public string geoscount { get; set; }
    }
}
