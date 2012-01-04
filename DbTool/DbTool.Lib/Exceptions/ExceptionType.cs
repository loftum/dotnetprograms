using System.ComponentModel;

namespace DbTool.Lib.Exceptions
{
    public enum ExceptionType
    {
        [Description("Already connected to {0}")]
        AlreadyConnected,
        [Description("Not connected to database")]
        NotConnected,
        [Description("Unknown database command '{0}'")]
        UnknownDatabaseCommand,
        [Description("Void?")]
        NoSettingGiven,
        [Description("{0}? Never heard about.")]
        UnknownSetting,
        [Description("There is no context defined. Create a context.")]
        NoContextExists,
        [Description("Unkonwn context: {0}")]
        UnknownContext,
        [Description("Context {0} already exists")]
        ContextAlreadyExists,
        [Description("No credentials are provided for connection {0}")]
        NoCredentialsProvided,
        [Description("Unknown database {0} in current context")]
        UnknownDatabase,
        [Description("Database {0} already exists")]
        DatabaseAlreadyExists,
        [Description("Invalid schema query")]
        InvalidSchemaQuery,
        [Description("No migrationinfo available for database {0}")]
        MissingMigrationInfo,
        [Description("Unknown migration type: {0}")]
        UnknownMigrationType,
        [Description("DatabaseType {0} is not supported with MigSharp")]
        UnsupportedMigSharpProviderName
    }
}