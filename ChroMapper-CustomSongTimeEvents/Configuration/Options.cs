﻿using System;
using System.Reflection;
using System.IO;
using UnityEngine;
using SimpleJSON;

namespace CustomSongTimeEvents.Configuration
{
    public class Options
    {
        private static Options instance;
        public static readonly string settingJsonFile = Application.persistentDataPath + "/CustomSongTimeEventsSetting.json";

        public bool songSpecificScript = true;
        public float songStartTime = 0;
        public string scriptFileName = "CustomSongTimeEvents.json";
        public static Options Instance
        {
            get
            {
                if (instance is null)
                    instance = SettingLoad();
                return instance;
            }
        }

        public static Options SettingLoad()
        {
            var options = new Options();
            if (!File.Exists(settingJsonFile))
                return options;
            var members = options.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public);
            using (var jsonReader = new StreamReader(settingJsonFile))
            {
                var optionsNode = JSON.Parse(jsonReader.ReadToEnd());
                foreach (var member in members)
                {
                    try
                    {
                        if (!(member is FieldInfo field))
                            continue;
                        var optionValue = optionsNode[field.Name];
                        if (optionValue != null)
                            field.SetValue(options, Convert.ChangeType(optionValue.Value, field.FieldType));
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Optiong {member.Name} member to load ERROR!.\n{e}");
                        options = new Options();
                    }
                }
            }
            return options;
        }
        public void SettingSave()
        {
            var optionsNode = new JSONObject();
            var members = this.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public);
            foreach (var member in members)
            {
                if (!(member is FieldInfo field))
                    continue;
                optionsNode[field.Name] = field.GetValue(this).ToString();
            }
            using (var jsonWriter = new StreamWriter(settingJsonFile, false))
                jsonWriter.Write(optionsNode.ToString(2));
        }
    }
}