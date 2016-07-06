using System;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace WpfTrainingTask1
{
	[MarkupExtensionReturnType(typeof(Style))]
	public class MultiStyleExtension : MarkupExtension
	{
		private readonly Style[] styles;

		public MultiStyleExtension(Style style1)
		{
			styles = new[] {style1};
		}

		public MultiStyleExtension(Style style1, Style style2)
		{
			styles = new[] { style1, style2 };
		}

		public MultiStyleExtension(Style style1, Style style2, Style style3)
		{
			styles = new[] { style1, style2, style3 };
		}

		public MultiStyleExtension(Style style1, Style style2, Style style3, Style style4)
		{
			styles = new[] { style1, style2, style3, style4 };
		}

		public MultiStyleExtension(Style style1, Style style2, Style style3, Style style4, Style style5)
		{
			styles = new[] { style1, style2, style3, style4, style5 };
		}

		/// <summary>
		/// Returns a style that merges all styles with the keys specified in the constructor.
		/// </summary>
		/// <param name="serviceProvider">The service provider for this markup extension.</param>
		/// <returns>A style that merges all styles with the keys specified in the constructor.</returns>
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			var result = styles.Aggregate(new Style(), (res, st) => res.Merge(st));

			return result;
		}
	}

	public static class MultiStyleMethods
	{
		/// <summary>
		/// Merges the two styles passed as parameters. The first style will be modified to include any 
		/// information present in the second. If there are collisions, the second style takes priority.
		/// </summary>
		/// <param name="style1">First style to merge, which will be modified to include information from the second one.</param>
		/// <param name="style2">Second style to merge.</param>
		public static Style Merge(this Style style1, Style style2)
		{
			if (style1 == null)
				throw new ArgumentNullException(nameof(style1));
			if (style2 == null)
				throw new ArgumentNullException(nameof(style2));
			if (style1.TargetType.IsAssignableFrom(style2.TargetType))
				style1.TargetType = style2.TargetType;
			if (style2.BasedOn != null)
				Merge(style1, style2.BasedOn);
			foreach (SetterBase currentSetter in style2.Setters)
				style1.Setters.Add(currentSetter);
			foreach (TriggerBase currentTrigger in style2.Triggers)
				style1.Triggers.Add(currentTrigger);
			// This code is only needed when using DynamicResources.
			foreach (object key in style2.Resources.Keys)
				style1.Resources[key] = style2.Resources[key];

			return style1;
		}
	}
}