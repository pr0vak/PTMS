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

    public static class Priorities
    {
        public const string Low = "low";
        public const string Medium = "medium";
        public const string High = "high";

        public static IReadOnlyList<string> AllPriorities
        {
            get => [Low, Medium, High];
        }
    }

    public static class Status
    {
        public const string InProgress = "in_progress";
        public const string Running = "running";
        public const string Suspended = "suspended";
        public const string Completed = "Completed";

        public static IReadOnlyList<string> AllPriorities
        {
            get => [InProgress, Running, Suspended, Completed];
        }
    }
}