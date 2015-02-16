// <copyright file="Result.cs" company="Company">
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
    using System.Collections.Generic;

    /// <summary>
    /// Class that represents the results of the search.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "JSON deserialization properties must be identical to API names")]
    [CLSCompliant(true)]
    public class Result
    {
        /// <summary>
        /// Gets or sets the unique firm ID.
        /// </summary>
        /// <value>
        /// The firm ID.
        /// </value>
        public string id { get; set; }

        /// <summary>
        /// Gets or sets the longitude coordinates on the map.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public string lon { get; set; }

        /// <summary>
        /// Gets or sets the latitude coordinates on the map.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public string lat { get; set; }

        /// <summary>
        /// Gets or sets the name of the firm.
        /// </summary>
        /// <value>
        /// The firms name.
        /// </value>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the distance from center of the search to the result.
        /// </summary>
        /// <value>
        /// The distance.
        /// </value>
        public string dist { get; set; }

        /// <summary>
        /// Gets or sets the unique hash.
        /// </summary>
        /// <value>
        /// The firm hash.
        /// </value>
        public string hash { get; set; }

        /// <summary>
        /// Gets or sets the city name.
        /// </summary>
        /// <value>
        /// The city name.
        /// </value>
        public string city_name { get; set; }

        /// <summary>
        /// Gets or sets the address of the firm.
        /// </summary>
        /// <value>
        /// The address the address.
        /// </value>
        public string address { get; set; }

        /// <summary>
        /// Gets or sets the rubrics.
        /// </summary>
        /// <value>
        /// The rubrics.
        /// </value>
        public IEnumerable<string> rubrics { get; set; }

        /// <summary>
        /// Gets or sets the firm group. IF firm has branches.
        /// </summary>
        /// <value>
        /// The firm group.
        /// </value>
        public FirmGroup firm_group { get; set; }

        /// <summary>
        /// Gets or sets the microcomment or advertising.
        /// </summary>
        /// <value>
        /// The  microcomment or advertising.
        /// </value>
        public string microcomment { get; set; }

        /// <summary>
        /// Gets or sets the reviews count on Flamp.ru.
        /// </summary>
        /// <value>
        /// The reviews count.
        /// </value>
        public string reviews_count { get; set; }

        /// <summary>
        /// Gets or sets the FAS warning.
        /// </summary>
        /// <value>
        /// The FAS warning.
        /// </value>
        public string fas_warning { get; set; }
    }
}
