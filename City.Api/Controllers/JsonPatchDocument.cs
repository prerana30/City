using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CityInfo.API.Controllers
{
    public class JsonPatchDocument<T>
    {
        internal void ApplyTo(PointOfInterestForUpdateDto pointOfInterestToPatch, ModelStateDictionary modelState)
        {
            throw new NotImplementedException();
        }

        internal void ApplyTo(CityDto existingCity, ModelStateDictionary modelState)
        {
            throw new NotImplementedException();
        }
    }
}