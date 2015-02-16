// <copyright file="Advertising.cs" company="Company">
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
    /// Class that represents the advertising.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "JSON deserialization properties must be identical to API names")]
    [CLSCompliant(true)]
    public class Advertising
    {
        /// <summary>
        /// Gets or sets the firm ID of advertising.
        /// </summary>
        /// <value>
        /// The firm ID.
        /// </value>
        public string firm_id { get; set; }

        /// <summary>
        /// Gets or sets the unique firm hash.
        /// </summary>
        /// <value>
        /// The firm hash.
        /// </value>
        public string hash { get; set; }

        /// <summary>
        /// Gets or sets the title of advertising.
        /// </summary>
        /// <value>
        /// The advertising title.
        /// </value>
        public string title { get; set; }

        /// <summary>
        /// Gets or sets the text of advertising.
        /// </summary>
        /// <value>
        /// The advertising text.
        /// </value>
        public string text { get; set; }

        /// <summary>
        /// Gets or sets the FAS warning.
        /// </summary>
        /// <value>
        /// The FAS warning.
        /// </value>
        public string fas_warning { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public string lon { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public string lat { get; set; }
    }
}
