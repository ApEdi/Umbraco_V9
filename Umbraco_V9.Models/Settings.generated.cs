//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v9.3.0+eea02137ae0b709861b45ded11882279a990c421
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Infrastructure.ModelsBuilder;
using Umbraco.Cms.Core;
using Umbraco.Extensions;

namespace Umbraco.Cms.Web.Common.PublishedModels
{
	// Mixin Content Type with alias "settings"
	/// <summary>Settings</summary>
	public partial interface ISettings : IPublishedContent
	{
		/// <summary>Show Navigation</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.3.0+eea02137ae0b709861b45ded11882279a990c421")]
		bool UmbracoNaviHide { get; }
	}

	/// <summary>Settings</summary>
	[PublishedModel("settings")]
	public partial class Settings : PublishedContentModel, ISettings
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.3.0+eea02137ae0b709861b45ded11882279a990c421")]
		public new const string ModelTypeAlias = "settings";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.3.0+eea02137ae0b709861b45ded11882279a990c421")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.3.0+eea02137ae0b709861b45ded11882279a990c421")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.3.0+eea02137ae0b709861b45ded11882279a990c421")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<Settings, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public Settings(IPublishedContent content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// Show Navigation
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.3.0+eea02137ae0b709861b45ded11882279a990c421")]
		[ImplementPropertyType("umbracoNaviHide")]
		public virtual bool UmbracoNaviHide => GetUmbracoNaviHide(this, _publishedValueFallback);

		/// <summary>Static getter for Show Navigation</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "9.3.0+eea02137ae0b709861b45ded11882279a990c421")]
		public static bool GetUmbracoNaviHide(ISettings that, IPublishedValueFallback publishedValueFallback) => that.Value<bool>(publishedValueFallback, "umbracoNaviHide");
	}
}
