﻿using JsonDiffPatchDotNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EwidencjaWSK.Services
{
    public static class ChangesComparisonService
    {
        public static string Changes(string oldValues, string newValues)
        {
            if (oldValues == null && newValues != null)
            {
                return "Stworzono nowe: " + newValues;
            }
            
            if (oldValues != null && newValues == null)
            {
                return "Usunieto: " + oldValues;
            }

            var deserilizeOldValues = JsonConvert.DeserializeObject(oldValues);
            var deserilizeNewValues = JsonConvert.DeserializeObject(newValues);

            var diffValues = new JsonDiffPatch();
            JToken diffResult = diffValues.Diff(oldValues, newValues);

            var serializeDiffBefore = (JsonConvert.SerializeObject(diffResult)).ToString();
            string serializeDiff = Regex.Replace(serializeDiffBefore, @"\\r\\n?|\n", "");

            return serializeDiff;
        }
    }
}
