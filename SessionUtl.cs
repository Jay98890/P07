﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public static class SessionUtl
{
    public static void SetObject(this ISession session, string key, object obj)
    {
        var settings = new JsonSerializerSettings();
        settings.TypeNameHandling = TypeNameHandling.Objects;
        string json = JsonConvert.SerializeObject(obj,settings);
        byte[] serializedResult = System.Text.Encoding.UTF8.GetBytes(json);
        session.Set(key, serializedResult);
    }

    public static TValue GetObject<TValue>(this ISession session, string key) where TValue: class
    {
        byte[] objdata = session.Get(key);
        if (objdata == null)
            return null;
        else
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Objects;
            string json = System.Text.Encoding.UTF8.GetString(objdata);
            TValue obj = JsonConvert.DeserializeObject<TValue>(json, settings);
            return obj;
        }   
    }
}

