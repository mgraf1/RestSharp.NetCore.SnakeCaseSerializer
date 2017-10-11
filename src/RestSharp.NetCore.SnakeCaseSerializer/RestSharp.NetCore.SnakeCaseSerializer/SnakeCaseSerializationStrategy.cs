using System.Text;

namespace RestSharp.SnakeCaseSerializer.NetCore
{
    public class SnakeCaseSerializationStrategy : PocoJsonSerializerStrategy
    {
        protected override string MapClrMemberNameToJsonFieldName(string clrPropertyName)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < clrPropertyName.Length; i++)
            {
                var c = clrPropertyName[i];
                var pc = i > 0 ? clrPropertyName[i - 1] : '\0';

                if (i > 0 && ShouldPrefixUnderscore(c, pc))
                    sb.AppendFormat("_{0}", char.ToLower(c));
                else
                    sb.Append(char.ToLower(c));
            }

            return sb.ToString();
        }

        private static bool ShouldPrefixUnderscore(char c, char pc)
        {
            if (pc == '_')
                return false;
            if (char.IsNumber(c) && !char.IsNumber(pc))
                return true;
            return char.IsUpper(c);
        }
    }
}
