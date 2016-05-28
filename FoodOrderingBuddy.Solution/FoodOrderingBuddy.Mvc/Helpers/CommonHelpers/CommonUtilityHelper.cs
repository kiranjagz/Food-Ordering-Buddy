using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace FoodOrderingBuddy.Helpers
{
    public static class CommonUtilityHelper
    {
        public static string SerializeObject<T>(T toSerialize)
        {
            string content = null;

            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                content = textWriter.ToString();
            }

            return content;
        }
    }
}