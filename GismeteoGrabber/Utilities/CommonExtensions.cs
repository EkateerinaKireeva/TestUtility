using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GismeteoGrabber.Utilities
{
    public static class CommonExtensions
    {
        public static IEnumerable<HtmlNode> SearchNodes(this HtmlNode node, string tag, string attribute, string attributeValue)
        {
            return node.Descendants(tag).Where(d =>
                 d.Attributes[attribute]?.Value?.Trim() == attributeValue).ToArray();
        }


        public static HtmlNode SearchNode(this HtmlNode node, string tag, string attribute, string attributeValue)
        {
            return node.Descendants(tag).FirstOrDefault(d => d.Attributes[attribute]?.Value?.Trim() == attributeValue);
        }


        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            if (collection == null || collection.Count() == 0)
                return true;

            return false;
        }


        public static string ClearTemperature(this string temperature)
        {
            if (string.IsNullOrEmpty(temperature))
                return temperature;

           return temperature.Replace("&minus;", "-");
        }
    }
}
