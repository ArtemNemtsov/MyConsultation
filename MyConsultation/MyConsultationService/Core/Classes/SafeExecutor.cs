using Microsoft.AspNetCore.Mvc;
using System;

namespace MediaStudioService.Core.Classes
{
    public static class SafeExecutor  
    {
        public static ObjectResult Run<T>(Func<T> func)
        {
            try
            {
                return new ObjectResult(RespоnceManager.CreateSucces(func()));
            }
            catch(Exception ex) 
            { 
                return new ObjectResult(RespоnceManager.CreateError(ex)); 
            }
        }
    }
}