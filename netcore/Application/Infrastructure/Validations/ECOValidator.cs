using System.Collections.Generic;

namespace Application.Infrastructure.Validations
{
    /// <summary>
    /// Validate ECO datas
    /// </summary>
    public static class ECOValidator
    {
        /// <summary>
        /// Makes sure int is a valid Environment, Organization or Community in the database
        /// </summary>
        /// <param name="ecoEntity"></param>
        /// <returns></returns>
        public static bool BeAValidateECO(int ecoEntity)
        {
            return ecoEntity >= 1 || ecoEntity <= 3;
        }

        /// <summary>
        /// Makes sure int is a valid Individual Community or Organization in the database
        /// </summary>
        /// <param name="icos"></param>
        /// <returns></returns>
        public static bool BeAValidateICO(ICollection<int> icos)
        {
            foreach (var ico in icos)
            {
                if (ico < 1 || ico > 3)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// ICO is not null and not empty (has the require number)
        /// </summary>
        /// <param name="icos"></param>
        /// <returns></returns>
        public static bool NotNullOrEmptyICO(ICollection<int> icos)
        {
            if (icos == null)
                return false;

            if (icos.Count < 1 || icos.Count > 3)
                return false;

            return true;
        }

    }
}