using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Threading;

[assembly: InternalsVisibleTo("NcServiceSearch.Tests")]

namespace NationalCriminalDatabase
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class NcSearchService : INcSearchService
    {

        #region Service constances

        private readonly int _maxResultCount = 500;
        private readonly int _ageMin = 0;
        private readonly int _ageMax = 120;
        private readonly double _weigthMin = 30;
        private readonly double _weigthMax = 150;
        private readonly double _heigthMin = 100;
        private readonly double _heigthMax = 220;

        #endregion


        public bool Search(SearchParameter request, string email, int maxResultCount)
        {
            if (request.FirstName == "")
                request.FirstName = null;
            if (request.LastName == "")
                request.LastName = null;
            if (request.Sex == "")
                request.Sex = null;
            if (request.FreeText == "")
                request.FreeText = null;

            if (!ValidateParameter(request))
                return false;

            if (!(new EmailValidator()).IsValidEmail(email))
                return false;

            if (maxResultCount > _maxResultCount)
                return false;
            
            ThreadPool.QueueUserWorkItem(
                delegate(object task) { new SearchCore().ExecuteTask((SearchTask) task); },
                new SearchTask
                {
                    Search = request,
                    Email = email,
                    MaxResultCount = maxResultCount
                });
            
            return true;
        }


        private bool ValidateFromBefore(double? v_from, double? v_before, double min, double max)
        {
            if (v_from < min || v_from > max)
                return false;
            if (v_before < min || v_before > max)
                return false;

            if (v_from != null && v_before == null)
                return false;
            if (v_from == null && v_before != null)
                return false;

            if (v_from != null && v_before != null && v_from > v_before)
                return false;

            return true;
        }

        private bool ValidateParameter(SearchParameter request)
        {
            if (!ValidateFromBefore(request.AgeFrom, request.AgeBefore, _ageMin, _ageMax))
                return false;

            if (!ValidateFromBefore(request.WeightFrom, request.WeightBefore, _weigthMin, _weigthMax))
                return false;

            if (!ValidateFromBefore(request.HeightFrom, request.HeightBefore, _heigthMin, _heigthMax))
                return false;

            if (request.FirstName == null &&
                request.LastName == null &&
                request.Sex == null &&
                (request.Nationality == null || request.Nationality.Count() == 0) &&
                (request.AgeFrom == null) &&
                (request.WeightFrom == null) &&
                (request.HeightFrom == null) &&
                (request.FreeText == null))
                return false;

            return true;
        }
    }
}