﻿namespace DatabaseAutoDeployment.Entity
{
    public class Migration : BaseEntity
    {
        public string ScriptName { get; set; }
        public string OriginalPath { get; set; }
    }
}
