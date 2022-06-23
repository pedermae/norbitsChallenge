
using NorbitsChallenge.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NorbitsChallenge.Dal
{
    public static class ParameterClass
    {
        public static SqlParameterCollection AddToTable(this SqlParameterCollection parameter, string Column, string Value)
        {
            parameter.Add($"@{Column}", System.Data.SqlDbType.NVarChar);
            parameter[$"@{Column}"].Value = Value;
            return parameter;
        }
        public static SqlParameterCollection AddToTable(this SqlParameterCollection parameter, string Column, int Value)
        {
            parameter.Add($"@{Column}", System.Data.SqlDbType.Int);
            parameter[$"@{Column}"].Value = Value;
            return parameter;
        }
    }
}
