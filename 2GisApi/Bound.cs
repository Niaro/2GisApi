// <copyright file="Bound.cs" company="Mindfor">
//     Copyright (c) 2015-2012, George Evstigneev All rights reserved.
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

namespace DoubleGisApiWrapper
{
	/// <summary>
	/// Bounds for Firms search. 
	/// </summary>
	public class Bound
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Bound"/> class. 
		/// </summary>
		/// <param name="upperLeftPoint"> The upper left point. </param>
		/// <param name="lowerRightPoint"> The lower right point. </param>
		public Bound(GeoPoint upperLeftPoint, GeoPoint lowerRightPoint)
		{
			UpperLeftPoint = upperLeftPoint;
			LowerRightPoint = lowerRightPoint;
		}

		/// <summary>
		/// Gets or sets the upper left point. 
		/// </summary>
		/// <value> The upper left. </value>
		public GeoPoint UpperLeftPoint { get; set; }

		/// <summary>
		/// Gets or sets the lower right point. 
		/// </summary>
		/// <value> The lower right point. </value>
		public GeoPoint LowerRightPoint { get; set; }
	}
}