﻿using Microsoft.AspNetCore.Mvc;


namespace City.Controller
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesControllers : ControllerBase

    {
        [HttpGet]
        public JsonResult GetCities()
        {
            return new JsonResult(
                 new List<object>
                 {
                 new { id=1, Name="New York City"},
                 new{ id=2, Name="Antwerp"} // added few lines.
                 });
        }
    }
}
