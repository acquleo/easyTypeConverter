using easyTypeConverter.Conversion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Conversion.Filters.Options
{
    public static class FilterOptionsExtensions
    {
        public static T WithAction<T>(this T obj, FilterAction action)
                where T : IFilterOptions
        {
            obj.Action = action;
            return obj;
        }

        public static T WithExitOnMatch<T>(this T obj)
                where T : IFilterOptions
        {
            obj.Action =  FilterAction.Exit;
            return obj;
        }
    }

    public static class IStringRemovePrefixInputFilterOptionsExtensions
    {
        public static T WithPrefix<T>(this T obj, string prefix)
                where T : IStringRemovePrefixFilterOptions
        {
            obj.Prefix = prefix;
            return obj;
        }
        public static T WithPrefix<T>(this T obj, StringComparison comparison)
                where T : IStringRemovePrefixFilterOptions
        {
            obj.Comparison = comparison;
            return obj;
        }
    }

    public static class IStringReplaceInputFilterOptionsExtensions
    {
        public static T WithComparer<T>(this T obj, StringComparison comparer)
                where T : IStringReplaceFilterOptions
        {
            obj.Comparison = comparer;
            return obj;
        }
        public static T WithReplace<T>(this T obj, string replace, string match)
                where T : IStringReplaceFilterOptions
        {
            obj.Matches.Add(new (match,replace));
            return obj;
        }

        public static T WithReplace<T>(this T obj, string replace, params string[] matches)
               where T : IStringReplaceFilterOptions
        {
            foreach(var match in matches)
                obj.Matches.Add(new(match, replace));
            return obj;
        }
    }

    public static class IStringRegexMatchReplaceInputFilterOptionsExtensions
    {
        public static T WithReplace<T>(this T obj, string replace, string match)
                where T : IStringRegexMatchReplaceFilterOptions
        {
            obj.Matches.Add(new(match, replace));
            return obj;
        }

        public static T WithReplace<T>(this T obj, string replace, params string[] matches)
               where T : IStringRegexMatchReplaceFilterOptions
        {
            foreach (var match in matches)
                obj.Matches.Add(new(match, replace));
            return obj;
        }
    }

    public static class IStringNumericBooleanFilterOptionsExtensions
    {
        public static T WithNumberStyle<T>(this T obj, NumberStyles numberStyle)
                where T : IStringNumericBooleanFilterOptions
        {
            obj.NumberStyle= numberStyle;
            return obj;
        }

        public static T WithCulture<T>(this T obj, CultureInfo culture)
               where T : IStringNumericBooleanFilterOptions
        {
            obj.Culture= culture;
            return obj;
        }
    }
}
