
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsmSharp.UI.Renderer.Scene2DPrimitives
{
	/// <summary>
	/// A simple 2D polygon.
	/// </summary>
	internal struct Polygon2D : IScene2DPrimitive
	{
		/// <summary>
		/// Gets or sets the x.
		/// </summary>
		/// <value>The x.</value>
		public float[] X {
			get;
			set;
		}
		
		/// <summary>
		/// Gets or sets the y.
		/// </summary>
		/// <value>The y.</value>
		public float[] Y {
			get;
			set;
		}
		
		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public int Color {
			get;
			set;
		}
		
		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>The size.</value>
		public float Width {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="OsmSharp.UI.Renderer.Scene2DPrimitives.Polygon2D"/> is to be filled.
		/// </summary>
		/// <value><c>true</c> if fill; otherwise, <c>false</c>.</value>
		public bool Fill {
			get;
			set;
		}
		
		#region IScene2DPrimitive implementation
		
		/// <summary>
		/// Returns true if the object is visible on the view.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <param name="view">View.</param>
		public bool IsVisibleIn (View2D view)
		{
			for(int idx = 0; idx < this.X.Length; idx++)
			{
				if(view.IsIn(this.X[idx], this.Y[idx]))
				{
					return true;
				}
			}
			return false;
		}
		
		#endregion
	}
}