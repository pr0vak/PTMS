namespace Api.Common;

public static class SharedData
{
    public static class Roles
    {
        public const string Administrator = "administrator";
        public const string ProjectManager = "project_manager";
        public const string TeamMember = "team_member";
        public const string Guest = "guest";

        public static IReadOnlyList<string> AllRoles
        {
            get => [Administrator, ProjectManager, TeamMember, Guest];
        }
    }
}