using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;

namespace NationalCriminalDatabase
{
    [DataContract]
    public class SearchParameter
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Sex { get; set; }

        [DataMember]
        public IEnumerable<string> Nationality { get; set; }

        [DataMember]
        public int? AgeFrom { get; set; }

        [DataMember]
        public int? AgeBefore { get; set; }

        [DataMember]
        public double? HeightFrom { get; set; }

        [DataMember]
        public double? HeightBefore { get; set; }

        [DataMember]
        public double? WeightFrom { get; set; }

        [DataMember]
        public double? WeightBefore { get; set; }

        [DataMember]
        public string FreeText { get; set; }

        public override string ToString()
        {
            string result = "";
            if (FirstName != null)
                result += "FirstName = " + FirstName + ", ";
            if (LastName != null)
                result += "LastName = " + LastName + ", ";
            if (Sex != null)
                result += "Sex = " + Sex + ", ";
            if (Nationality != null && Nationality.Count() != 0)
                result += Nationality.Aggregate("Nationality = {", (sum, s) => sum + s + ", ", s => s.TrimEnd(' ', ',')+ "}");
            if (AgeFrom != null)
                result += "AgeFrom = " + AgeFrom + ", ";
            if (AgeBefore != null)
                result += "AgeBefore = " + AgeBefore + ", ";
            if (HeightFrom != null)
                result += "HeightFrom = " + HeightFrom + ", ";
            if (HeightBefore != null)
                result += "HeightBefore = " + HeightBefore + ", ";
            if (WeightFrom != null)
                result += "WeightFrom = " + WeightFrom + ", ";
            if (WeightBefore != null)
                result += "WeightBefore = " + WeightBefore + ", ";
            if (FreeText != null)
                result += "FreeText = " + FreeText + ", ";
            result = result.TrimEnd(' ', ',');
            return result;

        }
    }
}