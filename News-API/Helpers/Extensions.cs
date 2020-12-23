using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace News_API.Helpers
{
   //2- setting Production Mood
   
    public static class Extensions
    {
         //التعامل مع الاخطاء الجزء 5
       public static void AddApplicationError(this HttpResponse response,string message){

           response.Headers.Add("Application-Error",message);
           response.Headers.Add("Access-Control-Expose-Headers","Application-Error");
           response.Headers.Add("Access-Control-Allow-Origin","*");
       } 
            // send proparties from PaginationHeader To Http Response Header
       public static void AddPagination(this HttpResponse response,int currentPage,int itemsPerPage,int totalItems,int totalPages)
       {
           var paginationHeader = new PaginationHeader(currentPage,itemsPerPage,totalItems,totalPages);
           var camelCaseFormatter= new JsonSerializerSettings();
           camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
           //SerializeObject : convert from Capital case To Camel Case (in order to angular understanding) 
           response.Headers.Add("Pagination",JsonConvert.SerializeObject(paginationHeader,camelCaseFormatter));
           response.Headers.Add("Access-Control-Expose-Headers","Pagination");

       }


    }
}